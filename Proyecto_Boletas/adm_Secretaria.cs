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
        }

        private void label15_Click(object sender, EventArgs e)
        {

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
                Conexion conexion = new Conexion();
                using (MySqlConnection conn = conexion.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT Nombre, Correo FROM usuarios WHERE Rol = 'Secretaria'";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    listBoxSecretarias.Items.Clear();

                    while (reader.Read())
                    {
                        string info = $"Nombre: {reader["Nombre"]} | Correo: {reader["Correo"]}";
                        listBoxSecretarias.Items.Add(info);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar secretarias: " + ex.Message);
            }
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

        private void adm_Secretaria_Load(object sender, EventArgs e)
        {
            txtUsuarioSecre.MaxLength = 20;
            txtCorreoSecre.MaxLength = 100;
            txtContrasenaSecre.MaxLength = 20;

            MostrarSecretarias();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (listBoxSecretarias.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una secretaria para eliminar.");
                return;
            }

            string nombreSeleccionado = listBoxSecretarias.SelectedItem.ToString()
                .Split('|')[0].Replace("Nombre:", "").Trim();

            Conexion conexion = new Conexion();
            using (MySqlConnection conn = conexion.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM usuarios WHERE Nombre = @nombre AND Rol = 'Secretaria'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nombre", nombreSeleccionado);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Secretaria eliminada correctamente.");
                MostrarSecretarias();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (listBoxSecretarias.SelectedItem == null)
            {
                MessageBox.Show("Selecciona una secretaria para modificar.");
                return;
            }

            string nombreSeleccionado = listBoxSecretarias.SelectedItem.ToString()
                .Split('|')[0].Replace("Nombre:", "").Trim();

            string nuevoCorreo = txtCorreoSecre.Text;
            string nuevaContrasena = txtContrasenaSecre.Text;

            if (nuevoCorreo == "" || nuevaContrasena == "")
            {
                MessageBox.Show("Ingresa los nuevos datos.");
                return;
            }

            Conexion conexion = new Conexion();
            using (MySqlConnection conn = conexion.GetConnection())
            {
                conn.Open();
                string query = "UPDATE usuarios SET Correo=@correo, Contrasena=@contrasena WHERE Nombre=@nombre AND Rol='Secretaria'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@correo", nuevoCorreo);
                cmd.Parameters.AddWithValue("@contrasena", nuevaContrasena);
                cmd.Parameters.AddWithValue("@nombre", nombreSeleccionado);
                cmd.ExecuteNonQuery();

                MessageBox.Show(" Datos actualizados correctamente.");
                MostrarSecretarias();
            }
        }
    }
}
