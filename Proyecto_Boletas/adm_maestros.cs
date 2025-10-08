using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Boletas
{
    public partial class adm_maestros : Form
    {
        public adm_maestros()
        {
            InitializeComponent();
            txtammaestro.MaxLength = 50;
            txtnombremaestro.MaxLength = 50;
            txtcorreomaestro.MaxLength = 50;
            txtapmaestro.MaxLength = 50;
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            if (btnVolver.Enabled)
            {
                Menu_principal nuevoFormulario = new Menu_principal(); // creas una instancia del otro form
                nuevoFormulario.Show();              // lo muestras
                this.Hide();
            }
        }

        private void adm_maestros_Load(object sender, EventArgs e)
        {
            MostrarMaestros();
        }


        private void MostrarMaestros()
        {
            try
            {
                flowMaestros.Controls.Clear();

                Conexion conexion = new Conexion();
                using (MySqlConnection conn = conexion.GetConnection())
                {
                    conn.Open();

                    string query = "SELECT id_maestro, NombreMaestro, ApellidoPMaestro, ApellidoMMaestro, Correo_maestro FROM maestro";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int idTemp = Convert.ToInt32(reader["id_maestro"]);
                        string nombreTemp = reader["NombreMaestro"].ToString();
                        string apPTemp = reader["ApellidoPMaestro"].ToString();
                        string apMTemp = reader["ApellidoMMaestro"].ToString();
                        string correoTemp = reader["Correo_maestro"].ToString();

                        string nombreCompleto = $"{nombreTemp} {apPTemp} {apMTemp}";

                        // Tarjeta visual
                        Panel card = new Panel
                        {
                            Width = 280,
                            Height = 100,
                            Margin = new Padding(10),
                            BackColor = Color.Bisque,
                            BorderStyle = BorderStyle.FixedSingle,
                            Cursor = Cursors.Hand
                        };

                        PictureBox picMaestro = new PictureBox
                        {
                            Image = Image.FromFile(Application.StartupPath + @"\Iconos\secretaria45.png"),
                            SizeMode = PictureBoxSizeMode.Zoom,
                            Location = new Point(10, 10),
                            Size = new Size(32, 32)
                        };

                        Label lblNombre = new Label
                        {
                            Text = nombreCompleto,
                            Font = new Font("Segoe UI", 10, FontStyle.Bold),
                            Location = new Point(45, 10),
                            AutoSize = true
                        };

                        PictureBox picCorreo = new PictureBox
                        {
                            Image = Image.FromFile(Application.StartupPath + @"\Iconos\correo45.png"),
                            SizeMode = PictureBoxSizeMode.Zoom,
                            Location = new Point(10, 45),
                            Size = new Size(32, 32)
                        };

                        Label lblCorreo = new Label
                        {
                            Text = correoTemp,
                            Font = new Font("Segoe UI", 9),
                            Location = new Point(45, 47),
                            AutoSize = true
                        };

                        Button btnEditar = new Button
                        {
                            Size = new Size(30, 25),
                            Location = new Point(200, 10),
                            FlatStyle = FlatStyle.Flat,
                            BackColor = Color.SandyBrown,
                            BackgroundImageLayout = ImageLayout.Zoom,
                            BackgroundImage = Image.FromFile(Application.StartupPath + @"\Iconos\editor32.png")
                        };
                        btnEditar.Click += (s, e) => EditarMaestro(nombreTemp, apPTemp, apMTemp, correoTemp);

                        Button btnEliminar = new Button
                        {
                            Size = new Size(30, 25),
                            Location = new Point(240, 10),
                            FlatStyle = FlatStyle.Flat,
                            BackColor = Color.IndianRed,
                            BackgroundImageLayout = ImageLayout.Zoom,
                            BackgroundImage = Image.FromFile(Application.StartupPath + @"\Iconos\delete32.png")
                        };
                        btnEliminar.Click += (s, e) => EliminarMaestro(idTemp);

                        card.Controls.Add(picMaestro);
                        card.Controls.Add(lblNombre);
                        card.Controls.Add(picCorreo);
                        card.Controls.Add(lblCorreo);
                        card.Controls.Add(btnEditar);
                        card.Controls.Add(btnEliminar);

                        flowMaestros.Controls.Add(card);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar maestros: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditarMaestro(string nombre, string apP, string apM, string correo)
        {
            txtnombremaestro.Text = nombre;
            txtapmaestro.Text = apP;
            txtammaestro.Text = apM;
            txtcorreomaestro.Text = correo;
            MessageBox.Show($"Ahora puedes editar los datos de {nombre} {apP}.", "Editar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void EliminarMaestro(int id)
        {
            if (MessageBox.Show($"¿Seguro que deseas eliminar este maestro?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Conexion conexion = new Conexion();
                using (MySqlConnection conn = conexion.GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM maestro WHERE id_maestro=@id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Maestro eliminado correctamente.", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarMaestros();
            }
        }

        private bool DatoExistenteNombreCompleto(string nombreCompleto)
        {
            Conexion conexion = new Conexion();
            using (MySqlConnection conn = conexion.GetConnection())
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM maestro WHERE CONCAT(LOWER(NombreMaestro), ' ', LOWER(ApellidoPMaestro), ' ', LOWER(ApellidoMMaestro)) = @valor";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@valor", nombreCompleto);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        private bool DatoExistenteCorreo(string correo)
        {
            Conexion conexion = new Conexion();
            using (MySqlConnection conn = conexion.GetConnection())
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM maestro WHERE LOWER(Correo_maestro) = @correo";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@correo", correo);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            int borderRadius = 25;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, panel1.Width, panel1.Height);
            int diameter = borderRadius * 2;

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseAllFigures();

            panel1.Region = new Region(path);
        }

        private void btnAltaMaestros_Click_1(object sender, EventArgs e)
        {

            string nombre = txtnombremaestro.Text.Trim();
            string apellidoP = txtapmaestro.Text.Trim();
            string apellidoM = txtammaestro.Text.Trim();
            string correo = txtcorreomaestro.Text.Trim();

            // Eliminar espacios dobles
            nombre = Regex.Replace(nombre, @"\s+", " ");
            apellidoP = Regex.Replace(apellidoP, @"\s+", " ");
            apellidoM = Regex.Replace(apellidoM, @"\s+", " ");
            correo = Regex.Replace(correo, @"\s+", "");

            // Validar campos vacíos
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellidoP) ||
                string.IsNullOrWhiteSpace(apellidoM) || string.IsNullOrWhiteSpace(correo))
            {
                MessageBox.Show("Por favor, completa todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar nombres y apellidos (solo letras y espacios, mínimo 2 letras)
            if (!Regex.IsMatch(nombre, @"^[A-Za-zÁÉÍÓÚáéíóúÑñ ]{2,}$") ||
                !Regex.IsMatch(apellidoP, @"^[A-Za-zÁÉÍÓÚáéíóúÑñ ]{2,}$") ||
                !Regex.IsMatch(apellidoM, @"^[A-Za-zÁÉÍÓÚáéíóúÑñ ]{2,}$"))
            {
                MessageBox.Show("Los nombres y apellidos deben tener al menos 2 letras y solo pueden contener letras y espacios.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar longitud
            if (nombre.Length > 50 || apellidoP.Length > 50 || apellidoM.Length > 50)
            {
                MessageBox.Show("Los nombres y apellidos no pueden tener más de 50 caracteres.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (correo.Length > 65)
            {
                MessageBox.Show("El correo no puede tener más de 65 caracteres.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar correo
            if (!Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Ingresa un correo electrónico válido (ejemplo: nombre@dominio.com).", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombreCompleto = $"{nombre.ToLower()} {apellidoP.ToLower()} {apellidoM.ToLower()}";

            // Verificar duplicados
            if (DatoExistenteNombreCompleto(nombreCompleto))
            {
                MessageBox.Show("Ya existe un maestro con ese nombre completo.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DatoExistenteCorreo(correo.ToLower()))
            {
                MessageBox.Show("El correo ya está registrado. Usa otro.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Conexion conexion = new Conexion();
                using (MySqlConnection conn = conexion.GetConnection())
                {
                    conn.Open();

                    // Verificar límite de 9 maestros
                    string queryCount = "SELECT COUNT(*) FROM maestro";
                    MySqlCommand cmdCount = new MySqlCommand(queryCount, conn);
                    int cantidadMaestros = Convert.ToInt32(cmdCount.ExecuteScalar());

                    if (cantidadMaestros >= 9)
                    {
                        MessageBox.Show("Ya no puedes registrar más maestros. El límite es 9.", "Límite alcanzado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Insertar nuevo maestro
                    string query = "INSERT INTO maestro (NombreMaestro, ApellidoPMaestro, ApellidoMMaestro, Correo_maestro) " +
                                   "VALUES (@nombre, @apP, @apM, @correo)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@apP", apellidoP);
                    cmd.Parameters.AddWithValue("@apM", apellidoM);
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Maestro registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpiar campos
                    txtnombremaestro.Clear();
                    txtapmaestro.Clear();
                    txtammaestro.Clear();
                    txtcorreomaestro.Clear();

                    MostrarMaestros();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar maestro: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtnombremaestro_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }

}

