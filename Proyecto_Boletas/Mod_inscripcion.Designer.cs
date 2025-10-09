namespace Proyecto_Boletas
{
    partial class Mod_inscripcion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mod_inscripcion));
            groupBox2 = new GroupBox();
            correo_tutor = new TextBox();
            label4 = new Label();
            label12 = new Label();
            telefono_tutor = new TextBox();
            label5 = new Label();
            btnvalidar_tutor = new Button();
            apellidoM_tutor = new TextBox();
            apellidoP_tutor = new TextBox();
            nombre_tutor = new TextBox();
            label13 = new Label();
            label19 = new Label();
            label20 = new Label();
            panel1 = new Panel();
            btnVolver = new Button();
            label14 = new Label();
            label18 = new Label();
            btnalta_inscripcion = new Button();
            label1 = new Label();
            Usuario = new Label();
            label2 = new Label();
            nombre_alumno = new TextBox();
            apellidoP_alumno = new TextBox();
            apellidoM_alumno = new TextBox();
            label6 = new Label();
            txtCurp = new TextBox();
            label10 = new Label();
            btnvalidar_alumno = new Button();
            label15 = new Label();
            label11 = new Label();
            nacimiento_alumno = new DateTimePicker();
            edad_alumno = new ComboBox();
            label3 = new Label();
            grupo_alumno = new ComboBox();
            groupBox1 = new GroupBox();
            label7 = new Label();
            combosgenero = new ComboBox();
            groupBox2.SuspendLayout();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox2
            // 
            groupBox2.BackColor = Color.FromArgb(212, 168, 133);
            groupBox2.Controls.Add(correo_tutor);
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(label12);
            groupBox2.Controls.Add(telefono_tutor);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(btnvalidar_tutor);
            groupBox2.Controls.Add(apellidoM_tutor);
            groupBox2.Controls.Add(apellidoP_tutor);
            groupBox2.Controls.Add(nombre_tutor);
            groupBox2.Controls.Add(label13);
            groupBox2.Controls.Add(label19);
            groupBox2.Controls.Add(label20);
            groupBox2.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold);
            groupBox2.Location = new Point(72, 423);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(647, 215);
            groupBox2.TabIndex = 22;
            groupBox2.TabStop = false;
            groupBox2.Text = "Alta de Tutor";
            // 
            // correo_tutor
            // 
            correo_tutor.Location = new Point(180, 171);
            correo_tutor.Name = "correo_tutor";
            correo_tutor.Size = new Size(258, 26);
            correo_tutor.TabIndex = 33;
            correo_tutor.TextChanged += correo_tutor_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(39, 179);
            label4.Name = "label4";
            label4.Size = new Size(64, 18);
            label4.TabIndex = 32;
            label4.Text = "Correo:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = Color.Transparent;
            label12.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label12.ForeColor = SystemColors.ActiveCaptionText;
            label12.Location = new Point(525, 150);
            label12.Name = "label12";
            label12.Size = new Size(59, 21);
            label12.TabIndex = 5;
            label12.Text = "Validar";
            // 
            // telefono_tutor
            // 
            telefono_tutor.Location = new Point(180, 139);
            telefono_tutor.Name = "telefono_tutor";
            telefono_tutor.Size = new Size(258, 26);
            telefono_tutor.TabIndex = 31;
            telefono_tutor.TextChanged += telefono_tutor_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(38, 147);
            label5.Name = "label5";
            label5.Size = new Size(75, 18);
            label5.TabIndex = 30;
            label5.Text = "Teléfono:";
            // 
            // btnvalidar_tutor
            // 
            btnvalidar_tutor.BackColor = Color.FromArgb(212, 168, 133);
            btnvalidar_tutor.BackgroundImageLayout = ImageLayout.Center;
            btnvalidar_tutor.FlatStyle = FlatStyle.Popup;
            btnvalidar_tutor.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnvalidar_tutor.Image = (Image)resources.GetObject("btnvalidar_tutor.Image");
            btnvalidar_tutor.Location = new Point(495, 74);
            btnvalidar_tutor.Name = "btnvalidar_tutor";
            btnvalidar_tutor.Size = new Size(113, 73);
            btnvalidar_tutor.TabIndex = 3;
            btnvalidar_tutor.UseVisualStyleBackColor = false;
            btnvalidar_tutor.Click += btnvalidar_tutor_Click;
            // 
            // apellidoM_tutor
            // 
            apellidoM_tutor.Location = new Point(180, 107);
            apellidoM_tutor.Name = "apellidoM_tutor";
            apellidoM_tutor.Size = new Size(258, 26);
            apellidoM_tutor.TabIndex = 28;
            apellidoM_tutor.TextChanged += apellidoM_tutor_TextChanged;
            // 
            // apellidoP_tutor
            // 
            apellidoP_tutor.Location = new Point(180, 75);
            apellidoP_tutor.Name = "apellidoP_tutor";
            apellidoP_tutor.Size = new Size(258, 26);
            apellidoP_tutor.TabIndex = 27;
            apellidoP_tutor.TextChanged += apellidoP_tutor_TextChanged;
            // 
            // nombre_tutor
            // 
            nombre_tutor.Location = new Point(180, 43);
            nombre_tutor.Name = "nombre_tutor";
            nombre_tutor.Size = new Size(258, 26);
            nombre_tutor.TabIndex = 26;
            nombre_tutor.TextChanged += nombre_tutor_TextChanged;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label13.Location = new Point(39, 110);
            label13.Name = "label13";
            label13.Size = new Size(141, 18);
            label13.TabIndex = 24;
            label13.Text = "Apellido Materno:";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label19.Location = new Point(39, 46);
            label19.Name = "label19";
            label19.Size = new Size(90, 18);
            label19.TabIndex = 25;
            label19.Text = "Nombre(s):";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label20.Location = new Point(39, 74);
            label20.Name = "label20";
            label20.Size = new Size(135, 18);
            label20.TabIndex = 23;
            label20.Text = "Apellido Paterno:";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(181, 131, 120);
            panel1.Controls.Add(btnVolver);
            panel1.Controls.Add(label14);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(721, 59);
            panel1.TabIndex = 20;
            // 
            // btnVolver
            // 
            btnVolver.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnVolver.BackColor = Color.FromArgb(247, 189, 86);
            btnVolver.BackgroundImage = (Image)resources.GetObject("btnVolver.BackgroundImage");
            btnVolver.Cursor = Cursors.Hand;
            btnVolver.FlatStyle = FlatStyle.Popup;
            btnVolver.Font = new Font("Century Gothic", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnVolver.ForeColor = SystemColors.ActiveCaptionText;
            btnVolver.Location = new Point(12, 6);
            btnVolver.Name = "btnVolver";
            btnVolver.RightToLeft = RightToLeft.Yes;
            btnVolver.Size = new Size(50, 50);
            btnVolver.TabIndex = 7;
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Cascadia Code", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label14.ForeColor = SystemColors.ControlLightLight;
            label14.Location = new Point(234, 13);
            label14.Name = "label14";
            label14.Size = new Size(322, 32);
            label14.TabIndex = 6;
            label14.Text = "Inscripción de Alumnos";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.BackColor = Color.Transparent;
            label18.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label18.ForeColor = SystemColors.ControlLightLight;
            label18.Location = new Point(360, 711);
            label18.Name = "label18";
            label18.Size = new Size(114, 21);
            label18.TabIndex = 5;
            label18.Text = "Guardar Datos";
            // 
            // btnalta_inscripcion
            // 
            btnalta_inscripcion.BackColor = Color.Transparent;
            btnalta_inscripcion.BackgroundImageLayout = ImageLayout.Center;
            btnalta_inscripcion.FlatStyle = FlatStyle.Popup;
            btnalta_inscripcion.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnalta_inscripcion.Image = (Image)resources.GetObject("btnalta_inscripcion.Image");
            btnalta_inscripcion.Location = new Point(360, 644);
            btnalta_inscripcion.Name = "btnalta_inscripcion";
            btnalta_inscripcion.Size = new Size(113, 73);
            btnalta_inscripcion.TabIndex = 3;
            btnalta_inscripcion.UseVisualStyleBackColor = false;
            btnalta_inscripcion.Click += btnalta_inscripcion_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(27, 72);
            label1.Name = "label1";
            label1.Size = new Size(135, 18);
            label1.TabIndex = 1;
            label1.Text = "Apellido Paterno:";
            // 
            // Usuario
            // 
            Usuario.AutoSize = true;
            Usuario.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Usuario.Location = new Point(27, 44);
            Usuario.Name = "Usuario";
            Usuario.Size = new Size(90, 18);
            Usuario.TabIndex = 3;
            Usuario.Text = "Nombre(s):";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(27, 108);
            label2.Name = "label2";
            label2.Size = new Size(141, 18);
            label2.TabIndex = 2;
            label2.Text = "Apellido Materno:";
            // 
            // nombre_alumno
            // 
            nombre_alumno.Location = new Point(168, 41);
            nombre_alumno.Name = "nombre_alumno";
            nombre_alumno.Size = new Size(295, 26);
            nombre_alumno.TabIndex = 4;
            nombre_alumno.TextChanged += nombre_alumno_TextChanged;
            // 
            // apellidoP_alumno
            // 
            apellidoP_alumno.Location = new Point(168, 73);
            apellidoP_alumno.Name = "apellidoP_alumno";
            apellidoP_alumno.Size = new Size(295, 26);
            apellidoP_alumno.TabIndex = 5;
            apellidoP_alumno.TextChanged += apellidoP_alumno_TextChanged;
            // 
            // apellidoM_alumno
            // 
            apellidoM_alumno.Location = new Point(168, 105);
            apellidoM_alumno.Name = "apellidoM_alumno";
            apellidoM_alumno.Size = new Size(295, 26);
            apellidoM_alumno.TabIndex = 6;
            apellidoM_alumno.TextChanged += apellidoM_alumno_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(26, 145);
            label6.Name = "label6";
            label6.Size = new Size(51, 18);
            label6.TabIndex = 17;
            label6.Text = "CURP:";
            // 
            // txtCurp
            // 
            txtCurp.Location = new Point(168, 137);
            txtCurp.Name = "txtCurp";
            txtCurp.Size = new Size(295, 26);
            txtCurp.TabIndex = 18;
            txtCurp.TextChanged += txtCurp_TextChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.Location = new Point(302, 234);
            label10.Name = "label10";
            label10.Size = new Size(59, 18);
            label10.TabIndex = 19;
            label10.Text = "Grupo:";
            // 
            // btnvalidar_alumno
            // 
            btnvalidar_alumno.BackColor = Color.FromArgb(212, 168, 133);
            btnvalidar_alumno.BackgroundImageLayout = ImageLayout.Center;
            btnvalidar_alumno.FlatStyle = FlatStyle.Popup;
            btnvalidar_alumno.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnvalidar_alumno.Image = (Image)resources.GetObject("btnvalidar_alumno.Image");
            btnvalidar_alumno.Location = new Point(495, 90);
            btnvalidar_alumno.Name = "btnvalidar_alumno";
            btnvalidar_alumno.Size = new Size(113, 73);
            btnvalidar_alumno.TabIndex = 3;
            btnvalidar_alumno.UseVisualStyleBackColor = false;
            btnvalidar_alumno.Click += btnvalidar_alumno_Click;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.BackColor = Color.Transparent;
            label15.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label15.ForeColor = SystemColors.ActiveCaptionText;
            label15.Location = new Point(525, 170);
            label15.Name = "label15";
            label15.Size = new Size(59, 21);
            label15.TabIndex = 5;
            label15.Text = "Validar";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label11.Location = new Point(27, 172);
            label11.Name = "label11";
            label11.Size = new Size(98, 36);
            label11.TabIndex = 21;
            label11.Text = "Fecha de\r\nNacimiento:";
            // 
            // nacimiento_alumno
            // 
            nacimiento_alumno.Location = new Point(168, 182);
            nacimiento_alumno.Name = "nacimiento_alumno";
            nacimiento_alumno.Size = new Size(295, 26);
            nacimiento_alumno.TabIndex = 23;
            nacimiento_alumno.ValueChanged += nacimiento_alumno_ValueChanged;
            // 
            // edad_alumno
            // 
            edad_alumno.FormattingEnabled = true;
            edad_alumno.Location = new Point(168, 226);
            edad_alumno.Name = "edad_alumno";
            edad_alumno.Size = new Size(65, 26);
            edad_alumno.TabIndex = 24;
            edad_alumno.SelectedIndexChanged += edad_alumno_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(27, 234);
            label3.Name = "label3";
            label3.Size = new Size(50, 18);
            label3.TabIndex = 25;
            label3.Text = "Edad:";
            // 
            // grupo_alumno
            // 
            grupo_alumno.FormattingEnabled = true;
            grupo_alumno.Location = new Point(398, 226);
            grupo_alumno.Name = "grupo_alumno";
            grupo_alumno.Size = new Size(65, 26);
            grupo_alumno.TabIndex = 26;
            grupo_alumno.SelectedIndexChanged += grupo_alumno_SelectedIndexChanged;
            // 
            // groupBox1
            // 
            groupBox1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox1.BackColor = Color.FromArgb(212, 168, 133);
            groupBox1.BackgroundImageLayout = ImageLayout.Stretch;
            groupBox1.Controls.Add(combosgenero);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(grupo_alumno);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(edad_alumno);
            groupBox1.Controls.Add(nacimiento_alumno);
            groupBox1.Controls.Add(label11);
            groupBox1.Controls.Add(label15);
            groupBox1.Controls.Add(btnvalidar_alumno);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(txtCurp);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(apellidoM_alumno);
            groupBox1.Controls.Add(apellidoP_alumno);
            groupBox1.Controls.Add(nombre_alumno);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(Usuario);
            groupBox1.Controls.Add(label1);
            groupBox1.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold);
            groupBox1.Location = new Point(72, 81);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(647, 313);
            groupBox1.TabIndex = 21;
            groupBox1.TabStop = false;
            groupBox1.Text = "Alta de Alumnos";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(302, 278);
            label7.Name = "label7";
            label7.Size = new Size(69, 18);
            label7.TabIndex = 27;
            label7.Text = "Género:";
            // 
            // combosgenero
            // 
            combosgenero.FormattingEnabled = true;
            combosgenero.Location = new Point(398, 270);
            combosgenero.Name = "combosgenero";
            combosgenero.Size = new Size(65, 26);
            combosgenero.TabIndex = 28;
            combosgenero.SelectedIndexChanged += combosgenero_SelectedIndexChanged;
            // 
            // Mod_inscripcion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(63, 75, 92);
            ClientSize = new Size(738, 351);
            Controls.Add(groupBox2);
            Controls.Add(label18);
            Controls.Add(groupBox1);
            Controls.Add(btnalta_inscripcion);
            Controls.Add(panel1);
            Name = "Mod_inscripcion";
            Text = "Mod_inscripcion";
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox2;
        private Panel panel1;
        private Button btnVolver;
        private Label label14;
        private TextBox correo_tutor;
        private Label label4;
        private TextBox telefono_tutor;
        private Label label5;
        private TextBox apellidoM_tutor;
        private Label label12;
        private Button btnvalidar_tutor;
        private TextBox apellidoP_tutor;
        private TextBox nombre_tutor;
        private Label label13;
        private Label label19;
        private Label label20;
        private Label label18;
        private Button btnalta_inscripcion;
        private Label label1;
        private Label Usuario;
        private Label label2;
        private TextBox nombre_alumno;
        private TextBox apellidoP_alumno;
        private TextBox apellidoM_alumno;
        private Label label6;
        private TextBox txtCurp;
        private Label label10;
        private Button btnvalidar_alumno;
        private Label label15;
        private Label label11;
        private DateTimePicker nacimiento_alumno;
        private ComboBox edad_alumno;
        private Label label3;
        private ComboBox grupo_alumno;
        private GroupBox groupBox1;
        private ComboBox combosgenero;
        private Label label7;
    }
}