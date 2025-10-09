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
            // Edad
            edad_alumno.Items.Clear();
            for (int i = 5; i <= 18; i++)
                edad_alumno.Items.Add(i);

            // Grupos
            grupo_alumno.Items.Clear();
            try
            {
                Conexion conexion = new Conexion();
                using (MySqlConnection conn = conexion.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT nombre_grupo FROM grupo ORDER BY nombre_grupo ASC";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                            grupo_alumno.Items.Add(reader.GetString("nombre_grupo"));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar grupos: " + ex.Message);
            }

            // Género
            combosgenero.Items.Clear();
            combosgenero.Items.Add("Masculino");
            combosgenero.Items.Add("Femenino");
            combosgenero.SelectedIndex = -1;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void nombre_alumno_TextChanged(object sender, EventArgs e)
        {

        }




        private bool ValidarAlumno(string nombres, string apellidoP, string apellidoM, int edad, string curp, out string mensajeError)
        {
            mensajeError = "";

            // Campos vacíos
            if (string.IsNullOrWhiteSpace(nombres) || string.IsNullOrWhiteSpace(apellidoP) || string.IsNullOrWhiteSpace(apellidoM))
            {
                mensajeError = "Completa todos los campos del alumno.";
                return false;
            }

            // Letras y espacios
            if (!Regex.IsMatch(nombres, @"^[A-Za-zÁÉÍÓÚáéíóúÑñ ]{2,}$") ||
                !Regex.IsMatch(apellidoP, @"^[A-Za-zÁÉÍÓÚáéíóúÑñ ]{2,}$") ||
                !Regex.IsMatch(apellidoM, @"^[A-Za-zÁÉÍÓÚáéíóúÑñ ]{2,}$"))
            {
                mensajeError = "Los nombres y apellidos solo pueden contener letras y espacios (mínimo 2 letras).";
                return false;
            }

            // Longitud
            if (nombres.Length > 100 || apellidoP.Length > 100 || apellidoM.Length > 100)
            {
                mensajeError = "Los nombres y apellidos no pueden tener más de 100 caracteres.";
                return false;
            }

            // CURP
            if (!ValidarCURPyEdad(curp, edad, out mensajeError))
                return false;

            return true;
        }

        // -------- VALIDACIONES DE TUTOR ----------
        private bool ValidarTutor(string nombre, string apellidoP, string apellidoM, string telefono, string correo, out string mensajeError)
        {
            mensajeError = "";

            // Campos vacíos
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellidoP) ||
                string.IsNullOrWhiteSpace(apellidoM) || string.IsNullOrWhiteSpace(telefono) ||
                string.IsNullOrWhiteSpace(correo))
            {
                mensajeError = "Completa todos los campos del tutor.";
                return false;
            }

            // Letras y espacios
            if (!Regex.IsMatch(nombre, @"^[A-Za-zÁÉÍÓÚáéíóúÑñ ]{2,}$") ||
                !Regex.IsMatch(apellidoP, @"^[A-Za-zÁÉÍÓÚáéíóúÑñ ]{2,}$") ||
                !Regex.IsMatch(apellidoM, @"^[A-Za-zÁÉÍÓÚáéíóúÑñ ]{2,}$"))
            {
                mensajeError = "Los nombres y apellidos del tutor solo pueden contener letras y espacios (mínimo 2 letras).";
                return false;
            }

            // Teléfono
            if (!Regex.IsMatch(telefono, @"^\d{10}$"))
            {
                mensajeError = "El teléfono debe tener 10 dígitos.";
                return false;
            }

            // Correo
            if (!Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                mensajeError = "Ingresa un correo electrónico válido.";
                return false;
            }

            if (nombre.Length > 100 || apellidoP.Length > 100 || apellidoM.Length > 100 || correo.Length > 100)
            {
                mensajeError = "Los campos no pueden superar los 100 caracteres.";
                return false;
            }

            return true;
        }

        // -------- VALIDACIÓN CURP ----------
        private bool ValidarFormatoCURP(string curp)
        {
            if (curp.Length != 18) return false;
            string patron = @"^[A-Z]{4}\d{6}[HM][A-Z]{2}[A-Z]{3}[A-Z0-9]\d$";
            return Regex.IsMatch(curp.ToUpper(), patron);
        }

        private DateTime? ObtenerFechaDesCURP(string curp)
        {
            try
            {
                string anio = curp.Substring(4, 2);
                string mes = curp.Substring(6, 2);
                string dia = curp.Substring(8, 2);

                int anioNum = int.Parse(anio);
                int anioCompleto = (anioNum <= 24) ? 2000 + anioNum : 1900 + anioNum;

                return new DateTime(anioCompleto, int.Parse(mes), int.Parse(dia));
            }
            catch { return null; }
        }

        private int CalcularEdad(DateTime fechaNacimiento)
        {
            int edad = DateTime.Now.Year - fechaNacimiento.Year;
            if (DateTime.Now < fechaNacimiento.AddYears(edad)) edad--;
            return edad;
        }

        private bool ValidarCURPyEdad(string curp, int edadSeleccionada, out string mensajeError)
        {
            mensajeError = "";
            curp = curp.ToUpper().Trim();

            if (curp.Length != 18)
            {
                mensajeError = "La CURP debe tener 18 caracteres.";
                return false;
            }

            if (!ValidarFormatoCURP(curp))
            {
                mensajeError = "Formato de CURP no válido.";
                return false;
            }

            DateTime? fecha = ObtenerFechaDesCURP(curp);
            if (fecha == null)
            {
                mensajeError = "Fecha en CURP no válida.";
                return false;
            }

            if (CalcularEdad(fecha.Value) != edadSeleccionada)
            {
                mensajeError = $"La edad seleccionada ({edadSeleccionada}) no coincide con la CURP ({CalcularEdad(fecha.Value)}).";
                return false;
            }

            return true;
        }

        private bool AlumnoExistente(string nombreCompleto)
        {
            try
            {
                Conexion conexion = new Conexion();
                using (MySqlConnection conn = conexion.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM alumnos WHERE LOWER(CONCAT(Nombre,' ',ApellidoPaterno,' ',ApellidoMaterno))=@nombre";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nombre", nombreCompleto);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
            catch { return false; }
        }

        private bool AlumnoCURPExistente(string curp)
        {
            try
            {
                Conexion conexion = new Conexion();
                using (MySqlConnection conn = conexion.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM alumnos WHERE CURP=@curp";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@curp", curp);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
            catch { return false; }
        }

        private bool TutorCorreoExistente(string correo)
        {
            try
            {
                Conexion conexion = new Conexion();
                using (MySqlConnection conn = conexion.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM tutores WHERE LOWER(Correo)=@correo";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@correo", correo);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
            catch { return false; }
        }

        private bool GrupoConCupo(string nombreGrupo, MySqlConnection conn)
        {
            string query = "SELECT COUNT(*) FROM alumnos WHERE id_grupo=(SELECT id_grupo FROM grupo WHERE nombre_grupo=@grupo)";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@grupo", nombreGrupo);
                return Convert.ToInt32(cmd.ExecuteScalar()) < 25;
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


            if (TutorCorreoExistente(tutorCorreo.ToLower()))
            {
                MessageBox.Show("El correo del tutor ya está registrado. Usa otro.", "Duplicado",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        }

        private void btnalta_inscripcion_Click(object sender, EventArgs e)
        {

            try
            {

                string nombreAlumno = Regex.Replace(nombre_alumno.Text.Trim(), @"\s+", " ");
                string apellidoPAlumno = Regex.Replace(apellidoP_alumno.Text.Trim(), @"\s+", " ");
                string apellidoMAlumno = Regex.Replace(apellidoM_alumno.Text.Trim(), @"\s+", " ");
                string curp = Regex.Replace(txtCurp.Text.Trim().ToUpper(), @"\s+", "");
                int edad = Convert.ToInt32(edad_alumno.SelectedItem);
                string generoSeleccionado = combosgenero.SelectedItem?.ToString();
                string grupoSeleccionado = grupo_alumno.SelectedItem?.ToString();
                DateTime fechaNac = nacimiento_alumno.Value;

                string nombreTutor = Regex.Replace(nombre_tutor.Text.Trim(), @"\s+", " ");
                string apellidoPTutor = Regex.Replace(apellidoP_tutor.Text.Trim(), @"\s+", " ");
                string apellidoMTutor = Regex.Replace(apellidoM_tutor.Text.Trim(), @"\s+", " ");
                string telefonoTutor = Regex.Replace(telefono_tutor.Text.Trim(), @"\s+", "");
                string correoTutor = Regex.Replace(correo_tutor.Text.Trim().ToLower(), @"\s+", "");

                if (!ValidarAlumno(nombreAlumno, apellidoPAlumno, apellidoMAlumno, edad, curp, out string msgAlumno))
                {
                    MessageBox.Show(msgAlumno, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidarTutor(nombreTutor, apellidoPTutor, apellidoMTutor, telefonoTutor, correoTutor, out string msgTutor))
                {
                    MessageBox.Show(msgTutor, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(grupoSeleccionado))
                {
                    MessageBox.Show("Selecciona un grupo para el alumno.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string nombreCompletoAlumno = $"{nombreAlumno.ToLower()} {apellidoPAlumno.ToLower()} {apellidoMAlumno.ToLower()}";

                if (AlumnoExistente(nombreCompletoAlumno))
                {
                    MessageBox.Show("Ya existe un alumno con ese nombre completo.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (AlumnoCURPExistente(curp))
                {
                    MessageBox.Show("La CURP ya está registrada.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (TutorCorreoExistente(correoTutor))
                {
                    MessageBox.Show("El correo del tutor ya está registrado.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                Conexion conexion = new Conexion();
                using (MySqlConnection conn = conexion.GetConnection())
                {
                    conn.Open();

                    // Validar cupo
                    if (!GrupoConCupo(grupoSeleccionado, conn))
                    {
                        MessageBox.Show("El grupo seleccionado ya está lleno (25 alumnos).", "Cupo lleno", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }


                    int tutorID;
                    string queryTutor = "SELECT TutorID FROM tutores WHERE LOWER(Correo)=@correo";
                    using (MySqlCommand cmd = new MySqlCommand(queryTutor, conn))
                    {
                        cmd.Parameters.AddWithValue("@correo", correoTutor);
                        object result = cmd.ExecuteScalar();
                        if (result != null) tutorID = Convert.ToInt32(result);
                        else
                        {
                            string insertTutor = @"INSERT INTO tutores (Nombre, ApellidoPaterno, ApellidoMaterno, Telefono, Correo)
                                                VALUES (@nombre, @apellidoP, @apellidoM, @telefono, @correo)";
                            using (MySqlCommand cmdInsert = new MySqlCommand(insertTutor, conn))
                            {
                                cmdInsert.Parameters.AddWithValue("@nombre", nombreTutor);
                                cmdInsert.Parameters.AddWithValue("@apellidoP", apellidoPTutor);
                                cmdInsert.Parameters.AddWithValue("@apellidoM", apellidoMTutor);
                                cmdInsert.Parameters.AddWithValue("@telefono", telefonoTutor);
                                cmdInsert.Parameters.AddWithValue("@correo", correoTutor);
                                cmdInsert.ExecuteNonQuery();
                                tutorID = (int)cmdInsert.LastInsertedId;
                            }
                        }
                    }


                    string insertAlumno = @"INSERT INTO alumnos
                        (id_grupo, Nombre, ApellidoPaterno, ApellidoMaterno, CURP, Edad, FechaNacimiento, TutorID, genero)
                        VALUES ((SELECT id_grupo FROM grupo WHERE nombre_grupo=@grupo),
                                @nombre, @apellidoP, @apellidoM, @curp, @edad, @fechaNac, @tutorID, @genero)";
                    using (MySqlCommand cmdAlumno = new MySqlCommand(insertAlumno, conn))
                    {
                        cmdAlumno.Parameters.AddWithValue("@grupo", grupoSeleccionado);
                        cmdAlumno.Parameters.AddWithValue("@nombre", nombreAlumno);
                        cmdAlumno.Parameters.AddWithValue("@apellidoP", apellidoPAlumno);
                        cmdAlumno.Parameters.AddWithValue("@apellidoM", apellidoMAlumno);
                        cmdAlumno.Parameters.AddWithValue("@curp", curp);
                        cmdAlumno.Parameters.AddWithValue("@edad", edad);
                        cmdAlumno.Parameters.AddWithValue("@fechaNac", fechaNac);
                        cmdAlumno.Parameters.AddWithValue("@tutorID", tutorID);
                        cmdAlumno.Parameters.AddWithValue("@genero", generoSeleccionado);
                        cmdAlumno.ExecuteNonQuery();
                    }

                    MessageBox.Show("Alumno registrado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpiar campos
                    nombre_alumno.Clear();
                    apellidoP_alumno.Clear();
                    apellidoM_alumno.Clear();
                    txtCurp.Clear();
                    edad_alumno.SelectedIndex = -1;
                    combosgenero.SelectedIndex = -1;
                    grupo_alumno.SelectedIndex = -1;

                    nombre_tutor.Clear();
                    apellidoP_tutor.Clear();
                    apellidoM_tutor.Clear();
                    telefono_tutor.Clear();
                    correo_tutor.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar el alumno: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void combosgenero_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
