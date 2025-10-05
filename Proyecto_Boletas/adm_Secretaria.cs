using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Boletas
{
    public partial class adm_Secretaria : Form
    {
        public adm_Secretaria()
        {
            InitializeComponent();
            txtUsuarioSecre.MaxLength = 20;
            txtCorreoSecre.MaxLength = 65;
            txtContrasenaSecre.MaxLength = 20;
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }
        private void adm_Secretaria_Load(object sender, EventArgs e)
        {
            MostrarSecretarias();
        }


        private void btnAltaSecretarias_Click(object sender, EventArgs e)
        {
            string nombre = txtUsuarioSecre.Text.Trim();
            string correo = txtCorreoSecre.Text.Trim();
            string contrasena = txtContrasenaSecre.Text;
            string rol = "Secretaria";
            DateTime fechaRegistro = DateTime.Now;

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(contrasena))
            {
                MessageBox.Show("Por favor, completa todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nombre.Length > 20)
            {
                MessageBox.Show("El nombre de usuario no puede tener más de 20 caracteres.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (!System.Text.RegularExpressions.Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Ingresa un correo electrónico válido (ejemplo: nombre@dominio.com).", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (correo.Length > 65)
            {
                MessageBox.Show("El correo no puede tener más de 65 caracteres.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(contrasena, @"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]).{8,}$"))
            {
                MessageBox.Show("La contraseña debe tener al menos:\n- 1 mayúscula\n- 1 número\n- 1 carácter especial\n- Mínimo 8 caracteres.", "Contraseña inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                Conexion conexion = new Conexion();
                using (MySqlConnection conn = conexion.GetConnection())
                {
                    conn.Open();
                    string query = "INSERT INTO usuarios (Nombre, Correo, Contrasena, Rol, FechaRegistro) VALUES (@nombre, @correo, @contrasena, @rol, @fecha)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.Parameters.AddWithValue("@contrasena", contrasena);
                    cmd.Parameters.AddWithValue("@rol", rol);
                    cmd.Parameters.AddWithValue("@fecha", fechaRegistro);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Secretaria registrada correctamente.");

                    txtUsuarioSecre.Clear();
                    txtCorreoSecre.Clear();
                    txtContrasenaSecre.Clear();
                    MostrarSecretarias();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar secretaria: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarSecretarias()
        {
            try
            {
                // Limpiar primero el FlowLayoutPanel
                flowSecretarias.Controls.Clear();

                Conexion conexion = new Conexion();
                using (MySqlConnection conn = conexion.GetConnection())
                {
                    conn.Open();

                    // Seleccionamos ID, Nombre, Correo y Contrasena
                    string query = "SELECT UsuarioID, Nombre, Correo, Contrasena FROM usuarios WHERE Rol = 'Secretaria'";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        // Capturamos valores en variables temporales
                        int idTemp = Convert.ToInt32(reader["UsuarioID"]);
                        string nombreTemp = reader["Nombre"].ToString();
                        string correoTemp = reader["Correo"].ToString();
                        string contrasenaTemp = reader["Contrasena"].ToString();

                        // Creamos la tarjeta visual
                        Panel card = new Panel
                        {
                            Width = 280,
                            Height = 100,
                            Margin = new Padding(10),
                            BackColor = Color.Bisque,
                            BorderStyle = BorderStyle.FixedSingle,
                            Cursor = Cursors.Hand
                        };

                        // Etiqueta de Nombre
                        Label lblNombre = new Label
                        {
                            Text = "👩 " + nombreTemp,
                            Font = new Font("Segoe UI", 10, FontStyle.Bold),
                            Location = new Point(10, 10),
                            AutoSize = true
                        };

                        // Etiqueta de Correo
                        Label lblCorreo = new Label
                        {
                            Text = "📧 " + correoTemp,
                            Font = new Font("Segoe UI", 9),
                            Location = new Point(10, 40),
                            AutoSize = true
                        };

                        // Botón Editar
                        Button btnEditar = new Button
                        {
                            Text = "✏️",
                            Font = new Font("Segoe UI Emoji", 10),
                            Size = new Size(35, 30),
                            Location = new Point(200, 10),
                            FlatStyle = FlatStyle.Flat,
                            BackColor = Color.SandyBrown
                        };
                        btnEditar.Click += (s, e) => EditarSecretaria(nombreTemp);

                        // Botón Eliminar
                        Button btnEliminar = new Button
                        {
                            Text = "❌",
                            Font = new Font("Segoe UI Emoji", 10),
                            Size = new Size(35, 30),
                            Location = new Point(240, 10),
                            FlatStyle = FlatStyle.Flat,
                            BackColor = Color.IndianRed
                        };
                        btnEliminar.Click += (s, e) => EliminarSecretaria(nombreTemp);

                        // Agregamos controles al panel
                        card.Controls.Add(lblNombre);
                        card.Controls.Add(lblCorreo);
                        card.Controls.Add(btnEditar);
                        card.Controls.Add(btnEliminar);

                        // Agregamos el panel al FlowLayoutPanel
                        flowSecretarias.Controls.Add(card);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar secretarias: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EliminarSecretaria(string nombre)
        {
            if (MessageBox.Show($"¿Seguro que deseas eliminar a {nombre}?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Conexion conexion = new Conexion();
                using (MySqlConnection conn = conexion.GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM usuarios WHERE Nombre=@nombre AND Rol='Secretaria'";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Secretaria eliminada correctamente.");
                MostrarSecretarias();
            }
        }

        private void EditarSecretaria(string nombre)
        {
            txtUsuarioSecre.Text = nombre; // Puedes cargar su info en los campos
            MessageBox.Show($"Ahora puedes editar los datos de {nombre}.", "Editar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool DatoExistente(string campo, string valor)
        {
            Conexion conexion = new Conexion();
            using (MySqlConnection conn = conexion.GetConnection())
            {
                conn.Open();
                string query = $"SELECT COUNT(*) FROM usuarios WHERE {campo} = @valor AND Rol = 'Secretaria'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@valor", valor);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0; // Retorna true si el dato ya existe
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Menu_principal nuevoFormulario = new Menu_principal();
            nuevoFormulario.Show();
            this.Hide();
        }

        private void flowSecretarias_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
