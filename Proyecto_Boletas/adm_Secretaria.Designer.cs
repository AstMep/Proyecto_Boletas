namespace Proyecto_Boletas
{
    partial class adm_Secretaria
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(adm_Secretaria));
            panel1 = new Panel();
            btnVolver = new Button();
            label14 = new Label();
            label18 = new Label();
            label15 = new Label();
            btnAltaSecretarias = new Button();
            label1 = new Label();
            label2 = new Label();
            groupBox1 = new GroupBox();
            txtContrasenaSecre = new TextBox();
            txtCorreoSecre = new TextBox();
            txtUsuarioSecre = new TextBox();
            Usuario = new Label();
            listBoxSecretarias = new ListBox();
            button1 = new Button();
            label3 = new Label();
            panel1.SuspendLayout();
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
            panel1.Size = new Size(801, 59);
            panel1.TabIndex = 15;
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
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Cascadia Code", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label14.ForeColor = SystemColors.ControlLightLight;
            label14.Location = new Point(190, 9);
            label14.Name = "label14";
            label14.Size = new Size(420, 32);
            label14.TabIndex = 6;
            label14.Text = "Administración de Secretarias";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.BackColor = Color.Transparent;
            label18.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label18.ForeColor = SystemColors.ControlLightLight;
            label18.Location = new Point(461, 148);
            label18.Name = "label18";
            label18.Size = new Size(113, 21);
            label18.TabIndex = 9;
            label18.Text = "de Secretarias";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.BackColor = Color.Transparent;
            label15.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label15.ForeColor = SystemColors.ControlLightLight;
            label15.Location = new Point(495, 127);
            label15.Name = "label15";
            label15.Size = new Size(43, 21);
            label15.TabIndex = 5;
            label15.Text = "Alta ";
            label15.Click += label15_Click;
            // 
            // btnAltaSecretarias
            // 
            btnAltaSecretarias.BackColor = Color.FromArgb(212, 168, 133);
            btnAltaSecretarias.BackgroundImageLayout = ImageLayout.Center;
            btnAltaSecretarias.FlatStyle = FlatStyle.Popup;
            btnAltaSecretarias.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAltaSecretarias.Image = (Image)resources.GetObject("btnAltaSecretarias.Image");
            btnAltaSecretarias.Location = new Point(461, 46);
            btnAltaSecretarias.Name = "btnAltaSecretarias";
            btnAltaSecretarias.Size = new Size(113, 73);
            btnAltaSecretarias.TabIndex = 3;
            btnAltaSecretarias.UseVisualStyleBackColor = false;
            btnAltaSecretarias.Click += btnAltaSecretarias_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(27, 88);
            label1.Name = "label1";
            label1.Size = new Size(64, 18);
            label1.TabIndex = 1;
            label1.Text = "Correo:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(27, 137);
            label2.Name = "label2";
            label2.Size = new Size(97, 18);
            label2.TabIndex = 2;
            label2.Text = "Contraseña:";
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.FromArgb(212, 168, 133);
            groupBox1.Controls.Add(label18);
            groupBox1.Controls.Add(txtContrasenaSecre);
            groupBox1.Controls.Add(txtCorreoSecre);
            groupBox1.Controls.Add(label15);
            groupBox1.Controls.Add(btnAltaSecretarias);
            groupBox1.Controls.Add(txtUsuarioSecre);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(Usuario);
            groupBox1.Controls.Add(label1);
            groupBox1.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold);
            groupBox1.Location = new Point(72, 89);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(647, 195);
            groupBox1.TabIndex = 18;
            groupBox1.TabStop = false;
            groupBox1.Text = "Alta de Secretarias";
            // 
            // txtContrasenaSecre
            // 
            txtContrasenaSecre.Location = new Point(130, 134);
            txtContrasenaSecre.Name = "txtContrasenaSecre";
            txtContrasenaSecre.Size = new Size(258, 26);
            txtContrasenaSecre.TabIndex = 6;
            // 
            // txtCorreoSecre
            // 
            txtCorreoSecre.Location = new Point(130, 88);
            txtCorreoSecre.Name = "txtCorreoSecre";
            txtCorreoSecre.Size = new Size(258, 26);
            txtCorreoSecre.TabIndex = 5;
            // 
            // txtUsuarioSecre
            // 
            txtUsuarioSecre.Location = new Point(130, 46);
            txtUsuarioSecre.Name = "txtUsuarioSecre";
            txtUsuarioSecre.Size = new Size(258, 26);
            txtUsuarioSecre.TabIndex = 4;
            // 
            // Usuario
            // 
            Usuario.AutoSize = true;
            Usuario.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Usuario.Location = new Point(27, 46);
            Usuario.Name = "Usuario";
            Usuario.Size = new Size(66, 18);
            Usuario.TabIndex = 3;
            Usuario.Text = "Usuario:";
            // 
            // listBoxSecretarias
            // 
            listBoxSecretarias.BackColor = Color.FromArgb(212, 168, 133);
            listBoxSecretarias.FormattingEnabled = true;
            listBoxSecretarias.ItemHeight = 15;
            listBoxSecretarias.Location = new Point(72, 309);
            listBoxSecretarias.Name = "listBoxSecretarias";
            listBoxSecretarias.Size = new Size(388, 199);
            listBoxSecretarias.TabIndex = 20;
            // 
            // button1
            // 
            button1.BackgroundImage = (Image)resources.GetObject("button1.BackgroundImage");
            button1.Cursor = Cursors.Hand;
            button1.Location = new Point(558, 318);
            button1.Name = "button1";
            button1.Size = new Size(43, 46);
            button1.TabIndex = 21;
            button1.TextImageRelation = TextImageRelation.ImageBeforeText;
            button1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(607, 331);
            label3.Name = "label3";
            label3.Size = new Size(66, 18);
            label3.TabIndex = 22;
            label3.Text = "Usuario:";
            // 
            // adm_Secretaria
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(63, 75, 92);
            ClientSize = new Size(801, 543);
            Controls.Add(label3);
            Controls.Add(button1);
            Controls.Add(listBoxSecretarias);
            Controls.Add(groupBox1);
            Controls.Add(panel1);
            Name = "adm_Secretaria";
            Text = "adm_Secretaria";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Button btnVolver;
        private Label label14;
        private Label label18;
        private Label label15;
        private Button btnAltaSecretarias;
        private Label label1;
        private Label label2;
        private GroupBox groupBox1;
        private Label Usuario;
        private TextBox txtContrasenaSecre;
        private TextBox txtCorreoSecre;
        private TextBox txtUsuarioSecre;
        private ListBox listBoxSecretarias;
        private Button button1;
        private Label label3;
    }
}