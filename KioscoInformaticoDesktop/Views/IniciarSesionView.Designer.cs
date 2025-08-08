namespace Desktop.Views
{
    partial class IniciarSesionView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IniciarSesionView));
            label1 = new Label();
            txtEmail = new TextBox();
            label2 = new Label();
            txtPassword = new TextBox();
            chkVerContraseña = new CheckBox();
            pictureBox1 = new PictureBox();
            btnIniciarSesion = new FontAwesome.Sharp.IconButton();
            btnCancelar = new FontAwesome.Sharp.IconButton();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(346, 55);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(49, 20);
            label1.TabIndex = 10;
            label1.Text = "Email:";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(416, 55);
            txtEmail.Margin = new Padding(2);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(240, 27);
            txtEmail.TabIndex = 11;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(320, 109);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(73, 20);
            label2.TabIndex = 12;
            label2.Text = "Password:";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(416, 109);
            txtPassword.Margin = new Padding(2);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(240, 27);
            txtPassword.TabIndex = 13;
            // 
            // chkVerContraseña
            // 
            chkVerContraseña.AutoSize = true;
            chkVerContraseña.Location = new Point(416, 155);
            chkVerContraseña.Margin = new Padding(2);
            chkVerContraseña.Name = "chkVerContraseña";
            chkVerContraseña.Size = new Size(128, 24);
            chkVerContraseña.TabIndex = 14;
            chkVerContraseña.Text = "Ver contraseña";
            chkVerContraseña.UseVisualStyleBackColor = true;
            chkVerContraseña.CheckedChanged += chkVerContraseña_CheckedChanged;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(12, 55);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(278, 242);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 16;
            pictureBox1.TabStop = false;
            // 
            // btnIniciarSesion
            // 
            btnIniciarSesion.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnIniciarSesion.BackColor = Color.DarkGray;
            btnIniciarSesion.IconChar = FontAwesome.Sharp.IconChar.CircleArrowRight;
            btnIniciarSesion.IconColor = Color.Black;
            btnIniciarSesion.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnIniciarSesion.ImageAlign = ContentAlignment.MiddleLeft;
            btnIniciarSesion.Location = new Point(320, 244);
            btnIniciarSesion.Margin = new Padding(2);
            btnIniciarSesion.Name = "btnIniciarSesion";
            btnIniciarSesion.Size = new Size(147, 53);
            btnIniciarSesion.TabIndex = 17;
            btnIniciarSesion.Text = "Iniciar sesión";
            btnIniciarSesion.TextAlign = ContentAlignment.MiddleRight;
            btnIniciarSesion.UseVisualStyleBackColor = false;
            btnIniciarSesion.Click += btnIniciarSesion_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancelar.BackColor = Color.LightGray;
            btnCancelar.IconChar = FontAwesome.Sharp.IconChar.Close;
            btnCancelar.IconColor = Color.Black;
            btnCancelar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnCancelar.ImageAlign = ContentAlignment.MiddleLeft;
            btnCancelar.Location = new Point(510, 244);
            btnCancelar.Margin = new Padding(2);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(146, 53);
            btnCancelar.TabIndex = 18;
            btnCancelar.Text = "Cancelar";
            btnCancelar.TextAlign = ContentAlignment.MiddleRight;
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click_1;
            // 
            // IniciarSesionView
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancelar;
            ClientSize = new Size(732, 337);
            ControlBox = false;
            Controls.Add(btnCancelar);
            Controls.Add(btnIniciarSesion);
            Controls.Add(pictureBox1);
            Controls.Add(chkVerContraseña);
            Controls.Add(txtPassword);
            Controls.Add(label2);
            Controls.Add(txtEmail);
            Controls.Add(label1);
            Margin = new Padding(2);
            Name = "IniciarSesionView";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Iniciar sesión";
            TopMost = true;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private TextBox txtEmail;
        private Label label2;
        private TextBox txtPassword;
        private CheckBox chkVerContraseña;
        private PictureBox pictureBox1;
        public FontAwesome.Sharp.IconButton btnIngresar;
        public FontAwesome.Sharp.IconButton btnIniciarSesion;
        public FontAwesome.Sharp.IconButton btnCancelar;
    }
}