using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Boletas
{
    internal class Conexion
    {
        private string conexionString = "server=localhost;port=3307;user=root;password=;database=boletasescolares;";

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(conexionString);
        }

    }

    public static class BitacoraManager
    {
        // 🚨 Propiedad para almacenar el ID del usuario logueado después del login.
        public static int UsuarioIDActual { get; set; } = 0;

        /// <summary>
        /// Registra una acción en la tabla 'bitacora'.
        /// </summary>
        /// <param name="descripcionAccion">Detalle de la actividad realizada.</param>
        public static void RegistrarAccion(string descripcionAccion)
        {
            // Verificación de seguridad: No registramos si no hay un usuario válido.
            if (UsuarioIDActual <= 0)
            {
                // Solo registramos una advertencia interna si el ID no está seteado
                Console.WriteLine("ADVERTENCIA: Fallo al registrar en Bitácora. UsuarioID no establecido.");
                return;
            }

            try
            {
                // 1. Instanciamos tu clase Conexion
                Conexion conexion = new Conexion();

                // 2. Usamos tu método GetConnection()
                using (MySqlConnection conn = conexion.GetConnection())
                {
                    string query = @"INSERT INTO bitacora 
                                     (UsuarioID, Accion, FechaAccion) 
                                     VALUES 
                                     (@UsuarioID, @Accion, NOW())";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UsuarioID", UsuarioIDActual);
                    cmd.Parameters.AddWithValue("@Accion", descripcionAccion);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // El registro de errores de la Bitácora NO debe detener la aplicación.
                MessageBox.Show("Error al registrar la acción en la Bitácora: " + ex.Message, "Error Interno", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
