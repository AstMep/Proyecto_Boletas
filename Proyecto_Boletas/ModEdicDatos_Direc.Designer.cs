namespace Proyecto_Boletas
{
    partial class ModEdicDatos_Direc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModEdicDatos_Direc));
            panel1 = new Panel();
            btnVolver = new Button();
            label14 = new Label();
            label2 = new Label();
            cmbMes = new ComboBox();
            label1 = new Label();
            cmbGrupo = new ComboBox();
            groupBox6 = new GroupBox();
            label12 = new Label();
            label13 = new Label();
            btnGenerarListas = new Button();
            groupBox1 = new GroupBox();
            label5 = new Label();
            label6 = new Label();
            btnGenerarLisProf = new Button();
            panel1.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(181, 131, 120);
            panel1.Controls.Add(btnVolver);
            panel1.Controls.Add(label14);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 59);
            panel1.TabIndex = 33;
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
            btnVolver.Location = new Point(39, 10);
            btnVolver.Name = "btnVolver";
            btnVolver.RightToLeft = RightToLeft.Yes;
            btnVolver.Size = new Size(48, 46);
            btnVolver.TabIndex = 7;
            btnVolver.UseVisualStyleBackColor = false;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Cascadia Code", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label14.ForeColor = SystemColors.ControlText;
            label14.Location = new Point(256, 15);
            label14.Name = "label14";
            label14.Size = new Size(224, 32);
            label14.TabIndex = 6;
            label14.Text = "Creación de PDF";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(225, 161);
            label2.Name = "label2";
            label2.Size = new Size(128, 21);
            label2.TabIndex = 32;
            label2.Text = "Seleccione Mes:";
            // 
            // cmbMes
            // 
            cmbMes.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMes.Items.AddRange(new object[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" });
            cmbMes.Location = new Point(374, 161);
            cmbMes.Name = "cmbMes";
            cmbMes.Size = new Size(133, 23);
            cmbMes.TabIndex = 31;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(225, 106);
            label1.Name = "label1";
            label1.Size = new Size(143, 21);
            label1.TabIndex = 30;
            label1.Text = "Seleccione Grupo:";
            // 
            // cmbGrupo
            // 
            cmbGrupo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGrupo.Location = new Point(374, 106);
            cmbGrupo.Name = "cmbGrupo";
            cmbGrupo.Size = new Size(133, 23);
            cmbGrupo.TabIndex = 29;
            // 
            // groupBox6
            // 
            groupBox6.BackColor = Color.FromArgb(157, 101, 101);
            groupBox6.Controls.Add(label12);
            groupBox6.Controls.Add(label13);
            groupBox6.Controls.Add(btnGenerarListas);
            groupBox6.Location = new Point(50, 86);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(160, 199);
            groupBox6.TabIndex = 28;
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
            btnGenerarListas.Location = new Point(21, 16);
            btnGenerarListas.Name = "btnGenerarListas";
            btnGenerarListas.Size = new Size(121, 135);
            btnGenerarListas.TabIndex = 3;
            btnGenerarListas.UseVisualStyleBackColor = false;
            btnGenerarListas.Click += btnGenerarListas_Click;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.FromArgb(157, 101, 101);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(btnGenerarLisProf);
            groupBox1.Location = new Point(587, 83);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(160, 199);
            groupBox1.TabIndex = 34;
            groupBox1.TabStop = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = SystemColors.ControlLightLight;
            label5.Location = new Point(11, 175);
            label5.Name = "label5";
            label5.Size = new Size(138, 21);
            label5.TabIndex = 5;
            label5.Text = "Lista de Maestros";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = SystemColors.ControlLightLight;
            label6.Location = new Point(45, 154);
            label6.Name = "label6";
            label6.Size = new Size(76, 21);
            label6.TabIndex = 4;
            label6.Text = "Generar  ";
            // 
            // btnGenerarLisProf
            // 
            btnGenerarLisProf.BackColor = Color.FromArgb(212, 168, 133);
            btnGenerarLisProf.BackgroundImageLayout = ImageLayout.Center;
            btnGenerarLisProf.FlatStyle = FlatStyle.Popup;
            btnGenerarLisProf.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGenerarLisProf.Image = (Image)resources.GetObject("btnGenerarLisProf.Image");
            btnGenerarLisProf.Location = new Point(21, 16);
            btnGenerarLisProf.Name = "btnGenerarLisProf";
            btnGenerarLisProf.Size = new Size(121, 135);
            btnGenerarLisProf.TabIndex = 3;
            btnGenerarLisProf.UseVisualStyleBackColor = false;
            btnGenerarLisProf.Click += btnGenerarLisProf_Click;
            // 
            // ModEdicDatos_Direc
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(63, 75, 92);
            ClientSize = new Size(800, 450);
            Controls.Add(groupBox1);
            Controls.Add(panel1);
            Controls.Add(label2);
            Controls.Add(cmbMes);
            Controls.Add(label1);
            Controls.Add(cmbGrupo);
            Controls.Add(groupBox6);
            Name = "ModEdicDatos_Direc";
            Text = "ModEdicDatos_Direc";
            Load += ModEdicDatos_Direc_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Button btnVolver;
        private Label label14;
        private Label label2;
        private ComboBox cmbMes;
        private Label label1;
        private ComboBox cmbGrupo;
        private GroupBox groupBox6;
        private Label label12;
        private Label label13;
        private Button btnGenerarListas;
        private Label label3;
        private ComboBox comboBox1;
        private GroupBox groupBox1;
        private Label label5;
        private Label label6;
        private Button btnGenerarLisProf;
    }
}