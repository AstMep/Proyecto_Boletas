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
        public Mod_Estadisticas()
        {
            InitializeComponent();
        }

  
        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void Mod_Estadisticas_Load(object sender, EventArgs e)
        {
      
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
    }
}
