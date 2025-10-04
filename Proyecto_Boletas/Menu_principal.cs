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
            Form1 nuevoFormulario = new Form1(); // creas una instancia del otro form
            nuevoFormulario.Show();              // lo muestras
            this.Hide();
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void Menu_principal_Load(object sender, EventArgs e)
        {

        }
    }
}
