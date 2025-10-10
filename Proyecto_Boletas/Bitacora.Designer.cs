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
            panel1 = new Panel();
            btnVolver = new Button();
            label14 = new Label();
            label2 = new Label();
            cmbMes = new ComboBox();
            label1 = new Label();
            cmbRoles = new ComboBox();
            groupBox6 = new GroupBox();
            label12 = new Label();
            label13 = new Label();
            btnGenerarBitacora = new Button();
            panel1.SuspendLayout();
            groupBox6.SuspendLayout();
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
            label14.Size = new Size(126, 32);
            label14.TabIndex = 6;
            label14.Text = "Bitacora";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(256, 199);
            label2.Name = "label2";
            label2.Size = new Size(128, 21);
            label2.TabIndex = 32;
            label2.Text = "Seleccione Mes:";
            // 
            // cmbMes
            // 
            cmbMes.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMes.Items.AddRange(new object[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" });
            cmbMes.Location = new Point(405, 199);
            cmbMes.Name = "cmbMes";
            cmbMes.Size = new Size(133, 23);
            cmbMes.TabIndex = 31;
            cmbMes.SelectedIndexChanged += cmbMes_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(256, 144);
            label1.Name = "label1";
            label1.Size = new Size(137, 21);
            label1.TabIndex = 30;
            label1.Text = "Seleccione Roles:";
            // 
            // cmbRoles
            // 
            cmbRoles.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRoles.Location = new Point(405, 144);
            cmbRoles.Name = "cmbRoles";
            cmbRoles.Size = new Size(133, 23);
            cmbRoles.TabIndex = 29;
            cmbRoles.SelectedIndexChanged += cmbRoles_SelectedIndexChanged;
            // 
            // groupBox6
            // 
            groupBox6.BackColor = Color.FromArgb(157, 101, 101);
            groupBox6.Controls.Add(label12);
            groupBox6.Controls.Add(label13);
            groupBox6.Controls.Add(btnGenerarBitacora);
            groupBox6.Location = new Point(64, 108);
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
            label12.Location = new Point(45, 175);
            label12.Name = "label12";
            label12.Size = new Size(70, 21);
            label12.TabIndex = 5;
            label12.Text = "Bitacora";
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
            // btnGenerarBitacora
            // 
            btnGenerarBitacora.BackColor = Color.FromArgb(212, 168, 133);
            btnGenerarBitacora.BackgroundImageLayout = ImageLayout.Center;
            btnGenerarBitacora.FlatStyle = FlatStyle.Popup;
            btnGenerarBitacora.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGenerarBitacora.Image = (Image)resources.GetObject("btnGenerarBitacora.Image");
            btnGenerarBitacora.Location = new Point(22, 16);
            btnGenerarBitacora.Name = "btnGenerarBitacora";
            btnGenerarBitacora.Size = new Size(121, 135);
            btnGenerarBitacora.TabIndex = 3;
            btnGenerarBitacora.UseVisualStyleBackColor = false;
            btnGenerarBitacora.Click += btnGenerarBitacora_Click;
            // 
            // Bitacora
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(63, 75, 92);
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Controls.Add(label2);
            Controls.Add(cmbMes);
            Controls.Add(label1);
            Controls.Add(cmbRoles);
            Controls.Add(groupBox6);
            Name = "Bitacora";
            Text = "Bitacora";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
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
        private ComboBox cmbRoles;
        private GroupBox groupBox6;
        private Label label12;
        private Label label13;
        private Button btnGenerarBitacora;
    }
}