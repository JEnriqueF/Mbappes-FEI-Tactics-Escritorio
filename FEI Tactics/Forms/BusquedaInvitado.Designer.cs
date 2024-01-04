namespace FEI_Tactics.Forms
{
    partial class BusquedaInvitado
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonBuscarPartida = new FontAwesome.Sharp.IconButton();
            this.buttonCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(176, 191);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 69);
            this.label1.TabIndex = 6;
            this.label1.Text = "Inicio";
            // 
            // buttonBuscarPartida
            // 
            this.buttonBuscarPartida.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonBuscarPartida.Enabled = false;
            this.buttonBuscarPartida.IconChar = FontAwesome.Sharp.IconChar.None;
            this.buttonBuscarPartida.IconColor = System.Drawing.Color.Black;
            this.buttonBuscarPartida.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.buttonBuscarPartida.Location = new System.Drawing.Point(215, 332);
            this.buttonBuscarPartida.Name = "buttonBuscarPartida";
            this.buttonBuscarPartida.Size = new System.Drawing.Size(145, 51);
            this.buttonBuscarPartida.TabIndex = 5;
            this.buttonBuscarPartida.Text = "Buscar partida";
            this.buttonBuscarPartida.UseVisualStyleBackColor = true;
            this.buttonBuscarPartida.Click += new System.EventHandler(this.btnBuscarPartida);
            // 
            // buttonCancelar
            // 
            this.buttonCancelar.BackColor = System.Drawing.Color.Red;
            this.buttonCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonCancelar.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonCancelar.Location = new System.Drawing.Point(215, 332);
            this.buttonCancelar.Name = "buttonCancelar";
            this.buttonCancelar.Size = new System.Drawing.Size(145, 51);
            this.buttonCancelar.TabIndex = 7;
            this.buttonCancelar.Text = "Cancelar";
            this.buttonCancelar.UseVisualStyleBackColor = false;
            this.buttonCancelar.Visible = false;
            // 
            // BusquedaInvitado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 611);
            this.Controls.Add(this.buttonCancelar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonBuscarPartida);
            this.Name = "BusquedaInvitado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BusquedaInvitado";
            this.Load += new System.EventHandler(this.BusquedaInvitado_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelarBusqueda;
        private System.Windows.Forms.Label label1;
        private FontAwesome.Sharp.IconButton buttonBuscarPartida;
        private System.Windows.Forms.Button buttonCancelar;
    }
}