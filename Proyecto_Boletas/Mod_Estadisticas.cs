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
    public partial class Mod_Estadisticas : Form
    {

        private string rolUsuario;


        public Mod_Estadisticas(string rol)
        {
            InitializeComponent();
            this.rolUsuario = rol;
            OcultarBotonesPorRol();
        }


        private void OcultarBotonesPorRol()
        {


            if (rolUsuario == "Secretaria")
            {


                if (btnAdmSecre != null)
                    btnAdmSecre.Visible = false;

                if (btnBitacora != null)
                    btnBitacora.Visible = false;

                if (btn_admaestros != null)
                    btn_admaestros.Visible = false;


            }

        }


        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void Mod_Estadisticas_Load(object sender, EventArgs e)
        {
            // (Esta función ahora se llama desde el constructor)
            // OcultarBotonesPorRol(); 
        }

        private void btnCEstadisticas_Click(object sender, EventArgs e)
        {
            try
            {
                GeneradorEstadisticas generador = new GeneradorEstadisticas();
                generador.CrearPDFEstadisticaTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el PDF: " + ex.Message);
            }
        }

        // 4. --- EVENTOS DE NAVEGACIÓN COMPLETOS ---

        private void btn_ingresar_Click(object sender, EventArgs e)
        {
            // Cerrar Sesión
            Form1 nuevoFormulario = new Form1();
            nuevoFormulario.Show();
            this.Close(); // Cierra este formulario
        }

        private void btn_inscripcion_Click(object sender, EventArgs e)
        {
            Mod_inscripcion inscripcion = new Mod_inscripcion(rolUsuario);
            inscripcion.Show();
            this.Hide(); // Oculta este formulario
        }

        private void btn_capturaCalif_Click(object sender, EventArgs e)
        {
            Mod_capCal capCal = new Mod_capCal(rolUsuario);
            capCal.Show();
            this.Hide();
        }

        private void btnAdmSecre_Click(object sender, EventArgs e)
        {
            
            adm_Secretaria admSecre = new adm_Secretaria();
            admSecre.Show();
            this.Hide();
        }

        private void btnBitacora_Click(object sender, EventArgs e)
        {
            // Este formulario tampoco necesita rol
            Bitacora bitacora = new Bitacora();
            bitacora.Show();
            this.Hide();
        }

        private void btnEdicionDatos_Click(object sender, EventArgs e)
        {
            Mod_Modificacion mod = new Mod_Modificacion(rolUsuario);
            mod.Show();
            this.Hide();
        }

        private void btnEnvioBoletas_Click(object sender, EventArgs e)
        {
            // Este es tu "Creación de PDFs"
            CreacionPDF_Direc pdf = new CreacionPDF_Direc(rolUsuario);
            pdf.Show();
            this.Hide();
        }

        private void panelApp_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}