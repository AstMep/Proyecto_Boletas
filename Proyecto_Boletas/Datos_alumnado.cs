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
using static Proyecto_Boletas.adm_maestros;

namespace Proyecto_Boletas
{
    public partial class Datos_alumnado : Form
    {
        public Datos_alumnado()
        {
            InitializeComponent();
        }

 public class ComboboxItem
{
    public string Text { get; set; }
    public object Value { get; set; }

    public override string ToString()
    {
        return Text; 
    }
}



        private void LlenarComboGrupos()
        {
            cmbGrupo.Items.Clear();

            using (MySqlConnection conn = new Conexion().GetConnection())
            {
                conn.Open();
                string query = "SELECT id_grupo, nombre_grupo FROM grupo";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ComboboxItem item = new ComboboxItem()
                    {
                        Text = reader["nombre_grupo"].ToString(),
                        Value = reader["id_grupo"]
                    };
                    cmbGrupo.Items.Add(item);
                }

                reader.Close();
            }
        }


        private void LlenarComboMeses()
        {
            cmbMes.Items.Clear(); 

            string[] meses = { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio",
                       "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

            cmbMes.Items.AddRange(meses);
        }





        private void btnGenerarListas_Click(object sender, EventArgs e)
        {
            if (cmbGrupo.SelectedItem == null || cmbMes.SelectedItem == null)
            {
                MessageBox.Show("Selecciona un grupo y un mes.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idGrupo = (int)((ComboboxItem)cmbGrupo.SelectedItem).Value; // Obtenemos el id real
            string mesSeleccionado = cmbMes.SelectedItem.ToString();


            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Archivo PDF|*.pdf";
            saveDialog.Title = "Guardar listas mensuales";
            saveDialog.FileName = $"Lista_{mesSeleccionado}.pdf";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                GeneradorListaA generador = new GeneradorListaA();
                generador.GenerarListasMensuales(saveDialog.FileName, idGrupo, mesSeleccionado);
            }
        }

        private void Datos_alumnado_Load(object sender, EventArgs e)
        {
            LlenarComboGrupos();
            LlenarComboMeses();
        }
    }
}
