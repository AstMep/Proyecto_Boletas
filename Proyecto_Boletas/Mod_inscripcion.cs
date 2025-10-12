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
        private List<string> todosLosGrupos = new List<string>();
        private string rolUsuario;
        public Mod_inscripcion(string rol)
        {
            InitializeComponent();

            txtCurp.MaxLength = 18;
            nombre_alumno.MaxLength = 30;
            apellidoP_alumno.MaxLength = 30;
            apellidoM_alumno.MaxLength = 30;
            nombre_tutor.MaxLength = 30;
            apellidoP_tutor.MaxLength = 30;
            apellidoM_tutor.MaxLength = 30;
            telefono_tutor.MaxLength = 10;
            telefono_tutor.KeyPress += telefono_tutor_KeyPress;
            correo_tutor.MaxLength = 50;
            txtCurp.CharacterCasing = CharacterCasing.Upper;
            nombre_alumno.CharacterCasing = CharacterCasing.Upper;
            apellidoP_alumno.CharacterCasing = CharacterCasing.Upper;
            apellidoM_alumno.CharacterCasing = CharacterCasing.Upper;
            nombre_tutor.CharacterCasing = CharacterCasing.Upper;
            apellidoP_tutor.CharacterCasing = CharacterCasing.Upper;
            apellidoM_tutor.CharacterCasing = CharacterCasing.Upper;

            this.

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
            for (int i = 6; i <= 14; i++)
                edad_alumno.Items.Add(i);

            // Grupos
            grupo_alumno.Items.Clear();
            // Ya NO es necesario usar todosLosGrupos si no se va a filtrar,
            // simplemente llenaremos el ComboBox como estaba originalmente.
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
                            grupo_alumno.Items.Add(reader.GetString("nombre_grupo")); // ⭐ Se llena el ComboBox
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
        private void telefono_tutor_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo números y teclas de control (como Backspace)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Bloquea el carácter no permitido
            }
        }

        private void AsignarGradoPorEdad(int edad)
        {
            string grado = "";

            // 1. Determinar el grado según la edad
            if (edad == 6)
                grado = "primero";
            else if (edad == 7)
                grado = "segundo";
            else if (edad == 8)
                grado = "tercero";
            else if (edad == 9)
                grado = "cuarto";
            else if (edad >= 10 && edad <= 12)
                grado = "quinto";
            else if (edad >= 13 && edad <= 14)
                grado = "sexto";
            else
            {
                grupo_alumno.SelectedIndex = -1;
                MessageBox.Show("No hay un grado asignado para esta edad.", "Advertencia",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Buscar el grupo que coincida con el grado y seleccionarlo
            for (int i = 0; i < grupo_alumno.Items.Count; i++)
            {
                string nombreGrupo = grupo_alumno.Items[i].ToString().ToLower();

                // La búsqueda se basa en que el nombre del grupo contenga la palabra clave del grado.
                if (nombreGrupo.Contains(grado))
                {
                    grupo_alumno.SelectedIndex = i;
                    // Al cambiar el SelectedIndex, se dispara grupo_alumno_SelectedIndexChanged,
                    // que obtiene el ID y muestra las materias (la lógica de la BD).
                    return;
                }
            }

            // 3. Mensaje si no se encuentra el grupo
            grupo_alumno.SelectedIndex = -1;
            MessageBox.Show($"No se encontró un grupo disponible para el grado '{grado}'.",
                "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void nombre_alumno_TextChanged(object sender, EventArgs e)
        {

        }




        private bool ValidarAlumno(string nombres, string apellidoP, string apellidoM, int edad, DateTime fechaNac, string curp, out string mensajeError)
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



            // ⭐ VALIDAR COHERENCIA: Edad, Fecha de Nacimiento y CURP
            if (!ValidarCoherenciaEdadFechaCURP(edad, fechaNac, curp, out mensajeError))
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

        private string ObtenerGeneroDesdeCURP(string curp)
        {
            if (curp.Length != 18)
                return null;

            char generoChar = curp[10]; // Posición 11 (índice 10)
            if (generoChar == 'H')
                return "Masculino";
            else if (generoChar == 'M')
                return "Femenino";
            else
                return null;
        }


        private int CalcularEdad(DateTime fechaNacimiento)
        {
            int edad = DateTime.Now.Year - fechaNacimiento.Year;
            if (DateTime.Now < fechaNacimiento.AddYears(edad)) edad--;
            return edad;
        }

        private bool ValidarCoherenciaEdadFechaCURP(int edad, DateTime fechaNac, string curp, out string mensajeError)
        {
            mensajeError = "";

            // 1. Validar que la edad coincida con la fecha de nacimiento
            int edadSegunFecha = CalcularEdad(fechaNac);
            if (edad != edadSegunFecha)
            {
                mensajeError = $"❌ INCOHERENCIA DETECTADA:\n\n" +
                              $"• Edad seleccionada: {edad} años\n" +
                              $"• Edad según fecha de nacimiento: {edadSegunFecha} años\n\n" +
                              $"La edad seleccionada debe coincidir con la fecha de nacimiento.";
                return false;
            }

            // 2. Validar CURP
            if (!ValidarCURPyEdad(curp, edad, out string msgCURP))
            {
                mensajeError = msgCURP;
                return false;
            }

            // 3. Validar que la fecha de la CURP coincida con la fecha de nacimiento
            DateTime? fechaCURP = ObtenerFechaDesCURP(curp);
            if (fechaCURP != null)
            {
                if (fechaCURP.Value.Date != fechaNac.Date)
                {
                    mensajeError = $"❌ INCOHERENCIA DETECTADA:\n\n" +
                                  $"• Fecha de nacimiento ingresada: {fechaNac.ToString("dd/MM/yyyy")}\n" +
                                  $"• Fecha según CURP: {fechaCURP.Value.ToString("dd/MM/yyyy")}\n\n" +
                                  $"Las fechas deben coincidir exactamente.";
                    return false;
                }
            }

            return true;
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

        private bool EsCadenaValida(string texto)
        {
            // Quitar espacios y pasar a mayúsculas
            texto = texto.Trim().ToUpper();

            // Verificar que solo tenga letras
            if (!texto.All(char.IsLetter))
                return false;

            // Si la longitud es menor a 2, no es válida
            if (texto.Length < 2)
                return false;

            // Verificar cuántos caracteres distintos hay
            int distintos = texto.Distinct().Count();

            // Debe haber al menos 2 diferentes (ejemplo: "Ana" = 2 letras distintas)
            return distintos >= 2;
        }


        private bool ValidarApellidosCoinciden(string apellidoPAlumno, string apellidoMAlumno,
                                       string apellidoPTutor, string apellidoMTutor,
                                       out string mensajeError)
        {
            mensajeError = "";

            // Convertir a minúsculas para comparación
            string apPAlumno = apellidoPAlumno.ToLower().Trim();
            string apMAlumno = apellidoMAlumno.ToLower().Trim();
            string apPTutor = apellidoPTutor.ToLower().Trim();
            string apMTutor = apellidoMTutor.ToLower().Trim();

            // Verificar si al menos un apellido del tutor coincide con algún apellido del alumno
            bool coincide = (apPAlumno == apPTutor) || (apPAlumno == apMTutor) ||
                            (apMAlumno == apPTutor) || (apMAlumno == apMTutor);

            if (!coincide)
            {
                mensajeError = "Al menos uno de los apellidos del tutor debe coincidir con los apellidos del alumno.\n\n" +
                              $"Apellidos del alumno: {apellidoPAlumno} {apellidoMAlumno}\n" +
                              $"Apellidos del tutor: {apellidoPTutor} {apellidoMTutor}";
                return false;
            }

            return true;
        }

        private void MostrarMateriasDeGrupo(int idGrupo)
        {
            try
            {
                Conexion conexion = new Conexion();
                using (MySqlConnection conn = conexion.GetConnection())
                {
                    conn.Open();

                    string query = @"SELECT Nombre AS 'Materia', Campo_formativo AS 'Campo formativo'
                             FROM materias
                             WHERE id_grupo = @idGrupo";

                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@idGrupo", idGrupo);

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvMateriasAlumno.DataSource = dt;

                    // Opcional: hacer que sea solo lectura
                    dgvMateriasAlumno.ReadOnly = true;
                    dgvMateriasAlumno.AllowUserToAddRows = false;
                    dgvMateriasAlumno.AllowUserToDeleteRows = false;
                    dgvMateriasAlumno.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar materias: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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



        private bool EsNombreValido(string nombre)
        {
            nombre = nombre.Trim();

            // Debe contener al menos una palabra
            string[] palabras = nombre.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (palabras.Length < 1)
                return false;

            // Cada palabra debe tener al menos 2 letras distintas y solo letras
            foreach (var palabra in palabras)
            {
                if (!palabra.All(char.IsLetter)) return false;       // Solo letras
                if (palabra.Length < 2) return false;                // Mínimo 2 caracteres
                if (palabra.Distinct().Count() < 2) return false;    // Al menos 2 letras distintas
            }

            return true;
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

        }

        private void combosgenero_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void dgvMateriasAlumno_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Mod_inscripcion_Load(object sender, EventArgs e)
        {
            // Ajustes visuales del DataGridView
            dgvMateriasAlumno.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMateriasAlumno.ReadOnly = true;
            dgvMateriasAlumno.AllowUserToAddRows = false;
            dgvMateriasAlumno.AllowUserToDeleteRows = false;
            dgvMateriasAlumno.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMateriasAlumno.Font = new Font("Segoe UI", 10); // Cambia tamaño de letra
            dgvMateriasAlumno.RowTemplate.Height = 30; // Aumenta altura de las filas

        }

        private void btn_inscripcion_Click(object sender, EventArgs e)
        {
            Mod_inscripcion nuevoFormulario = new Mod_inscripcion("Director"); // creas una instancia del otro form
            nuevoFormulario.Show();              // lo muestras
            this.Hide();
        }

        private void btn_capturaCalif_Click(object sender, EventArgs e)
        {

        }

        private void btnAdmSecre_Click(object sender, EventArgs e)
        {
            adm_Secretaria nuevoFormulario = new adm_Secretaria(); // creas una instancia del otro form
            nuevoFormulario.Show();              // lo muestras
            this.Hide();
        }

        private void btnBitacora_Click(object sender, EventArgs e)
        {
            Bitacora nuevoFormulario = new Bitacora();
            nuevoFormulario.Show();
            this.Hide();
        }

        private void btnEdicionDatos_Click(object sender, EventArgs e)
        {

        }

        private void btnCreacionBoletas_Click(object sender, EventArgs e)
        {
            CreacionPDF_Direc nuevoFormulario = new CreacionPDF_Direc();
            nuevoFormulario.Show();
            this.Hide();
        }

        private void btn_admaestros_Click(object sender, EventArgs e)
        {
            adm_maestros nuevoForulario = new adm_maestros(); // creas una instancia del otro form
            nuevoForulario.Show();              // lo muestras
            this.Hide();
        }

        private void nombre_alumno_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void btnalta_inscripcion2_Click(object sender, EventArgs e)
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
                DateTime fechaNac = nacimiento_alumno.Value.Date;

                string nombreTutor = Regex.Replace(nombre_tutor.Text.Trim(), @"\s+", " ");
                string apellidoPTutor = Regex.Replace(apellidoP_tutor.Text.Trim(), @"\s+", " ");
                string apellidoMTutor = Regex.Replace(apellidoM_tutor.Text.Trim(), @"\s+", " ");
                string telefonoTutor = Regex.Replace(telefono_tutor.Text.Trim(), @"\s+", "");
                string correoTutor = Regex.Replace(correo_tutor.Text.Trim().ToLower(), @"\s+", "");

                // ====== NUEVA VALIDACIÓN: EVITAR NOMBRES SIN SENTIDO ======


                if (!EsCadenaValida(apellidoPAlumno))
                {
                    MessageBox.Show("El apellido paterno del alumno no es válido. Debe contener al menos dos letras diferentes y solo caracteres alfabéticos.",
                        "Error en apellido paterno", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    apellidoP_alumno.Focus();
                    return;
                }

                if (!EsCadenaValida(apellidoMAlumno))
                {
                    MessageBox.Show("El apellido materno del alumno no es válido. Debe contener al menos dos letras diferentes y solo caracteres alfabéticos.",
                        "Error en apellido materno", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    apellidoM_alumno.Focus();
                    return;
                }

                if (!EsNombreValido(nombreAlumno))
                {
                    MessageBox.Show("El nombre del alumno no es válido. Debe contener al menos dos palabras, cada una con al menos dos letras distintas y solo caracteres alfabéticos.",
                        "Error en nombre del alumno", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    nombre_alumno.Focus();
                    return;
                }

                if (!EsNombreValido(nombreTutor))
                {
                    MessageBox.Show("El nombre del tutor no es válido. Debe contener al menos dos palabras, cada una con al menos dos letras distintas y solo caracteres alfabéticos.",
                        "Error en nombre del tutor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    nombre_tutor.Focus();
                    return;
                }


                if (!ValidarAlumno(nombreAlumno, apellidoPAlumno, apellidoMAlumno, edad, fechaNac, curp, out string msgAlumno))
                {
                    MessageBox.Show(msgAlumno, "Error - Datos del Alumno", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ========== ⭐ NUEVA VALIDACIÓN: APELLIDOS COINCIDAN ==========
                if (!ValidarApellidosCoinciden(apellidoPAlumno, apellidoMAlumno,
                                               apellidoPTutor, apellidoMTutor,
                                               out string msgApellidos))
                {
                    MessageBox.Show(msgApellidos, "Error - Apellidos no Coinciden",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void txtCurp_TextChanged_1(object sender, EventArgs e)
        {
            string curp = txtCurp.Text.Trim().ToUpper();

            // Solo procesar si tiene los 18 caracteres válidos
            if (curp.Length == 18)
            {
                // Obtener la fecha desde la CURP
                DateTime? fechaCURP = ObtenerFechaDesCURP(curp);

                if (fechaCURP != null)
                {
                    nacimiento_alumno.Value = fechaCURP.Value;

                    int edadCalculada = CalcularEdad(fechaCURP.Value);
                    if (edadCalculada >= 6 && edadCalculada <= 14)
                        edad_alumno.SelectedItem = edadCalculada;
                }

                // 🔹 Obtener género desde la CURP
                string generoCURP = ObtenerGeneroDesdeCURP(curp);

                if (generoCURP != null)
                {
                    // Si no hay selección, asigna automáticamente
                    if (combosgenero.SelectedIndex == -1)
                    {
                        combosgenero.SelectedItem = generoCURP;
                    }
                    else
                    {
                        // Si ya hay un valor seleccionado, verificar coincidencia
                        string generoSeleccionado = combosgenero.SelectedItem.ToString();
                        if (generoSeleccionado != generoCURP)
                        {
                            MessageBox.Show(
                                $"⚠ El género seleccionado ({generoSeleccionado}) no coincide con el género indicado en la CURP ({generoCURP}).",
                                "Inconsistencia de género",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );

                            // Opcional: corregir automáticamente
                            combosgenero.SelectedItem = generoCURP;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("La CURP ingresada tiene un formato incorrecto para determinar el género.",
                        "Error en CURP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void edad_alumno_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (edad_alumno.SelectedItem != null)
            {
                int edadSeleccionada = Convert.ToInt32(edad_alumno.SelectedItem);
                AsignarGradoPorEdad(edadSeleccionada);

                if (nacimiento_alumno.Value.Date == DateTime.Now.Date)
                {
                    DateTime fechaAproximada = DateTime.Now.AddYears(-edadSeleccionada);
                    nacimiento_alumno.Value = fechaAproximada;
                }
            }
        }

        private void nacimiento_alumno_ValueChanged_1(object sender, EventArgs e)
        {
            // Calcular edad automáticamente
            DateTime fechaNac = nacimiento_alumno.Value;
            int edadCalculada = CalcularEdad(fechaNac);

            // Seleccionar la edad en el ComboBox si está en el rango válido
            if (edadCalculada >= 5 && edadCalculada <= 18)
            {
                edad_alumno.SelectedItem = edadCalculada;
            }
            else
            {
                edad_alumno.SelectedIndex = -1;
                MessageBox.Show($"La edad calculada ({edadCalculada} años) está fuera del rango permitido (5-18 años).",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void grupo_alumno_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (grupo_alumno.SelectedItem == null)
                return;

            try
            {
                string nombreGrupo = grupo_alumno.SelectedItem.ToString();

                Conexion conexion = new Conexion();
                using (MySqlConnection conn = conexion.GetConnection())
                {
                    conn.Open();

                    // 1️⃣ Obtener el ID del grupo seleccionado
                    string queryId = "SELECT id_grupo FROM grupo WHERE nombre_grupo = @nombre";
                    MySqlCommand cmdId = new MySqlCommand(queryId, conn);
                    cmdId.Parameters.AddWithValue("@nombre", nombreGrupo);

                    object result = cmdId.ExecuteScalar();

                    if (result != null)
                    {
                        int idGrupo = Convert.ToInt32(result);

                        // 2️⃣ Mostrar las materias existentes de ese grupo
                        MostrarMateriasDeGrupo(idGrupo);
                    }
                    else
                    {
                        MessageBox.Show("No se encontró el grupo seleccionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar materias: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtCurp_Leave(object sender, EventArgs e)
        {
            
        }
    }
}
