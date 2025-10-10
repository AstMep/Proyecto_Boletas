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
    public partial class ModEdicDatos_Direc : Form
    {
        public ModEdicDatos_Direc()
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


        private void ModEdicDatos_Direc_Load(object sender, EventArgs e)
        {
            LlenarComboGrupos();
            LlenarComboMeses();
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

        private void btnGenerarLisProf_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Crear el diálogo para que el usuario elija dónde guardar el archivo
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Archivos PDF (*.pdf)|*.pdf";
                saveFileDialog.FileName = "Lista_Profesores_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf";
                saveFileDialog.Title = "Guardar Lista de Profesores";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string rutaArchivo = saveFileDialog.FileName;

                    // 2. Crear instancia del generador y ejecutar la generación del PDF
                    GeneradorListaProf generador = new GeneradorListaProf();
                    generador.GenerarLista(rutaArchivo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar la generación de la lista de profesores: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
