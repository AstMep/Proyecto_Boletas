namespace Proyecto_Boletas
{
    partial class adm_Secretaria
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(adm_Secretaria));
            panelApp = new Panelito();
            flowSecretarias = new FlowLayoutPanel();
            groupBox1 = new GroupBox();
            label18 = new Label();
            txtContrasenaSecre = new TextBox();
            txtCorreoSecre = new TextBox();
            label15 = new Label();
            btnAltaSecretarias = new Button();
            txtUsuarioSecre = new TextBox();
            label2 = new Label();
            Usuario = new Label();
            label3 = new Label();
            panel1 = new Panel();
            label14 = new Label();
            btn_ingresar = new Button();
            panelMenu = new Panelito();
            btn_admaestros = new Button();
            btnEnvioBoletas = new Button();
            btnEdicionDatos = new Button();
            btnBitacora = new Button();
            btnAdmSecre = new Button();
            btnEstadisticas = new Button();
            btn_capturaCalif = new Button();
            btn_inscripcion = new Button();
            panelLogo = new Panelito();
            label4 = new Label();
            Logo = new PictureBox();
            label1 = new Label();
            panelApp.SuspendLayout();
            groupBox1.SuspendLayout();
            panel1.SuspendLayout();
            panelMenu.SuspendLayout();
            panelLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Logo).BeginInit();
            SuspendLayout();
            // 
            // panelApp
            // 
            panelApp.BackColor = Color.FromArgb(181, 131, 120);
            panelApp.BorderRadius = 20;
            panelApp.Controls.Add(flowSecretarias);
            panelApp.Controls.Add(groupBox1);
            panelApp.Controls.Add(panel1);
            panelApp.Dock = DockStyle.Fill;
            panelApp.Location = new Point(336, 0);
            panelApp.Margin = new Padding(6, 6, 6, 6);
            panelApp.Name = "panelApp";
            panelApp.Size = new Size(1543, 1129);
            panelApp.TabIndex = 3;
            // 
            // flowSecretarias
            // 
            flowSecretarias.AutoScroll = true;
            flowSecretarias.BackColor = Color.FromArgb(63, 75, 92);
            flowSecretarias.Location = new Point(145, 631);
            flowSecretarias.Margin = new Padding(6, 6, 6, 6);
            flowSecretarias.Name = "flowSecretarias";
            flowSecretarias.Size = new Size(1241, 478);
            flowSecretarias.TabIndex = 26;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.FromArgb(63, 75, 92);
            groupBox1.Controls.Add(label18);
            groupBox1.Controls.Add(txtContrasenaSecre);
            groupBox1.Controls.Add(txtCorreoSecre);
            groupBox1.Controls.Add(label15);
            groupBox1.Controls.Add(btnAltaSecretarias);
            groupBox1.Controls.Add(txtUsuarioSecre);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(Usuario);
            groupBox1.Controls.Add(label3);
            groupBox1.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold);
            groupBox1.Location = new Point(145, 179);
            groupBox1.Margin = new Padding(6, 6, 6, 6);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(6, 6, 6, 6);
            groupBox1.Size = new Size(1241, 416);
            groupBox1.TabIndex = 22;
            groupBox1.TabStop = false;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.BackColor = Color.Transparent;
            label18.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label18.ForeColor = Color.PeachPuff;
            label18.Location = new Point(919, 299);
            label18.Margin = new Padding(6, 0, 6, 0);
            label18.Name = "label18";
            label18.Size = new Size(196, 45);
            label18.TabIndex = 9;
            label18.Text = "Información";
            // 
            // txtContrasenaSecre
            // 
            txtContrasenaSecre.BackColor = SystemColors.WindowFrame;
            txtContrasenaSecre.Location = new Point(241, 286);
            txtContrasenaSecre.Margin = new Padding(6, 6, 6, 6);
            txtContrasenaSecre.Name = "txtContrasenaSecre";
            txtContrasenaSecre.Size = new Size(476, 44);
            txtContrasenaSecre.TabIndex = 6;
            // 
            // txtCorreoSecre
            // 
            txtCorreoSecre.BackColor = SystemColors.WindowFrame;
            txtCorreoSecre.Location = new Point(241, 188);
            txtCorreoSecre.Margin = new Padding(6, 6, 6, 6);
            txtCorreoSecre.Name = "txtCorreoSecre";
            txtCorreoSecre.Size = new Size(476, 44);
            txtCorreoSecre.TabIndex = 5;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.BackColor = Color.Transparent;
            label15.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label15.ForeColor = Color.PeachPuff;
            label15.Location = new Point(947, 254);
            label15.Margin = new Padding(6, 0, 6, 0);
            label15.Name = "label15";
            label15.Size = new Size(138, 45);
            label15.TabIndex = 5;
            label15.Text = "Guardar";
            // 
            // btnAltaSecretarias
            // 
            btnAltaSecretarias.BackColor = Color.FromArgb(212, 168, 133);
            btnAltaSecretarias.BackgroundImageLayout = ImageLayout.Center;
            btnAltaSecretarias.FlatStyle = FlatStyle.Popup;
            btnAltaSecretarias.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAltaSecretarias.Image = (Image)resources.GetObject("btnAltaSecretarias.Image");
            btnAltaSecretarias.Location = new Point(947, 132);
            btnAltaSecretarias.Margin = new Padding(6, 6, 6, 6);
            btnAltaSecretarias.Name = "btnAltaSecretarias";
            btnAltaSecretarias.Size = new Size(97, 111);
            btnAltaSecretarias.TabIndex = 3;
            btnAltaSecretarias.UseVisualStyleBackColor = false;
            // 
            // txtUsuarioSecre
            // 
            txtUsuarioSecre.BackColor = SystemColors.WindowFrame;
            txtUsuarioSecre.Location = new Point(241, 98);
            txtUsuarioSecre.Margin = new Padding(6, 6, 6, 6);
            txtUsuarioSecre.Name = "txtUsuarioSecre";
            txtUsuarioSecre.Size = new Size(476, 44);
            txtUsuarioSecre.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.PeachPuff;
            label2.Location = new Point(50, 292);
            label2.Margin = new Padding(6, 0, 6, 0);
            label2.Name = "label2";
            label2.Size = new Size(194, 37);
            label2.TabIndex = 2;
            label2.Text = "Contraseña:";
            // 
            // Usuario
            // 
            Usuario.AutoSize = true;
            Usuario.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Usuario.ForeColor = Color.PeachPuff;
            Usuario.Location = new Point(50, 98);
            Usuario.Margin = new Padding(6, 0, 6, 0);
            Usuario.Name = "Usuario";
            Usuario.Size = new Size(131, 37);
            Usuario.TabIndex = 3;
            Usuario.Text = "Usuario:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.PeachPuff;
            label3.Location = new Point(50, 188);
            label3.Margin = new Padding(6, 0, 6, 0);
            label3.Name = "label3";
            label3.Size = new Size(125, 37);
            label3.TabIndex = 1;
            label3.Text = "Correo:";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(39, 66, 89);
            panel1.Controls.Add(label14);
            panel1.Controls.Add(btn_ingresar);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(6, 6, 6, 6);
            panel1.Name = "panel1";
            panel1.Size = new Size(1543, 126);
            panel1.TabIndex = 21;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Cascadia Code", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label14.ForeColor = SystemColors.ControlLightLight;
            label14.Location = new Point(422, 28);
            label14.Margin = new Padding(6, 0, 6, 0);
            label14.Name = "label14";
            label14.Size = new Size(839, 63);
            label14.TabIndex = 10;
            label14.Text = "Administración de Secretarias";
            // 
            // btn_ingresar
            // 
            btn_ingresar.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btn_ingresar.BackColor = Color.FromArgb(247, 189, 86);
            btn_ingresar.Cursor = Cursors.Hand;
            btn_ingresar.FlatStyle = FlatStyle.Popup;
            btn_ingresar.Font = new Font("Century Gothic", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_ingresar.ForeColor = SystemColors.ActiveCaptionText;
            btn_ingresar.Location = new Point(32, 34);
            btn_ingresar.Margin = new Padding(6, 6, 6, 6);
            btn_ingresar.Name = "btn_ingresar";
            btn_ingresar.RightToLeft = RightToLeft.Yes;
            btn_ingresar.Size = new Size(288, 62);
            btn_ingresar.TabIndex = 9;
            btn_ingresar.Text = "Cerrar Sesión ";
            btn_ingresar.UseVisualStyleBackColor = false;
            btn_ingresar.Click += btn_ingresar_Click;
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.FromArgb(39, 66, 89);
            panelMenu.BorderRadius = 20;
            panelMenu.Controls.Add(btn_admaestros);
            panelMenu.Controls.Add(btnEnvioBoletas);
            panelMenu.Controls.Add(btnEdicionDatos);
            panelMenu.Controls.Add(btnBitacora);
            panelMenu.Controls.Add(btnAdmSecre);
            panelMenu.Controls.Add(btnEstadisticas);
            panelMenu.Controls.Add(btn_capturaCalif);
            panelMenu.Controls.Add(btn_inscripcion);
            panelMenu.Controls.Add(panelLogo);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Margin = new Padding(6, 6, 6, 6);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(336, 1129);
            panelMenu.TabIndex = 2;
            // 
            // btn_admaestros
            // 
            btn_admaestros.BackgroundImageLayout = ImageLayout.None;
            btn_admaestros.Dock = DockStyle.Top;
            btn_admaestros.FlatAppearance.BorderSize = 0;
            btn_admaestros.FlatStyle = FlatStyle.Flat;
            btn_admaestros.Font = new Font("Microsoft Sans Serif", 12.75F, FontStyle.Bold);
            btn_admaestros.ForeColor = SystemColors.ControlLightLight;
            btn_admaestros.Image = (Image)resources.GetObject("btn_admaestros.Image");
            btn_admaestros.ImageAlign = ContentAlignment.MiddleLeft;
            btn_admaestros.Location = new Point(0, 951);
            btn_admaestros.Margin = new Padding(6, 6, 6, 6);
            btn_admaestros.Name = "btn_admaestros";
            btn_admaestros.Size = new Size(336, 109);
            btn_admaestros.TabIndex = 7;
            btn_admaestros.Text = "Adm. Maestros";
            btn_admaestros.UseVisualStyleBackColor = true;
            btn_admaestros.Click += btn_admaestros_Click;
            // 
            // btnEnvioBoletas
            // 
            btnEnvioBoletas.BackgroundImageLayout = ImageLayout.None;
            btnEnvioBoletas.Dock = DockStyle.Top;
            btnEnvioBoletas.FlatAppearance.BorderSize = 0;
            btnEnvioBoletas.FlatStyle = FlatStyle.Flat;
            btnEnvioBoletas.Font = new Font("Microsoft Sans Serif", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEnvioBoletas.ForeColor = SystemColors.ControlLightLight;
            btnEnvioBoletas.Image = (Image)resources.GetObject("btnEnvioBoletas.Image");
            btnEnvioBoletas.ImageAlign = ContentAlignment.MiddleLeft;
            btnEnvioBoletas.Location = new Point(0, 842);
            btnEnvioBoletas.Margin = new Padding(6, 6, 6, 6);
            btnEnvioBoletas.Name = "btnEnvioBoletas";
            btnEnvioBoletas.Size = new Size(336, 109);
            btnEnvioBoletas.TabIndex = 6;
            btnEnvioBoletas.Text = "Creación \r\nde PDFS";
            btnEnvioBoletas.UseVisualStyleBackColor = true;
            btnEnvioBoletas.Click += btnEnvioBoletas_Click;
            // 
            // btnEdicionDatos
            // 
            btnEdicionDatos.BackgroundImageLayout = ImageLayout.None;
            btnEdicionDatos.Dock = DockStyle.Top;
            btnEdicionDatos.FlatAppearance.BorderSize = 0;
            btnEdicionDatos.FlatStyle = FlatStyle.Flat;
            btnEdicionDatos.Font = new Font("Microsoft Sans Serif", 12.75F, FontStyle.Bold);
            btnEdicionDatos.ForeColor = SystemColors.ControlLight;
            btnEdicionDatos.Image = (Image)resources.GetObject("btnEdicionDatos.Image");
            btnEdicionDatos.ImageAlign = ContentAlignment.MiddleLeft;
            btnEdicionDatos.Location = new Point(0, 733);
            btnEdicionDatos.Margin = new Padding(6, 6, 6, 6);
            btnEdicionDatos.Name = "btnEdicionDatos";
            btnEdicionDatos.Size = new Size(336, 109);
            btnEdicionDatos.TabIndex = 5;
            btnEdicionDatos.Text = "Edición de Datos";
            btnEdicionDatos.UseVisualStyleBackColor = true;
            btnEdicionDatos.Click += btnEdicionDatos_Click;
            // 
            // btnBitacora
            // 
            btnBitacora.BackgroundImageLayout = ImageLayout.None;
            btnBitacora.Dock = DockStyle.Top;
            btnBitacora.FlatAppearance.BorderSize = 0;
            btnBitacora.FlatStyle = FlatStyle.Flat;
            btnBitacora.Font = new Font("Microsoft Sans Serif", 12.75F, FontStyle.Bold);
            btnBitacora.ForeColor = SystemColors.ControlLight;
            btnBitacora.Image = (Image)resources.GetObject("btnBitacora.Image");
            btnBitacora.ImageAlign = ContentAlignment.MiddleLeft;
            btnBitacora.Location = new Point(0, 624);
            btnBitacora.Margin = new Padding(6, 6, 6, 6);
            btnBitacora.Name = "btnBitacora";
            btnBitacora.Size = new Size(336, 109);
            btnBitacora.TabIndex = 4;
            btnBitacora.Text = "Bitacora";
            btnBitacora.UseVisualStyleBackColor = true;
            btnBitacora.Click += btnBitacora_Click;
            // 
            // btnAdmSecre
            // 
            btnAdmSecre.BackgroundImageLayout = ImageLayout.None;
            btnAdmSecre.Dock = DockStyle.Top;
            btnAdmSecre.FlatAppearance.BorderSize = 0;
            btnAdmSecre.FlatStyle = FlatStyle.Flat;
            btnAdmSecre.Font = new Font("Microsoft Sans Serif", 12.75F, FontStyle.Bold);
            btnAdmSecre.ForeColor = SystemColors.ControlLight;
            btnAdmSecre.Image = (Image)resources.GetObject("btnAdmSecre.Image");
            btnAdmSecre.ImageAlign = ContentAlignment.MiddleLeft;
            btnAdmSecre.Location = new Point(0, 515);
            btnAdmSecre.Margin = new Padding(6, 6, 6, 6);
            btnAdmSecre.Name = "btnAdmSecre";
            btnAdmSecre.Size = new Size(336, 109);
            btnAdmSecre.TabIndex = 3;
            btnAdmSecre.Text = "Adm. Secretarias";
            btnAdmSecre.UseVisualStyleBackColor = true;
            btnAdmSecre.Click += btnAdmSecre_Click;
            // 
            // btnEstadisticas
            // 
            btnEstadisticas.BackgroundImageLayout = ImageLayout.None;
            btnEstadisticas.Dock = DockStyle.Top;
            btnEstadisticas.FlatAppearance.BorderSize = 0;
            btnEstadisticas.FlatStyle = FlatStyle.Flat;
            btnEstadisticas.Font = new Font("Microsoft Sans Serif", 12.75F, FontStyle.Bold);
            btnEstadisticas.ForeColor = SystemColors.ControlLight;
            btnEstadisticas.Image = (Image)resources.GetObject("btnEstadisticas.Image");
            btnEstadisticas.ImageAlign = ContentAlignment.MiddleLeft;
            btnEstadisticas.Location = new Point(0, 406);
            btnEstadisticas.Margin = new Padding(6, 6, 6, 6);
            btnEstadisticas.Name = "btnEstadisticas";
            btnEstadisticas.Size = new Size(336, 109);
            btnEstadisticas.TabIndex = 2;
            btnEstadisticas.Text = "Estadisticas";
            btnEstadisticas.UseVisualStyleBackColor = true;
            // 
            // btn_capturaCalif
            // 
            btn_capturaCalif.BackgroundImageLayout = ImageLayout.None;
            btn_capturaCalif.Dock = DockStyle.Top;
            btn_capturaCalif.FlatAppearance.BorderSize = 0;
            btn_capturaCalif.FlatStyle = FlatStyle.Flat;
            btn_capturaCalif.Font = new Font("Microsoft Sans Serif", 12.75F, FontStyle.Bold);
            btn_capturaCalif.ForeColor = SystemColors.ControlLight;
            btn_capturaCalif.Image = (Image)resources.GetObject("btn_capturaCalif.Image");
            btn_capturaCalif.ImageAlign = ContentAlignment.MiddleLeft;
            btn_capturaCalif.Location = new Point(0, 276);
            btn_capturaCalif.Margin = new Padding(6, 6, 6, 6);
            btn_capturaCalif.Name = "btn_capturaCalif";
            btn_capturaCalif.Size = new Size(336, 130);
            btn_capturaCalif.TabIndex = 1;
            btn_capturaCalif.Text = "Captura de \r\nCalificaciones";
            btn_capturaCalif.UseVisualStyleBackColor = true;
            btn_capturaCalif.Click += btn_capturaCalif_Click;
            // 
            // btn_inscripcion
            // 
            btn_inscripcion.BackgroundImageLayout = ImageLayout.None;
            btn_inscripcion.Dock = DockStyle.Top;
            btn_inscripcion.FlatAppearance.BorderSize = 0;
            btn_inscripcion.FlatStyle = FlatStyle.Flat;
            btn_inscripcion.Font = new Font("Microsoft Sans Serif", 12.75F, FontStyle.Bold);
            btn_inscripcion.ForeColor = SystemColors.ButtonHighlight;
            btn_inscripcion.Image = (Image)resources.GetObject("btn_inscripcion.Image");
            btn_inscripcion.ImageAlign = ContentAlignment.MiddleLeft;
            btn_inscripcion.Location = new Point(0, 171);
            btn_inscripcion.Margin = new Padding(6, 6, 6, 6);
            btn_inscripcion.Name = "btn_inscripcion";
            btn_inscripcion.Size = new Size(336, 105);
            btn_inscripcion.TabIndex = 0;
            btn_inscripcion.Text = "Inscripción";
            btn_inscripcion.UseVisualStyleBackColor = true;
            btn_inscripcion.Click += btn_inscripcion_Click;
            // 
            // panelLogo
            // 
            panelLogo.BackColor = Color.FromArgb(39, 66, 89);
            panelLogo.BorderRadius = 20;
            panelLogo.Controls.Add(label4);
            panelLogo.Controls.Add(Logo);
            panelLogo.Controls.Add(label1);
            panelLogo.Dock = DockStyle.Top;
            panelLogo.Location = new Point(0, 0);
            panelLogo.Margin = new Padding(6, 6, 6, 6);
            panelLogo.Name = "panelLogo";
            panelLogo.Size = new Size(336, 171);
            panelLogo.TabIndex = 0;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Consolas", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(17, 107);
            label4.Margin = new Padding(6, 0, 6, 0);
            label4.Name = "label4";
            label4.Size = new Size(324, 52);
            label4.TabIndex = 40;
            label4.Text = "INSTITUTO MANUEL M. ACOSTA\r\n    ";
            // 
            // Logo
            // 
            Logo.Image = Properties.Resources.logo_escuela1;
            Logo.Location = new Point(108, 11);
            Logo.Margin = new Padding(6, 6, 6, 6);
            Logo.Name = "Logo";
            Logo.Size = new Size(106, 100);
            Logo.SizeMode = PictureBoxSizeMode.Zoom;
            Logo.TabIndex = 39;
            Logo.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(-35, 134);
            label1.Margin = new Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new Size(524, 32);
            label1.TabIndex = 1;
            label1.Text = "___________________________________________________";
            // 
            // adm_Secretaria
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(63, 75, 92);
            ClientSize = new Size(1879, 1129);
            Controls.Add(panelApp);
            Controls.Add(panelMenu);
            Margin = new Padding(6, 6, 6, 6);
            Name = "adm_Secretaria";
            Text = "adm_Secretaria";
            TransparencyKey = Color.Transparent;
            Load += adm_Secretaria_Load;
            Paint += panel1_Paint;
            panelApp.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panelMenu.ResumeLayout(false);
            panelLogo.ResumeLayout(false);
            panelLogo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Logo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panelito panelApp;
        private FlowLayoutPanel flowSecretarias;
        private GroupBox groupBox1;
        private Label label18;
        private TextBox txtContrasenaSecre;
        private TextBox txtCorreoSecre;
        private Label label15;
        private Button btnAltaSecretarias;
        private TextBox txtUsuarioSecre;
        private Label label2;
        private Label Usuario;
        private Label label3;
        private Panel panel1;
        private Label label14;
        private Button btn_ingresar;
        private Panelito panelMenu;
        private Button btn_admaestros;
        private Button btnEnvioBoletas;
        private Button btnEdicionDatos;
        private Button btnBitacora;
        private Button btnAdmSecre;
        private Button btnEstadisticas;
        private Button btn_capturaCalif;
        private Button btn_inscripcion;
        private Panelito panelLogo;
        private Label label1;
        private Label label4;
        private PictureBox Logo;
    }
}