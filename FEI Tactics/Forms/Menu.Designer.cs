namespace FEI_Tactics
{
    partial class Menu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.imMenuPrincipal = new FontAwesome.Sharp.IconMenuItem();
            this.imMazo = new FontAwesome.Sharp.IconMenuItem();
            this.imPerfil = new FontAwesome.Sharp.IconMenuItem();
            this.imConfiguracion = new FontAwesome.Sharp.IconMenuItem();
            this.iconMenuItem1 = new FontAwesome.Sharp.IconMenuItem();
            this.contenedor = new System.Windows.Forms.Panel();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.AutoSize = false;
            this.menuStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.imMenuPrincipal,
            this.imMazo,
            this.imPerfil,
            this.imConfiguracion});
            this.menuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip.Location = new System.Drawing.Point(0, 659);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(580, 46);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // imMenuPrincipal
            // 
            this.imMenuPrincipal.IconChar = FontAwesome.Sharp.IconChar.BattleNet;
            this.imMenuPrincipal.IconColor = System.Drawing.Color.Black;
            this.imMenuPrincipal.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.imMenuPrincipal.Name = "imMenuPrincipal";
            this.imMenuPrincipal.Size = new System.Drawing.Size(142, 42);
            this.imMenuPrincipal.Text = "Menu principal";
            this.imMenuPrincipal.Click += new System.EventHandler(this.menuPrincipal);
            // 
            // imMazo
            // 
            this.imMazo.IconChar = FontAwesome.Sharp.IconChar.Bolt;
            this.imMazo.IconColor = System.Drawing.Color.Black;
            this.imMazo.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.imMazo.Name = "imMazo";
            this.imMazo.Size = new System.Drawing.Size(80, 42);
            this.imMazo.Text = "Mazo";
            this.imMazo.Click += new System.EventHandler(this.mazo);
            // 
            // imPerfil
            // 
            this.imPerfil.IconChar = FontAwesome.Sharp.IconChar.UserAlt;
            this.imPerfil.IconColor = System.Drawing.Color.Black;
            this.imPerfil.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.imPerfil.Name = "imPerfil";
            this.imPerfil.Size = new System.Drawing.Size(76, 42);
            this.imPerfil.Text = "Perfil";
            this.imPerfil.Click += new System.EventHandler(this.perfil);
            // 
            // imConfiguracion
            // 
            this.imConfiguracion.IconChar = FontAwesome.Sharp.IconChar.Gear;
            this.imConfiguracion.IconColor = System.Drawing.Color.Black;
            this.imConfiguracion.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.imConfiguracion.Name = "imConfiguracion";
            this.imConfiguracion.Size = new System.Drawing.Size(136, 42);
            this.imConfiguracion.Text = "Configuración";
            this.imConfiguracion.Click += new System.EventHandler(this.configuracion);
            // 
            // iconMenuItem1
            // 
            this.iconMenuItem1.IconChar = FontAwesome.Sharp.IconChar.None;
            this.iconMenuItem1.IconColor = System.Drawing.Color.Black;
            this.iconMenuItem1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconMenuItem1.Name = "iconMenuItem1";
            this.iconMenuItem1.Size = new System.Drawing.Size(32, 19);
            this.iconMenuItem1.Text = "iconMenuItem1";
            // 
            // contenedor
            // 
            this.contenedor.Location = new System.Drawing.Point(0, -2);
            this.contenedor.Name = "contenedor";
            this.contenedor.Size = new System.Drawing.Size(600, 658);
            this.contenedor.TabIndex = 1;
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 705);
            this.Controls.Add(this.contenedor);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MenuPrincipal";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private FontAwesome.Sharp.IconMenuItem imMenuPrincipal;
        private FontAwesome.Sharp.IconMenuItem imMazo;
        private FontAwesome.Sharp.IconMenuItem imPerfil;
        private FontAwesome.Sharp.IconMenuItem imConfiguracion;
        private FontAwesome.Sharp.IconMenuItem iconMenuItem1;
        private System.Windows.Forms.Panel contenedor;
    }
}