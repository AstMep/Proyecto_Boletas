namespace Proyecto_Boletas
{
    partial class Bitacora
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Bitacora));
            panelApp = new Panelito();
            label2 = new Label();
            cmbMes = new ComboBox();
            label3 = new Label();
            cmbRoles = new ComboBox();
            label12 = new Label();
            label13 = new Label();
            btnGenerarBitacora = new Button();
            panel1 = new Panel();
            label14 = new Label();
            btn_ingresar = new Button();
            panelMenu = new Panelito();
            panelito1 = new Panelito();
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
            panelito2 = new Panelito();
            panelApp.SuspendLayout();
            panel1.SuspendLayout();
            panelMenu.SuspendLayout();
            panelito1.SuspendLayout();
            panelLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Logo).BeginInit();
            panelito2.SuspendLayout();
            SuspendLayout();
            // 
            // panelApp
            // 
            panelApp.BackColor = Color.FromArgb(181, 131, 120);
            panelApp.BorderRadius = 20;
            panelApp.Controls.Add(panelito2);
            panelApp.Controls.Add(panel1);
            panelApp.Dock = DockStyle.Fill;
            panelApp.Location = new Point(207, 0);
            panelApp.Margin = new Padding(3, 4, 3, 4);
            panelApp.Name = "panelApp";
            panelApp.Size = new Size(847, 668);
            panelApp.TabIndex = 3;
            panelApp.Paint += panelApp_Paint;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(220, 139);
            label2.Name = "label2";
            label2.Size = new Size(157, 28);
            label2.TabIndex = 37;
            label2.Text = "Seleccione Mes:";
            // 
            // cmbMes
            // 
            cmbMes.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMes.Items.AddRange(new object[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" });
            cmbMes.Location = new Point(391, 139);
            cmbMes.Margin = new Padding(3, 4, 3, 4);
            cmbMes.Name = "cmbMes";
            cmbMes.Size = new Size(151, 28);
            cmbMes.TabIndex = 36;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.ControlLightLight;
            label3.Location = new Point(220, 66);
            label3.Name = "label3";
            label3.Size = new Size(168, 28);
            label3.TabIndex = 35;
            label3.Text = "Seleccione Roles:";
            // 
            // cmbRoles
            // 
            cmbRoles.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRoles.Location = new Point(391, 66);
            cmbRoles.Margin = new Padding(3, 4, 3, 4);
            cmbRoles.Name = "cmbRoles";
            cmbRoles.Size = new Size(151, 28);
            cmbRoles.TabIndex = 34;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.ForeColor = SystemColors.ControlLightLight;
            label12.Location = new Point(55, 197);
            label12.Name = "label12";
            label12.Size = new Size(84, 28);
            label12.TabIndex = 5;
            label12.Text = "Bitacora";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label13.ForeColor = SystemColors.ControlLightLight;
            label13.Location = new Point(55, 169);
            label13.Name = "label13";
            label13.Size = new Size(96, 28);
            label13.TabIndex = 4;
            label13.Text = "Generar  ";
            // 
            // btnGenerarBitacora
            // 
            btnGenerarBitacora.BackColor = Color.FromArgb(212, 168, 133);
            btnGenerarBitacora.BackgroundImageLayout = ImageLayout.Center;
            btnGenerarBitacora.FlatStyle = FlatStyle.Popup;
            btnGenerarBitacora.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGenerarBitacora.Image = (Image)resources.GetObject("btnGenerarBitacora.Image");
            btnGenerarBitacora.Location = new Point(45, 19);
            btnGenerarBitacora.Margin = new Padding(3, 4, 3, 4);
            btnGenerarBitacora.Name = "btnGenerarBitacora";
            btnGenerarBitacora.Size = new Size(128, 148);
            btnGenerarBitacora.TabIndex = 3;
            btnGenerarBitacora.UseVisualStyleBackColor = false;
            btnGenerarBitacora.Click += btnGenerarBitacora_Click_1;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(39, 66, 89);
            panel1.Controls.Add(label14);
            panel1.Controls.Add(btn_ingresar);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(847, 79);
            panel1.TabIndex = 21;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Cascadia Code", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label14.ForeColor = SystemColors.ControlText;
            label14.Location = new Point(330, 16);
            label14.Name = "label14";
            label14.Size = new Size(161, 40);
            label14.TabIndex = 10;
            label14.Text = "Bitacora";
            // 
            // btn_ingresar
            // 
            btn_ingresar.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btn_ingresar.BackColor = Color.FromArgb(247, 189, 86);
            btn_ingresar.Cursor = Cursors.Hand;
            btn_ingresar.FlatStyle = FlatStyle.Popup;
            btn_ingresar.Font = new Font("Century Gothic", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_ingresar.ForeColor = SystemColors.ActiveCaptionText;
            btn_ingresar.Location = new Point(19, 21);
            btn_ingresar.Margin = new Padding(3, 4, 3, 4);
            btn_ingresar.Name = "btn_ingresar";
            btn_ingresar.RightToLeft = RightToLeft.Yes;
            btn_ingresar.Size = new Size(177, 39);
            btn_ingresar.TabIndex = 9;
            btn_ingresar.Text = "Cerrar Sesión ";
            btn_ingresar.UseVisualStyleBackColor = false;
            btn_ingresar.Click += btn_ingresar_Click;
            // 
            // panelMenu
            // 
            panelMenu.BackColor = Color.FromArgb(39, 66, 89);
            panelMenu.BorderRadius = 20;
            panelMenu.Controls.Add(panelito1);
            panelMenu.Dock = DockStyle.Left;
            panelMenu.Location = new Point(0, 0);
            panelMenu.Margin = new Padding(3, 4, 3, 4);
            panelMenu.Name = "panelMenu";
            panelMenu.Size = new Size(207, 668);
            panelMenu.TabIndex = 2;
            // 
            // panelito1
            // 
            panelito1.BackColor = Color.FromArgb(39, 66, 89);
            panelito1.BorderRadius = 20;
            panelito1.Controls.Add(btn_admaestros);
            panelito1.Controls.Add(btnEnvioBoletas);
            panelito1.Controls.Add(btnEdicionDatos);
            panelito1.Controls.Add(btnBitacora);
            panelito1.Controls.Add(btnAdmSecre);
            panelito1.Controls.Add(btnEstadisticas);
            panelito1.Controls.Add(btn_capturaCalif);
            panelito1.Controls.Add(btn_inscripcion);
            panelito1.Controls.Add(panelLogo);
            panelito1.Dock = DockStyle.Left;
            panelito1.Location = new Point(0, 0);
            panelito1.Margin = new Padding(3, 4, 3, 4);
            panelito1.Name = "panelito1";
            panelito1.Size = new Size(207, 668);
            panelito1.TabIndex = 4;
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
            btn_admaestros.Location = new Point(0, 593);
            btn_admaestros.Margin = new Padding(3, 4, 3, 4);
            btn_admaestros.Name = "btn_admaestros";
            btn_admaestros.Size = new Size(207, 68);
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
            btnEnvioBoletas.Font = new Font("Agency FB", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEnvioBoletas.ForeColor = SystemColors.ControlLightLight;
            btnEnvioBoletas.Image = (Image)resources.GetObject("btnEnvioBoletas.Image");
            btnEnvioBoletas.ImageAlign = ContentAlignment.MiddleLeft;
            btnEnvioBoletas.Location = new Point(0, 525);
            btnEnvioBoletas.Margin = new Padding(3, 4, 3, 4);
            btnEnvioBoletas.Name = "btnEnvioBoletas";
            btnEnvioBoletas.Size = new Size(207, 68);
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
            btnEdicionDatos.Font = new Font("Agency FB", 12.75F, FontStyle.Bold);
            btnEdicionDatos.ForeColor = SystemColors.ControlLight;
            btnEdicionDatos.Image = (Image)resources.GetObject("btnEdicionDatos.Image");
            btnEdicionDatos.ImageAlign = ContentAlignment.MiddleLeft;
            btnEdicionDatos.Location = new Point(0, 457);
            btnEdicionDatos.Margin = new Padding(3, 4, 3, 4);
            btnEdicionDatos.Name = "btnEdicionDatos";
            btnEdicionDatos.Size = new Size(207, 68);
            btnEdicionDatos.TabIndex = 5;
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
            btnBitacora.Location = new Point(0, 389);
            btnBitacora.Margin = new Padding(3, 4, 3, 4);
            btnBitacora.Name = "btnBitacora";
            btnBitacora.Size = new Size(207, 68);
            btnBitacora.TabIndex = 4;
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
            btnAdmSecre.Location = new Point(0, 321);
            btnAdmSecre.Margin = new Padding(3, 4, 3, 4);
            btnAdmSecre.Name = "btnAdmSecre";
            btnAdmSecre.Size = new Size(207, 68);
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
            btnEstadisticas.Font = new Font("Agency FB", 12.75F, FontStyle.Bold);
            btnEstadisticas.ForeColor = SystemColors.ControlLight;
            btnEstadisticas.Image = (Image)resources.GetObject("btnEstadisticas.Image");
            btnEstadisticas.ImageAlign = ContentAlignment.MiddleLeft;
            btnEstadisticas.Location = new Point(0, 253);
            btnEstadisticas.Margin = new Padding(3, 4, 3, 4);
            btnEstadisticas.Name = "btnEstadisticas";
            btnEstadisticas.Size = new Size(207, 68);
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
            btn_capturaCalif.Location = new Point(0, 172);
            btn_capturaCalif.Margin = new Padding(3, 4, 3, 4);
            btn_capturaCalif.Name = "btn_capturaCalif";
            btn_capturaCalif.Size = new Size(207, 81);
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
            btn_inscripcion.Location = new Point(0, 107);
            btn_inscripcion.Margin = new Padding(3, 4, 3, 4);
            btn_inscripcion.Name = "btn_inscripcion";
            btn_inscripcion.Size = new Size(207, 65);
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
            panelLogo.Margin = new Padding(3, 4, 3, 4);
            panelLogo.Name = "panelLogo";
            panelLogo.Size = new Size(207, 107);
            panelLogo.TabIndex = 0;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Consolas", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(10, 67);
            label4.Name = "label4";
            label4.Size = new Size(216, 34);
            label4.TabIndex = 40;
            label4.Text = "INSTITUTO MANUEL M. ACOSTA\r\n    ";
            // 
            // Logo
            // 
            Logo.Image = Properties.Resources.logo_escuela1;
            Logo.Location = new Point(66, 7);
            Logo.Margin = new Padding(3, 4, 3, 4);
            Logo.Name = "Logo";
            Logo.Size = new Size(65, 63);
            Logo.SizeMode = PictureBoxSizeMode.Zoom;
            Logo.TabIndex = 39;
            Logo.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(-22, 84);
            label1.Name = "label1";
            label1.Size = new Size(315, 20);
            label1.TabIndex = 1;
            label1.Text = "___________________________________________________";
            // 
            // panelito2
            // 
            panelito2.BackColor = Color.FromArgb(157, 101, 101);
            panelito2.BorderRadius = 20;
            panelito2.Controls.Add(btnGenerarBitacora);
            panelito2.Controls.Add(label2);
            panelito2.Controls.Add(label12);
            panelito2.Controls.Add(cmbMes);
            panelito2.Controls.Add(label13);
            panelito2.Controls.Add(label3);
            panelito2.Controls.Add(cmbRoles);
            panelito2.Location = new Point(63, 132);
            panelito2.Name = "panelito2";
            panelito2.Size = new Size(695, 257);
            panelito2.TabIndex = 38;
            // 
            // Bitacora
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(63, 75, 92);
            ClientSize = new Size(1054, 668);
            Controls.Add(panelApp);
            Controls.Add(panelMenu);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Bitacora";
            Text = "Bitacora";
            panelApp.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panelMenu.ResumeLayout(false);
            panelito1.ResumeLayout(false);
            panelLogo.ResumeLayout(false);
            panelLogo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Logo).EndInit();
            panelito2.ResumeLayout(false);
            panelito2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panelito panelApp;
        private Label label2;
        private ComboBox cmbMes;
        private Label label3;
        private ComboBox cmbRoles;
        private Label label12;
        private Label label13;
        private Button btnGenerarBitacora;
        private Panel panel1;
        private Label label14;
        private Button btn_ingresar;
        private Panelito panelMenu;
        private Panelito panelito1;
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
        private Panelito panelito2;
    }
}