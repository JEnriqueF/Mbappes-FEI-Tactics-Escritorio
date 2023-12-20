namespace FEI_Tactics.Forms
{
    partial class RegistroCuenta
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
            this.labelTitulo = new System.Windows.Forms.Label();
            this.labelGamertag = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.tbGamertag = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.buttonRegresar = new System.Windows.Forms.Button();
            this.buttonRegistrar = new System.Windows.Forms.Button();
            this.labelSelecFotoPerfil = new System.Windows.Forms.Label();
            this.dgFotosPerfil = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgFotosPerfil)).BeginInit();
            this.SuspendLayout();
            // 
            // labelTitulo
            // 
            this.labelTitulo.AutoSize = true;
            this.labelTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitulo.Location = new System.Drawing.Point(213, 9);
            this.labelTitulo.Name = "labelTitulo";
            this.labelTitulo.Size = new System.Drawing.Size(355, 46);
            this.labelTitulo.TabIndex = 0;
            this.labelTitulo.Text = "Registro de cuenta";
            // 
            // labelGamertag
            // 
            this.labelGamertag.AutoSize = true;
            this.labelGamertag.Location = new System.Drawing.Point(133, 78);
            this.labelGamertag.Name = "labelGamertag";
            this.labelGamertag.Size = new System.Drawing.Size(133, 16);
            this.labelGamertag.TabIndex = 1;
            this.labelGamertag.Text = "Ingrese su gamertag:";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(133, 127);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(142, 16);
            this.labelPassword.TabIndex = 2;
            this.labelPassword.Text = "Ingrese su contraseña:";
            // 
            // tbGamertag
            // 
            this.tbGamertag.Location = new System.Drawing.Point(292, 75);
            this.tbGamertag.Name = "tbGamertag";
            this.tbGamertag.Size = new System.Drawing.Size(373, 22);
            this.tbGamertag.TabIndex = 3;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(292, 124);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(373, 22);
            this.tbPassword.TabIndex = 4;
            // 
            // buttonRegresar
            // 
            this.buttonRegresar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonRegresar.Location = new System.Drawing.Point(693, 406);
            this.buttonRegresar.Name = "buttonRegresar";
            this.buttonRegresar.Size = new System.Drawing.Size(95, 32);
            this.buttonRegresar.TabIndex = 5;
            this.buttonRegresar.Text = "Regresar";
            this.buttonRegresar.UseVisualStyleBackColor = true;
            this.buttonRegresar.Click += new System.EventHandler(this.buttonRegresar_Click);
            // 
            // buttonRegistrar
            // 
            this.buttonRegistrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonRegistrar.Location = new System.Drawing.Point(592, 406);
            this.buttonRegistrar.Name = "buttonRegistrar";
            this.buttonRegistrar.Size = new System.Drawing.Size(95, 32);
            this.buttonRegistrar.TabIndex = 6;
            this.buttonRegistrar.Text = "Registrar";
            this.buttonRegistrar.UseVisualStyleBackColor = true;
            // 
            // labelSelecFotoPerfil
            // 
            this.labelSelecFotoPerfil.AutoSize = true;
            this.labelSelecFotoPerfil.Location = new System.Drawing.Point(133, 176);
            this.labelSelecFotoPerfil.Name = "labelSelecFotoPerfil";
            this.labelSelecFotoPerfil.Size = new System.Drawing.Size(171, 16);
            this.labelSelecFotoPerfil.TabIndex = 9;
            this.labelSelecFotoPerfil.Text = "Seleccione su foto de perfil:";
            // 
            // dgFotosPerfil
            // 
            this.dgFotosPerfil.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFotosPerfil.Location = new System.Drawing.Point(136, 226);
            this.dgFotosPerfil.Name = "dgFotosPerfil";
            this.dgFotosPerfil.RowHeadersWidth = 51;
            this.dgFotosPerfil.RowTemplate.Height = 24;
            this.dgFotosPerfil.Size = new System.Drawing.Size(529, 150);
            this.dgFotosPerfil.TabIndex = 12;
            // 
            // RegistroCuenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgFotosPerfil);
            this.Controls.Add(this.labelSelecFotoPerfil);
            this.Controls.Add(this.buttonRegistrar);
            this.Controls.Add(this.buttonRegresar);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbGamertag);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.labelGamertag);
            this.Controls.Add(this.labelTitulo);
            this.Name = "RegistroCuenta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RegistroCuenta";
            ((System.ComponentModel.ISupportInitialize)(this.dgFotosPerfil)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitulo;
        private System.Windows.Forms.Label labelGamertag;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.TextBox tbGamertag;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Button buttonRegresar;
        private System.Windows.Forms.Button buttonRegistrar;
        private System.Windows.Forms.Label labelSelecFotoPerfil;
        private System.Windows.Forms.DataGridView dgFotosPerfil;
    }
}