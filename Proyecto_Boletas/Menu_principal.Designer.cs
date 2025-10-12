namespace Proyecto_Boletas
{
    partial class Menu_principal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu_principal));
            panelApp = new Panelito();
            pictureBox1 = new PictureBox();
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
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel1.SuspendLayout();
            panelMenu.SuspendLayout();
            panelLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Logo).BeginInit();
            SuspendLayout();
            // 
            // panelApp
            // 
            panelApp.AutoScroll = true;
            panelApp.BackColor = Color.FromArgb(181, 131, 120);
            panelApp.BorderRadius = 20;
            panelApp.Controls.Add(pictureBox1);
            panelApp.Controls.Add(panel1);
            panelApp.Dock = DockStyle.Fill;
            panelApp.Location = new Point(181, 0);
            panelApp.Name = "panelApp";
            panelApp.Size = new Size(704, 517);
            panelApp.TabIndex = 3;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.logo_escuela1;
            pictureBox1.Location = new Point(95, 65);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(476, 301);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 23;
            pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(39, 66, 89);
            panel1.Controls.Add(label14);
            panel1.Controls.Add(btn_ingresar);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(704, 59);
            panel1.TabIndex = 21;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Cascadia Code", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label14.ForeColor = SystemColors.ControlLightLight;
            label14.Location = new Point(206, 13);
            label14.Name = "label14";
            label14.Size = new Size(406, 32);
            label14.TabIndex = 10;
            label14.Text = "Bienvenido al Sistema FAY JC";
            // 
            // btn_ingresar
            // 
            btn_ingresar.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btn_ingresar.BackColor = Color.FromArgb(247, 189, 86);
            btn_ingresar.Cursor = Cursors.Hand;
            btn_ingresar.FlatStyle = FlatStyle.Popup;
            btn_ingresar.Font = new Font("Century Gothic", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_ingresar.ForeColor = SystemColors.ActiveCaptionText;
            btn_ingresar.Location = new Point(17, 16);
            btn_ingresar.Name = "btn_ingresar";
            btn_ingresar.RightToLeft = RightToLeft.Yes;
            btn_ingresar.Size = new Size(155, 29);
            btn_ingresar.TabIndex = 9;
            btn_ingresar.Text = "Cerrar Sesión ";
            btn_ingresar.UseVisualStyleBackColor = false;
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
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(181, 517);
            panelMenu.TabIndex = 2;
            // 
            // btn_admaestros
            // 
            btn_admaestros.BackgroundImageLayout = ImageLayout.None;
            btn_admaestros.Dock = DockStyle.Top;
            btn_admaestros.FlatAppearance.BorderSize = 0;
            btn_admaestros.FlatStyle = FlatStyle.Flat;
            btn_admaestros.Font = new Font("Agency FB", 12.75F, FontStyle.Bold);
            btn_admaestros.ForeColor = SystemColors.ControlLightLight;
            btn_admaestros.Image = (Image)resources.GetObject("btn_admaestros.Image");
            btn_admaestros.ImageAlign = ContentAlignment.MiddleLeft;
            btn_admaestros.Location = new Point(0, 445);
            btn_admaestros.Name = "btn_admaestros";
            btn_admaestros.Size = new Size(181, 51);
            btn_admaestros.TabIndex = 16;
            btn_admaestros.Text = "Adm. Maestros";
            btn_admaestros.UseVisualStyleBackColor = true;
            // 
            // btnEnvioBoletas
            // 
            btnEnvioBoletas.BackgroundImageLayout = ImageLayout.None;
            btnEnvioBoletas.Dock = DockStyle.Top;
            btnEnvioBoletas.FlatAppearance.BorderSize = 0;
            btnEnvioBoletas.FlatStyle = FlatStyle.Flat;
            btnEnvioBoletas.Font = new Font("Agency FB", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEnvioBoletas.ForeColor = SystemColors.ControlLightLight;
            btnEnvioBoletas.Image = (Image)resources.GetObject("btnEnvioBoletas.Image");
            btnEnvioBoletas.ImageAlign = ContentAlignment.MiddleLeft;
            btnEnvioBoletas.Location = new Point(0, 394);
            btnEnvioBoletas.Name = "btnEnvioBoletas";
            btnEnvioBoletas.Size = new Size(181, 51);
            btnEnvioBoletas.TabIndex = 15;
            btnEnvioBoletas.Text = "Creación de PDFS";
            btnEnvioBoletas.UseVisualStyleBackColor = true;
            // 
            // btnEdicionDatos
            // 
            btnEdicionDatos.BackgroundImageLayout = ImageLayout.None;
            btnEdicionDatos.Dock = DockStyle.Top;
            btnEdicionDatos.FlatAppearance.BorderSize = 0;
            btnEdicionDatos.FlatStyle = FlatStyle.Flat;
            btnEdicionDatos.Font = new Font("Agency FB", 12.75F, FontStyle.Bold);
            btnEdicionDatos.ForeColor = SystemColors.ControlLight;
            btnEdicionDatos.Image = (Image)resources.GetObject("btnEdicionDatos.Image");
            btnEdicionDatos.ImageAlign = ContentAlignment.MiddleLeft;
            btnEdicionDatos.Location = new Point(0, 343);
            btnEdicionDatos.Name = "btnEdicionDatos";
            btnEdicionDatos.Size = new Size(181, 51);
            btnEdicionDatos.TabIndex = 14;
            btnEdicionDatos.Text = "Edición de Datos";
            btnEdicionDatos.UseVisualStyleBackColor = true;
            // 
            // btnBitacora
            // 
            btnBitacora.BackgroundImageLayout = ImageLayout.None;
            btnBitacora.Dock = DockStyle.Top;
            btnBitacora.FlatAppearance.BorderSize = 0;
            btnBitacora.FlatStyle = FlatStyle.Flat;
            btnBitacora.Font = new Font("Agency FB", 12.75F, FontStyle.Bold);
            btnBitacora.ForeColor = SystemColors.ControlLight;
            btnBitacora.Image = (Image)resources.GetObject("btnBitacora.Image");
            btnBitacora.ImageAlign = ContentAlignment.MiddleLeft;
            btnBitacora.Location = new Point(0, 292);
            btnBitacora.Name = "btnBitacora";
            btnBitacora.Size = new Size(181, 51);
            btnBitacora.TabIndex = 13;
            btnBitacora.Text = "Bitacora";
            btnBitacora.UseVisualStyleBackColor = true;
            // 
            // btnAdmSecre
            // 
            btnAdmSecre.BackgroundImageLayout = ImageLayout.None;
            btnAdmSecre.Dock = DockStyle.Top;
            btnAdmSecre.FlatAppearance.BorderSize = 0;
            btnAdmSecre.FlatStyle = FlatStyle.Flat;
            btnAdmSecre.Font = new Font("Agency FB", 12.75F, FontStyle.Bold);
            btnAdmSecre.ForeColor = SystemColors.ControlLight;
            btnAdmSecre.Image = (Image)resources.GetObject("btnAdmSecre.Image");
            btnAdmSecre.ImageAlign = ContentAlignment.MiddleLeft;
            btnAdmSecre.Location = new Point(0, 241);
            btnAdmSecre.Name = "btnAdmSecre";
            btnAdmSecre.Size = new Size(181, 51);
            btnAdmSecre.TabIndex = 12;
            btnAdmSecre.Text = "Adm. Secretarias";
            btnAdmSecre.UseVisualStyleBackColor = true;
            // 
            // btnEstadisticas
            // 
            btnEstadisticas.BackgroundImageLayout = ImageLayout.None;
            btnEstadisticas.Dock = DockStyle.Top;
            btnEstadisticas.FlatAppearance.BorderSize = 0;
            btnEstadisticas.FlatStyle = FlatStyle.Flat;
            btnEstadisticas.Font = new Font("Agency FB", 12.75F, FontStyle.Bold);
            btnEstadisticas.ForeColor = SystemColors.ControlLight;
            btnEstadisticas.Image = (Image)resources.GetObject("btnEstadisticas.Image");
            btnEstadisticas.ImageAlign = ContentAlignment.MiddleLeft;
            btnEstadisticas.Location = new Point(0, 190);
            btnEstadisticas.Name = "btnEstadisticas";
            btnEstadisticas.Size = new Size(181, 51);
            btnEstadisticas.TabIndex = 11;
            btnEstadisticas.Text = "Estadisticas";
            btnEstadisticas.UseVisualStyleBackColor = true;
            // 
            // btn_capturaCalif
            // 
            btn_capturaCalif.BackgroundImageLayout = ImageLayout.None;
            btn_capturaCalif.Dock = DockStyle.Top;
            btn_capturaCalif.FlatAppearance.BorderSize = 0;
            btn_capturaCalif.FlatStyle = FlatStyle.Flat;
            btn_capturaCalif.Font = new Font("Agency FB", 12.75F, FontStyle.Bold);
            btn_capturaCalif.ForeColor = SystemColors.ControlLight;
            btn_capturaCalif.Image = (Image)resources.GetObject("btn_capturaCalif.Image");
            btn_capturaCalif.ImageAlign = ContentAlignment.MiddleLeft;
            btn_capturaCalif.Location = new Point(0, 129);
            btn_capturaCalif.Name = "btn_capturaCalif";
            btn_capturaCalif.Size = new Size(181, 61);
            btn_capturaCalif.TabIndex = 10;
            btn_capturaCalif.Text = "Captura de \r\nCalificaciones";
            btn_capturaCalif.UseVisualStyleBackColor = true;
            // 
            // btn_inscripcion
            // 
            btn_inscripcion.BackgroundImageLayout = ImageLayout.None;
            btn_inscripcion.Dock = DockStyle.Top;
            btn_inscripcion.FlatAppearance.BorderSize = 0;
            btn_inscripcion.FlatStyle = FlatStyle.Flat;
            btn_inscripcion.Font = new Font("Agency FB", 12.75F, FontStyle.Bold);
            btn_inscripcion.ForeColor = SystemColors.ButtonHighlight;
            btn_inscripcion.Image = (Image)resources.GetObject("btn_inscripcion.Image");
            btn_inscripcion.ImageAlign = ContentAlignment.MiddleLeft;
            btn_inscripcion.Location = new Point(0, 80);
            btn_inscripcion.Name = "btn_inscripcion";
            btn_inscripcion.Size = new Size(181, 49);
            btn_inscripcion.TabIndex = 8;
            btn_inscripcion.Text = "Inscripción";
            btn_inscripcion.UseVisualStyleBackColor = true;
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
            panelLogo.Name = "panelLogo";
            panelLogo.Size = new Size(181, 80);
            panelLogo.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Consolas", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(9, 50);
            label4.Name = "label4";
            label4.Size = new Size(163, 26);
            label4.TabIndex = 40;
            label4.Text = "INSTITUTO MANUEL M. ACOSTA\r\n    ";
            // 
            // Logo
            // 
            Logo.Image = Properties.Resources.logo_escuela1;
            Logo.Location = new Point(58, 5);
            Logo.Name = "Logo";
            Logo.Size = new Size(57, 47);
            Logo.SizeMode = PictureBoxSizeMode.Zoom;
            Logo.TabIndex = 39;
            Logo.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(-19, 63);
            label1.Name = "label1";
            label1.Size = new Size(262, 15);
            label1.TabIndex = 1;
            label1.Text = "___________________________________________________";
            // 
            // Menu_principal
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.FromArgb(181, 131, 120);
            BackgroundImageLayout = ImageLayout.None;
            ClientSize = new Size(885, 517);
            Controls.Add(panelApp);
            Controls.Add(panelMenu);
            Name = "Menu_principal";
            Text = "Menu_principal";
            Load += Menu_principal_Load;
            panelApp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
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
        private Panel panel1;
        private Label label14;
        private Button btn_ingresar;
        private Panelito panelMenu;
        private PictureBox pictureBox1;
        private Button btn_admaestros;
        private Button btnEnvioBoletas;
        private Button btnEdicionDatos;
        private Button btnBitacora;
        private Button btnAdmSecre;
        private Button btnEstadisticas;
        private Button btn_capturaCalif;
        private Button btn_inscripcion;
        private Panelito panelLogo;
        private Label label4;
        private PictureBox Logo;
        private Label label1;
    }
}