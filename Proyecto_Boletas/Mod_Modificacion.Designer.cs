namespace Proyecto_Boletas
{
    partial class Mod_Modificacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mod_Modificacion));
            panelApp = new Panelito();
            panelito3 = new Panelito();
            label7 = new Label();
            btnGuardarModificacion = new Button();
            label16 = new Label();
            cbGrupoPer = new ComboBox();
            label17 = new Label();
            cbAlumno = new ComboBox();
            panelito2 = new Panelito();
            label15 = new Label();
            pictureBox1 = new PictureBox();
            correo_tutor = new TextBox();
            label8 = new Label();
            telefono_tutor = new TextBox();
            label9 = new Label();
            apellidoM_tutor = new TextBox();
            apellidoP_tutor = new TextBox();
            nombre_tutor = new TextBox();
            label13 = new Label();
            label19 = new Label();
            label20 = new Label();
            panelito1 = new Panelito();
            combosgenero = new ComboBox();
            label10 = new Label();
            grupo_alumno = new ComboBox();
            label11 = new Label();
            edad_alumno = new ComboBox();
            nacimiento_alumno = new DateTimePicker();
            label18 = new Label();
            label21 = new Label();
            label6 = new Label();
            label12 = new Label();
            pictureBox2 = new PictureBox();
            txtCurp = new TextBox();
            apellidoM_alumno = new TextBox();
            apellidoP_alumno = new TextBox();
            nombre_alumno = new TextBox();
            label2 = new Label();
            Usuario = new Label();
            label5 = new Label();
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
            panelito3.SuspendLayout();
            panelito2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panelito1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
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
            panelApp.Controls.Add(panelito3);
            panelApp.Controls.Add(panelito2);
            panelApp.Controls.Add(panelito1);
            panelApp.Controls.Add(panel1);
            panelApp.Dock = DockStyle.Fill;
            panelApp.Location = new Point(181, 0);
            panelApp.Name = "panelApp";
            panelApp.Size = new Size(841, 514);
            panelApp.TabIndex = 8;
            // 
            // panelito3
            // 
            panelito3.BackColor = Color.FromArgb(157, 101, 101);
            panelito3.BorderRadius = 20;
            panelito3.Controls.Add(label7);
            panelito3.Controls.Add(btnGuardarModificacion);
            panelito3.Controls.Add(label16);
            panelito3.Controls.Add(cbGrupoPer);
            panelito3.Controls.Add(label17);
            panelito3.Controls.Add(cbAlumno);
            panelito3.Location = new Point(81, 77);
            panelito3.Name = "panelito3";
            panelito3.Size = new Size(711, 151);
            panelito3.TabIndex = 24;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = SystemColors.ControlLightLight;
            label7.Location = new Point(47, 102);
            label7.Name = "label7";
            label7.Size = new Size(68, 21);
            label7.TabIndex = 53;
            label7.Text = "Guardar";
            // 
            // btnGuardarModificacion
            // 
            btnGuardarModificacion.BackColor = Color.Transparent;
            btnGuardarModificacion.FlatStyle = FlatStyle.Flat;
            btnGuardarModificacion.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardarModificacion.Image = (Image)resources.GetObject("btnGuardarModificacion.Image");
            btnGuardarModificacion.Location = new Point(31, 31);
            btnGuardarModificacion.Name = "btnGuardarModificacion";
            btnGuardarModificacion.Size = new Size(84, 68);
            btnGuardarModificacion.TabIndex = 51;
            btnGuardarModificacion.UseVisualStyleBackColor = false;
            btnGuardarModificacion.Click += btnGuardarModificacion_Click;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label16.ForeColor = SystemColors.ControlLightLight;
            label16.Location = new Point(144, 49);
            label16.Name = "label16";
            label16.Size = new Size(143, 21);
            label16.TabIndex = 49;
            label16.Text = "Seleccione Grupo:";
            // 
            // cbGrupoPer
            // 
            cbGrupoPer.DropDownStyle = ComboBoxStyle.DropDownList;
            cbGrupoPer.Location = new Point(309, 47);
            cbGrupoPer.Name = "cbGrupoPer";
            cbGrupoPer.Size = new Size(379, 23);
            cbGrupoPer.TabIndex = 48;
            cbGrupoPer.SelectedIndexChanged += cbGrupoPer_SelectedIndexChanged;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label17.ForeColor = SystemColors.ControlLightLight;
            label17.Location = new Point(133, 78);
            label17.Name = "label17";
            label17.Size = new Size(154, 21);
            label17.TabIndex = 46;
            label17.Text = "Seleccione Alumno:";
            // 
            // cbAlumno
            // 
            cbAlumno.DropDownStyle = ComboBoxStyle.DropDownList;
            cbAlumno.Location = new Point(309, 76);
            cbAlumno.Name = "cbAlumno";
            cbAlumno.Size = new Size(379, 23);
            cbAlumno.TabIndex = 47;
            cbAlumno.SelectedIndexChanged += cbAlumno_SelectedIndexChanged;
            // 
            // panelito2
            // 
            panelito2.BackColor = Color.FromArgb(157, 101, 101);
            panelito2.BorderRadius = 20;
            panelito2.Controls.Add(label15);
            panelito2.Controls.Add(pictureBox1);
            panelito2.Controls.Add(correo_tutor);
            panelito2.Controls.Add(label8);
            panelito2.Controls.Add(telefono_tutor);
            panelito2.Controls.Add(label9);
            panelito2.Controls.Add(apellidoM_tutor);
            panelito2.Controls.Add(apellidoP_tutor);
            panelito2.Controls.Add(nombre_tutor);
            panelito2.Controls.Add(label13);
            panelito2.Controls.Add(label19);
            panelito2.Controls.Add(label20);
            panelito2.Location = new Point(80, 589);
            panelito2.Name = "panelito2";
            panelito2.Size = new Size(712, 238);
            panelito2.TabIndex = 23;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Century Gothic", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label15.Location = new Point(255, 16);
            label15.Name = "label15";
            label15.Size = new Size(238, 25);
            label15.TabIndex = 48;
            label15.Text = "-- DATOS DEL TUTOR --";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(615, 174);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(74, 64);
            pictureBox1.TabIndex = 34;
            pictureBox1.TabStop = false;
            // 
            // correo_tutor
            // 
            correo_tutor.Location = new Point(236, 191);
            correo_tutor.Name = "correo_tutor";
            correo_tutor.Size = new Size(349, 23);
            correo_tutor.TabIndex = 44;
            correo_tutor.TextChanged += correo_tutor_TextChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.Location = new Point(49, 199);
            label8.Name = "label8";
            label8.Size = new Size(64, 18);
            label8.TabIndex = 43;
            label8.Text = "Correo:";
            // 
            // telefono_tutor
            // 
            telefono_tutor.Location = new Point(236, 159);
            telefono_tutor.Name = "telefono_tutor";
            telefono_tutor.Size = new Size(349, 23);
            telefono_tutor.TabIndex = 42;
            telefono_tutor.TextChanged += telefono_tutor_TextChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(48, 167);
            label9.Name = "label9";
            label9.Size = new Size(75, 18);
            label9.TabIndex = 41;
            label9.Text = "Teléfono:";
            // 
            // apellidoM_tutor
            // 
            apellidoM_tutor.Location = new Point(236, 127);
            apellidoM_tutor.Name = "apellidoM_tutor";
            apellidoM_tutor.Size = new Size(349, 23);
            apellidoM_tutor.TabIndex = 40;
            apellidoM_tutor.TextChanged += apellidoM_tutor_TextChanged;
            // 
            // apellidoP_tutor
            // 
            apellidoP_tutor.Location = new Point(236, 95);
            apellidoP_tutor.Name = "apellidoP_tutor";
            apellidoP_tutor.Size = new Size(349, 23);
            apellidoP_tutor.TabIndex = 39;
            apellidoP_tutor.TextChanged += apellidoP_tutor_TextChanged;
            // 
            // nombre_tutor
            // 
            nombre_tutor.Location = new Point(237, 58);
            nombre_tutor.Name = "nombre_tutor";
            nombre_tutor.Size = new Size(348, 23);
            nombre_tutor.TabIndex = 38;
            nombre_tutor.TextChanged += nombre_tutor_TextChanged;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label13.Location = new Point(49, 130);
            label13.Name = "label13";
            label13.Size = new Size(141, 18);
            label13.TabIndex = 36;
            label13.Text = "Apellido Materno:";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label19.Location = new Point(49, 58);
            label19.Name = "label19";
            label19.Size = new Size(90, 18);
            label19.TabIndex = 37;
            label19.Text = "Nombre(s):";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label20.Location = new Point(49, 94);
            label20.Name = "label20";
            label20.Size = new Size(135, 18);
            label20.TabIndex = 35;
            label20.Text = "Apellido Paterno:";
            // 
            // panelito1
            // 
            panelito1.BackColor = Color.FromArgb(212, 168, 133);
            panelito1.BorderRadius = 20;
            panelito1.Controls.Add(combosgenero);
            panelito1.Controls.Add(label10);
            panelito1.Controls.Add(grupo_alumno);
            panelito1.Controls.Add(label11);
            panelito1.Controls.Add(edad_alumno);
            panelito1.Controls.Add(nacimiento_alumno);
            panelito1.Controls.Add(label18);
            panelito1.Controls.Add(label21);
            panelito1.Controls.Add(label6);
            panelito1.Controls.Add(label12);
            panelito1.Controls.Add(pictureBox2);
            panelito1.Controls.Add(txtCurp);
            panelito1.Controls.Add(apellidoM_alumno);
            panelito1.Controls.Add(apellidoP_alumno);
            panelito1.Controls.Add(nombre_alumno);
            panelito1.Controls.Add(label2);
            panelito1.Controls.Add(Usuario);
            panelito1.Controls.Add(label5);
            panelito1.Location = new Point(81, 246);
            panelito1.Name = "panelito1";
            panelito1.Size = new Size(711, 329);
            panelito1.TabIndex = 22;
            // 
            // combosgenero
            // 
            combosgenero.FormattingEnabled = true;
            combosgenero.Location = new Point(474, 289);
            combosgenero.Name = "combosgenero";
            combosgenero.Size = new Size(118, 23);
            combosgenero.TabIndex = 56;
            combosgenero.SelectedIndexChanged += combosgenero_SelectedIndexChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.Location = new Point(399, 292);
            label10.Name = "label10";
            label10.Size = new Size(69, 18);
            label10.TabIndex = 55;
            label10.Text = "Género:";
            // 
            // grupo_alumno
            // 
            grupo_alumno.FormattingEnabled = true;
            grupo_alumno.Location = new Point(474, 245);
            grupo_alumno.Name = "grupo_alumno";
            grupo_alumno.Size = new Size(118, 23);
            grupo_alumno.TabIndex = 54;
            grupo_alumno.SelectedIndexChanged += grupo_alumno_SelectedIndexChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label11.Location = new Point(68, 257);
            label11.Name = "label11";
            label11.Size = new Size(50, 18);
            label11.TabIndex = 53;
            label11.Text = "Edad:";
            // 
            // edad_alumno
            // 
            edad_alumno.FormattingEnabled = true;
            edad_alumno.Location = new Point(244, 250);
            edad_alumno.Name = "edad_alumno";
            edad_alumno.Size = new Size(123, 23);
            edad_alumno.TabIndex = 52;
            edad_alumno.SelectedIndexChanged += edad_alumno_SelectedIndexChanged;
            // 
            // nacimiento_alumno
            // 
            nacimiento_alumno.Location = new Point(244, 201);
            nacimiento_alumno.Name = "nacimiento_alumno";
            nacimiento_alumno.Size = new Size(348, 23);
            nacimiento_alumno.TabIndex = 51;
            nacimiento_alumno.ValueChanged += nacimiento_alumno_ValueChanged;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label18.Location = new Point(68, 195);
            label18.Name = "label18";
            label18.Size = new Size(98, 36);
            label18.TabIndex = 50;
            label18.Text = "Fecha de\r\nNacimiento:";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label21.Location = new Point(399, 248);
            label21.Name = "label21";
            label21.Size = new Size(59, 18);
            label21.TabIndex = 49;
            label21.Text = "Grupo:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(68, 177);
            label6.Name = "label6";
            label6.Size = new Size(51, 18);
            label6.TabIndex = 48;
            label6.Text = "CURP:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Century Gothic", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.Location = new Point(237, 27);
            label12.Name = "label12";
            label12.Size = new Size(268, 25);
            label12.TabIndex = 47;
            label12.Text = "-- DATOS DEL ALUMNO --";
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(625, 153);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(63, 66);
            pictureBox2.TabIndex = 46;
            pictureBox2.TabStop = false;
            // 
            // txtCurp
            // 
            txtCurp.Location = new Point(244, 172);
            txtCurp.Name = "txtCurp";
            txtCurp.Size = new Size(348, 23);
            txtCurp.TabIndex = 37;
            txtCurp.TextChanged += txtCurp_TextChanged;
            // 
            // apellidoM_alumno
            // 
            apellidoM_alumno.Location = new Point(244, 140);
            apellidoM_alumno.Name = "apellidoM_alumno";
            apellidoM_alumno.Size = new Size(348, 23);
            apellidoM_alumno.TabIndex = 35;
            apellidoM_alumno.TextChanged += apellidoM_alumno_TextChanged;
            // 
            // apellidoP_alumno
            // 
            apellidoP_alumno.Location = new Point(244, 108);
            apellidoP_alumno.Name = "apellidoP_alumno";
            apellidoP_alumno.Size = new Size(348, 23);
            apellidoP_alumno.TabIndex = 34;
            apellidoP_alumno.TextChanged += apellidoP_alumno_TextChanged;
            // 
            // nombre_alumno
            // 
            nombre_alumno.Location = new Point(244, 77);
            nombre_alumno.Name = "nombre_alumno";
            nombre_alumno.Size = new Size(348, 23);
            nombre_alumno.TabIndex = 33;
            nombre_alumno.TextChanged += nombre_alumno_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(68, 148);
            label2.Name = "label2";
            label2.Size = new Size(141, 18);
            label2.TabIndex = 31;
            label2.Text = "Apellido Materno:";
            // 
            // Usuario
            // 
            Usuario.AutoSize = true;
            Usuario.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Usuario.Location = new Point(68, 84);
            Usuario.Name = "Usuario";
            Usuario.Size = new Size(90, 18);
            Usuario.TabIndex = 32;
            Usuario.Text = "Nombre(s):";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(68, 112);
            label5.Name = "label5";
            label5.Size = new Size(135, 18);
            label5.TabIndex = 30;
            label5.Text = "Apellido Paterno:";
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
            label14.ForeColor = SystemColors.ButtonHighlight;
            label14.Location = new Point(336, 13);
            label14.Name = "label14";
            label14.Size = new Size(238, 32);
            label14.TabIndex = 12;
            label14.Text = "Edición de Datos";
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
            panelMenu.Size = new Size(181, 514);
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
            btn_admaestros.Size = new Size(181, 51);
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
            btnEdicionDatos.Font = new Font("Microsoft Sans Serif", 12.75F, FontStyle.Bold);
            btnEdicionDatos.ForeColor = SystemColors.ControlLight;
            btnEdicionDatos.Image = (Image)resources.GetObject("btnEdicionDatos.Image");
            btnEdicionDatos.ImageAlign = ContentAlignment.MiddleLeft;
            btnEdicionDatos.Location = new Point(0, 343);
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
            btnBitacora.Font = new Font("Microsoft Sans Serif", 12.75F, FontStyle.Bold);
            btnBitacora.ForeColor = SystemColors.ControlLight;
            btnBitacora.Image = (Image)resources.GetObject("btnBitacora.Image");
            btnBitacora.ImageAlign = ContentAlignment.MiddleLeft;
            btnBitacora.Location = new Point(0, 292);
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
            btnAdmSecre.Font = new Font("Microsoft Sans Serif", 12.75F, FontStyle.Bold);
            btnAdmSecre.ForeColor = SystemColors.ControlLight;
            btnAdmSecre.Image = (Image)resources.GetObject("btnAdmSecre.Image");
            btnAdmSecre.ImageAlign = ContentAlignment.MiddleLeft;
            btnAdmSecre.Location = new Point(0, 241);
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
            btnEstadisticas.Font = new Font("Microsoft Sans Serif", 12.75F, FontStyle.Bold);
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
            btn_capturaCalif.Font = new Font("Microsoft Sans Serif", 12.75F, FontStyle.Bold);
            btn_capturaCalif.ForeColor = SystemColors.ControlLight;
            btn_capturaCalif.Image = (Image)resources.GetObject("btn_capturaCalif.Image");
            btn_capturaCalif.ImageAlign = ContentAlignment.MiddleLeft;
            btn_capturaCalif.Location = new Point(0, 129);
            btn_capturaCalif.Name = "btn_capturaCalif";
            btn_capturaCalif.Size = new Size(181, 61);
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
            btn_inscripcion.Font = new Font("Microsoft Sans Serif", 12.75F, FontStyle.Bold);
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
            // Mod_Modificacion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1022, 514);
            Controls.Add(panelApp);
            Controls.Add(panelMenu);
            Name = "Mod_Modificacion";
            Text = "Mod_Modificacion";
            Load += Mod_Modificacion_Load;
            panelApp.ResumeLayout(false);
            panelito3.ResumeLayout(false);
            panelito3.PerformLayout();
            panelito2.ResumeLayout(false);
            panelito2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panelito1.ResumeLayout(false);
            panelito1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
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
        private Panelito panelito2;
        private PictureBox pictureBox2;
        private TextBox txtCurp;
        private TextBox apellidoM_alumno;
        private TextBox apellidoP_alumno;
        private TextBox nombre_alumno;
        private Label label2;
        private Label Usuario;
        private Label label5;
        private PictureBox pictureBox1;
        private TextBox correo_tutor;
        private Label label8;
        private TextBox telefono_tutor;
        private Label label9;
        private TextBox apellidoM_tutor;
        private TextBox apellidoP_tutor;
        private TextBox nombre_tutor;
        private Label label13;
        private Label label19;
        private Label label20;
        private Label label15;
        private Label label12;
        private Panelito panelito3;
        private Label label16;
        private ComboBox cbGrupoPer;
        private Label label17;
        private ComboBox cbAlumno;
        private Button btnGuardarModificacion;
        private Label label6;
        private Label label7;
        private ComboBox combosgenero;
        private Label label10;
        private ComboBox grupo_alumno;
        private Label label11;
        private ComboBox edad_alumno;
        private DateTimePicker nacimiento_alumno;
        private Label label18;
        private Label label21;
    }
}