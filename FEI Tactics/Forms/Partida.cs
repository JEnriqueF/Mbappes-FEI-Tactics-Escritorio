﻿using FEI_Tactics.Models;
using FEI_Tactics.Services;
using FEI_Tactics.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FEI_Tactics.Forms
{
    public partial class Partida : Form
    {
        JugadorResponse oponente;
        FotoPerfilResponse fotoPerfilOponente;
        string gamertagOponente;
        List<Escenario> escenarios;
        List<PictureBox> pictureBoxesMazo = new List<PictureBox>();
        List<Label> labelsCartasMazo = new List<Label>();
        List<Label> labelsMiCartaTiro = new List<Label>();
        List<Label> labelsPoder = new List<Label>();
        List<Label> labelsPoderOponente = new List<Label>();
        List<PictureBox> pictureBoxesTableroMisCartas = new List<PictureBox>();
        List<PictureBox> pictureBoxesTableroOponente = new List<PictureBox>();
        List<PictureBox> pictureBoxesEscenarios = new List<PictureBox>();
        List<Carta> cartas = new List<Carta>();
        int energia = 0;
        int turno = 1;
        int costoCartaRemovida = 0;
        string gamertagInvitado;
        string respuestaAbandonarPartida;
        int[] numerosMazo;

        public Partida(string gamertagOponente, string gamertagInvitado)
        {
            InitializeComponent();
            this.gamertagOponente = gamertagOponente;
            this.gamertagInvitado = gamertagInvitado;
            EstablecerOrdenEjecucion();
        }

        private void EstablecerOrdenEjecucion()
        {
            if (gamertagOponente.StartsWith("guest"))
            {
                RecuperarEscenariosInfoAsync().ContinueWith(t =>
                {
                    CargarImagenesEscenarios();
                    buttonAbandonarPartida.Enabled = true;
                    buttonTerminarTurno.Enabled = true;
                    lbOponente.Text = gamertagOponente;
                    CargarMazo();
                }, TaskScheduler.FromCurrentSynchronizationContext());
            } else
            {
                ObtenerDatosOponente(gamertagOponente)
                .ContinueWith(t =>
                {
                    return RecuperarEscenariosInfoAsync();
                }, TaskScheduler.FromCurrentSynchronizationContext())
                .Unwrap()
                .ContinueWith(t =>
                {
                    CargarImagenesEscenarios();
                    buttonAbandonarPartida.Enabled = true;
                    buttonTerminarTurno.Enabled = true;
                    CargarMazo();
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        private void buttonAbandonarPartida_Click(object sender, EventArgs e)
        {
            if (Mensaje.MostrarMensajeConfirmacion())
            {
                CancelarPartida();
            }
        }

        private async void buttonTerminarTurno_Click(object sender, EventArgs e)
        {
            buttonTerminarTurno.Enabled = false;

            try
            {
                PartidaResponse partidaResponse;

                for (int i = 0; i < pictureBoxesTableroMisCartas.Count; i++)
                {
                    if (pictureBoxesTableroMisCartas[i].Image != null && (bool) pictureBoxesTableroMisCartas[i].Tag != true)
                    {
                        pictureBoxesTableroMisCartas[i].BorderStyle = BorderStyle.Fixed3D;
                    }
                }

                Movimiento movimiento;
                List<Movimiento> listaMovimientos = new List<Movimiento>();
                bool movimientoRegistrado = false;

                for (int i = 0; i < pictureBoxesTableroMisCartas.Count; i++)
                {
                    if (!labelsMiCartaTiro[i].Text.Equals("0"))
                    {
                        if (pictureBoxesTableroMisCartas[i].BorderStyle == BorderStyle.Fixed3D && (bool)pictureBoxesTableroMisCartas[i].Tag != true)
                        {
                            movimiento = new Movimiento();

                            movimiento.IDEscenario = (int)pictureBoxesEscenarios[i].Tag;
                            movimiento.IDCarta = int.Parse(labelsMiCartaTiro[i].Text);

                            pictureBoxesTableroMisCartas[i].Tag = true;

                            listaMovimientos.Add(movimiento);

                            movimientoRegistrado = true;
                        }
                    }
                }

                if(!movimientoRegistrado)
                {
                    movimiento = new Movimiento();

                    movimiento.IDEscenario = 0;
                    movimiento.IDCarta = 0;

                    listaMovimientos.Add(movimiento);
                }

                int cicloPartida = 0;

                do
                {
                    if(cicloPartida == 5)
                    {
                        CancelarPartida();
                    }

                    if (gamertagInvitado.StartsWith("guest"))
                    {
                        partidaResponse = await PartidaService.MandarMovimientosAsync(gamertagInvitado, listaMovimientos);
                    } else
                    {
                        partidaResponse = await PartidaService.MandarMovimientosAsync(Jugador.Instancia.Gamertag, listaMovimientos);
                    }

                    await Task.Delay(5000);

                    if (turno < 4 && partidaResponse.Respuesta != null && !partidaResponse.Respuesta.Equals("Jugador no encontrado en la partida"))
                    {
                        cicloPartida++;
                    } else if (turno < 4 && partidaResponse.Respuesta != null && partidaResponse.Respuesta.Equals("Jugador no encontrado en la partida"))
                    {
                        CancelarPartida();
                    } else if (turno < 4 && partidaResponse.Respuesta == null)
                    {
                        int indiceCarta;
                        for (int i = 0; i < pictureBoxesEscenarios.Count; i++)
                        {
                            for (int y = 0; y < partidaResponse.listaMovimientos.Count; y++)
                            {
                                if ((int)pictureBoxesEscenarios[i].Tag == partidaResponse.listaMovimientos[y].IDEscenario)
                                {
                                    indiceCarta = cartas.FindIndex(x => x.IDCarta == partidaResponse.listaMovimientos[y].IDCarta);
                                    pictureBoxesTableroOponente[i].Image = ConvertidorImagen.DeBase64AImagen(cartas[indiceCarta].Imagen);
                                    labelsPoderOponente[i].Text = cartas[indiceCarta].Poder.ToString();
                                    break;
                                }
                            }
                        }
                    }
                } while (partidaResponse.Respuesta != null && (partidaResponse.Respuesta.Equals("Ya se jugó un movimiento para Jugador en este turno") || 
                        !partidaResponse.Respuesta.Equals("Jugador no encontrado en la partida") || partidaResponse.Respuesta.Equals("Turno Jugado")));

                energia += 2;
                lbEnergia.Text = energia.ToString();
            } catch (Exception ex)
            {
                Mensaje.MostrarMensaje($"{ex.Message}", "Conexión con el servidor no establecida", MessageBoxIcon.Error);
            }

            if(turno == 4)
            {
                int totalMisPuntos = 0, totalPuntosOponente = 0;

                totalMisPuntos += int.Parse(lbMiPoder1.Text) + int.Parse(lbMiPoder2.Text) + int.Parse(lbMiPoder3.Text);
                totalPuntosOponente += int.Parse(lbPoderOponente1.Text) + int.Parse(lbPoderOponente2.Text) + int.Parse(lbPoderOponente3.Text);

                if(totalMisPuntos > totalPuntosOponente)
                {
                    if (gamertagInvitado.StartsWith("guest"))
                    {
                        Mensaje.MostrarMensaje("¡Has ganado!", "¡Felicidades!", MessageBoxIcon.Exclamation);
                        DialogResult = DialogResult.OK;
                        this.Close();
                    } else
                    {
                        GuardarResultado(1);
                    }
                } else if(totalMisPuntos < totalPuntosOponente)
                {
                    if (gamertagInvitado.StartsWith("guest"))
                    {
                        Mensaje.MostrarMensaje("Has perdido", "Suerte la próxima vez", MessageBoxIcon.Exclamation);
                        DialogResult = DialogResult.OK;
                        this.Close();
                    } else
                    {
                        GuardarResultado(0);
                    }
                } else if(totalMisPuntos == totalPuntosOponente)
                {
                    Mensaje.MostrarMensaje("¡Empate!", "¡Reñido!", MessageBoxIcon.Exclamation);
                    DialogResult = DialogResult.Abort;
                    this.Close();
                }
            }

            turno++;
            lbTurno.Text = turno.ToString();

            buttonTerminarTurno.Enabled = true;
        }

        private async void CancelarPartida()
        {
            try
            {
                if (gamertagInvitado.StartsWith("guest"))
                {
                    respuestaAbandonarPartida = await MatchMakingService.CancelarPartidaAsync(gamertagInvitado);

                    if (respuestaAbandonarPartida.Equals("Partida cancelada correctamente"))
                    {
                        Mensaje.MostrarMensaje("Has perdido", "Suerte la próxima vez", MessageBoxIcon.Exclamation);
                        DialogResult = DialogResult.OK;
                        this.Close();
                    } else
                    {
                        Mensaje.MostrarMensaje("¡Has ganado!", "¡Felicidades!", MessageBoxIcon.Exclamation);
                        DialogResult = DialogResult.OK;
                        this.Close();
                    }
                } else
                {
                    respuestaAbandonarPartida = await MatchMakingService.CancelarPartidaAsync(Jugador.Instancia.Gamertag);

                    if (respuestaAbandonarPartida.Equals("Partida cancelada correctamente"))
                    {
                        GuardarResultado(0);
                    } else
                    {
                        GuardarResultado(1);
                    }
                }
            } catch (Exception ex)
            {
                Mensaje.MostrarMensaje($"{ex.Message}", "Conexión con el servidor no establecida", MessageBoxIcon.Error);
            }
        }

        private async void GuardarResultado(int resultado)
        {
            if(resultado == 1)
            {
                string respuesta = await MatchMakingService.GuardarResultadoAsync(Jugador.Instancia.Gamertag, 1);

                if (respuesta.Equals("Resultados Guardados"))
                {
                    Mensaje.MostrarMensaje("¡Has ganado!", "¡Felicidades!", MessageBoxIcon.Exclamation);
                    DialogResult = DialogResult.OK;
                    this.Close();
                } else
                {
                    Mensaje.MostrarMensaje("No se pudo guardar el resultado", "Error", MessageBoxIcon.Error);
                }
            }else if(resultado == 0)
            {
                string respuesta = await MatchMakingService.GuardarResultadoAsync(Jugador.Instancia.Gamertag, 0);

                if (respuesta.Equals("Resultados Guardados"))
                {
                    Mensaje.MostrarMensaje("Has perdido", "Suerte la próxima vez", MessageBoxIcon.Exclamation);
                    DialogResult = DialogResult.Cancel;
                    this.Close();
                } else
                {
                    Mensaje.MostrarMensaje("No se pudo guardar el resultado", "Error", MessageBoxIcon.Error);
                }
            }
        }

        public async Task ObtenerDatosOponente(string gamertag)
        {
            try
            {
                oponente = await JugadorService.RecuperarOponenteAsync(gamertag);

                if (oponente != null)
                {
                    lbOponente.Text = oponente.Gamertag;
                    RecuperarFotoPerfilOponenteAsync();
                }
                else
                {
                    Mensaje.MostrarMensaje("No se encontró al jugador oponente.", "Error", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Mensaje.MostrarMensaje($"{ex.Message}", "Conexión con el servidor no establecida", MessageBoxIcon.Error);
            }
        }

        private async void RecuperarFotoPerfilOponenteAsync()
        {
            try
            {
                List<FotoPerfil> fotosPerfil = await JugadorService.RecuperarFotosPerfilAsync();
                foreach (var fotoPerfil in fotosPerfil)
                {
                    if (fotoPerfil.IDFoto == oponente.IdFoto)
                    {
                        Image image = ConvertidorImagen.DeBase64AImagen(fotoPerfil.Foto);
                        fotoPerfilOponente = new FotoPerfilResponse(fotoPerfil.IDFoto, image);
                        pbFotoPerfilEnemigo.Image = fotoPerfilOponente.Foto;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Mensaje.MostrarMensaje($"{ex.Message}", "Conexión con el servidor no establecida", MessageBoxIcon.Error);
            }
        }

        public async Task RecuperarEscenariosInfoAsync()
        {
            try
            {
                escenarios = await EscenarioService.RecuperarEscenariosAsync();
            }
            catch (Exception ex)
            {
                buttonAbandonarPartida.Enabled = true;
                buttonTerminarTurno.Enabled = false;
                Mensaje.MostrarMensaje($"{ex.Message}", "Conexión con el servidor no establecida", MessageBoxIcon.Error);
            }
        }

        public void CargarImagenesEscenarios()
        {
            if (escenarios != null && escenarios.Count > 0)
            {
                pbEscenario1.Image = ConvertidorImagen.DeBase64AImagen(escenarios[0].Imagen);
                pbEscenario1.Tag = escenarios[0].IDEscenario;

                pbEscenario2.Image = ConvertidorImagen.DeBase64AImagen(escenarios[1].Imagen);
                pbEscenario2.Tag = escenarios[1].IDEscenario;

                pbEscenario3.Image = ConvertidorImagen.DeBase64AImagen(escenarios[2].Imagen);
                pbEscenario3.Tag = escenarios[2].IDEscenario;
            }
            else
            {
                Mensaje.MostrarMensaje("No existen escenarios.", "Error al recuperar los escenarios", MessageBoxIcon.Error);
            }
        }

        public async void CargarMazo()
        {
            if (gamertagInvitado.StartsWith("guest"))
            {
                numerosMazo = new int[] {9,10,11,12};
                cartas = await CartasService.RecuperarMazoAsync();
            } else
            {
                numerosMazo = Jugador.Instancia.Mazo.Split(',').Select(s => Convert.ToInt32(s.Trim())).ToArray();
                cartas = Carta.Instancia;
            }
            int indiceNumerosMazo = 0;
            //cartas = Carta.Instancia;
            int indiceCarta = 0;

            int[] idCartas = new int[cartas.Count];

            pictureBoxesMazo.Add(pbMiCarta1);
            pictureBoxesMazo.Add(pbMiCarta2);
            pictureBoxesMazo.Add(pbMiCarta3);
            pictureBoxesMazo.Add(pbMiCarta4);

            labelsCartasMazo.Add(lbCartaMazo1);
            labelsCartasMazo.Add(lbCartaMazo2);
            labelsCartasMazo.Add(lbCartaMazo3);
            labelsCartasMazo.Add(lbCartaMazo4);

            labelsPoder.Add(lbPoder1);
            labelsPoder.Add(lbPoder2);
            labelsPoder.Add(lbPoder3);
            labelsPoder.Add(lbPoder4);

            labelsPoderOponente.Add(lbPoderOponente1);
            labelsPoderOponente.Add(lbPoderOponente2);
            labelsPoderOponente.Add(lbPoderOponente3);

            pbMiCartaTiro1.Tag = false;
            pbMiCartaTiro2.Tag = false;
            pbMiCartaTiro3.Tag = false;

            pictureBoxesTableroMisCartas.Add(pbMiCartaTiro1);
            pictureBoxesTableroMisCartas.Add(pbMiCartaTiro2);
            pictureBoxesTableroMisCartas.Add(pbMiCartaTiro3);

            labelsMiCartaTiro.Add(lbMiCarta1);
            labelsMiCartaTiro.Add(lbMiCarta2);
            labelsMiCartaTiro.Add(lbMiCarta3);

            pictureBoxesTableroOponente.Add(pbCartaEnemigo1);
            pictureBoxesTableroOponente.Add(pbCartaEnemigo2);
            pictureBoxesTableroOponente.Add(pbCartaEnemigo3);

            pictureBoxesEscenarios.Add(pbEscenario1);
            pictureBoxesEscenarios.Add(pbEscenario2);
            pictureBoxesEscenarios.Add(pbEscenario3);

            int controladorIdCarta = 0;
            foreach (Carta carta in cartas)
            {
                idCartas[controladorIdCarta] = carta.IDCarta;
                controladorIdCarta++;
            }

            if (gamertagInvitado.StartsWith("guest"))
            {
                int i = 0;
                foreach(int numero in numerosMazo)
                {
                    pictureBoxesMazo[i].Image = ConvertidorImagen.DeBase64AImagen(cartas[i].Imagen);
                    labelsCartasMazo[i].Text = numero.ToString();
                    pictureBoxesMazo[i].Tag = cartas[i].Costo;
                    labelsPoder[i].Text = cartas[i].Poder.ToString();

                    i++;
                }
            } else
            {
                foreach (int numero in idCartas)
                {
                    if (Jugador.Instancia.Mazo.Contains(numero.ToString()) && numero != 0)
                    {
                        indiceNumerosMazo = Array.IndexOf(numerosMazo, numero);
                        indiceCarta = cartas.FindIndex(x => x.IDCarta == numero);

                        pictureBoxesMazo[indiceNumerosMazo].Image = ConvertidorImagen.DeBase64AImagen(cartas[indiceCarta].Imagen);
                        labelsCartasMazo[indiceNumerosMazo].Text = numero.ToString();
                        pictureBoxesMazo[indiceNumerosMazo].Tag = cartas[indiceCarta].Costo;
                        labelsPoder[indiceNumerosMazo].Text = cartas[indiceCarta].Poder.ToString();

                        numerosMazo[indiceNumerosMazo] = 0;
                    }
                }
            }

            energia = 2;
            lbEnergia.Text = energia.ToString();
            lbTurno.Text = turno.ToString();
        }

        private void insertarCarta1(object sender, EventArgs e)
        {
            for(int i = 0; i < pictureBoxesMazo.Count; i++)
            {
                if (pbMiCartaTiro1.Image == null)
                {
                    if (pictureBoxesMazo[i].BorderStyle == BorderStyle.FixedSingle)
                    {
                        pbMiCartaTiro1.Image = pictureBoxesMazo[i].Image;
                        lbMiCarta1.Text = labelsCartasMazo[i].Text;
                        
                        pictureBoxesMazo[i].Image = null;
                        pictureBoxesMazo[i].Visible = false;
                        
                        pbMiCartaTiro1.Enabled = false;
                        
                        deseleccionarCarta();
                        desactivarTableroMisCartas();

                        energia -= (int) pictureBoxesMazo[i].Tag;
                        lbEnergia.Text = energia.ToString();

                        lbMiPoder1.Text = labelsPoder[i].Text;
                        return;
                    }
                }else if (pbMiCartaTiro1.Image != null)
                {
                    for(int inn = 0; inn < labelsCartasMazo.Count; inn++)
                    {
                        if (labelsCartasMazo[inn].Text.Equals(lbMiCarta1.Text))
                        {
                            pictureBoxesMazo[inn].Image = pbMiCartaTiro1.Image;
                            pictureBoxesMazo[inn].Visible = true;
                            costoCartaRemovida = (int) pictureBoxesMazo[inn].Tag;
                            break;
                        }
                    }

                    if (pictureBoxesMazo[i].BorderStyle == BorderStyle.FixedSingle)
                    {
                        energia += costoCartaRemovida;
                        lbEnergia.Text = energia.ToString();

                        pbMiCartaTiro1.Image = pictureBoxesMazo[i].Image;
                        lbMiCarta1.Text = labelsCartasMazo[i].Text;
                        
                        pictureBoxesMazo[i].Image = null;
                        pictureBoxesMazo[i].Visible = false;
                        
                        pbMiCartaTiro1.Enabled = false;
                        
                        deseleccionarCarta();
                        desactivarTableroMisCartas();

                        energia -= (int)pictureBoxesMazo[i].Tag;
                        lbEnergia.Text = energia.ToString();

                        lbMiPoder1.Text = labelsPoder[i].Text;
                        return;
                    }
                }
                
            }
        }

        private void insertarCarta2(object sender, EventArgs e)
        {
            for (int i = 0; i < pictureBoxesMazo.Count; i++)
            {
                if (pbMiCartaTiro2.Image == null)
                {
                    if (pictureBoxesMazo[i].BorderStyle == BorderStyle.FixedSingle)
                    {
                        pbMiCartaTiro2.Image = pictureBoxesMazo[i].Image;
                        lbMiCarta2.Text = labelsCartasMazo[i].Text;
                        
                        pictureBoxesMazo[i].Image = null;
                        pictureBoxesMazo[i].Visible = false;
                        
                        pbMiCartaTiro2.Enabled = false;
                        
                        deseleccionarCarta();
                        desactivarTableroMisCartas();

                        energia -= (int)pictureBoxesMazo[i].Tag;
                        lbEnergia.Text = energia.ToString();

                        lbMiPoder2.Text = labelsPoder[i].Text;
                        return;
                    }
                } else if (pbMiCartaTiro2.Image != null)
                {
                    for (int inn = 0; inn < labelsCartasMazo.Count; inn++)
                    {
                        if (labelsCartasMazo[inn].Text.Equals(lbMiCarta2.Text))
                        {
                            pictureBoxesMazo[inn].Image = pbMiCartaTiro2.Image;
                            pictureBoxesMazo[inn].Visible = true;
                            costoCartaRemovida = (int)pictureBoxesMazo[inn].Tag;
                            break;
                        }
                    }

                    if (pictureBoxesMazo[i].BorderStyle == BorderStyle.FixedSingle)
                    {
                        energia += costoCartaRemovida;
                        lbEnergia.Text = energia.ToString();

                        pbMiCartaTiro2.Image = pictureBoxesMazo[i].Image;
                        lbMiCarta2.Text = labelsCartasMazo[i].Text;
                        
                        pictureBoxesMazo[i].Image = null;
                        pictureBoxesMazo[i].Visible = false;
                        
                        pbMiCartaTiro2.Enabled = false;
                        
                        deseleccionarCarta();
                        desactivarTableroMisCartas();

                        energia -= (int)pictureBoxesMazo[i].Tag;
                        lbEnergia.Text = energia.ToString();

                        lbMiPoder2.Text = labelsPoder[i].Text;
                        return;
                    }
                }

            }
        }

        private void insertarCarta3(object sender, EventArgs e)
        {
            for (int i = 0; i < pictureBoxesMazo.Count; i++)
            {
                if (pbMiCartaTiro3.Image == null)
                {
                    if (pictureBoxesMazo[i].BorderStyle == BorderStyle.FixedSingle)
                    {
                        pbMiCartaTiro3.Image = pictureBoxesMazo[i].Image;
                        lbMiCarta3.Text = labelsCartasMazo[i].Text;
                        
                        pictureBoxesMazo[i].Image = null;
                        pictureBoxesMazo[i].Visible = false;
                        
                        pbMiCartaTiro3.Enabled = false;
                        
                        deseleccionarCarta();
                        desactivarTableroMisCartas();

                        energia -= (int)pictureBoxesMazo[i].Tag;
                        lbEnergia.Text = energia.ToString();

                        lbMiPoder3.Text = labelsPoder[i].Text;
                        return;
                    }
                } else if (pbMiCartaTiro3.Image != null)
                {
                    for (int inn = 0; inn < labelsCartasMazo.Count; inn++)
                    {
                        if (labelsCartasMazo[inn].Text.Equals(lbMiCarta3.Text))
                        {
                            pictureBoxesMazo[inn].Image = pbMiCartaTiro3.Image;
                            pictureBoxesMazo[inn].Visible = true;
                            costoCartaRemovida = (int)pictureBoxesMazo[inn].Tag;
                            break;
                        }
                    }

                    if (pictureBoxesMazo[i].BorderStyle == BorderStyle.FixedSingle)
                    {
                        energia += costoCartaRemovida;
                        lbEnergia.Text = energia.ToString();

                        pbMiCartaTiro3.Image = pictureBoxesMazo[i].Image;
                        lbMiCarta3.Text = labelsCartasMazo[i].Text;
                        
                        pictureBoxesMazo[i].Image = null;
                        pictureBoxesMazo[i].Visible = false;
                        
                        pbMiCartaTiro3.Enabled = false;
                        
                        deseleccionarCarta();
                        desactivarTableroMisCartas();

                        energia -= (int)pictureBoxesMazo[i].Tag;
                        lbEnergia.Text = energia.ToString();

                        lbMiPoder3.Text = labelsPoder[i].Text;
                        return;
                    }
                }

            }
        }

        private void seleccionarCarta1(object sender, EventArgs e)
        {
            if ( (int) pbMiCarta1.Tag > energia)
            {
                Mensaje.MostrarMensaje("No se puede jugar esta carta porque no tienes suficiente energía.", "Carta excede nivel de energía", MessageBoxIcon.Error);
                return;
            }
            deseleccionarCarta();
            pbMiCarta1.BorderStyle = BorderStyle.FixedSingle;
            activarTableroMisCartas();
        }

        private void seleccionarCarta2(object sender, EventArgs e)
        {
            if ( (int) pbMiCarta2.Tag > energia)
            {
                Mensaje.MostrarMensaje("No se puede jugar esta carta porque no tienes suficiente energía.", "Carta excede nivel de energía", MessageBoxIcon.Error);
                return;
            }
            deseleccionarCarta();
            pbMiCarta2.BorderStyle = BorderStyle.FixedSingle;
            activarTableroMisCartas();
        }

        private void seleccionarCarta3(object sender, EventArgs e)
        {
            if ( (int) pbMiCarta3.Tag > energia)
            {
                Mensaje.MostrarMensaje("No se puede jugar esta carta porque no tienes suficiente energía.", "Carta excede nivel de energía", MessageBoxIcon.Error);
                return;
            }
            deseleccionarCarta();
            pbMiCarta3.BorderStyle = BorderStyle.FixedSingle;
            activarTableroMisCartas();
        }

        private void seleccionarCarta4(object sender, EventArgs e)
        {
            if ( (int) pbMiCarta4.Tag > energia)
            {
                Mensaje.MostrarMensaje("No se puede jugar esta carta porque no tienes suficiente energía.", "Carta excede nivel de energía", MessageBoxIcon.Error);
                return;
            }
            deseleccionarCarta();
            pbMiCarta4.BorderStyle = BorderStyle.FixedSingle;
            activarTableroMisCartas();
        }

        private void deseleccionarCarta()
        {
            pbMiCarta1.BorderStyle = BorderStyle.None;
            pbMiCarta2.BorderStyle = BorderStyle.None;
            pbMiCarta3.BorderStyle = BorderStyle.None;
            pbMiCarta4.BorderStyle = BorderStyle.None;
        }

        private void activarTableroMisCartas()
        {
            if(pbMiCartaTiro1.BorderStyle == BorderStyle.FixedSingle)
            {
                pbMiCartaTiro1.Enabled = true;
            }
            if(pbMiCartaTiro2.BorderStyle == BorderStyle.FixedSingle)
            {
                pbMiCartaTiro2.Enabled = true;
            }
            if(pbMiCartaTiro3.BorderStyle == BorderStyle.FixedSingle)
            {
                pbMiCartaTiro3.Enabled = true;
            }
        }

        public void desactivarTableroMisCartas()
        {
            if (pbMiCartaTiro1.BorderStyle == BorderStyle.FixedSingle)
            {
                pbMiCartaTiro1.Enabled = false;
            }
            if (pbMiCartaTiro2.BorderStyle == BorderStyle.FixedSingle)
            {
                pbMiCartaTiro2.Enabled = false;
            }
            if (pbMiCartaTiro3.BorderStyle == BorderStyle.FixedSingle)
            {
                pbMiCartaTiro3.Enabled = false;
            }
        }
    }
}
