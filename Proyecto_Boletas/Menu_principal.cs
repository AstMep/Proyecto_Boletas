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
    public partial class Menu_principal : Form
    {
        private string rolUsuario;
        public Menu_principal(string rol = "Director")
        {
            InitializeComponent();
            rolUsuario = rol;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }


        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void Menu_principal_Load(object sender, EventArgs e)
        {

        }

        private void btn_ingresar_Click_1(object sender, EventArgs e)
        {
            Form1 nuevoFormulario = new Form1();
            nuevoFormulario.Show();
            this.Close();
        }

        private void btn_inscripcion_Click_1(object sender, EventArgs e)
        {
            Mod_inscripcion inscripcion = new Mod_inscripcion(rolUsuario);
            inscripcion.Show();
            this.Hide();
        }

        private void btn_capturaCalif_Click(object sender, EventArgs e)
        {
            Mod_capCal cap_calificacion = new Mod_capCal(rolUsuario);
            cap_calificacion.Show();
            this.Hide();
        }

        private void btnAdmSecre_Click_1(object sender, EventArgs e)
        {
            adm_Secretaria administrar_secre = new adm_Secretaria();
            administrar_secre.Show();
            this.Hide();
        }

        private void btnBitacora_Click_1(object sender, EventArgs e)
        {
            Bitacora bitacora = new Bitacora();
            bitacora.Show();
            this.Hide();
        }

        private void btnEnvioBoletas_Click_1(object sender, EventArgs e)
        {
            CreacionPDF_Direc pdf_director = new CreacionPDF_Direc(rolUsuario);
            pdf_director.Show();
            this.Hide();
        }

        private void btn_admaestros_Click_1(object sender, EventArgs e)
        {
            adm_maestros administrar_maestros = new adm_maestros();
            administrar_maestros.Show();
            this.Hide();
        }

        private void btnEstadisticas_Click(object sender, EventArgs e)
        {

        }

        private void btnEdicionDatos_Click_1(object sender, EventArgs e)
        {
            
            Mod_Modificacion Modificacion = new Mod_Modificacion(rolUsuario);
            Modificacion.Show();
            this.Hide();
        }
    }
}