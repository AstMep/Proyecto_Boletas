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
            OcultarBotonesPorRol();
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

        private void cmbAlumno_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panelApp_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_capturaCalif_Click(object sender, EventArgs e)
        {

        }
    }
}
