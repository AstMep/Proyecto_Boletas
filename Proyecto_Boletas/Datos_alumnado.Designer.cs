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
            groupBox6 = new GroupBox();
            label12 = new Label();
            label13 = new Label();
            btnGenerarListas = new Button();
            cmbGrupo = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            cmbMes = new ComboBox();
            panel1 = new Panel();
            btnVolver = new Button();
            label14 = new Label();
            groupBox6.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox6
            // 
            groupBox6.BackColor = Color.FromArgb(157, 101, 101);
            groupBox6.Controls.Add(label12);
            groupBox6.Controls.Add(label13);
            groupBox6.Controls.Add(btnGenerarListas);
            groupBox6.Location = new Point(55, 121);
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
            btnGenerarListas.Location = new Point(20, 8);
            btnGenerarListas.Name = "btnGenerarListas";
            btnGenerarListas.Size = new Size(121, 135);
            btnGenerarListas.TabIndex = 3;
            btnGenerarListas.UseVisualStyleBackColor = false;
            btnGenerarListas.Click += btnGenerarListas_Click;
            // 
            // cmbGrupo
            // 
            cmbGrupo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGrupo.Location = new Point(405, 142);
            cmbGrupo.Name = "cmbGrupo";
            cmbGrupo.Size = new Size(133, 23);
            cmbGrupo.TabIndex = 15;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(256, 142);
            label1.Name = "label1";
            label1.Size = new Size(143, 21);
            label1.TabIndex = 16;
            label1.Text = "Seleccione Grupo:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(256, 197);
            label2.Name = "label2";
            label2.Size = new Size(128, 21);
            label2.TabIndex = 18;
            label2.Text = "Seleccione Mes:";
            // 
            // cmbMes
            // 
            cmbMes.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMes.Items.AddRange(new object[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" });
            cmbMes.Location = new Point(405, 197);
            cmbMes.Name = "cmbMes";
            cmbMes.Size = new Size(133, 23);
            cmbMes.TabIndex = 17;
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
            panel1.TabIndex = 27;
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
            label14.Size = new Size(210, 32);
            label14.TabIndex = 6;
            label14.Text = "Datos Alumnado";
            // 
            // Datos_alumnado
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(63, 75, 92);
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(label2);
            Controls.Add(cmbMes);
            Controls.Add(label1);
            Controls.Add(cmbGrupo);
            Controls.Add(groupBox6);
            Name = "Datos_alumnado";
            Text = "Datos_alumnado";
            Load += Datos_alumnado_Load;
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox6;
        private Label label12;
        private Label label13;
        private Button btnGenerarListas;
        private ComboBox cmbGrupo;
        private Label label1;
        private Label label2;
        private ComboBox cmbMes;
        private Panel panel1;
        private Button btnVolver;
        private Label label14;
    }
}