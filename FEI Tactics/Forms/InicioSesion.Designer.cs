namespace FEI_Tactics
{
    partial class InicioSesion
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
            this.tbGamertag = new System.Windows.Forms.TextBox();
            this.tbContrasenia = new System.Windows.Forms.TextBox();
            this.buttonInicioSesion = new System.Windows.Forms.Button();
            this.labelGamertag = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.buttonRegistrarse = new System.Windows.Forms.Button();
            this.buttonInvitado = new System.Windows.Forms.Button();
            this.labelTitulo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbGamertag
            // 
            this.tbGamertag.Location = new System.Drawing.Point(261, 125);
            this.tbGamertag.Name = "tbGamertag";
            this.tbGamertag.Size = new System.Drawing.Size(279, 22);
            this.tbGamertag.TabIndex = 0;
            // 
            // tbContrasenia
            // 
            this.tbContrasenia.Location = new System.Drawing.Point(261, 214);
            this.tbContrasenia.Name = "tbContrasenia";
            this.tbContrasenia.PasswordChar = '*';
            this.tbContrasenia.Size = new System.Drawing.Size(279, 22);
            this.tbContrasenia.TabIndex = 1;
            // 
            // buttonInicioSesion
            // 
            this.buttonInicioSesion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonInicioSesion.Location = new System.Drawing.Point(329, 282);
            this.buttonInicioSesion.Name = "buttonInicioSesion";
            this.buttonInicioSesion.Size = new System.Drawing.Size(158, 32);
            this.buttonInicioSesion.TabIndex = 2;
            this.buttonInicioSesion.Text = "Iniciar sesión";
            this.buttonInicioSesion.UseVisualStyleBackColor = true;
            this.buttonInicioSesion.Click += new System.EventHandler(this.clicBtnIniciarSesion);
            // 
            // labelGamertag
            // 
            this.labelGamertag.AutoSize = true;
            this.labelGamertag.Location = new System.Drawing.Point(366, 95);
            this.labelGamertag.Name = "labelGamertag";
            this.labelGamertag.Size = new System.Drawing.Size(67, 16);
            this.labelGamertag.TabIndex = 3;
            this.labelGamertag.Text = "Gamertag";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(366, 185);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(76, 16);
            this.labelPassword.TabIndex = 4;
            this.labelPassword.Text = "Contraseña";
            // 
            // buttonRegistrarse
            // 
            this.buttonRegistrarse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonRegistrarse.Location = new System.Drawing.Point(329, 331);
            this.buttonRegistrarse.Name = "buttonRegistrarse";
            this.buttonRegistrarse.Size = new System.Drawing.Size(158, 32);
            this.buttonRegistrarse.TabIndex = 5;
            this.buttonRegistrarse.Text = "Registrarse";
            this.buttonRegistrarse.UseVisualStyleBackColor = true;
            this.buttonRegistrarse.Click += new System.EventHandler(this.buttonRegistrarse_Click);
            // 
            // buttonInvitado
            // 
            this.buttonInvitado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonInvitado.Location = new System.Drawing.Point(329, 380);
            this.buttonInvitado.Name = "buttonInvitado";
            this.buttonInvitado.Size = new System.Drawing.Size(158, 32);
            this.buttonInvitado.TabIndex = 6;
            this.buttonInvitado.Text = "Jugar como invitado";
            this.buttonInvitado.UseVisualStyleBackColor = true;
            this.buttonInvitado.Click += new System.EventHandler(this.jugarInvitado);
            // 
            // labelTitulo
            // 
            this.labelTitulo.AutoSize = true;
            this.labelTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitulo.Location = new System.Drawing.Point(274, 9);
            this.labelTitulo.Name = "labelTitulo";
            this.labelTitulo.Size = new System.Drawing.Size(238, 51);
            this.labelTitulo.TabIndex = 7;
            this.labelTitulo.Text = "FEI Tactics";
            // 
            // InicioSesion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelTitulo);
            this.Controls.Add(this.buttonInvitado);
            this.Controls.Add(this.buttonRegistrarse);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.labelGamertag);
            this.Controls.Add(this.buttonInicioSesion);
            this.Controls.Add(this.tbContrasenia);
            this.Controls.Add(this.tbGamertag);
            this.Name = "InicioSesion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InicioSesion";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbGamertag;
        private System.Windows.Forms.TextBox tbContrasenia;
        private System.Windows.Forms.Button buttonInicioSesion;
        private System.Windows.Forms.Label labelGamertag;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Button buttonRegistrarse;
        private System.Windows.Forms.Button buttonInvitado;
        private System.Windows.Forms.Label labelTitulo;
    }
}