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
    public partial class Form_Secretaria : Form
    {
        public Form_Secretaria()
        {
            InitializeComponent();
        }

        private void btn_ingresar_Click(object sender, EventArgs e)
        {
            Form1 nuevoFormulario = new Form1(); // creas una instancia del otro form
            nuevoFormulario.Show();              // lo muestras
            this.Hide();
        }

        private void btn_inscripcion_Click(object sender, EventArgs e)
        {
            Mod_inscripcion nuevoFormulario = new Mod_inscripcion("Secretaria"); // creas una instancia del otro form
            nuevoFormulario.Show();              // lo muestras
            this.Hide();
        }

        private void btnEnvioBoletas_Click(object sender, EventArgs e)
        {
            CreacionPDF_Direc formularioPDF = new CreacionPDF_Direc("Secretaria");  // ⭐ Pasar "Secretaria"
            formularioPDF.Show();
            this.Hide(); ;
        }

        private void btnDatosAlumnado_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelApp_Paint(object sender, PaintEventArgs e)
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
            Mod_inscripcion nuevoFormulario = new Mod_inscripcion("Secretaria");
            nuevoFormulario.Show();
            this.Close();
        }

        private void btn_capturaCalif_Click(object sender, EventArgs e)
        {
            Mod_capCal cap_calificacion = new Mod_capCal("Secretaria");  // ⭐ Pasar "Secretaria"
            cap_calificacion.Show();
            this.Hide();

        }

        private void btnEnvioBoletas_Click_1(object sender, EventArgs e)
        {
            CreacionPDF_Direc formularioPDF = new CreacionPDF_Direc("Secretaria");  // ⭐ Pasar "Secretaria"
            formularioPDF.Show();
            this.Hide();
        }
    }
}
