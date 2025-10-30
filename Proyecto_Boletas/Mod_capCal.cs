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
    public partial class Mod_capCal : Form
    {
        private string rolUsuario;
        private System.Windows.Forms.GroupBox groupBoxMateriaDinamica;
        public Mod_capCal(string rol = "Director")
        {
            InitializeComponent();
            rolUsuario = rol;
            OcultarBotonesPorRol();
            CargarMeses();
            CargarGrupos();
            OcultarBotonesPorRol();

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
                    "DIAGNÓSTICO", // Tu valor agregado en mayúsculas
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

        private void ActualizarControlesMateria(int idGrupo)
        {
            // 1. ELIMINAR EL CONTENEDOR ANTERIOR (si existe)
            if (groupBoxMateriaDinamica != null && this.Controls.Contains(groupBoxMateriaDinamica))
            {
                this.Controls.Remove(groupBoxMateriaDinamica);
                groupBoxMateriaDinamica.Dispose();
                groupBoxMateriaDinamica = null;
            }

            // 2. DETERMINAR EL TEXTO Y NOMBRE DEL CONTROL BASADO EN EL ID
            string textoLabel;
            string nombreComboBox;

            // Grados 1 y 2 (IDs 2 y 3)
            if (idGrupo >= 2 && idGrupo <= 3)
            {
                textoLabel = "Conoc. del Med.";
                nombreComboBox = "cmbConoc";
            }
            // Grados 3 a 6 (IDs 4 a 7)
            else if (idGrupo >= 4 && idGrupo <= 7)
            {
                textoLabel = "Ciencias N";
                nombreComboBox = "cmbCien";
            }
            else
            {
                // ID fuera de rango (1, 0, o mayor a 7). Solo limpiamos y salimos.
                return;
            }

            // 3. CREAR EL GROUPBOX CONTENEDOR 
            groupBoxMateriaDinamica = new GroupBox();
            groupBoxMateriaDinamica.Text = "Materia Específica"; // Título visible
            groupBoxMateriaDinamica.Size = new Size(330, 80);
            // ⚠️ POSICIÓN CLAVE: Ajusta estas coordenadas si el GroupBox colisiona con otros elementos.
            // Lo colocaremos debajo de los GroupBox existentes. Asumo que están cerca de (20, 300).
            groupBoxMateriaDinamica.Location = new Point(20, 450);
            groupBoxMateriaDinamica.Name = nombreComboBox.Replace("cmb", "gb"); // "gbConoc" o "gbCien"
            groupBoxMateriaDinamica.ForeColor = SystemColors.ControlLightLight; // Asegura que el título sea visible

            // 4. CREAR LABEL DINÁMICO (Propiedades de tu ejemplo)
            Label lbMateria = new Label();
            lbMateria.AutoSize = true;
            lbMateria.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            // Aseguramos un color de texto contrastante
            lbMateria.ForeColor = Color.White;
            // Posición (25, 31) relativa al GroupBox
            lbMateria.Location = new Point(25, 31);
            lbMateria.Name = "lbMateria";
            lbMateria.Text = textoLabel;
            lbMateria.TabIndex = 62;

            // 5. CREAR COMBOBOX DINÁMICO (Propiedades de tu ejemplo)
            ComboBox cmbMateria = new ComboBox();
            cmbMateria.DropDownStyle = ComboBoxStyle.DropDownList;
            // Posición (205, 31) relativa al GroupBox
            cmbMateria.Location = new Point(205, 31);
            cmbMateria.Margin = new Padding(3, 4, 3, 4);
            cmbMateria.Name = nombreComboBox; // cmbConoc o cmbCien
            cmbMateria.Size = new Size(91, 28);
            cmbMateria.TabIndex = 63;

            // 6. AÑADIR LOS CONTROLES AL GROUPBOX
            groupBoxMateriaDinamica.Controls.Add(lbMateria);
            groupBoxMateriaDinamica.Controls.Add(cmbMateria);

            // 7. AÑADIR EL GROUPBOX AL FORMULARIO
            this.Controls.Add(groupBoxMateriaDinamica);


        }
        private List<string> ObtenerAlumnosPorGrupo(int idGrupo)
        {
            List<string> nombresCompletos = new List<string>();
            // 💡 Instanciamos tu clase Conexion
            Conexion db = new Conexion();

            string query = "SELECT Nombre, ApellidoPaterno, ApellidoMaterno " +
                           "FROM alumnos " +
                           "WHERE id_grupo = @idGrupo " +
                           "ORDER BY ApellidoPaterno, ApellidoMaterno, Nombre";

            try
            {
                // 💡 Usamos tu método GetConnection() para obtener la conexión
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
                            nombresCompletos.Add(nombreCompleto);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error de DB al cargar alumnos: " + ex.Message, "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error general al cargar alumnos: " + ex.Message, "Error General", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return nombresCompletos;
        }
        private void CargarGrupos()
        {
            try
            {
                List<Grupo> grupos = ObtenerGruposDeDB();
                cmbGrup.DataSource = grupos;
                // El DisplayMember se usará si no has sobreescrito ToString() en la clase Grupo.
                // Como sí lo hicimos, no es estrictamente necesario, pero es buena práctica:
                cmbGrup.DisplayMember = "NombreGrupo";
                cmbGrup.ValueMember = "IdGrupo"; // Esto no se usará directamente en el evento si usamos el objeto 'Grupo'

                // Asegúrate de que el primer elemento no esté seleccionado si quieres forzar una elección
                cmbGrup.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar grupos: " + ex.Message);
            }
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

            // Evita ejecutar código si no hay un elemento seleccionado o si el cambio es por la carga inicial.
            if (cmbGrup.SelectedItem == null)
            {
                cbAlumno.DataSource = null; // 
                ActualizarControlesMateria(0);
                return;
            }

            // 1. Obtener el ID del grupo seleccionado
            Grupo grupoSeleccionado = (Grupo)cmbGrup.SelectedItem;
            int idGrupo = grupoSeleccionado.IdGrupo;

            // 💡 3. LLAMADA PARA CREAR O ELIMINAR EL GROUPBOX DE MATERIA
            ActualizarControlesMateria(idGrupo);

            try
            {
                // 2. Obtener los alumnos filtrados
                List<string> alumnos = ObtenerAlumnosPorGrupo(idGrupo);

                // 3. Cargar el ComboBox de Alumnos
                cbAlumno.DataSource = alumnos;
                cbAlumno.SelectedIndex = -1; // Deseleccionar el primer alumno
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar alumnos: " + ex.Message);
            }
        }



        private void cbAlumno_SelectedIndexChanged(object sender, EventArgs e)
        {


        }



        private void cbmes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Mod_capCal_Load_1(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

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

        
       

        
        

        
    }
}
