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
        public Menu_principal()
        {
            InitializeComponent();//HOLI
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btn_ingresar_Click(object sender, EventArgs e)
        {
            Form1 nuevoFormulario = new Form1();
            nuevoFormulario.Show();
            this.Hide();
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void Menu_principal_Load(object sender, EventArgs e)
        {

        }

        private void btnAdmSecre_Click(object sender, EventArgs e)
        {
            adm_Secretaria nuevoFormulario = new adm_Secretaria(); // creas una instancia del otro form
            nuevoFormulario.Show();              // lo muestras
            this.Hide();
        }

        private void btn_inscripcion_Click(object sender, EventArgs e)
        {
            Mod_inscripcion nuevoFormulario = new Mod_inscripcion("Director"); // creas una instancia del otro form
            nuevoFormulario.Show();              // lo muestras
            this.Hide();
        }

        private void btn_admaestros_Click(object sender, EventArgs e)
        {
            adm_maestros nuevoForulario = new adm_maestros(); // creas una instancia del otro form
            nuevoForulario.Show();              // lo muestras
            this.Hide();
        }

        private void btnEdicionDatos_Click(object sender, EventArgs e)
        {

        }

        private void btnEnvioBoletas_Click(object sender, EventArgs e)
        {
            ModEdicDatos_Direc nuevoFormulario = new ModEdicDatos_Direc();
            nuevoFormulario.Show();
            this.Hide();
        }

        private void btnBitacora_Click(object sender, EventArgs e)
        {
            Bitacora nuevoFormulario = new Bitacora();
            nuevoFormulario.Show();
            this.Hide();
        }
    }
}
