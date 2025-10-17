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
        public Mod_capCal(string rol = "Director")
        {
            InitializeComponent();
            rolUsuario = rol;
            OcultarBotonesPorRol();
        }

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

    
        
        private void Mod_capCal_Load(object sender, EventArgs e)

        {
            OcultarBotonesPorRol(); // Mantenemos la lógica de roles
            CargarGrupos(); // Nueva llamada para cargar cmbGrup
            LlenarComboMeses(); // Nueva llamada para cargar cbmes
        }

        private void CargarGrupos()
        {
            try
            {
                cmbGrup.Items.Clear();

                using (MySqlConnection conn = new Conexion().GetConnection())
                {
                    conn.Open();
                    // Asegúrate que la tabla de grupos se llama 'grupos' (o 'grupo')
                    string query = "SELECT id_grupo, nombre_grupo FROM grupos ORDER BY nombre_grupo";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            cmbGrup.Items.Add(new ComboboxItem
                            {
                                Text = dr["nombre_grupo"].ToString(),
                                Value = dr["id_grupo"]
                            });
                        }
                    }
                }

                // Opcional: Seleccionar el primer elemento para cargar los alumnos automáticamente
                if (cmbGrup.Items.Count > 0)
                {
                    cmbGrup.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los grupos: " + ex.Message, "Error de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LlenarComboMeses()
        {
            cbmes.Items.Clear();

            string[] meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio",
                       "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

            for (int i = 0; i < meses.Length; i++)
            {
                cbmes.Items.Add(new ComboboxItem
                {
                    Text = meses[i],
                    Value = i + 1 // El ID del mes (1 para Enero, 12 para Diciembre)
                });
            }

            // Opcional: Seleccionar el mes actual
            if (cbmes.Items.Count > 0)
            {
                cbmes.SelectedIndex = DateTime.Now.Month - 1; // Ajuste de índice (0-11)
            }
        }


        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; } // Usamos 'object' para ser flexible con el tipo de ID

            public override string ToString()
            {
                return Text;
            }
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



        private void panelApp_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_capturaCalif_Click(object sender, EventArgs e)
        {

        }

        private void panelApp_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void cmbGrup_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbAlumno.Items.Clear();
            LimpiarCamposCalificaciones(); // Limpia calificaciones al cambiar de grupo

            if (cmbGrup.SelectedItem != null)
            {
                int idGrupo = (int)((ComboboxItem)cmbGrup.SelectedItem).Value;

                try
                {
                    using (MySqlConnection conn = new Conexion().GetConnection())
                    {
                        conn.Open();
                        string query = "SELECT AlumnoID, Nombre, ApellidoPaterno, ApellidoMaterno FROM alumnos WHERE id_grupo = @idGrupo ORDER BY ApellidoPaterno";

                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@idGrupo", idGrupo);

                            using (MySqlDataReader dr = cmd.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    string nombreCompleto = $"{dr["ApellidoPaterno"]} {dr["ApellidoMaterno"]} {dr["Nombre"]}";

                                    cbAlumno.Items.Add(new ComboboxItem
                                    {
                                        Text = nombreCompleto,
                                        Value = dr["AlumnoID"]
                                    });
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar alumnos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cbAlumno_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpiarCamposCalificaciones();

            if (cbAlumno.SelectedItem != null && cbmes.SelectedItem != null)
            {
                int alumnoID = (int)((ComboboxItem)cbAlumno.SelectedItem).Value;
                int mesID = (int)((ComboboxItem)cbmes.SelectedItem).Value;

                try
                {
                    using (MySqlConnection conn = new Conexion().GetConnection())
                    {
                        conn.Open();

                        string query = @"
                            SELECT
                                Espanol, Matematicas, Tecnologias, Ingles, Artes, FormacionCivicaYEtica, EducacionFisica
                            FROM
                                calificaciones_mensuales
                            WHERE
                                AlumnoID = @alumnoID AND MesID = @mesID";

                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@alumnoID", alumnoID);
                            cmd.Parameters.AddWithValue("@mesID", mesID);

                            using (MySqlDataReader dr = cmd.ExecuteReader())
                            {
                                if (dr.Read())
                                {
                                    // Muestra las calificaciones
                                    cbEspanol.Text = dr["Espanol"].ToString();
                                    cbMatematicas.Text = dr["Matematicas"].ToString();
                                    cbTecnologias.Text = dr["Tecnologias"].ToString();
                                    cbIngles.Text = dr["Ingles"].ToString();
                                    cbArtes.Text = dr["Artes"].ToString();
                                    cbFormacion.Text = dr["FormacionCivicaYEtica"].ToString();
                                    cbEducacinF.Text = dr["EducacionFisica"].ToString();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar las calificaciones: " + ex.Message, "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void LimpiarCamposCalificaciones()
        {
            // Limpia los campos al cambiar de selección
            cbEspanol.Text = "";
            cbMatematicas.Text = "";
            cbTecnologias.Text = "";
            cbIngles.Text = "";
            cbArtes.Text = "";
            cbFormacion.Text = "";
            cbEducacinF.Text = "";
        }
        private void cbmes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbAlumno.SelectedItem != null)
            {
                cbAlumno_SelectedIndexChanged(sender, e);
            }
            else
            {
                LimpiarCamposCalificaciones();
            }
        }

        private void Mod_capCal_Load_1(object sender, EventArgs e)
        {

        }
    }
}
