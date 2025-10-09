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
            flowSecretarias = new FlowLayoutPanel();
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
            panel1.Margin = new Padding(6, 6, 6, 6);
            panel1.Name = "panel1";
            panel1.Size = new Size(1488, 126);
            panel1.TabIndex = 15;
            panel1.Paint += panel1_Paint;
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
            btnVolver.Location = new Point(65, 6);
            btnVolver.Margin = new Padding(6, 6, 6, 6);
            btnVolver.Name = "btnVolver";
            btnVolver.RightToLeft = RightToLeft.Yes;
            btnVolver.Size = new Size(93, 107);
            btnVolver.TabIndex = 7;
            btnVolver.UseVisualStyleBackColor = false;
            btnVolver.Click += btnVolver_Click;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Cascadia Code", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label14.ForeColor = SystemColors.ControlText;
            label14.Location = new Point(353, 19);
            label14.Margin = new Padding(6, 0, 6, 0);
            label14.Name = "label14";
            label14.Size = new Size(839, 63);
            label14.TabIndex = 6;
            label14.Text = "Administración de Secretarias";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.BackColor = Color.Transparent;
            label18.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label18.ForeColor = SystemColors.ControlLightLight;
            label18.Location = new Point(919, 299);
            label18.Margin = new Padding(6, 0, 6, 0);
            label18.Name = "label18";
            label18.Size = new Size(196, 45);
            label18.TabIndex = 9;
            label18.Text = "Información";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.BackColor = Color.Transparent;
            label15.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label15.ForeColor = SystemColors.ControlLightLight;
            label15.Location = new Point(947, 254);
            label15.Margin = new Padding(6, 0, 6, 0);
            label15.Name = "label15";
            label15.Size = new Size(138, 45);
            label15.TabIndex = 5;
            label15.Text = "Guardar";
            label15.Click += label15_Click;
            // 
            // btnAltaSecretarias
            // 
            btnAltaSecretarias.BackColor = Color.FromArgb(212, 168, 133);
            btnAltaSecretarias.BackgroundImageLayout = ImageLayout.Center;
            btnAltaSecretarias.FlatStyle = FlatStyle.Popup;
            btnAltaSecretarias.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAltaSecretarias.Image = (Image)resources.GetObject("btnAltaSecretarias.Image");
            btnAltaSecretarias.Location = new Point(947, 132);
            btnAltaSecretarias.Margin = new Padding(6, 6, 6, 6);
            btnAltaSecretarias.Name = "btnAltaSecretarias";
            btnAltaSecretarias.Size = new Size(97, 111);
            btnAltaSecretarias.TabIndex = 3;
            btnAltaSecretarias.UseVisualStyleBackColor = false;
            btnAltaSecretarias.Click += btnAltaSecretarias_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(50, 188);
            label1.Margin = new Padding(6, 0, 6, 0);
            label1.Name = "label1";
            label1.Size = new Size(125, 37);
            label1.TabIndex = 1;
            label1.Text = "Correo:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(50, 292);
            label2.Margin = new Padding(6, 0, 6, 0);
            label2.Name = "label2";
            label2.Size = new Size(194, 37);
            label2.TabIndex = 2;
            label2.Text = "Contraseña:";
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.FromArgb(63, 75, 92);
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
            groupBox1.Location = new Point(134, 190);
            groupBox1.Margin = new Padding(6, 6, 6, 6);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(6, 6, 6, 6);
            groupBox1.Size = new Size(1241, 416);
            groupBox1.TabIndex = 18;
            groupBox1.TabStop = false;
            groupBox1.Enter += groupBox1_Enter;
            // 
            // txtContrasenaSecre
            // 
            txtContrasenaSecre.BackColor = SystemColors.WindowFrame;
            txtContrasenaSecre.Location = new Point(241, 286);
            txtContrasenaSecre.Margin = new Padding(6, 6, 6, 6);
            txtContrasenaSecre.Name = "txtContrasenaSecre";
            txtContrasenaSecre.Size = new Size(476, 44);
            txtContrasenaSecre.TabIndex = 6;
            // 
            // txtCorreoSecre
            // 
            txtCorreoSecre.BackColor = SystemColors.WindowFrame;
            txtCorreoSecre.Location = new Point(241, 188);
            txtCorreoSecre.Margin = new Padding(6, 6, 6, 6);
            txtCorreoSecre.Name = "txtCorreoSecre";
            txtCorreoSecre.Size = new Size(476, 44);
            txtCorreoSecre.TabIndex = 5;
            // 
            // txtUsuarioSecre
            // 
            txtUsuarioSecre.BackColor = SystemColors.WindowFrame;
            txtUsuarioSecre.Location = new Point(241, 98);
            txtUsuarioSecre.Margin = new Padding(6, 6, 6, 6);
            txtUsuarioSecre.Name = "txtUsuarioSecre";
            txtUsuarioSecre.Size = new Size(476, 44);
            txtUsuarioSecre.TabIndex = 4;
            // 
            // Usuario
            // 
            Usuario.AutoSize = true;
            Usuario.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Usuario.Location = new Point(50, 98);
            Usuario.Margin = new Padding(6, 0, 6, 0);
            Usuario.Name = "Usuario";
            Usuario.Size = new Size(131, 37);
            Usuario.TabIndex = 3;
            Usuario.Text = "Usuario:";
            // 
            // flowSecretarias
            // 
            flowSecretarias.AutoScroll = true;
            flowSecretarias.BackColor = Color.FromArgb(63, 75, 92);
            flowSecretarias.Location = new Point(111, 655);
            flowSecretarias.Margin = new Padding(6, 6, 6, 6);
            flowSecretarias.Name = "flowSecretarias";
            flowSecretarias.Size = new Size(1263, 478);
            flowSecretarias.TabIndex = 25;
            flowSecretarias.Paint += flowSecretarias_Paint;
            // 
            // adm_Secretaria
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(63, 75, 92);
            ClientSize = new Size(1488, 1158);
            Controls.Add(flowSecretarias);
            Controls.Add(groupBox1);
            Controls.Add(panel1);
            Margin = new Padding(6, 6, 6, 6);
            Name = "adm_Secretaria";
            Text = "adm_Secretaria";
            TransparencyKey = Color.Transparent;
            Load += adm_Secretaria_Load;
            Paint += panel1_Paint;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
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
        private FlowLayoutPanel flowSecretarias;
    }
}