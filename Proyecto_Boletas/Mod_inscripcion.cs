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
    public partial class Mod_inscripcion : Form
    {
        private string rolUsuario;
        public Mod_inscripcion(string rol)
        {
            InitializeComponent();
            rolUsuario = rol;
        }


        private void btnVolver_Click(object sender, EventArgs e)
        {

            if (rolUsuario == "Secretaria")
            {
                Form_Secretaria formSecretaria = new Form_Secretaria();
                formSecretaria.Show();
                this.Hide();
            }
            else if (rolUsuario == "Director")
            {
                Menu_principal formDirector = new Menu_principal();
                formDirector.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Rol no reconocido.");
            }



        }
    }
}
