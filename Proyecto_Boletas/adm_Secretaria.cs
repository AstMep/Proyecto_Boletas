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
            string nombre = txtUsuarioSecre.Text;
            string correo = txtCorreoSecre.Text;
            string contrasena = txtContrasenaSecre.Text;
            string rol = "Secretaria";
            DateTime fechaRegistro = DateTime.Now;

            if (nombre == "" || correo == "" || contrasena == "")
            {
                MessageBox.Show("Por favor, completa todos los campos.");
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
                    MessageBox.Show("✅ Secretaria registrada correctamente");

                    txtUsuarioSecre.Clear();
                    txtCorreoSecre.Clear();
                    txtContrasenaSecre.Clear();

                    MostrarSecretarias();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar secretaria: " + ex.Message);
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

        private void adm_Secretaria_Load(object sender, EventArgs e)
        {
            MostrarSecretarias();
        }


    }
}
