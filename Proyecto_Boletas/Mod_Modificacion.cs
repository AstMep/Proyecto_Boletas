using MySql.Data.MySqlClient;
using System.Data;
using System.Text.RegularExpressions;
using static Proyecto_Boletas.Mod_capCal;

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
using static Proyecto_Boletas.Mod_capCal;

namespace Proyecto_Boletas

{

    public partial class Mod_Modificacion : Form
    {
        private string rolUsuario; // Variable para almacenar el rol
        private bool estaCargandoDatos = false;
        public Mod_Modificacion()
        {
            InitializeComponent();
            rolUsuario = "Director"; // Valor por defecto
            ConfigurarControles();
            CargarGrupos();
            OcultarBotonesPorRol();
        }

        // ⭐ Constructor CON parámetros (para usar en el código)
        public Mod_Modificacion(string rol)
        {
            InitializeComponent();
            rolUsuario = rol;
            ConfigurarControles();
            CargarGrupos();
            OcultarBotonesPorRol();
        }


        // ⭐ NUEVO: Método para ocultar botones según el rol
        private void OcultarBotonesPorRol()
        {
            if (rolUsuario == "Secretaria")
            {
                // Ocultar botones solo para secretaria
                btnAdmSecre.Visible = false;
                btnBitacora.Visible = false;
                btn_admaestros.Visible = false;
            }
            else if (rolUsuario == "Director")
            {
                // Mostrar todos los botones para director
                btnAdmSecre.Visible = true;
                btnBitacora.Visible = true;
                btn_admaestros.Visible = true;
            }
        }

        // ⭐ CONFIGURACIÓN INICIAL DE CONTROLES
        private void ConfigurarControles()
        {
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

            // Cargar edades
            edad_alumno.Items.Clear();
            for (int i = 6; i <= 18; i++)
                edad_alumno.Items.Add(i);

            // Cargar géneros
            combosgenero.Items.Clear();
            combosgenero.Items.Add("Masculino");
            combosgenero.Items.Add("Femenino");
            combosgenero.SelectedIndex = -1;
        }

        // ⭐ VALIDACIÓN: Solo números en teléfono
        private void telefono_tutor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // ========== VALIDACIONES DE ALUMNO ==========
        private bool ValidarAlumno(string nombres, string apellidoP, string apellidoM, int edad, DateTime fechaNac, string curp, out string mensajeError)
        {
            mensajeError = "";

            if (string.IsNullOrWhiteSpace(nombres) || string.IsNullOrWhiteSpace(apellidoP) || string.IsNullOrWhiteSpace(apellidoM))
            {
                mensajeError = "Completa todos los campos del alumno.";
                return false;
            }

            if (!Regex.IsMatch(nombres, @"^[A-Za-zÁÉÍÓÚáéíóúÑñ ]{2,}$") ||
                !Regex.IsMatch(apellidoP, @"^[A-Za-zÁÉÍÓÚáéíóúÑñ ]{2,}$") ||
                !Regex.IsMatch(apellidoM, @"^[A-Za-zÁÉÍÓÚáéíóúÑñ ]{2,}$"))
            {
                mensajeError = "Los nombres y apellidos solo pueden contener letras y espacios (mínimo 2 letras).";
                return false;
            }

            if (nombres.Length > 100 || apellidoP.Length > 100 || apellidoM.Length > 100)
            {
                mensajeError = "Los nombres y apellidos no pueden tener más de 100 caracteres.";
                return false;
            }

            if (!ValidarCoherenciaEdadFechaCURP(edad, fechaNac, curp, out mensajeError))
                return false;

            return true;
        }

        // ⭐ VALIDACIÓN: Coherencia entre Edad, Fecha y CURP
        private bool ValidarCoherenciaEdadFechaCURP(int edad, DateTime fechaNac, string curp, out string mensajeError)
        {
            mensajeError = "";

            int edadSegunFecha = CalcularEdad(fechaNac);
            if (edad != edadSegunFecha)
            {
                mensajeError = $"❌ INCOHERENCIA DETECTADA:\n\n" +
                              $"• Edad seleccionada: {edad} años\n" +
                              $"• Edad según fecha de nacimiento: {edadSegunFecha} años\n\n" +
                              $"La edad seleccionada debe coincidir con la fecha de nacimiento.";
                return false;
            }

            return true;
        }
            

        // ⭐ VALIDACIÓN: Formato de CURP
        private bool ValidarFormatoCURP(string curp)
        {
            if (curp.Length != 18) return false;
            string patron = @"^[A-Z]{4}\d{6}[HM][A-Z]{2}[A-Z]{3}[A-Z0-9]\d$";
            return Regex.IsMatch(curp.ToUpper(), patron);
        }

        // ⭐ EXTRACCIÓN: Fecha desde CURP
        private DateTime? ObtenerFechaDesCURP(string curp)
        {
            try
            {
                string anio = curp.Substring(4, 2);
                string mes = curp.Substring(6, 2);
                string dia = curp.Substring(8, 2);

                int anioNum = int.Parse(anio);
                int anioActualCorto = DateTime.Now.Year % 100;
                int anioCompleto;

                if (anioNum <= anioActualCorto)
                {
                    // Si el año de la CURP (ej. 25) es menor o igual al actual (25), es del siglo 2000.
                    anioCompleto = 2000 + anioNum;
                }
                else
                {
                    // Si el año de la CURP (ej. 99) es mayor al actual (25), es del siglo 1900.
                    anioCompleto = 1900 + anioNum;
                }

                return new DateTime(anioCompleto, int.Parse(mes), int.Parse(dia));
            }
            catch { return null; }
        }

        private void ActualizarEdadYGrupo(DateTime fechaNacimiento)
        {
            if (estaCargandoDatos) return;

            int edadCalculada = CalcularEdad(fechaNacimiento);

            // Solo actualiza si la edad está en el rango permitido
            if (edadCalculada >= 6 && edadCalculada <= 18)
            {
                estaCargandoDatos = true; // Prevenir bucles de eventos

                edad_alumno.SelectedItem = edadCalculada;
                AsignarGrupoAutomaticamente(edadCalculada); // Esta función ya la tienes

                estaCargandoDatos = false; // Liberar el seguro
            }
            else
            {
                edad_alumno.SelectedIndex = -1; // Limpiar la edad si está fuera de rango
            }
        }

        // ⭐ EXTRACCIÓN: Género desde CURP
        private string ObtenerGeneroDesdeCURP(string curp)
        {
            if (curp.Length != 18)
                return null;

            char generoChar = curp[10];
            if (generoChar == 'H')
                return "Masculino";
            else if (generoChar == 'M')
                return "Femenino";
            else
                return null;
        }

        // ⭐ CÁLCULO: Edad
        private int CalcularEdad(DateTime fechaNacimiento)
        {
            int edad = DateTime.Now.Year - fechaNacimiento.Year;
            if (DateTime.Now < fechaNacimiento.AddYears(edad)) edad--;
            return edad;
        }

        // ⭐ VALIDACIÓN: CURP y Edad
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

        // ========== VALIDACIONES DE TUTOR ==========
        private bool ValidarTutor(string nombre, string apellidoP, string apellidoM, string telefono, string correo, out string mensajeError)
        {
            mensajeError = "";

            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellidoP) ||
                string.IsNullOrWhiteSpace(apellidoM) || string.IsNullOrWhiteSpace(telefono) ||
                string.IsNullOrWhiteSpace(correo))
            {
                mensajeError = "Completa todos los campos del tutor.";
                return false;
            }

            if (!ValidarTelefonoMexicano(telefono, out mensajeError))
            {
                return false;
            }

            if (!ValidarCorreoPermitido(correo, out mensajeError))
            {
                return false;
            }

            if (!Regex.IsMatch(nombre, @"^[A-Za-zÁÉÍÓÚáéíóúÑñ ]{2,}$") ||
                !Regex.IsMatch(apellidoP, @"^[A-Za-zÁÉÍÓÚáéíóúÑñ ]{2,}$") ||
                !Regex.IsMatch(apellidoM, @"^[A-Za-zÁÉÍÓÚáéíóúÑñ ]{2,}$"))
            {
                mensajeError = "Los nombres y apellidos del tutor solo pueden contener letras y espacios (mínimo 2 letras).";
                return false;
            }

            if (nombre.Length > 100 || apellidoP.Length > 100 || apellidoM.Length > 100 || correo.Length > 100)
            {
                mensajeError = "Los campos no pueden superar los 100 caracteres.";
                return false;
            }

            return true;
        }

        // ⭐ VALIDACIÓN: Teléfono mexicano
        private bool ValidarTelefonoMexicano(string telefono, out string mensajeError)
        {
            mensajeError = "";
            telefono = telefono.Replace(" ", "").Replace("-", "");

            if (telefono.Length != 10)
            {
                mensajeError = "El teléfono debe tener exactamente 10 dígitos.";
                return false;
            }

            if (!Regex.IsMatch(telefono, @"^\d{10}$"))
            {
                mensajeError = "El teléfono solo puede contener números.";
                return false;
            }

            if (telefono[0] == '0' || telefono[0] == '1')
            {
                mensajeError = "El teléfono no puede iniciar con 0 o 1.";
                return false;
            }

            if (telefono.Distinct().Count() == 1)
            {
                mensajeError = "El teléfono no puede tener todos los dígitos iguales.";
                return false;
            }

            if (TienePatronRepetitivo(telefono))
            {
                mensajeError = "El teléfono no puede tener patrones repetitivos (ej: 0101010101).";
                return false;
            }

            if (EsSecuenciaAscendente(telefono))
            {
                mensajeError = "El teléfono no puede ser una secuencia numérica.";
                return false;
            }

            if (EsSecuenciaDescendente(telefono))
            {
                mensajeError = "El teléfono no puede ser una secuencia numérica.";
                return false;
            }

            return true;
        }

        private bool TienePatronRepetitivo(string telefono)
        {
            for (int longitud = 2; longitud <= 5; longitud++)
            {
                if (telefono.Length % longitud == 0)
                {
                    string patron = telefono.Substring(0, longitud);
                    bool esRepetitivo = true;

                    for (int i = longitud; i < telefono.Length; i += longitud)
                    {
                        string segmento = telefono.Substring(i, longitud);
                        if (segmento != patron)
                        {
                            esRepetitivo = false;
                            break;
                        }
                    }

                    if (esRepetitivo)
                        return true;
                }
            }

            return false;
        }

        private bool EsSecuenciaAscendente(string telefono)
        {
            for (int i = 0; i < telefono.Length - 1; i++)
            {
                int digito1 = int.Parse(telefono[i].ToString());
                int digito2 = int.Parse(telefono[i + 1].ToString());

                if (digito2 != (digito1 + 1) % 10)
                {
                    return false;
                }
            }
            return true;
        }

        private bool EsSecuenciaDescendente(string telefono)
        {
            for (int i = 0; i < telefono.Length - 1; i++)
            {
                int digito1 = int.Parse(telefono[i].ToString());
                int digito2 = int.Parse(telefono[i + 1].ToString());

                if (digito2 != (digito1 - 1 + 10) % 10)
                {
                    return false;
                }
            }
            return true;
        }

        // ⭐ VALIDACIÓN: Correo permitido (Gmail, Yahoo, Outlook)
        private bool ValidarCorreoPermitido(string correo, out string mensajeError)
        {
            mensajeError = "";
            correo = correo.ToLower().Trim();

            if (!Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                mensajeError = "El formato del correo no es válido.";
                return false;
            }

            string[] dominiosPermitidos = { "@gmail.com", "@yahoo.com", "@yahoo.com.mx", "@outlook.com", "@hotmail.com" };

            bool esPermitido = false;
            foreach (string dominio in dominiosPermitidos)
            {
                if (correo.EndsWith(dominio))
                {
                    esPermitido = true;
                    break;
                }
            }

            if (!esPermitido)
            {
                mensajeError = "Solo se permiten correos de Gmail, Yahoo, Outlook o Hotmail.\n\n" +
                              "Dominios válidos:\n" +
                              "• @gmail.com\n" +
                              "• @yahoo.com o @yahoo.com.mx\n" +
                              "• @outlook.com\n" +
                              "• @hotmail.com";
                return false;
            }

            return true;
        }

        // ⭐ VALIDACIÓN: Nombres válidos (sin repeticiones sin sentido)
        private bool EsCadenaValida(string texto)
        {
            texto = texto.Trim().ToUpper();

            if (!texto.All(char.IsLetter))
                return false;

            if (texto.Length < 2)
                return false;

            int distintos = texto.Distinct().Count();
            return distintos >= 2;
        }

        private bool EsNombreValido(string nombre)
        {
            nombre = nombre.Trim();

            string[] palabras = nombre.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (palabras.Length < 1)
                return false;

            foreach (var palabra in palabras)
            {
                if (!palabra.All(char.IsLetter)) return false;
                if (palabra.Length < 2) return false;
                if (palabra.Distinct().Count() < 2) return false;
            }

            return true;
        }

        // ========== VERIFICACIÓN DE DUPLICADOS (MODIFICADO PARA EDICIÓN) ==========
        private bool AlumnoCURPExistenteExceptoActual(string curp, int alumnoIDActual)
        {
            try
            {
                Conexion conexion = new Conexion();
                using (MySqlConnection conn = conexion.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM alumnos WHERE CURP=@curp AND AlumnoID!=@alumnoID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@curp", curp);
                    cmd.Parameters.AddWithValue("@alumnoID", alumnoIDActual);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
            catch { return false; }
        }

        private bool TutorCorreoExistenteExceptoActual(string correo, int tutorIDActual)
        {
            try
            {
                Conexion conexion = new Conexion();
                using (MySqlConnection conn = conexion.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM tutores WHERE LOWER(Correo)=@correo AND TutorID!=@tutorID";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@correo", correo.ToLower());
                    cmd.Parameters.AddWithValue("@tutorID", tutorIDActual);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
            catch { return false; }
        }

        // ========== CÓDIGO ORIGINAL CON VALIDACIONES INTEGRADAS ==========

        public class AlumnoComboBoxItem
        {
            public int AlumnoID { get; set; }
            public string NombreCompleto { get; set; }

            public override string ToString()
            {
                return NombreCompleto;
            }
        }

        private List<Grupo> ObtenerGruposDeDB()
        {
            List<Grupo> grupos = new List<Grupo>();
            Conexion db = new Conexion();
            string query = "SELECT id_grupo, nombre_grupo FROM grupo";

            try
            {
                using (MySqlConnection connection = db.GetConnection())
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    connection.Open();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Grupo grupo = new Grupo
                            {
                                IdGrupo = reader.GetInt32("id_grupo"),
                                NombreGrupo = reader.GetString("nombre_grupo")
                            };
                            grupos.Add(grupo);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error de DB al cargar grupos: " + ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error general al cargar grupos: " + ex.Message, "Error General", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return grupos;
        }

        private List<AlumnoComboBoxItem> ObtenerAlumnosPorGrupo(int idGrupo)
        {
            List<AlumnoComboBoxItem> alumnosItems = new List<AlumnoComboBoxItem>();
            Conexion db = new Conexion();

            string query = "SELECT AlumnoID, Nombre, ApellidoPaterno, ApellidoMaterno " +
                           "FROM alumnos " +
                           "WHERE id_grupo = @idGrupo " +
                           "ORDER BY ApellidoPaterno, ApellidoMaterno, Nombre";

            try
            {
                using (MySqlConnection connection = db.GetConnection())
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idGrupo", idGrupo);
                    connection.Open();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nombreCompleto = $"{reader.GetString("Nombre")} {reader.GetString("ApellidoPaterno")} {reader.GetString("ApellidoMaterno")}";

                            AlumnoComboBoxItem item = new AlumnoComboBoxItem
                            {
                                AlumnoID = reader.GetInt32("AlumnoID"),
                                NombreCompleto = nombreCompleto
                            };
                            alumnosItems.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de DB al cargar alumnos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return alumnosItems;
        }

        private DataTable ObtenerDatosAlumnoTutor(int alumnoID)
        {
            DataTable dt = new DataTable();
            Conexion db = new Conexion();

            string query = @"
        SELECT 
            A.AlumnoID, A.id_grupo, A.Nombre AS NombreAlumno, A.ApellidoPaterno AS APA, A.ApellidoMaterno AS AMA, 
            A.CURP, A.Edad, A.FechaNacimiento, A.TutorID, A.genero,
            T.Nombre AS NombreTutor, T.ApellidoPaterno AS APT, T.ApellidoMaterno AS AMT, 
            T.Telefono, T.Correo
        FROM 
            alumnos A
        INNER JOIN 
            tutores T ON A.TutorID = T.TutorID
        WHERE 
            A.AlumnoID = @alumnoID";

            try
            {
                using (MySqlConnection connection = db.GetConnection())
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@alumnoID", alumnoID);

                    MySqlDataAdapter da = new MySqlDataAdapter(command);
                    connection.Open();
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener datos del alumno: " + ex.Message, "Error de DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }

        private void CargarGrupos()
        {
            try
            {
                List<Grupo> grupos = ObtenerGruposDeDB();
                cbGrupoPer.DataSource = grupos;
                cbGrupoPer.DisplayMember = "NombreGrupo";
                cbGrupoPer.ValueMember = "IdGrupo";
                cbGrupoPer.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar grupos: " + ex.Message);
            }
        }

        private void cbGrupoPer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbGrupoPer.SelectedItem == null || cbGrupoPer.SelectedIndex == -1)
            {
                cbAlumno.DataSource = null;
                return;
            }

            Grupo grupoSeleccionado = (Grupo)cbGrupoPer.SelectedItem;
            int idGrupo = grupoSeleccionado.IdGrupo;

            try
            {
                List<AlumnoComboBoxItem> alumnos = ObtenerAlumnosPorGrupo(idGrupo);
                cbAlumno.DataSource = alumnos;
                cbAlumno.DisplayMember = "NombreCompleto";
                cbAlumno.ValueMember = "AlumnoID";
                cbAlumno.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar alumnos: " + ex.Message);
            }
        }

        private void cbAlumno_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpiarCampos();

            if (cbAlumno.SelectedItem == null || cbAlumno.SelectedIndex == -1)
            {
                return;
            }

            AlumnoComboBoxItem alumnoSeleccionado = (AlumnoComboBoxItem)cbAlumno.SelectedItem;
            int alumnoID = alumnoSeleccionado.AlumnoID;

            DataTable datos = ObtenerDatosAlumnoTutor(alumnoID);

            if (datos.Rows.Count > 0)
            {
                estaCargandoDatos = true; //  Activar bandera ANTES de cargar datos

                DataRow row = datos.Rows[0];

                // Cargar datos del alumno
                nombre_alumno.Text = row["NombreAlumno"].ToString();
                apellidoP_alumno.Text = row["APA"].ToString();
                apellidoM_alumno.Text = row["AMA"].ToString();
                txtCurp.Text = row["CURP"].ToString();

                // Cargar edad y fecha
                int edadDB = Convert.ToInt32(row["Edad"]);
                edad_alumno.SelectedItem = edadDB;
                nacimiento_alumno.Value = Convert.ToDateTime(row["FechaNacimiento"]);

                // Convertir género de DB (M/F) a formato ComboBox (Masculino/Femenino)
                string generoDb = row["genero"].ToString().Trim().ToUpper();
                if (generoDb == "M")
                {
                    combosgenero.SelectedItem = "Masculino";
                }
                else if (generoDb == "F")
                {
                    combosgenero.SelectedItem = "Femenino";
                }

                // Cargar datos del tutor
                nombre_tutor.Text = row["NombreTutor"].ToString();
                apellidoP_tutor.Text = row["APT"].ToString();
                apellidoM_tutor.Text = row["AMT"].ToString();
                telefono_tutor.Text = row["Telefono"].ToString();
                correo_tutor.Text = row["Correo"].ToString();

                // Asignar grupo basado en id_grupo del alumno
                int idGrupoActual = Convert.ToInt32(row["id_grupo"]);
                for (int i = 0; i < cbGrupoPer.Items.Count; i++)
                {
                    Grupo grupo = (Grupo)cbGrupoPer.Items[i];
                    if (grupo.IdGrupo == idGrupoActual)
                    {
                        cbGrupoPer.SelectedIndex = i;
                        break;
                    }
                }

                this.Tag = row["AlumnoID"].ToString();

                estaCargandoDatos = false; // Desactivar bandera DESPUÉS de cargar todos los datos
            }
        }

        private void LimpiarCampos()
        {
            nombre_alumno.Text = "";
            apellidoP_alumno.Text = "";
            apellidoM_alumno.Text = "";
            txtCurp.Text = "";
            edad_alumno.SelectedIndex = -1;
            combosgenero.SelectedIndex = -1;

            nombre_tutor.Text = "";
            apellidoP_tutor.Text = "";
            apellidoM_tutor.Text = "";
            telefono_tutor.Text = "";
            correo_tutor.Text = "";

            this.Tag = null;
        }

        private int ObtenerTutorIDDeAlumno(int alumnoID)
        {
            int tutorID = -1;
            Conexion db = new Conexion();
            string query = "SELECT TutorID FROM alumnos WHERE AlumnoID = @alumnoID";

            try
            {
                using (MySqlConnection connection = db.GetConnection())
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@alumnoID", alumnoID);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        tutorID = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener TutorID: " + ex.Message);
            }
            return tutorID;
        }

        private void ActualizarAlumno(int alumnoID, int tutorID)
        {
            Conexion db = new Conexion();
            int nuevoIdGrupo = ((Grupo)cbGrupoPer.SelectedItem).IdGrupo;
            string generoCombo = combosgenero.SelectedItem.ToString();
            string generoParaDB = "";

            if (generoCombo == "Masculino")
            {
                generoParaDB = "M";
            }
            else if (generoCombo == "Femenino")
            {
                generoParaDB = "F";
            }

            string query = @"
        UPDATE alumnos SET 
            id_grupo = @id_grupo, 
            Nombre = @nombre, 
            ApellidoPaterno = @apaterno, 
            ApellidoMaterno = @amaterno, 
            CURP = @curp, 
            Edad = @edad, 
            FechaNacimiento = @fechaNac, 
            TutorID = @tutorID, 
            genero = @genero
        WHERE AlumnoID = @alumnoID";

            using (MySqlConnection connection = db.GetConnection())
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id_grupo", nuevoIdGrupo);
                command.Parameters.AddWithValue("@nombre", nombre_alumno.Text);
                command.Parameters.AddWithValue("@apaterno", apellidoP_alumno.Text);
                command.Parameters.AddWithValue("@amaterno", apellidoM_alumno.Text);
                command.Parameters.AddWithValue("@curp", txtCurp.Text);
                command.Parameters.AddWithValue("@edad", Convert.ToInt32(edad_alumno.SelectedItem));
                command.Parameters.AddWithValue("@fechaNac", nacimiento_alumno.Value.Date);
                command.Parameters.AddWithValue("@tutorID", tutorID);
                command.Parameters.AddWithValue("@genero", generoParaDB);
                command.Parameters.AddWithValue("@alumnoID", alumnoID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private void ActualizarTutor(int tutorID)
        {
            Conexion db = new Conexion();
            string query = @"
        UPDATE tutores SET 
            Nombre = @nombre, 
            ApellidoPaterno = @apaterno, 
            ApellidoMaterno = @amaterno, 
            Telefono = @telefono, 
            Correo = @correo
        WHERE TutorID = @tutorID";

            using (MySqlConnection connection = db.GetConnection())
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@nombre", nombre_tutor.Text);
                command.Parameters.AddWithValue("@apaterno", apellidoP_tutor.Text);
                command.Parameters.AddWithValue("@amaterno", apellidoM_tutor.Text);
                command.Parameters.AddWithValue("@telefono", telefono_tutor.Text);
                command.Parameters.AddWithValue("@correo", correo_tutor.Text);
                command.Parameters.AddWithValue("@tutorID", tutorID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // ========== EVENTOS DE CONTROLES ==========

        private void Mod_Modificacion_Load(object sender, EventArgs e)
        {

        }

        private void nombre_alumno_TextChanged(object sender, EventArgs e)
        {

        }

        private void apellidoP_alumno_TextChanged(object sender, EventArgs e)
        {

        }

        private void apellidoM_alumno_TextChanged(object sender, EventArgs e)
        {

        }

        // ⭐ NUEVO: Asignar grupo automáticamente según la edad
        private void AsignarGrupoAutomaticamente(int edad)
        {
            if (estaCargandoDatos)
                return;
            string gradoBuscado = "";

            // Determinar el grado según la edad
            if (edad == 6) gradoBuscado = "primero";
            else if (edad == 7) gradoBuscado = "segundo";
            else if (edad == 8) gradoBuscado = "tercero";
            else if (edad == 9) gradoBuscado = "cuarto";
            else if (edad >= 10 && edad <= 12) gradoBuscado = "quinto";
            else if (edad >= 13 && edad <= 18) gradoBuscado = "sexto";
            else
            {
                return; // No asignar grupo si está fuera de rango
            }

            // Buscar el grupo que coincida
            for (int i = 0; i < cbGrupoPer.Items.Count; i++)
            {
                Grupo grupo = (Grupo)cbGrupoPer.Items[i];
                string nombreGrupo = grupo.NombreGrupo.ToLower();

                if (nombreGrupo.Contains(gradoBuscado))
                {
                    cbGrupoPer.SelectedIndex = i;
                    return;
                }
            }
        }
        // ⭐ EVENTO: Autocompletar desde CURP
        private void txtCurp_TextChanged(object sender, EventArgs e)
        {
            if (estaCargandoDatos) return;

            string curp = txtCurp.Text.Trim().ToUpper();

            if (curp.Length == 18 && ValidarFormatoCURP(curp))
            {
                DateTime? fechaCURP = ObtenerFechaDesCURP(curp);

                if (fechaCURP != null)
                {
                    // Actualizar fecha, edad y grupo con una sola llamada
                    estaCargandoDatos = true;
                    nacimiento_alumno.Value = fechaCURP.Value;
                    estaCargandoDatos = false;

                    ActualizarEdadYGrupo(fechaCURP.Value);
                }

                // La lógica del género se queda igual
                string generoCURP = ObtenerGeneroDesdeCURP(curp);
                if (generoCURP != null)
                {
                    combosgenero.SelectedItem = generoCURP;
                }
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

        // ⭐ EVENTO: Sincronizar edad con fecha de nacimiento
        private void edad_alumno_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // ⭐ EVENTO: Calcular edad automáticamente
        private void nacimiento_alumno_ValueChanged(object sender, EventArgs e)
        {
            if (estaCargandoDatos) return;

            // Simplemente llama a la nueva función centralizada
            ActualizarEdadYGrupo(nacimiento_alumno.Value);
        }

        private void grupo_alumno_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void combosgenero_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // ⭐⭐⭐ BOTÓN GUARDAR CON TODAS LAS VALIDACIONES ⭐⭐⭐
        private void btnGuardarModificacion_Click(object sender, EventArgs e)
        {
            if (this.Tag == null)
            {
                MessageBox.Show("No se ha seleccionado ningún alumno para modificar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int alumnoID = Convert.ToInt32(this.Tag);

            try
            {
                // 1️⃣ VALIDAR QUE LOS CAMPOS NO ESTÉN VACÍOS
                if (string.IsNullOrWhiteSpace(nombre_alumno.Text))
                {
                    MessageBox.Show("El nombre del alumno es obligatorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    nombre_alumno.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(apellidoP_alumno.Text))
                {
                    MessageBox.Show("El apellido paterno del alumno es obligatorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    apellidoP_alumno.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(apellidoM_alumno.Text))
                {
                    MessageBox.Show("El apellido materno del alumno es obligatorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    apellidoM_alumno.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtCurp.Text))
                {
                    MessageBox.Show("La CURP del alumno es obligatoria.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCurp.Focus();
                    return;
                }

                if (edad_alumno.SelectedItem == null)
                {
                    MessageBox.Show("Debes seleccionar la edad del alumno.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    edad_alumno.Focus();
                    return;
                }

                if (combosgenero.SelectedItem == null)
                {
                    MessageBox.Show("Debes seleccionar el género del alumno.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    combosgenero.Focus();
                    return;
                }

                if (cbGrupoPer.SelectedItem == null)
                {
                    MessageBox.Show("Debes seleccionar un grupo para el alumno.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbGrupoPer.Focus();
                    return;
                }

                // 2️⃣ LIMPIAR Y OBTENER DATOS
                string nombreAlumno = Regex.Replace(nombre_alumno.Text.Trim(), @"\s+", " ");
                string apellidoPAlumno = Regex.Replace(apellidoP_alumno.Text.Trim(), @"\s+", " ");
                string apellidoMAlumno = Regex.Replace(apellidoM_alumno.Text.Trim(), @"\s+", " ");
                string curp = Regex.Replace(txtCurp.Text.Trim().ToUpper(), @"\s+", "");
                int edad = Convert.ToInt32(edad_alumno.SelectedItem);
                string generoSeleccionado = combosgenero.SelectedItem.ToString();
                DateTime fechaNac = nacimiento_alumno.Value.Date;

                string nombreTutor = Regex.Replace(nombre_tutor.Text.Trim(), @"\s+", " ");
                string apellidoPTutor = Regex.Replace(apellidoP_tutor.Text.Trim(), @"\s+", " ");
                string apellidoMTutor = Regex.Replace(apellidoM_tutor.Text.Trim(), @"\s+", " ");
                string telefonoTutor = Regex.Replace(telefono_tutor.Text.Trim(), @"\s+", "");
                string correoTutor = Regex.Replace(correo_tutor.Text.Trim().ToLower(), @"\s+", "");

                // 3️⃣ VALIDAR NOMBRES SIN SENTIDO
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
                    MessageBox.Show("El nombre del alumno no es válido. Debe contener al menos dos letras distintas y solo caracteres alfabéticos.",
                        "Error en nombre del alumno", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    nombre_alumno.Focus();
                    return;
                }

                if (!EsNombreValido(nombreTutor))
                {
                    MessageBox.Show("El nombre del tutor no es válido. Al menos dos letras distintas y solo caracteres alfabéticos.",
                        "Error en nombre del tutor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    nombre_tutor.Focus();
                    return;
                }

                // 4️⃣ VALIDAR ALUMNO (con validaciones menos estrictas para edición)
                if (!ValidarAlumno(nombreAlumno, apellidoPAlumno, apellidoMAlumno, edad, fechaNac, curp, out string msgAlumno))
                {
                    MessageBox.Show(msgAlumno, "Error - Datos del Alumno", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 5️⃣ VALIDAR TUTOR
                if (!ValidarTutor(nombreTutor, apellidoPTutor, apellidoMTutor, telefonoTutor, correoTutor, out string msgTutor))
                {
                    MessageBox.Show(msgTutor, "Error - Datos del Tutor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 6️⃣ OBTENER TUTOR ID
                int tutorID = ObtenerTutorIDDeAlumno(alumnoID);

                if (tutorID == -1)
                {
                    MessageBox.Show("No se pudo obtener el ID del tutor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 7️⃣ VERIFICAR DUPLICADOS (EXCEPTO EL ALUMNO/TUTOR ACTUAL)
                if (AlumnoCURPExistenteExceptoActual(curp, alumnoID))
                {
                    MessageBox.Show("La CURP ya está registrada por otro alumno.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCurp.Focus();
                    return;
                }

                if (TutorCorreoExistenteExceptoActual(correoTutor, tutorID))
                {
                    MessageBox.Show("El correo del tutor ya está registrado por otro tutor.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    correo_tutor.Focus();
                    return;
                }

                // 8️⃣ ACTUALIZAR EN BASE DE DATOS
                ActualizarAlumno(alumnoID, tutorID);
                ActualizarTutor(tutorID);

                MessageBox.Show("✅ Datos actualizados exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 9️⃣ LIMPIAR CAMPOS
                LimpiarCampos();
                cbGrupoPer.SelectedIndex = -1;
                cbAlumno.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar los cambios: " + ex.Message + "\n\nStack Trace: " + ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panelito1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_ingresar_Click(object sender, EventArgs e)
        {
            Form1 nuevoFormulario = new Form1();
            nuevoFormulario.Show();
            this.Close();
        }

        private void btn_inscripcion_Click(object sender, EventArgs e)
        {
            Mod_inscripcion formInscripcion = new Mod_inscripcion(rolUsuario);
            formInscripcion.Show();
            this.Hide();
        }

        private void btn_capturaCalif_Click(object sender, EventArgs e)
        {
            Mod_capCal formCaptura = new Mod_capCal(rolUsuario);
            formCaptura.Show();
            this.Hide();
        }

        private void btnEstadisticas_Click(object sender, EventArgs e)
        {
            Mod_Estadisticas estadisticas = new Mod_Estadisticas(rolUsuario);
            estadisticas.Show();
            this.Hide();
        }

        private void btnAdmSecre_Click(object sender, EventArgs e)
        {
            adm_Secretaria formSecretaria = new adm_Secretaria();
            formSecretaria.Show();
            this.Hide();
        }

        private void btnBitacora_Click(object sender, EventArgs e)
        {
            Bitacora formBitacora = new Bitacora();
            formBitacora.Show();
            this.Hide();
        }

        private void btnEdicionDatos_Click(object sender, EventArgs e)
        {
            Mod_Modificacion formModificacion = new Mod_Modificacion(rolUsuario);
            formModificacion.Show();
            this.Hide();
        }

        private void btnEnvioBoletas_Click(object sender, EventArgs e)
        {
            CreacionPDF_Direc formPDF = new CreacionPDF_Direc(rolUsuario);
            formPDF.Show();
            this.Hide();
        }

        private void btn_admaestros_Click(object sender, EventArgs e)
        {
            adm_maestros formMaestros = new adm_maestros();
            formMaestros.Show();
            this.Hide();
        }

        private void panelApp_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}