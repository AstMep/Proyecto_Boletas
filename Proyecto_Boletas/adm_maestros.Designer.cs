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
            label1 = new Label();
            panel1 = new Panel();
            btnVolver = new Button();
            label14 = new Label();
            groupBox1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // flowMaestros
            // 
            flowMaestros.AutoScroll = true;
            flowMaestros.BackColor = Color.FromArgb(63, 75, 92);
            flowMaestros.Location = new Point(60, 448);
            flowMaestros.Name = "flowMaestros";
            flowMaestros.Size = new Size(783, 187);
            flowMaestros.TabIndex = 28;
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
            groupBox1.Controls.Add(label1);
            groupBox1.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold);
            groupBox1.Location = new Point(72, 82);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(772, 294);
            groupBox1.TabIndex = 27;
            groupBox1.TabStop = false;
            groupBox1.Enter += groupBox1_Enter;
            // 
            // grupo_asignado
            // 
            grupo_asignado.DropDownStyle = ComboBoxStyle.DropDownList;
            grupo_asignado.Location = new Point(130, 226);
            grupo_asignado.Name = "grupo_asignado";
            grupo_asignado.Size = new Size(121, 26);
            grupo_asignado.TabIndex = 0;
            grupo_asignado.SelectedIndexChanged += grupo_asignado_SelectedIndexChanged;
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
            btnAltaMaestros.Click += btnAltaMaestros_Click_1;
            // 
            // txtnombremaestro
            // 
            txtnombremaestro.BackColor = SystemColors.WindowFrame;
            txtnombremaestro.Location = new Point(130, 46);
            txtnombremaestro.Name = "txtnombremaestro";
            txtnombremaestro.Size = new Size(258, 26);
            txtnombremaestro.TabIndex = 4;
            txtnombremaestro.TextChanged += txtnombremaestro_TextChanged;
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Century Gothic", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(28, 82);
            label1.Name = "label1";
            label1.Size = new Size(71, 36);
            label1.TabIndex = 1;
            label1.Text = "Apellido\r\nPaterno:";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(181, 131, 120);
            panel1.Controls.Add(btnVolver);
            panel1.Controls.Add(label14);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(916, 59);
            panel1.TabIndex = 26;
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
            btnVolver.Click += btnVolver_Click;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Cascadia Code", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label14.ForeColor = SystemColors.ControlText;
            label14.Location = new Point(190, 9);
            label14.Name = "label14";
            label14.Size = new Size(378, 32);
            label14.TabIndex = 6;
            label14.Text = "Administración de Maestros";
            // 
            // adm_maestros
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(933, 502);
            Controls.Add(flowMaestros);
            Controls.Add(groupBox1);
            Controls.Add(panel1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "adm_maestros";
            Text = "adm_maestros";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowMaestros;
        private GroupBox groupBox1;
        private Label label18;
        private TextBox txtammaestro;
        private TextBox txtapmaestro;
        private Label label15;
        private Button btnAltaMaestros;
        private TextBox txtnombremaestro;
        private Label label2;
        private Label Usuario;
        private Label label1;
        private Panel panel1;
        private Button btnVolver;
        private Label label14;
        private TextBox txtcorreomaestro;
        private Label label5;
        private Label label6;
        private Label label3;
        private ComboBox grupo_asignado;
    }
}