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
    public partial class Mod_inscripcion : Form
    {
        private string rolUsuario;
        public Mod_inscripcion(string rol)
        {
            InitializeComponent();
            rolUsuario = rol;
            CargarCombos();
        }


        private void btnVolver_Click(object sender, EventArgs e)
        {

            if (rolUsuario == "Secretaria")
            {
                Form_Secretaria formSecretaria = new Form_Secretaria();
                formSecretaria.Show();
                this.Hide();
            }
            else if (rolUsuario == "Director")
            {
                Menu_principal formDirector = new Menu_principal();
                formDirector.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Rol no reconocido.");
            }



        }

        private void CargarCombos()
        {
            // Cargar edades (por ejemplo de 5 a 18 años)
            edad_alumno.Items.Clear();
            for (int i = 5; i <= 18; i++)
            {
                edad_alumno.Items.Add(i);
            }

            // Cargar grupos
            grupo_alumno.Items.Clear();
            grupo_alumno.Items.AddRange(new object[] {
                "1°A", "1°B", 
                "2°A", "2°B", 
                "3°A", "3°B"
            });
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void nombre_alumno_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnvalidar_alumno_Click(object sender, EventArgs e)
        {
            // Obtener y limpiar datos del alumno
            string nombres = nombre_alumno.Text.Trim();
            string apellidoP = apellidoP_alumno.Text.Trim();
            string apellidoM = apellidoM_alumno.Text.Trim();
            string curp = txtCurp.Text.Trim().ToUpper();



            // Eliminar espacios dobles
            nombres = Regex.Replace(nombres, @"\s+", " ");
            apellidoP = Regex.Replace(apellidoP, @"\s+", " ");
            apellidoM = Regex.Replace(apellidoM, @"\s+", " ");
            curp = Regex.Replace(curp, @"\s+", "");  // ← Limpiar curp


            // ========== VALIDACIONES DEL ALUMNO ==========
            if (string.IsNullOrWhiteSpace(nombres) || string.IsNullOrWhiteSpace(apellidoP) ||
                string.IsNullOrWhiteSpace(apellidoM))
            {
                MessageBox.Show("Por favor, completa todos los campos del alumno.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (edad_alumno.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, selecciona la edad del alumno.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (grupo_alumno.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, selecciona el grupo del alumno.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

           

            // Validar nombres y apellidos del alumno (solo letras y espacios, mínimo 2 letras)
            if (!Regex.IsMatch(nombres, @"^[A-Za-zÁÉÍÓÚáéíóúÑñ ]{2,}$") ||
                !Regex.IsMatch(apellidoP, @"^[A-Za-zÁÉÍÓÚáéíóúÑñ ]{2,}$") ||
                !Regex.IsMatch(apellidoM, @"^[A-Za-zÁÉÍÓÚáéíóúÑñ ]{2,}$"))
            {
                MessageBox.Show("Los nombres y apellidos del alumno deben tener al menos 2 letras y solo pueden contener letras y espacios.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar longitud del alumno
            if (nombres.Length > 100 || apellidoP.Length > 100 || apellidoM.Length > 100)
            {
                MessageBox.Show("Los nombres y apellidos del alumno no pueden tener más de 100 caracteres.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            // Preparar datos
            string nombreCompletoAlumno = $"{nombres.ToLower()} {apellidoP.ToLower()} {apellidoM.ToLower()}";
            
            int edad = Convert.ToInt32(edad_alumno.SelectedItem);
            string grupo = grupo_alumno.SelectedItem.ToString();
            string mensajeError;

            // Verificar duplicados del alumno
            if (AlumnoExistente(nombreCompletoAlumno))
            {
                MessageBox.Show("Ya existe un alumno con ese nombre completo.", "Duplicado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidarCURPyEdad(curp, edad, out mensajeError))
            {
                MessageBox.Show(mensajeError, "Error en CURP",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // ← Si la CURP no es válida, SE DETIENE AQUÍ
            }



        }


        

        // Método para validar formato de CURP (18 caracteres)
        private bool ValidarFormatoCURP(string curp)
        {
            // CURP debe tener exactamente 18 caracteres
            if (curp.Length != 18)
                return false;

            // Patrón de CURP: 4 letras, 6 dígitos (fecha), 1 letra (sexo), 2 letras (estado), 3 consonantes, 2 dígitos
            string patron = @"^[A-Z]{4}\d{6}[HM][A-Z]{2}[A-Z]{3}[A-Z0-9]\d$";
            return Regex.IsMatch(curp.ToUpper(), patron);
        }

        // Método para extraer fecha de nacimiento de la CURP
        private DateTime? ObtenerFechaDesCURP(string curp)
        {
            try
            {
                // La fecha está en las posiciones 4-9 (AAMMDD)
                string anio = curp.Substring(4, 2);   // Posición 4-5: Año
                string mes = curp.Substring(6, 2);    // Posición 6-7: Mes
                string dia = curp.Substring(8, 2);    // Posición 8-9: Día

                // Determinar el siglo (1900 o 2000)
                int anioCompleto;
                int anioNum = int.Parse(anio);

                // Si el año es mayor a 24, es del siglo 1900, sino es del 2000
                if (anioNum <= 24) // Ajusta este valor según el año actual
                    anioCompleto = 2000 + anioNum;
                else
                    anioCompleto = 1900 + anioNum;

                DateTime fechaNacimiento = new DateTime(anioCompleto, int.Parse(mes), int.Parse(dia));
                return fechaNacimiento;
            }
            catch
            {
                return null;
            }
        }

        // Método para calcular edad desde una fecha
        private int CalcularEdad(DateTime fechaNacimiento)
        {
            int edad = DateTime.Now.Year - fechaNacimiento.Year;

            // Ajustar si aún no ha cumplido años este año
            if (DateTime.Now < fechaNacimiento.AddYears(edad))
                edad--;

            return edad;
        }

        // Método para validar CURP completa (formato + coherencia con edad)
        private bool ValidarCURPyEdad(string curp, int edadSeleccionada, out string mensajeError)
        {
            mensajeError = "";
            curp = curp.ToUpper().Trim();

            // 1. Validar que tenga exactamente 18 caracteres
            if (curp.Length != 18)
            {
                mensajeError = "La CURP debe tener exactamente 18 caracteres.";
                return false;
            }

            // 2. Validar formato de CURP
            if (!ValidarFormatoCURP(curp))
            {
                mensajeError = "El formato de la CURP no es válido.";
                return false;
            }

            // 3. Extraer fecha de nacimiento de la CURP
            DateTime? fechaCURP = ObtenerFechaDesCURP(curp);

            if (fechaCURP == null)
            {
                mensajeError = "La fecha en la CURP no es válida.";
                return false;
            }

            // 4. Calcular edad según la CURP
            int edadSegunCURP = CalcularEdad(fechaCURP.Value);

            // 5. Verificar que la edad seleccionada coincida con la CURP
            if (edadSegunCURP != edadSeleccionada)
            {
                mensajeError = $"La edad seleccionada ({edadSeleccionada} años) no coincide con la edad según la CURP ({edadSegunCURP} años).";
                return false;
            }

            return true;
        }



        // Método para verificar si el alumno ya existe
        private bool AlumnoExistente(string nombreCompleto)
        {
            try
            {
                Conexion conexion = new Conexion();
                using (MySqlConnection conn = conexion.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM alumnos WHERE " +
                                   "LOWER(CONCAT(Nombres, ' ', ApellidoPaterno, ' ', ApellidoMaterno)) = @nombreCompleto";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nombreCompleto", nombreCompleto);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al verificar alumno: " + ex.Message);
                return false;
            }
        }

        // Método para verificar si el correo del tutor ya existe
        private bool TutorCorreoExistente(string correo)
        {
            try
            {
                Conexion conexion = new Conexion();
                using (MySqlConnection conn = conexion.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM tutores WHERE LOWER(Correo) = @correo";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@correo", correo);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al verificar correo: " + ex.Message);
                return false;
            }
        }

        private void apellidoP_alumno_TextChanged(object sender, EventArgs e)
        {

        }

        private void apellidoM_alumno_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCurp_TextChanged(object sender, EventArgs e)
        {

        }

        private void nacimiento_alumno_ValueChanged(object sender, EventArgs e)
        {

        }

        private void edad_alumno_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void grupo_alumno_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void nombre_tutor_TextChanged(object sender, EventArgs e)
        {

        }

        private void apellidoP_tutor_TextChanged(object sender, EventArgs e)
        {

        }

        private void apellidoM_tutor_TextChanged(object sender, EventArgs e)
        {

        }

        private void telefono_tutor_TextChanged(object sender, EventArgs e)
        {

        }

        private void correo_tutor_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnvalidar_tutor_Click(object sender, EventArgs e)
        {
            // Obtener y limpiar datos del tutor
            string tutorNombre = nombre_tutor.Text.Trim();
            string tutorApellidoP = apellidoP_tutor.Text.Trim();
            string tutorApellidoM = apellidoM_tutor.Text.Trim();
            string tutorTelefono = telefono_tutor.Text.Trim();
            string tutorCorreo = correo_tutor.Text.Trim();

            tutorNombre = Regex.Replace(tutorNombre, @"\s+", " ");
            tutorApellidoP = Regex.Replace(tutorApellidoP, @"\s+", " ");
            tutorApellidoM = Regex.Replace(tutorApellidoM, @"\s+", " ");
            tutorTelefono = Regex.Replace(tutorTelefono, @"\s+", "");
            tutorCorreo = Regex.Replace(tutorCorreo, @"\s+", "");

            // ========== VALIDACIONES DEL TUTOR ==========
            if (string.IsNullOrWhiteSpace(tutorNombre) || string.IsNullOrWhiteSpace(tutorApellidoP) ||
                string.IsNullOrWhiteSpace(tutorApellidoM) || string.IsNullOrWhiteSpace(tutorTelefono) ||
                string.IsNullOrWhiteSpace(tutorCorreo))
            {
                MessageBox.Show("Por favor, completa todos los campos del tutor.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar nombres y apellidos del tutor
            if (!Regex.IsMatch(tutorNombre, @"^[A-Za-zÁÉÍÓÚáéíóúÑñ ]{2,}$") ||
                !Regex.IsMatch(tutorApellidoP, @"^[A-Za-zÁÉÍÓÚáéíóúÑñ ]{2,}$") ||
                !Regex.IsMatch(tutorApellidoM, @"^[A-Za-zÁÉÍÓÚáéíóúÑñ ]{2,}$"))
            {
                MessageBox.Show("Los nombres y apellidos del tutor deben tener al menos 2 letras y solo pueden contener letras y espacios.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar longitud del tutor
            if (tutorNombre.Length > 100 || tutorApellidoP.Length > 100 || tutorApellidoM.Length > 100)
            {
                MessageBox.Show("Los nombres y apellidos del tutor no pueden tener más de 100 caracteres.",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar teléfono (10 dígitos)
            if (!Regex.IsMatch(tutorTelefono, @"^\d{10}$"))
            {
                MessageBox.Show("El teléfono debe tener exactamente 10 dígitos.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar correo
            if (!Regex.IsMatch(tutorCorreo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Ingresa un correo electrónico válido (ejemplo: nombre@dominio.com).",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (tutorCorreo.Length > 100)
            {
                MessageBox.Show("El correo no puede tener más de 100 caracteres.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombreCompletoTutor = $"{tutorNombre.ToLower()} {tutorApellidoP.ToLower()} {tutorApellidoM.ToLower()}";

            // Verificar duplicados del tutor
            if (TutorCorreoExistente(tutorCorreo.ToLower()))
            {
                MessageBox.Show("El correo del tutor ya está registrado. Usa otro.", "Duplicado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        }

        private void btnalta_inscripcion_Click(object sender, EventArgs e)
        {

        }
    }
}
