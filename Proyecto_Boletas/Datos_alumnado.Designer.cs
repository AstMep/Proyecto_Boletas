namespace Proyecto_Boletas
{
    partial class Datos_alumnado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Datos_alumnado));
            panelApp = new Panelito();
            groupBox6 = new GroupBox();
            label12 = new Label();
            label13 = new Label();
            btnGenerarListas = new Button();
            label2 = new Label();
            cmbGrupo = new ComboBox();
            panel1 = new Panel();
            label14 = new Label();
            btn_ingresar = new Button();
            cmbMes = new ComboBox();
            label3 = new Label();
            panelMenu = new Panelito();
            panelito1 = new Panelito();
            btnEnvioBoletas = new Button();
            btnEdicionDatos = new Button();
            btnEstadisticas = new Button();
            btn_capturaCalif = new Button();
            btn_inscripcion = new Button();
            panelLogo = new Panelito();
            label4 = new Label();
            Logo = new PictureBox();
            label1 = new Label();
            panelApp.SuspendLayout();
            groupBox6.SuspendLayout();
            panel1.SuspendLayout();
            panelMenu.SuspendLayout();
            panelito1.SuspendLayout();
            panelLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Logo).BeginInit();
            SuspendLayout();
            // 
            // panelApp
            // 
            panelApp.AutoScroll = true;
            panelApp.BackColor = Color.FromArgb(181, 131, 120);
            panelApp.BorderRadius = 20;
            panelApp.Controls.Add(groupBox6);
            panelApp.Controls.Add(label2);
            panelApp.Controls.Add(cmbGrupo);
            panelApp.Controls.Add(panel1);
            panelApp.Controls.Add(cmbMes);
            panelApp.Controls.Add(label3);
            panelApp.Dock = DockStyle.Fill;
            panelApp.Location = new Point(181, 0);
            panelApp.Name = "panelApp";
            panelApp.Size = new Size(757, 485);
            panelApp.TabIndex = 3;
            // 
            // groupBox6
            // 
            groupBox6.BackColor = Color.FromArgb(157, 101, 101);
            groupBox6.Controls.Add(label12);
            groupBox6.Controls.Add(label13);
            groupBox6.Controls.Add(btnGenerarListas);
            groupBox6.Location = new Point(93, 104);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(160, 199);
            groupBox6.TabIndex = 14;
            groupBox6.TabStop = false;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.ForeColor = SystemColors.ControlLightLight;
            label12.Location = new Point(11, 175);
            label12.Name = "label12";
            label12.Size = new Size(143, 21);
            label12.TabIndex = 5;
            label12.Text = "Lista de Asistencia";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label13.ForeColor = SystemColors.ControlLightLight;
            label13.Location = new Point(45, 154);
            label13.Name = "label13";
            label13.Size = new Size(76, 21);
            label13.TabIndex = 4;
            label13.Text = "Generar  ";
            // 
            // btnGenerarListas
            // 
            btnGenerarListas.BackColor = Color.FromArgb(212, 168, 133);
            btnGenerarListas.BackgroundImageLayout = ImageLayout.Center;
            btnGenerarListas.FlatStyle = FlatStyle.Popup;
            btnGenerarListas.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGenerarListas.Image = (Image)resources.GetObject("btnGenerarListas.Image");
            btnGenerarListas.Location = new Point(22, 16);
            btnGenerarListas.Name = "btnGenerarListas";
            btnGenerarListas.Size = new Size(121, 135);
            btnGenerarListas.TabIndex = 3;
            btnGenerarListas.UseVisualStyleBackColor = false;
            btnGenerarListas.Click += btnGenerarListas_Click_1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(287, 184);
            label2.Name = "label2";
            label2.Size = new Size(128, 21);
            label2.TabIndex = 18;
            label2.Text = "Seleccione Mes:";
            // 
            // cmbGrupo
            // 
            cmbGrupo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGrupo.Location = new Point(436, 129);
            cmbGrupo.Name = "cmbGrupo";
            cmbGrupo.Size = new Size(133, 23);
            cmbGrupo.TabIndex = 15;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(39, 66, 89);
            panel1.Controls.Add(label14);
            panel1.Controls.Add(btn_ingresar);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(757, 59);
            panel1.TabIndex = 21;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Cascadia Code", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label14.ForeColor = SystemColors.ControlLightLight;
            label14.Location = new Point(336, 13);
            label14.Name = "label14";
            label14.Size = new Size(238, 32);
            label14.TabIndex = 11;
            label14.Text = "Creación de PDFS";
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
            // cmbMes
            // 
            cmbMes.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMes.Items.AddRange(new object[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" });
            cmbMes.Location = new Point(436, 184);
            cmbMes.Name = "cmbMes";
            cmbMes.Size = new Size(133, 23);
            cmbMes.TabIndex = 17;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.ControlLightLight;
            label3.Location = new Point(287, 129);
            label3.Name = "label3";
            label3.Size = new Size(143, 21);
            label3.TabIndex = 16;
            label3.Text = "Seleccione Grupo:";
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.FromArgb(39, 66, 89);
            panelMenu.BorderRadius = 20;
            panelMenu.Controls.Add(panelito1);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(181, 485);
            panelMenu.TabIndex = 2;
            panelMenu.Paint += panelMenu_Paint;
            // 
            // panelito1
            // 
            panelito1.BackColor = Color.FromArgb(39, 66, 89);
            panelito1.BorderRadius = 20;
            panelito1.Controls.Add(btnEnvioBoletas);
            panelito1.Controls.Add(btnEdicionDatos);
            panelito1.Controls.Add(btnEstadisticas);
            panelito1.Controls.Add(btn_capturaCalif);
            panelito1.Controls.Add(btn_inscripcion);
            panelito1.Controls.Add(panelLogo);
            panelito1.Dock = DockStyle.Left;
            panelito1.Location = new Point(0, 0);
            panelito1.Name = "panelito1";
            panelito1.Size = new Size(181, 485);
            panelito1.TabIndex = 4;
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
            btnEnvioBoletas.Location = new Point(0, 292);
            btnEnvioBoletas.Name = "btnEnvioBoletas";
            btnEnvioBoletas.Size = new Size(181, 51);
            btnEnvioBoletas.TabIndex = 6;
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
            btnEdicionDatos.Location = new Point(0, 241);
            btnEdicionDatos.Name = "btnEdicionDatos";
            btnEdicionDatos.Size = new Size(181, 51);
            btnEdicionDatos.TabIndex = 5;
            btnEdicionDatos.Text = "Edición de Datos";
            btnEdicionDatos.UseVisualStyleBackColor = true;
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
            btn_capturaCalif.Font = new Font("Agency FB", 12.75F, FontStyle.Bold);
            btn_capturaCalif.ForeColor = SystemColors.ControlLight;
            btn_capturaCalif.Image = (Image)resources.GetObject("btn_capturaCalif.Image");
            btn_capturaCalif.ImageAlign = ContentAlignment.MiddleLeft;
            btn_capturaCalif.Location = new Point(0, 129);
            btn_capturaCalif.Name = "btn_capturaCalif";
            btn_capturaCalif.Size = new Size(181, 61);
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
            btn_inscripcion.Font = new Font("Agency FB", 12.75F, FontStyle.Bold);
            btn_inscripcion.ForeColor = SystemColors.ButtonHighlight;
            btn_inscripcion.Image = (Image)resources.GetObject("btn_inscripcion.Image");
            btn_inscripcion.ImageAlign = ContentAlignment.MiddleLeft;
            btn_inscripcion.Location = new Point(0, 80);
            btn_inscripcion.Name = "btn_inscripcion";
            btn_inscripcion.Size = new Size(181, 49);
            btn_inscripcion.TabIndex = 0;
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
            // Datos_alumnado
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(63, 75, 92);
            ClientSize = new Size(938, 485);
            Controls.Add(panelApp);
            Controls.Add(panelMenu);
            Name = "Datos_alumnado";
            Text = "Datos_alumnado";
            Load += Datos_alumnado_Load;
            panelApp.ResumeLayout(false);
            panelApp.PerformLayout();
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panelMenu.ResumeLayout(false);
            panelito1.ResumeLayout(false);
            panelLogo.ResumeLayout(false);
            panelLogo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Logo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panelito panelApp;
        private GroupBox groupBox6;
        private Label label12;
        private Label label13;
        private Button btnGenerarListas;
        private Label label2;
        private ComboBox cmbGrupo;
        private ComboBox cmbMes;
        private Label label3;
        private Panel panel1;
        private Button btn_ingresar;
        private Panelito panelMenu;
        private Label label14;
        private Panelito panelito1;
        private Button btnEnvioBoletas;
        private Button btnEdicionDatos;
        private Button btnEstadisticas;
        private Button btn_capturaCalif;
        private Button btn_inscripcion;
        private Panelito panelLogo;
        private Label label4;
        private PictureBox Logo;
        private Label label1;
    }
}