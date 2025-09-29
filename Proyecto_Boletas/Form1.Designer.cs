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
            pictureBox1 = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            btn_ingresar = new Button();
            groupBox1 = new GroupBox();
            panel2 = new Panel();
            textBox1 = new TextBox();
            panel1 = new Panel();
            tb_usuario = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(6, 34);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(432, 396);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(465, 68);
            label1.Name = "label1";
            label1.Size = new Size(88, 30);
            label1.TabIndex = 1;
            label1.Text = "Usuario";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(454, 195);
            label2.Name = "label2";
            label2.Size = new Size(123, 30);
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
            btn_ingresar.Location = new Point(550, 340);
            btn_ingresar.Name = "btn_ingresar";
            btn_ingresar.RightToLeft = RightToLeft.Yes;
            btn_ingresar.Size = new Size(111, 36);
            btn_ingresar.TabIndex = 3;
            btn_ingresar.Text = "Ingresar";
            btn_ingresar.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            groupBox1.BackgroundImage = Properties.Resources.D4A885_Tumbleweed;
            groupBox1.Controls.Add(panel2);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(panel1);
            groupBox1.Controls.Add(tb_usuario);
            groupBox1.Controls.Add(pictureBox1);
            groupBox1.Controls.Add(btn_ingresar);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(12, 27);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(872, 462);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Inicio de Sesión";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Black;
            panel2.Location = new Point(469, 259);
            panel2.Name = "panel2";
            panel2.Size = new Size(212, 2);
            panel2.TabIndex = 7;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(212, 168, 133);
            textBox1.BorderStyle = BorderStyle.None;
            textBox1.Location = new Point(472, 245);
            textBox1.Name = "textBox1";
            textBox1.PasswordChar = '*';
            textBox1.Size = new Size(212, 16);
            textBox1.TabIndex = 6;
            textBox1.UseSystemPasswordChar = true;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Black;
            panel1.Location = new Point(469, 141);
            panel1.Name = "panel1";
            panel1.Size = new Size(215, 2);
            panel1.TabIndex = 5;
            // 
            // tb_usuario
            // 
            tb_usuario.BackColor = Color.FromArgb(212, 168, 133);
            tb_usuario.BorderStyle = BorderStyle.None;
            tb_usuario.Location = new Point(472, 127);
            tb_usuario.Name = "tb_usuario";
            tb_usuario.Size = new Size(215, 16);
            tb_usuario.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SlateGray;
            BackgroundImage = Properties.Resources._274259;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(907, 538);
            Controls.Add(groupBox1);
            DoubleBuffered = true;
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label1;
        private Label label2;
        private Button btn_ingresar;
        private GroupBox groupBox1;
        private TextBox tb_usuario;
        private Panel panel1;
        private Panel panel2;
        private TextBox textBox1;
    }
}
