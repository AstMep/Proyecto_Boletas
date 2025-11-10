namespace Proyecto_Boletas
{
    partial class Mod_Estadisticas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mod_Estadisticas));
            panelApp = new Panelito();
            panelito1 = new Panelito();
            label5 = new Label();
            btnCEstadisticas = new Button();
            label9 = new Label();
            label3 = new Label();
            label2 = new Label();
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
            panelito1.SuspendLayout();
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
            panelApp.Controls.Add(panelito1);
            panelApp.Controls.Add(label2);
            panelApp.Controls.Add(panel1);
            panelApp.Dock = DockStyle.Fill;
            panelApp.Location = new Point(201, 0);
            panelApp.Name = "panelApp";
            panelApp.Size = new Size(824, 502);
            panelApp.TabIndex = 8;
            panelApp.Paint += panelApp_Paint;
            // 
            // panelito1
            // 
            panelito1.BackColor = Color.FromArgb(157, 101, 101);
            panelito1.BorderRadius = 20;
            panelito1.Controls.Add(label5);
            panelito1.Controls.Add(btnCEstadisticas);
            panelito1.Controls.Add(label9);
            panelito1.Controls.Add(label3);
            panelito1.Location = new Point(94, 159);
            panelito1.Name = "panelito1";
            panelito1.Size = new Size(659, 266);
            panelito1.TabIndex = 23;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Yu Gothic UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = SystemColors.ActiveCaptionText;
            label5.Location = new Point(232, 97);
            label5.Name = "label5";
            label5.Size = new Size(355, 120);
            label5.TabIndex = 55;
            label5.Text = resources.GetString("label5.Text");
            // 
            // btnCEstadisticas
            // 
            btnCEstadisticas.BackColor = Color.FromArgb(212, 168, 133);
            btnCEstadisticas.BackgroundImage = Properties.Resources.estadisticas128;
            btnCEstadisticas.BackgroundImageLayout = ImageLayout.None;
            btnCEstadisticas.Location = new Point(62, 50);
            btnCEstadisticas.Name = "btnCEstadisticas";
            btnCEstadisticas.Size = new Size(147, 143);
            btnCEstadisticas.TabIndex = 54;
            btnCEstadisticas.UseVisualStyleBackColor = false;
            btnCEstadisticas.Click += btnCEstadisticas_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Yu Gothic UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.ForeColor = SystemColors.ControlLightLight;
            label9.Location = new Point(255, 22);
            label9.Name = "label9";
            label9.Size = new Size(250, 60);
            label9.TabIndex = 53;
            label9.Text = "Sección de Creación \r\n  de PDF de la institución";
            label9.Click += label9_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.ControlLightLight;
            label3.Location = new Point(62, 196);
            label3.Name = "label3";
            label3.Size = new Size(136, 21);
            label3.TabIndex = 36;
            label3.Text = "Crear Estadisticas";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Cascadia Code", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ButtonFace;
            label2.Location = new Point(246, 97);
            label2.Name = "label2";
            label2.Size = new Size(322, 32);
            label2.TabIndex = 22;
            label2.Text = "Estadisticas Escolares";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(39, 66, 89);
            panel1.Controls.Add(label14);
            panel1.Controls.Add(btn_ingresar);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(824, 59);
            panel1.TabIndex = 21;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Cascadia Code", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label14.ForeColor = SystemColors.ControlText;
            label14.Location = new Point(246, 13);
            label14.Name = "label14";
            label14.Size = new Size(392, 32);
            label14.TabIndex = 12;
            label14.Text = "Bienvenido al Sitema FAY JC";
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
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(201, 502);
            panelMenu.TabIndex = 7;
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
            btn_admaestros.Location = new Point(0, 445);
            btn_admaestros.Name = "btn_admaestros";
            btn_admaestros.Size = new Size(201, 51);
            btn_admaestros.TabIndex = 7;
            btn_admaestros.Text = "Adm. Maestros";
            btn_admaestros.UseVisualStyleBackColor = true;
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
            btnEnvioBoletas.Location = new Point(0, 394);
            btnEnvioBoletas.Name = "btnEnvioBoletas";
            btnEnvioBoletas.Size = new Size(201, 51);
            btnEnvioBoletas.TabIndex = 6;
            btnEnvioBoletas.Text = "Creación de PDFS";
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
            btnEdicionDatos.Location = new Point(0, 343);
            btnEdicionDatos.Name = "btnEdicionDatos";
            btnEdicionDatos.Size = new Size(201, 51);
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
            btnBitacora.Location = new Point(0, 292);
            btnBitacora.Name = "btnBitacora";
            btnBitacora.Size = new Size(201, 51);
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
            btnAdmSecre.Location = new Point(0, 241);
            btnAdmSecre.Name = "btnAdmSecre";
            btnAdmSecre.Size = new Size(201, 51);
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
            btnEstadisticas.Location = new Point(0, 190);
            btnEstadisticas.Name = "btnEstadisticas";
            btnEstadisticas.Size = new Size(201, 51);
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
            btn_capturaCalif.Location = new Point(0, 129);
            btn_capturaCalif.Name = "btn_capturaCalif";
            btn_capturaCalif.Size = new Size(201, 61);
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
            btn_inscripcion.Location = new Point(0, 80);
            btn_inscripcion.Name = "btn_inscripcion";
            btn_inscripcion.Size = new Size(201, 49);
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
            panelLogo.Name = "panelLogo";
            panelLogo.Size = new Size(201, 80);
            panelLogo.TabIndex = 0;
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
            // Mod_Estadisticas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1025, 502);
            Controls.Add(panelApp);
            Controls.Add(panelMenu);
            Name = "Mod_Estadisticas";
            Text = "Mod_Estadisticas";
            Load += Mod_Estadisticas_Load;
            panelApp.ResumeLayout(false);
            panelApp.PerformLayout();
            panelito1.ResumeLayout(false);
            panelito1.PerformLayout();
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
        private Label label2;
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
        private Label label4;
        private PictureBox Logo;
        private Label label1;
        private Panelito panelito1;
        private Label label3;
        private Label label9;
        private Button btnCEstadisticas;
        private Label label5;
    }
}