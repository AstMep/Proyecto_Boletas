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

namespace Proyecto_Boletas
{
    public partial class Mod_Modificacion : Form
    {
        public Mod_Modificacion()
        {
            InitializeComponent();
            CargarGrupos();
        }

        // Dentro de tu namespace Proyecto_Boletas
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
            // 💡 Instanciamos tu clase Conexion
            Conexion db = new Conexion();
            string query = "SELECT id_grupo, nombre_grupo FROM grupo";

            try
            {
                // 💡 Usamos tu método GetConnection() para obtener la conexión
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

        private List<AlumnoComboBoxItem> ObtenerAlumnosPorGrupo(int idGrupo) // 👈 ¡Cambio aquí!
        {
            List<AlumnoComboBoxItem> alumnosItems = new List<AlumnoComboBoxItem>(); // 👈 ¡Cambio aquí!
            Conexion db = new Conexion();

            // 💡 ¡Agregamos AlumnoID!
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

                            // 💡 Creamos el nuevo objeto
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

            return alumnosItems; // 👈 ¡Cambio aquí!
        }
        private DataTable ObtenerDatosAlumnoTutor(int alumnoID)
        {
            DataTable dt = new DataTable();
            Conexion db = new Conexion();

            // Consulta SQL con JOIN de alumnos y tutores
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
                    da.Fill(dt); // Llenamos el DataTable con el resultado
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
                // El DisplayMember se usará si no has sobreescrito ToString() en la clase Grupo.
                // Como sí lo hicimos, no es estrictamente necesario, pero es buena práctica:
                cbGrupoPer.DisplayMember = "NombreGrupo";
                cbGrupoPer.ValueMember = "IdGrupo"; // Esto no se usará directamente en el evento si usamos el objeto 'Grupo'

                // Asegúrate de que el primer elemento no esté seleccionado si quieres forzar una elección
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
                // 2. Obtener los alumnos filtrados (ahora objetos)
                List<AlumnoComboBoxItem> alumnos = ObtenerAlumnosPorGrupo(idGrupo); // 👈 ¡Cambio aquí!

                // 3. Cargar el ComboBox de Alumnos
                cbAlumno.DataSource = alumnos;
                cbAlumno.DisplayMember = "NombreCompleto"; // Indicamos qué mostrar
                cbAlumno.ValueMember = "AlumnoID"; // Indicamos qué valor subyace
                cbAlumno.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar alumnos: " + ex.Message);
            }
        }
        private void cbAlumno_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Limpiar campos primero (opcional pero recomendado)
            LimpiarCampos();

            if (cbAlumno.SelectedItem == null || cbAlumno.SelectedIndex == -1)
            {
                return;
            }

            // Obtener el ID del alumno seleccionado
            AlumnoComboBoxItem alumnoSeleccionado = (AlumnoComboBoxItem)cbAlumno.SelectedItem;
            int alumnoID = alumnoSeleccionado.AlumnoID;

            DataTable datos = ObtenerDatosAlumnoTutor(alumnoID);

            if (datos.Rows.Count > 0)
            {
                DataRow row = datos.Rows[0];

                // Asignar datos del Alumno
                // 💡 Asume que tienes TextBox con los nombres: nombre_alumno, apellidoP_alumno, etc.
                nombre_alumno.Text = row["NombreAlumno"].ToString();
                apellidoP_alumno.Text = row["APA"].ToString();
                apellidoM_alumno.Text = row["AMA"].ToString();
                txtCurp.Text = row["CURP"].ToString();
                // Cuidado con tipos de control: Edad (ComboBox/TextBox), FechaNacimiento (DateTimePicker), Genero (ComboBox)
                edad_alumno.Text = row["Edad"].ToString();
                nacimiento_alumno.Value = Convert.ToDateTime(row["FechaNacimiento"]); // Asume un DateTimePicker
                combosgenero.Text = row["genero"].ToString();

                // Asignar datos del Tutor
                // 💡 Asume que tienes TextBox con los nombres: nombre_tutor, apellidoP_tutor, etc.
                nombre_tutor.Text = row["NombreTutor"].ToString();
                apellidoP_tutor.Text = row["APT"].ToString();
                apellidoM_tutor.Text = row["AMT"].ToString();
                telefono_tutor.Text = row["Telefono"].ToString();
                correo_tutor.Text = row["Correo"].ToString();

                // Guardar el AlumnoID y TutorID en tags o en variables de la clase para usarlos en la actualización
                // Esto es crucial para la función de MODIFICAR.
                this.Tag = row["AlumnoID"].ToString(); // Guardar AlumnoID en la propiedad Tag del formulario
                                                       // O si tienes un control específico para guardar el TutorID
                                                       // txtTutorID.Tag = row["TutorID"].ToString(); 
            }
        }

        private void LimpiarCampos()
        {
            // Limpiar todos los campos del alumno
            nombre_alumno.Text = "";
            apellidoP_alumno.Text = "";
            apellidoM_alumno.Text = "";
            txtCurp.Text = "";
            edad_alumno.SelectedIndex = -1;
            // nacimiento_alumno.Value = DateTime.Now; // O un valor por defecto
            combosgenero.SelectedIndex = -1;

            // Limpiar campos del tutor
            nombre_tutor.Text = "";
            apellidoP_tutor.Text = "";
            apellidoM_tutor.Text = "";
            telefono_tutor.Text = "";
            correo_tutor.Text = "";

            this.Tag = null; // Limpiar el ID
        }

        // Función auxiliar para obtener el TutorID (si no lo guardaste en la carga)
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
                // Manejo de error
            }
            return tutorID;
        }

        private void ActualizarAlumno(int alumnoID, int tutorID)
        {
            Conexion db = new Conexion();
            // 💡 Asegúrate de obtener el id_grupo del ComboBox de grupos (cbGrupoPer)
            int nuevoIdGrupo = ((Grupo)cbGrupoPer.SelectedItem).IdGrupo;

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
                command.Parameters.AddWithValue("@edad", Convert.ToInt32(edad_alumno.Text)); // Cuidado con la conversión
                command.Parameters.AddWithValue("@fechaNac", nacimiento_alumno.Value.Date); // Solo la fecha
                command.Parameters.AddWithValue("@tutorID", tutorID);
                command.Parameters.AddWithValue("@genero", combosgenero.Text);
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

        private void txtCurp_TextChanged(object sender, EventArgs e)
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

        private void edad_alumno_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void nacimiento_alumno_ValueChanged(object sender, EventArgs e)
        {

        }

        private void grupo_alumno_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void combosgenero_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnGuardarModificacion_Click(object sender, EventArgs e)
        {
            if (this.Tag == null)
            {
                MessageBox.Show("No se ha seleccionado ningún alumno para modificar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int alumnoID = Convert.ToInt32(this.Tag);

           

            int tutorID = ObtenerTutorIDDeAlumno(alumnoID); // 👈 Necesitas implementar esta función

            if (tutorID == -1)
            {
                MessageBox.Show("No se pudo obtener el ID del tutor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Iniciar la transacción y las actualizaciones
            try
            {
                ActualizarAlumno(alumnoID, tutorID);
                ActualizarTutor(tutorID);
                MessageBox.Show("Datos actualizados exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Opcional: Recargar los datos del alumno para asegurar que se muestren los cambios
                // cbAlumno_SelectedIndexChanged(null, null); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar los cambios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
