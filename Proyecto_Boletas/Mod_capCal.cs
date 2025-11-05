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
using static Proyecto_Boletas.Mod_capCal;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Proyecto_Boletas
{
    public partial class Mod_capCal : Form
    {
        private string rolUsuario;
        private int idGrupoSeleccionado;
        public Mod_capCal(string rol = "Director")
        {
            InitializeComponent();
            rolUsuario = rol;
            OcultarBotonesPorRol();
            CargarMeses();
            CargarGrupos();
            CargarCalificaciones();
            CargarInasistencias();

        }

        private void OcultarBotonesPorRol()
        {
            if (rolUsuario == "Secretaria")
            {
                // Ocultar botones solo para secretaria
                button6.Visible = false;
                button5.Visible = false;
                button2.Visible = false;
            }
            else if (rolUsuario == "Director")
            {
                // Mostrar todos los botones para director
                button6.Visible = true;
                button5.Visible = true;
                button2.Visible = true;
            }
        }

        private void LimpiarCamposDeCaptura()
        {
            // Calificaciones de las Materias
            cbEspanol.SelectedIndex = -1;
            cbIngles.SelectedIndex = -1;
            cbArtes.SelectedIndex = -1;
            cbMatematicas.SelectedIndex = -1;
            cbTecnologias.SelectedIndex = -1;
            cbC.SelectedIndex = -1; // Materia Condicional (C. Naturales / C. del Medio)
            cbFormacion.SelectedIndex = -1;
            cbEducacinF.SelectedIndex = -1;

            // Inasistencias
            cbInasistencias.SelectedIndex = -1;

            // Alumno
            cbAlumno.SelectedIndex = -1;
            // Nota: El grupo y el mes NO se limpian, ya que la intención es seguir capturando 
            // alumnos de ese mismo grupo y periodo.
        }
        private string MapearMateriaCienciasParaDB(string nombreInterfaz)
        {
            string limpio = nombreInterfaz.ToUpper().Replace(".", "").Trim();

            if (limpio.Contains("DEL MEDIO"))
            {
                return "CONOCIMIENTO DEL MEDIO";
            }
            else if (limpio.Contains("NATURALES"))
            {
                return "CIENCIAS NATURALES";
            }
            else if (limpio.Contains("FORM CÍV Y ÉTICA"))
            {
                return "FORM. CÍV Y ÉTICA";
            }
            // Añade cualquier otra abreviatura que uses si no es Ciencias/F. Civica

            return nombreInterfaz; // Devolver el original si no se encuentra
        }
        public class Grupo
        {
            public int IdGrupo { get; set; }
            public string NombreGrupo { get; set; }

            // Este método es crucial para que el ComboBox muestre el nombre
            public override string ToString()
            {
                return NombreGrupo;
            }
        }
        // Método de ejemplo para obtener datos (deberás adaptarlo a tu capa de datos)
        private void CargarMeses()
        {
            List<string> meses = new List<string>
                {
                    "AGOSTO (DIAGNÓSTICO)", // Tu valor agregado en mayúsculas
                    "SEPTIEMBRE",
                    "OCTUBRE",
                    "NOVIEMBRE",
                    "DICIEMBRE",
                    "ENERO",
                    "FEBRERO",
                    "MARZO",
                    "ABRIL",
                    "MAYO",
                    "JUNIO",
                    // Agrega otros meses si son necesarios
                };

            cbmes.DataSource = meses;
            cbmes.SelectedIndex = -1; // Ningún mes seleccionado por defecto
        }

        private void CargarCalificaciones()
        {
            List<int> calificaciones = new List<int>
    {
        10, 9, 8, 7, 6, 5,
    };
            cbC.DataSource = new List<int>(calificaciones);
            cbC.SelectedIndex = -1;

            cbArtes.DataSource = new List<int>(calificaciones);
            cbArtes.SelectedIndex = -1;

            cbEspanol.DataSource = new List<int>(calificaciones);
            cbEspanol.SelectedIndex = -1;

            cbFormacion.DataSource = new List<int>(calificaciones);
            cbFormacion.SelectedIndex = -1;

            cbIngles.DataSource = new List<int>(calificaciones);
            cbIngles.SelectedIndex = -1;

            cbMatematicas.DataSource = new List<int>(calificaciones);
            cbMatematicas.SelectedIndex = -1;

            cbEducacinF.DataSource = new List<int>(calificaciones);
            cbEducacinF.SelectedIndex = -1;

            cbTecnologias.DataSource = new List<int>(calificaciones);
            cbTecnologias.SelectedIndex = -1;
        }

        private void CargarInasistencias()
        {
            List<int> calificaciones = new List<int>
                {
                    1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,
                };

            cbInasistencias.DataSource = calificaciones;
            cbInasistencias.SelectedIndex = -1;
        }
        private List<Grupo> ObtenerGruposDeDB()
        {
            List<Grupo> grupos = new List<Grupo>();
            // 💡 Instanciamos tu clase Conexion
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
                // Aquí puede ocurrir el error de "Format of the initialization string" si la cadena de Conexion tiene problemas.
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error general al cargar grupos: " + ex.Message, "Error General", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return grupos;
        }


        private List<string> ObtenerAlumnosPorGrupo(int idGrupo, string periodo) // Correcto: DOS PARÁMETROS
        {
            List<string> nombresCompletos = new List<string>();
            Conexion db = new Conexion();

            // Consulta SQL con la cláusula NOT IN para excluir alumnos ya calificados en este período
            string query = "SELECT a.Nombre, a.ApellidoPaterno, a.ApellidoMaterno " +
                           "FROM alumnos a " +
                           "WHERE a.id_grupo = @idGrupo " +
                           "AND a.AlumnoID NOT IN (" +
                           "    SELECT c.AlumnoID FROM calificaciones c " +
                           "    WHERE c.Periodo = @periodo" +
                           ") " +
                           "ORDER BY a.ApellidoPaterno, a.ApellidoMaterno, a.Nombre";

            try
            {
                using (MySqlConnection connection = db.GetConnection())
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@idGrupo", idGrupo);
                    command.Parameters.AddWithValue("@periodo", periodo); // Parámetro necesario para el filtro
                    connection.Open();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nombreCompleto = $"{reader.GetString("Nombre")} {reader.GetString("ApellidoPaterno")} {reader.GetString("ApellidoMaterno")}";
                            nombresCompletos.Add(nombreCompleto);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error de DB al cargar alumnos filtrados: " + ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error general al cargar alumnos filtrados: " + ex.Message, "Error General", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return nombresCompletos;
        }


        private void CargarAlumnosFiltrados()
        {
            // Asegurarse de que el grupo y el mes estén seleccionados
            if (cmbGrup.SelectedItem == null || cbmes.SelectedItem == null)
            {
                cbAlumno.DataSource = null;
                return;
            }

            // 1. Obtener los IDs y Periodo
            // idGrupoSeleccionado ya debe estar actualizado por el evento cmbGrup_SelectedIndexChanged
            string mesSeleccionadoApp = cbmes.SelectedItem.ToString();
            string periodoBD = ConvertirMesParaBD(mesSeleccionadoApp);

            if (idGrupoSeleccionado > 0)
            {
                try
                {
                    // 2. Obtener los alumnos EXCLUYENDO a los ya capturados
                    List<string> alumnosPendientes = ObtenerAlumnosPorGrupo(idGrupoSeleccionado, periodoBD);

                    // 3. Cargar el ComboBox de Alumnos
                    cbAlumno.DataSource = alumnosPendientes;
                    cbAlumno.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar alumnos: " + ex.Message);
                }
            }
        }

        private void CargarGrupos()
        {
            try
            {
                List<Grupo> grupos = ObtenerGruposDeDB();
                cmbGrup.DataSource = grupos;

                cmbGrup.DisplayMember = "NombreGrupo";
                cmbGrup.ValueMember = "IdGrupo";
                cmbGrup.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar grupos: " + ex.Message);
            }
        }


        private void GuardarCalificacionesMensuales(object sender, EventArgs e)
        {


            if (cmbGrup.SelectedItem == null || cbAlumno.SelectedItem == null || cbmes.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona un Grupo, un Alumno y un Mes.", "Datos Faltantes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            string mesSeleccionadoApp = cbmes.SelectedItem.ToString();

            string periodoBD = ConvertirMesParaBD(mesSeleccionadoApp);


            int alumnoId = ObtenerAlumnoIdPorNombre(cbAlumno.SelectedItem.ToString());

            if (alumnoId == 0)
            {
                MessageBox.Show("No se pudo identificar al AlumnoID.", "Error Interno", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            int inasistenciasMensuales = (int)cbInasistencias.SelectedItem;


            var materiasACapturar = new List<(System.Windows.Forms.ComboBox cbCal, string nombreMateria)>
    {
        (cbEspanol, "ESPAÑOL"),
        (cbIngles, "INGLÉS"),
        (cbArtes, "ARTES"),
        (cbMatematicas, "MATEMÁTICAS"),
        (cbTecnologias, "TECNOLOGÍA"),
        (cbFormacion, "FORM. CÍV Y ÉTICA"),
        (cbEducacinF, "ED. FISICA"),
        (cbC, lbC.Text.ToUpper())  };

            bool capturaExitosa = true;
            int idGrupoActual = idGrupoSeleccionado;

            foreach (var materia in materiasACapturar)
            {
                if (materia.cbCal.SelectedItem != null)
                {
                    int calificacion = (int)materia.cbCal.SelectedItem;

                    // 💡 SOLUCIÓN CLAVE: Convertir el nombre de la interfaz al nombre que espera la BD
                    string nombreMateriaBD;

                    if (materia.nombreMateria == lbC.Text.ToUpper())
                    {
                        // Si es la materia condicional (usando lbC.Text) o Formación Cívica
                        nombreMateriaBD = MapearMateriaCienciasParaDB(materia.nombreMateria);
                    }
                    else
                    {
                        // Para las materias normales (Español, Matemáticas, etc.)
                        nombreMateriaBD = materia.nombreMateria;
                    }

                    // --- El guardado usa el nombre mapeado y limpio ---
                    int materiaId = ObtenerMateriaIdPorNombre(nombreMateriaBD, idGrupoActual);

                    if (materiaId > 0)
                    {
                        if (!InsertarOActualizarCalificacion(alumnoId, materiaId, calificacion, inasistenciasMensuales, periodoBD))
                        {
                            capturaExitosa = false;
                            MessageBox.Show($"Error al guardar la calificación para la materia: {materia.nombreMateria}.", "Error de DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Advertencia: No se encontró el ID para la materia: {materia.nombreMateria}. Verifique que el nombre '{nombreMateriaBD}' exista en la tabla materias para el grupo {idGrupoActual}.", "Advertencia de DB", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                // Si materia.cbCal.SelectedItem es null, simplemente pasa a la siguiente materia sin generar error.
            }

            if (capturaExitosa)
            {
                // ... (Código de mensaje de éxito y limpieza)
                MessageBox.Show("¡Captura de calificaciones e inasistencias mensuales completada!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCamposDeCaptura();

                // Y recargar la lista de alumnos
                CargarAlumnosFiltrados();
            }
        }

        private bool InsertarOActualizarCalificacion(int alumnoId, int materiaId, int calificacion, int inasistencias, string periodo)
        {
            Conexion db = new Conexion();

            try
            {
                // 💡 1. Usamos el bloque 'using' para asegurar que la conexión se cierre.
                using (MySqlConnection connection = db.GetConnection())
                {
                    // ✅ CORRECCIÓN CLAVE: Abrir la conexión a la base de datos.
                    connection.Open();

                    // 2. Verificar si la calificación ya existe.
                    string checkQuery = "SELECT CalificacionID FROM calificaciones " +
                                        "WHERE AlumnoID = @alumnoId AND MateriaID = @materiaId AND Periodo = @periodo LIMIT 1";

                    using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@alumnoId", alumnoId);
                        checkCommand.Parameters.AddWithValue("@materiaId", materiaId);
                        checkCommand.Parameters.AddWithValue("@periodo", periodo);

                        object existingId = checkCommand.ExecuteScalar();

                        // 3. Determinar INSERT o UPDATE.
                        string sql;
                        if (existingId != null)
                        {
                            sql = "UPDATE calificaciones SET Calificacion = @calif, Inasistencias = @inasis, FechaRegistro = NOW() " +
                                  "WHERE CalificacionID = @calificacionId";
                        }
                        else
                        {
                            sql = "INSERT INTO calificaciones (AlumnoID, MateriaID, Calificacion, Periodo, Inasistencias, FechaRegistro) " +
                                  "VALUES (@alumnoId, @materiaId, @calif, @periodo, @inasis, NOW())";
                        }

                        // 4. Ejecutar el comando final.
                        using (MySqlCommand command = new MySqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@calif", calificacion);
                            command.Parameters.AddWithValue("@inasis", inasistencias);

                            if (existingId == null)
                            {
                                // Parámetros para INSERT
                                command.Parameters.AddWithValue("@alumnoId", alumnoId);
                                command.Parameters.AddWithValue("@materiaId", materiaId);
                                command.Parameters.AddWithValue("@periodo", periodo);
                            }
                            else
                            {
                                // Parámetro para UPDATE
                                command.Parameters.AddWithValue("@calificacionId", existingId);
                            }

                            command.ExecuteNonQuery();
                            return true;
                        } // Fin using command
                    } // Fin using checkCommand
                } // Fin using connection
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error de BD: {ex.Message}", "Error de Guardado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private string ConvertirMesParaBD(string mesCompleto)
        {
            switch (mesCompleto.ToUpper())
            {
                case "AGOSTO (DIAGNÓSTICO)":
                    return "DIAGNOSTICO";
                case "SEPTIEMBRE":
                    return "SEP";
                case "OCTUBRE":
                    return "OCT";
                case "NOVIEMBRE":
                    return "NOV";
                case "DICIEMBRE":
                    return "DIC";
                case "ENERO":
                    return "ENE";
                case "FEBRERO":
                    return "FEB";
                case "MARZO":
                    return "MAR";
                case "ABRIL":
                    return "ABR";
                case "MAYO":
                    return "MAY";
                case "JUNIO":
                    return "JUN";
                default:
                    return mesCompleto.ToUpper(); // Devuelve el valor por si hay otro mes no mapeado
            }
        }

        private int ObtenerAlumnoIdPorNombre(string nombreCompleto)
        {

            string[] partes = nombreCompleto.Split(' ');

            if (partes.Length < 3)
            {
                MessageBox.Show("El formato del nombre del alumno es incorrecto.", "Error de Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }

            string nombre = partes[0];
            string apPaterno = partes[1];
            string apMaterno = partes[2];
            int alumnoId = 0;

            Conexion db = new Conexion();
            string query = "SELECT AlumnoID FROM alumnos " +
                           "WHERE Nombre = @nombre AND ApellidoPaterno = @apPaterno AND ApellidoMaterno = @apMaterno LIMIT 1";

            try
            {
                using (MySqlConnection connection = db.GetConnection())
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@nombre", nombre);
                    command.Parameters.AddWithValue("@apPaterno", apPaterno);
                    command.Parameters.AddWithValue("@apMaterno", apMaterno);
                    connection.Open();

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        alumnoId = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener AlumnoID: " + ex.Message, "Error de DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return alumnoId;
        }

        private int ObtenerMateriaIdPorNombre(string nombreMateria, int idGrupo)
        {
            int materiaId = 0;
            Conexion db = new Conexion();

            // Filtramos por Nombre y id_grupo para manejar materias condicionales (ej. Conoc. del Medio vs. C. Naturales)
            string query = "SELECT MateriaID FROM materias " +
                           "WHERE Nombre = @nombreMateria AND id_grupo = @idGrupo LIMIT 1";

            try
            {
                using (MySqlConnection connection = db.GetConnection())
                {
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@nombreMateria", nombreMateria);
                    command.Parameters.AddWithValue("@idGrupo", idGrupo);
                    connection.Open();

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        materiaId = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener MateriaID: " + ex.Message, "Error de DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return materiaId;
        }







        // Llama a CargarGrupos() en el constructor de tu formulario o en el evento Load.
        private void Mod_capCal_Load(object sender, EventArgs e)

        {
            OcultarBotonesPorRol(); // Mantenemos la lógica de roles

        }


        private void btn_ingresar_Click(object sender, EventArgs e)
        {
            Form1 nuevoFormulario = new Form1();
            nuevoFormulario.Show();
            this.Close();
        }

        private void btn_inscripcion_Click(object sender, EventArgs e)
        {
            Mod_inscripcion inscripcion = new Mod_inscripcion(rolUsuario);
            inscripcion.Show();
            this.Hide();
        }

        private void btnAdmSecre_Click(object sender, EventArgs e)
        {
            adm_Secretaria administrar_secre = new adm_Secretaria();
            administrar_secre.Show();
            this.Hide();
        }

        private void btnBitacora_Click(object sender, EventArgs e)
        {
            Bitacora bitacora = new Bitacora();
            bitacora.Show();
            this.Hide();
        }

        private void btnEnvioBoletas_Click(object sender, EventArgs e)
        {
            CreacionPDF_Direc pdf_director = new CreacionPDF_Direc(rolUsuario);
            pdf_director.Show();
            this.Hide();
        }

        private void btn_admaestros_Click(object sender, EventArgs e)
        {
            adm_maestros administrar_maestros = new adm_maestros();
            administrar_maestros.Show();
            this.Hide();

        }
        private void btnEdicionDatos_Click(object sender, EventArgs e)
        {
            Mod_Modificacion modificacion = new Mod_Modificacion(rolUsuario);
            modificacion.Show();
            this.Hide();
        }





        private void btn_capturaCalif_Click(object sender, EventArgs e)
        {

        }

        private void panelApp_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void cmbGrup_SelectedIndexChanged(object sender, EventArgs e)
        {

            cbAlumno.DataSource = null;
            if (cmbGrup.SelectedItem == null)
            {
                return;
            }

            // 2. OBTENER Y GUARDAR EL ID DEL GRUPO SELECCIONADO (Variable de clase)
            Grupo grupoSeleccionado = (Grupo)cmbGrup.SelectedItem;
            idGrupoSeleccionado = grupoSeleccionado.IdGrupo;

            // 3. LÓGICA DE LA MATERIA CONDICIONAL (Determinar el nombre de la materia de ciencias según el GRADO)
            // El nombre de la materia condicional DEPENDE del grado (idGrupo), NO de la calificación seleccionada.
            if (idGrupoSeleccionado == 2 || idGrupoSeleccionado == 3) // 1º y 2º grado
            {
                lbC.Text = "Conoc. Del Medio";
            }
            else if (idGrupoSeleccionado >= 4 && idGrupoSeleccionado <= 7) // 3º a 6º grado
            {
                lbC.Text = "C. Naturales";
            }
            else
            {
                lbC.Text = "MATERIA DE CIENCIAS"; // Contingencia
            }

            // 4. CARGAR ALUMNOS FILTRADOS
            // Llama a la función unificada que usa idGrupoSeleccionado y cbmes.SelectedItem
            CargarAlumnosFiltrados();
        }



        private void cbAlumno_SelectedIndexChanged(object sender, EventArgs e)
        {


        }



        private void cbmes_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarAlumnosFiltrados();
        }

        private void Mod_capCal_Load_1(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

            // Opcional: Recargar los datos para ver que se hayan guardado o resetear la interfaz.
            // RecargarDatosDeCaptura();
        }

        private void btn_inscripcion_Click_1(object sender, EventArgs e)
        {
            Mod_inscripcion inscripcion = new Mod_inscripcion(rolUsuario);
            inscripcion.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void btnCapturacalif_Click(object sender, EventArgs e)
        {
            GuardarCalificacionesMensuales(sender, e);
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void panelito1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
