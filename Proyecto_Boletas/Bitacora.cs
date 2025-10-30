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
    public partial class Bitacora : Form
    {
        public Bitacora()
        {
            InitializeComponent();
            CargarRoles();
            CargarMeses();
        }

        private void CargarRoles()
        {
            try
            {
                // 1. Instanciar la clase Conexion
                Conexion conexion = new Conexion();

                // 2. Usar GetConnection()
                using (MySqlConnection conn = conexion.GetConnection())
                {
                    string query = "SELECT DISTINCT Rol FROM usuarios ORDER BY Rol ASC";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dtRoles = new DataTable();

                    conn.Open();
                    da.Fill(dtRoles);

                    // Añadir la opción "Todos" al inicio
                    DataRow rowTodos = dtRoles.NewRow();
                    rowTodos["Rol"] = "Todos";
                    dtRoles.Rows.InsertAt(rowTodos, 0);

                    // Asignar el DataTable al ComboBox cmbRol
                    cmbRoles.DataSource = dtRoles;
                    cmbRoles.DisplayMember = "Rol";
                    cmbRoles.ValueMember = "Rol";
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al cargar los roles: " + ex.Message, "Error de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error inesperado al cargar roles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 2. Método para cargar los meses
        private void CargarMeses()
        {
            var meses = new[]
            {
                new { Id = 0, Nombre = "Todos los Meses" },
                new { Id = 1, Nombre = "Enero" },
                new { Id = 2, Nombre = "Febrero" },
                new { Id = 3, Nombre = "Marzo" },
                new { Id = 4, Nombre = "Abril" },
                new { Id = 5, Nombre = "Mayo" },
                new { Id = 6, Nombre = "Junio" },
                new { Id = 7, Nombre = "Julio" },
                new { Id = 8, Nombre = "Agosto" },
                new { Id = 9, Nombre = "Septiembre" },
                new { Id = 10, Nombre = "Octubre" },
                new { Id = 11, Nombre = "Noviembre" },
                new { Id = 12, Nombre = "Diciembre" }
            };

            // Asignar los datos al ComboBox cmbMes
            cmbMes.DataSource = meses;
            cmbMes.DisplayMember = "Nombre";
            cmbMes.ValueMember = "Id";
        }



        private void cmbRoles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbMes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnGenerarBitacora_Click_1(object sender, EventArgs e)
        {
            try
            {

                string rol = cmbRoles.SelectedValue.ToString();
                int mes = Convert.ToInt32(cmbMes.SelectedValue);

                string rutaDelLogo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logo.png");

                GenerarBitacoraPDF generador = new GenerarBitacoraPDF();
                generador.GenerarPDF(rol, mes, rutaDelLogo);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar generar el PDF. Asegúrese de que ha seleccionado una opción en los filtros: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            Mod_inscripcion inscripcion = new Mod_inscripcion("Director");
            inscripcion.Show();
            this.Hide();
        }

        private void btn_capturaCalif_Click(object sender, EventArgs e)
        {
            Mod_capCal cap_calificacion = new Mod_capCal("Director");
            cap_calificacion.Show();
            this.Hide();
        }

        private void btnAdmSecre_Click(object sender, EventArgs e)
        {
            adm_Secretaria administrar_secre = new adm_Secretaria();
            administrar_secre.Show();
            this.Hide();
        }

        private void btnEnvioBoletas_Click(object sender, EventArgs e)
        {
            CreacionPDF_Direc pdf_director = new CreacionPDF_Direc();
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

        private void btnEdicionDatos_Click(object sender, EventArgs e)
        {
            Mod_Modificacion modificacion = new Mod_Modificacion();
            modificacion.Show();
            this.Hide();
        }
    }
}
