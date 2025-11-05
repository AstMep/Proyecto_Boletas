namespace Proyecto_Boletas
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            label2 = new Label();
            btn_ingresar = new Button();
            groupBox1 = new GroupBox();
            label3 = new Label();
            label4 = new Label();
            panel3 = new Panel();
            panel4 = new Panel();
            btnMostrarContrasena = new Button();
            panel2 = new Panel();
            txtbox_contrasena = new TextBox();
            panel1 = new Panel();
            txtbox_usuario = new TextBox();
            pictureBox2 = new PictureBox();
            groupBox1.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(181, 131, 120);
            label1.Font = new Font("Palatino Linotype", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(15, 34);
            label1.Name = "label1";
            label1.Size = new Size(89, 28);
            label1.TabIndex = 1;
            label1.Text = "Usuario";
            label1.Click += label1_Click_1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(181, 131, 120);
            label2.Font = new Font("Palatino Linotype", 15.75F, FontStyle.Bold);
            label2.Location = new Point(15, 158);
            label2.Name = "label2";
            label2.Size = new Size(124, 28);
            label2.TabIndex = 2;
            label2.Text = "Contraseña";
            // 
            // btn_ingresar
            // 
            btn_ingresar.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btn_ingresar.BackColor = Color.SkyBlue;
            btn_ingresar.BackgroundImage = Properties.Resources._3f4f5c;
            btn_ingresar.Cursor = Cursors.Hand;
            btn_ingresar.FlatStyle = FlatStyle.Popup;
            btn_ingresar.Font = new Font("Century Gothic", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_ingresar.ForeColor = SystemColors.ControlLightLight;
            btn_ingresar.Image = Properties.Resources._3f4f5c;
            btn_ingresar.Location = new Point(96, 245);
            btn_ingresar.Name = "btn_ingresar";
            btn_ingresar.RightToLeft = RightToLeft.Yes;
            btn_ingresar.Size = new Size(111, 36);
            btn_ingresar.TabIndex = 3;
            btn_ingresar.Text = "Ingresar";
            btn_ingresar.UseVisualStyleBackColor = false;
            btn_ingresar.Click += btn_ingresar_Click;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.Transparent;
            groupBox1.BackgroundImage = Properties.Resources.D4A885_Tumbleweed;
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(panel3);
            groupBox1.Controls.Add(pictureBox2);
            groupBox1.Location = new Point(25, 32);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(978, 479);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Inicio de Sesión";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(333, 47);
            label3.Name = "label3";
            label3.Size = new Size(541, 37);
            label3.TabIndex = 9;
            label3.Text = "INSTITUTO MANUEL M. ACOSTA";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label4.Location = new Point(688, 436);
            label4.Name = "label4";
            label4.Size = new Size(251, 20);
            label4.TabIndex = 14;
            label4.Text = "Derechos reservaod a FAY JC";
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(157, 101, 101);
            panel3.Controls.Add(panel4);
            panel3.Location = new Point(453, 87);
            panel3.Name = "panel3";
            panel3.Size = new Size(330, 330);
            panel3.TabIndex = 13;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(181, 131, 120);
            panel4.Controls.Add(btnMostrarContrasena);
            panel4.Controls.Add(panel2);
            panel4.Controls.Add(txtbox_contrasena);
            panel4.Controls.Add(panel1);
            panel4.Controls.Add(txtbox_usuario);
            panel4.Controls.Add(btn_ingresar);
            panel4.Controls.Add(label1);
            panel4.Controls.Add(label2);
            panel4.Location = new Point(8, 9);
            panel4.Name = "panel4";
            panel4.Size = new Size(315, 312);
            panel4.TabIndex = 8;
            // 
            // btnMostrarContrasena
            // 
            btnMostrarContrasena.BackColor = Color.FromArgb(181, 131, 120);
            btnMostrarContrasena.FlatStyle = FlatStyle.Flat;
            btnMostrarContrasena.Location = new Point(269, 194);
            btnMostrarContrasena.Margin = new Padding(2, 1, 2, 1);
            btnMostrarContrasena.Name = "btnMostrarContrasena";
            btnMostrarContrasena.Size = new Size(30, 22);
            btnMostrarContrasena.TabIndex = 9;
            btnMostrarContrasena.Text = "👁️";
            btnMostrarContrasena.UseVisualStyleBackColor = false;
            btnMostrarContrasena.Click += btnMostrarContrasena_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Black;
            panel2.Location = new Point(15, 220);
            panel2.Name = "panel2";
            panel2.Size = new Size(284, 1);
            panel2.TabIndex = 7;
            // 
            // txtbox_contrasena
            // 
            txtbox_contrasena.BackColor = Color.FromArgb(181, 131, 120);
            txtbox_contrasena.BorderStyle = BorderStyle.None;
            txtbox_contrasena.Location = new Point(15, 205);
            txtbox_contrasena.Name = "txtbox_contrasena";
            txtbox_contrasena.Size = new Size(284, 16);
            txtbox_contrasena.TabIndex = 6;
            txtbox_contrasena.TextChanged += txtbox_contrasena_TextChanged;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Black;
            panel1.Location = new Point(12, 104);
            panel1.Name = "panel1";
            panel1.Size = new Size(287, 1);
            panel1.TabIndex = 5;
            // 
            // txtbox_usuario
            // 
            txtbox_usuario.BackColor = Color.FromArgb(181, 131, 120);
            txtbox_usuario.BorderStyle = BorderStyle.None;
            txtbox_usuario.Location = new Point(9, 89);
            txtbox_usuario.Name = "txtbox_usuario";
            txtbox_usuario.Size = new Size(287, 16);
            txtbox_usuario.TabIndex = 4;
            txtbox_usuario.TextChanged += txtbox_usuario_TextChanged;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(49, 60);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(398, 339);
            pictureBox2.TabIndex = 8;
            pictureBox2.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SlateGray;
            BackgroundImage = Properties.Resources._274259;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1015, 513);
            Controls.Add(groupBox1);
            DoubleBuffered = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Programa Boleta";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel3.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Label label1;
        private Label label2;
        private Button btn_ingresar;
        private GroupBox groupBox1;
        private TextBox txtbox_usuario;
        private Panel panel1;
        private Panel panel2;
        private TextBox txtbox_contrasena;
        private PictureBox pictureBox2;
        private Label label3;
        private Panel panel3;
        private Label label4;
        private Panel panel4;
        private Button btnMostrarContrasena;
    }
}
