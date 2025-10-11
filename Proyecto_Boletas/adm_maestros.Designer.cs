namespace Proyecto_Boletas
{
    partial class adm_maestros
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(adm_maestros));
            panelApp = new Panelito();
            flowMaestros = new FlowLayoutPanel();
            groupBox1 = new GroupBox();
            grupo_asignado = new ComboBox();
            label3 = new Label();
            label6 = new Label();
            txtcorreomaestro = new TextBox();
            label5 = new Label();
            label18 = new Label();
            txtammaestro = new TextBox();
            txtapmaestro = new TextBox();
            label15 = new Label();
            btnAltaMaestros = new Button();
            txtnombremaestro = new TextBox();
            label2 = new Label();
            Usuario = new Label();
            label4 = new Label();
            panel1 = new Panel();
            btn_ingresar = new Button();
            panelMenu = new Panelito();
            btn_admaestros = new Button();
            btnCreacionPDF = new Button();
            btnEdicionDatos = new Button();
            btnBitacora = new Button();
            btnAdmSecre = new Button();
            btnEstadisticas = new Button();
            btn_capturaCalif = new Button();
            btn_inscripcion = new Button();
            panelLogo = new Panelito();
            label1 = new Label();
            Logo = new PictureBox();
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
            panelApp.Controls.Add(flowMaestros);
            panelApp.Controls.Add(groupBox1);
            panelApp.Controls.Add(panel1);
            panelApp.Dock = DockStyle.Fill;
            panelApp.Location = new Point(181, 0);
            panelApp.Name = "panelApp";
            panelApp.Size = new Size(835, 493);
            panelApp.TabIndex = 3;
            // 
            // flowMaestros
            // 
            flowMaestros.AutoScroll = true;
            flowMaestros.BackColor = Color.FromArgb(63, 75, 92);
            flowMaestros.Location = new Point(93, 372);
            flowMaestros.Name = "flowMaestros";
            flowMaestros.Size = new Size(620, 170);
            flowMaestros.TabIndex = 29;
            flowMaestros.Paint += flowMaestros_Paint;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.FromArgb(63, 75, 92);
            groupBox1.Controls.Add(grupo_asignado);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(txtcorreomaestro);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label18);
            groupBox1.Controls.Add(txtammaestro);
            groupBox1.Controls.Add(txtapmaestro);
            groupBox1.Controls.Add(label15);
            groupBox1.Controls.Add(btnAltaMaestros);
            groupBox1.Controls.Add(txtnombremaestro);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(Usuario);
            groupBox1.Controls.Add(label4);
            groupBox1.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold);
            groupBox1.Location = new Point(68, 80);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(645, 272);
            groupBox1.TabIndex = 28;
            groupBox1.TabStop = false;
            // 
            // grupo_asignado
            // 
            grupo_asignado.DropDownStyle = ComboBoxStyle.DropDownList;
            grupo_asignado.Location = new Point(130, 226);
            grupo_asignado.Name = "grupo_asignado";
            grupo_asignado.Size = new Size(121, 26);
            grupo_asignado.TabIndex = 0;
            grupo_asignado.SelectedIndexChanged += grupo_asignado_SelectedIndexChanged_1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(28, 229);
            label3.Name = "label3";
            label3.Size = new Size(59, 18);
            label3.TabIndex = 17;
            label3.Text = "Grupo:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(27, 128);
            label6.Name = "label6";
            label6.Size = new Size(74, 36);
            label6.TabIndex = 16;
            label6.Text = "Apellido\r\nMaterno:";
            // 
            // txtcorreomaestro
            // 
            txtcorreomaestro.BackColor = SystemColors.WindowFrame;
            txtcorreomaestro.Location = new Point(130, 177);
            txtcorreomaestro.Name = "txtcorreomaestro";
            txtcorreomaestro.Size = new Size(258, 26);
            txtcorreomaestro.TabIndex = 13;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(28, 182);
            label5.Name = "label5";
            label5.Size = new Size(64, 18);
            label5.TabIndex = 10;
            label5.Text = "Correo:";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.BackColor = Color.Transparent;
            label18.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label18.ForeColor = SystemColors.ControlLightLight;
            label18.Location = new Point(495, 141);
            label18.Name = "label18";
            label18.Size = new Size(99, 21);
            label18.TabIndex = 9;
            label18.Text = "Información";
            // 
            // txtammaestro
            // 
            txtammaestro.BackColor = SystemColors.WindowFrame;
            txtammaestro.Location = new Point(130, 134);
            txtammaestro.Name = "txtammaestro";
            txtammaestro.Size = new Size(258, 26);
            txtammaestro.TabIndex = 6;
            // 
            // txtapmaestro
            // 
            txtapmaestro.BackColor = SystemColors.WindowFrame;
            txtapmaestro.Location = new Point(130, 88);
            txtapmaestro.Name = "txtapmaestro";
            txtapmaestro.Size = new Size(258, 26);
            txtapmaestro.TabIndex = 5;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.BackColor = Color.Transparent;
            label15.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label15.ForeColor = SystemColors.ControlLightLight;
            label15.Location = new Point(510, 120);
            label15.Name = "label15";
            label15.Size = new Size(68, 21);
            label15.TabIndex = 5;
            label15.Text = "Guardar";
            // 
            // btnAltaMaestros
            // 
            btnAltaMaestros.BackColor = Color.FromArgb(212, 168, 133);
            btnAltaMaestros.BackgroundImageLayout = ImageLayout.Center;
            btnAltaMaestros.FlatStyle = FlatStyle.Popup;
            btnAltaMaestros.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAltaMaestros.Image = (Image)resources.GetObject("btnAltaMaestros.Image");
            btnAltaMaestros.Location = new Point(521, 65);
            btnAltaMaestros.Name = "btnAltaMaestros";
            btnAltaMaestros.Size = new Size(52, 52);
            btnAltaMaestros.TabIndex = 3;
            btnAltaMaestros.UseVisualStyleBackColor = false;
            btnAltaMaestros.Click += btnAltaMaestros_Click;
            // 
            // txtnombremaestro
            // 
            txtnombremaestro.BackColor = SystemColors.WindowFrame;
            txtnombremaestro.Location = new Point(130, 46);
            txtnombremaestro.Name = "txtnombremaestro";
            txtnombremaestro.Size = new Size(258, 26);
            txtnombremaestro.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(27, 138);
            label2.Name = "label2";
            label2.Size = new Size(0, 18);
            label2.TabIndex = 2;
            // 
            // Usuario
            // 
            Usuario.AutoSize = true;
            Usuario.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Usuario.Location = new Point(27, 46);
            Usuario.Name = "Usuario";
            Usuario.Size = new Size(72, 18);
            Usuario.TabIndex = 3;
            Usuario.Text = "Nombre:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(28, 82);
            label4.Name = "label4";
            label4.Size = new Size(71, 36);
            label4.TabIndex = 1;
            label4.Text = "Apellido\r\nPaterno:";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(39, 66, 89);
            panel1.Controls.Add(btn_ingresar);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(835, 59);
            panel1.TabIndex = 21;
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
            panelMenu.Controls.Add(btnCreacionPDF);
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
            panelMenu.Size = new Size(181, 493);
            panelMenu.TabIndex = 2;
            // 
            // btn_admaestros
            // 
            btn_admaestros.BackgroundImageLayout = ImageLayout.None;
            btn_admaestros.Dock = DockStyle.Top;
            btn_admaestros.FlatAppearance.BorderSize = 0;
            btn_admaestros.FlatStyle = FlatStyle.Flat;
            btn_admaestros.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_admaestros.ImageAlign = ContentAlignment.MiddleLeft;
            btn_admaestros.Location = new Point(0, 432);
            btn_admaestros.Name = "btn_admaestros";
            btn_admaestros.Size = new Size(181, 51);
            btn_admaestros.TabIndex = 7;
            btn_admaestros.Text = "Adm. Maestros";
            btn_admaestros.UseVisualStyleBackColor = true;
            // 
            // btnCreacionPDF
            // 
            btnCreacionPDF.BackgroundImageLayout = ImageLayout.None;
            btnCreacionPDF.Dock = DockStyle.Top;
            btnCreacionPDF.FlatAppearance.BorderSize = 0;
            btnCreacionPDF.FlatStyle = FlatStyle.Flat;
            btnCreacionPDF.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCreacionPDF.ImageAlign = ContentAlignment.MiddleLeft;
            btnCreacionPDF.Location = new Point(0, 381);
            btnCreacionPDF.Name = "btnCreacionPDF";
            btnCreacionPDF.Size = new Size(181, 51);
            btnCreacionPDF.TabIndex = 6;
            btnCreacionPDF.Text = "Creación de PDFS";
            btnCreacionPDF.UseVisualStyleBackColor = true;
            // 
            // btnEdicionDatos
            // 
            btnEdicionDatos.BackgroundImageLayout = ImageLayout.None;
            btnEdicionDatos.Dock = DockStyle.Top;
            btnEdicionDatos.FlatAppearance.BorderSize = 0;
            btnEdicionDatos.FlatStyle = FlatStyle.Flat;
            btnEdicionDatos.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEdicionDatos.ImageAlign = ContentAlignment.MiddleLeft;
            btnEdicionDatos.Location = new Point(0, 330);
            btnEdicionDatos.Name = "btnEdicionDatos";
            btnEdicionDatos.Size = new Size(181, 51);
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
            btnBitacora.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBitacora.ImageAlign = ContentAlignment.MiddleLeft;
            btnBitacora.Location = new Point(0, 279);
            btnBitacora.Name = "btnBitacora";
            btnBitacora.Size = new Size(181, 51);
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
            btnAdmSecre.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAdmSecre.ImageAlign = ContentAlignment.MiddleLeft;
            btnAdmSecre.Location = new Point(0, 228);
            btnAdmSecre.Name = "btnAdmSecre";
            btnAdmSecre.Size = new Size(181, 51);
            btnAdmSecre.TabIndex = 3;
            btnAdmSecre.Text = "Adm. Secretarias";
            btnAdmSecre.UseVisualStyleBackColor = true;
            // 
            // btnEstadisticas
            // 
            btnEstadisticas.BackgroundImageLayout = ImageLayout.None;
            btnEstadisticas.Dock = DockStyle.Top;
            btnEstadisticas.FlatAppearance.BorderSize = 0;
            btnEstadisticas.FlatStyle = FlatStyle.Flat;
            btnEstadisticas.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEstadisticas.ImageAlign = ContentAlignment.MiddleLeft;
            btnEstadisticas.Location = new Point(0, 177);
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
            btn_capturaCalif.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_capturaCalif.ImageAlign = ContentAlignment.MiddleLeft;
            btn_capturaCalif.Location = new Point(0, 129);
            btn_capturaCalif.Name = "btn_capturaCalif";
            btn_capturaCalif.Size = new Size(181, 48);
            btn_capturaCalif.TabIndex = 1;
            btn_capturaCalif.Text = "Captura de \r\nCalificaciones";
            btn_capturaCalif.UseVisualStyleBackColor = true;
            // 
            // btn_inscripcion
            // 
            btn_inscripcion.BackgroundImageLayout = ImageLayout.None;
            btn_inscripcion.Dock = DockStyle.Top;
            btn_inscripcion.FlatAppearance.BorderSize = 0;
            btn_inscripcion.FlatStyle = FlatStyle.Flat;
            btn_inscripcion.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
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
            panelLogo.Controls.Add(label1);
            panelLogo.Controls.Add(Logo);
            panelLogo.Dock = DockStyle.Top;
            panelLogo.Location = new Point(0, 0);
            panelLogo.Name = "panelLogo";
            panelLogo.Size = new Size(181, 80);
            panelLogo.TabIndex = 0;
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
            // Logo
            // 
            Logo.Image = Properties.Resources.logo_escuela1;
            Logo.Location = new Point(3, 0);
            Logo.Name = "Logo";
            Logo.Size = new Size(65, 60);
            Logo.SizeMode = PictureBoxSizeMode.Zoom;
            Logo.TabIndex = 2;
            Logo.TabStop = false;
            // 
            // adm_maestros
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(63, 75, 92);
            ClientSize = new Size(1016, 493);
            Controls.Add(panelApp);
            Controls.Add(panelMenu);
            Margin = new Padding(3, 2, 3, 2);
            Name = "adm_maestros";
            Text = "adm_maestros";
            Load += adm_maestros_Load;
            panelApp.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel1.ResumeLayout(false);
            panelMenu.ResumeLayout(false);
            panelLogo.ResumeLayout(false);
            panelLogo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Logo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panelito panelApp;
        private FlowLayoutPanel flowMaestros;
        private GroupBox groupBox1;
        private ComboBox grupo_asignado;
        private Label label3;
        private Label label6;
        private TextBox txtcorreomaestro;
        private Label label5;
        private Label label18;
        private TextBox txtammaestro;
        private TextBox txtapmaestro;
        private Label label15;
        private Button btnAltaMaestros;
        private TextBox txtnombremaestro;
        private Label label2;
        private Label Usuario;
        private Label label4;
        private Panel panel1;
        private Button btn_ingresar;
        private Panelito panelMenu;
        private Button btn_admaestros;
        private Button btnCreacionPDF;
        private Button btnEdicionDatos;
        private Button btnBitacora;
        private Button btnAdmSecre;
        private Button btnEstadisticas;
        private Button btn_capturaCalif;
        private Button btn_inscripcion;
        private Panelito panelLogo;
        private Label label1;
        private PictureBox Logo;
    }
}