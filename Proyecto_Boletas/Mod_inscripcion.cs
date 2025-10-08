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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void nombre_alumno_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnvalidar_alumno_Click(object sender, EventArgs e)
        {

        }

        private void apellidoP_alumno_TextChanged(object sender, EventArgs e)
        {

        }

        private void apellidoM_alumno_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCurp_TextChanged(object sender, EventArgs e)
        {

        }

        private void nacimiento_alumno_ValueChanged(object sender, EventArgs e)
        {

        }

        private void edad_alumno_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void grupo_alumno_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void nombre_tutor_TextChanged(object sender, EventArgs e)
        {

        }

        private void apellidoP_tutor_TextChanged(object sender, EventArgs e)
        {

        }

        private void apellidoM_tutor_TextChanged(object sender, EventArgs e)
        {

        }

        private void telefono_tutor_TextChanged(object sender, EventArgs e)
        {

        }

        private void correo_tutor_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnvalidar_tutor_Click(object sender, EventArgs e)
        {

        }

        private void btnalta_inscripcion_Click(object sender, EventArgs e)
        {

        }
    }
}
