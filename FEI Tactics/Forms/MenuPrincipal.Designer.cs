using System.Drawing;

namespace FEI_Tactics
{
    partial class MenuPrincipal
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
            this.buttonBuscarPartida = new FontAwesome.Sharp.IconButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lbGamertag = new System.Windows.Forms.Label();
            this.pbFotoPerfil = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbFotoPerfil)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonBuscarPartida
            // 
            this.buttonBuscarPartida.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonBuscarPartida.Enabled = false;
            this.buttonBuscarPartida.IconChar = FontAwesome.Sharp.IconChar.None;
            this.buttonBuscarPartida.IconColor = System.Drawing.Color.Black;
            this.buttonBuscarPartida.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.buttonBuscarPartida.Location = new System.Drawing.Point(219, 378);
            this.buttonBuscarPartida.Name = "buttonBuscarPartida";
            this.buttonBuscarPartida.Size = new System.Drawing.Size(145, 51);
            this.buttonBuscarPartida.TabIndex = 0;
            this.buttonBuscarPartida.Text = "Buscar partida";
            this.buttonBuscarPartida.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(180, 237);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 69);
            this.label1.TabIndex = 1;
            this.label1.Text = "Inicio";
            // 
            // lbGamertag
            // 
            this.lbGamertag.AutoSize = true;
            this.lbGamertag.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGamertag.Location = new System.Drawing.Point(80, 29);
            this.lbGamertag.Name = "lbGamertag";
            this.lbGamertag.Size = new System.Drawing.Size(119, 29);
            this.lbGamertag.TabIndex = 2;
            this.lbGamertag.Text = "Gamertag";
            // 
            // pbFotoPerfil
            // 
            this.pbFotoPerfil.Location = new System.Drawing.Point(12, 12);
            this.pbFotoPerfil.Name = "pbFotoPerfil";
            this.pbFotoPerfil.Size = new System.Drawing.Size(62, 62);
            this.pbFotoPerfil.TabIndex = 3;
            this.pbFotoPerfil.TabStop = false;
            this.pbFotoPerfil.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // MenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 658);
            this.Controls.Add(this.pbFotoPerfil);
            this.Controls.Add(this.lbGamertag);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonBuscarPartida);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MenuPrincipal";
            this.Text = "MenuPrincipal";
            ((System.ComponentModel.ISupportInitialize)(this.pbFotoPerfil)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FontAwesome.Sharp.IconButton buttonBuscarPartida;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbGamertag;
        private System.Windows.Forms.PictureBox pbFotoPerfil;
    }
}