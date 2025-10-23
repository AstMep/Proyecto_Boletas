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
    public partial class CreacionPDF_Direc : Form
    {
        private string rolUsuario;

        public CreacionPDF_Direc(string rol = "Director")
        {
            InitializeComponent();
            rolUsuario = rol;
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
            OcultarBotonesPorRol();
            CargarGrupos();
            cmbTrimestreGrup.Items.AddRange(new string[] { "1er Trimestre", "2do Trimestre", "3er Trimestre" });
            LlenarComboTrimestres();
        }

        private void CargarGrupos()
        {
            try
            {
                using (MySqlConnection conn = new Conexion().GetConnection())
                {
                    conn.Open();
                    string query = "SELECT id_grupo, nombre_grupo FROM grupo";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        // 🎯 Limpiamos y llenamos AMBOS ComboBoxes de grupo
                        cmbGrup.Items.Clear();      // ComboBox de Boleta Grupal
                        cbGrupoPer.Items.Clear();   // ComboBox de Boleta Personal

                        while (dr.Read())
                        {
                            ComboboxItem item = new ComboboxItem
                            {
                                Text = dr["nombre_grupo"].ToString(),
                                Value = dr["id_grupo"]
                            };

                            cmbGrup.Items.Add(item);
                            cbGrupoPer.Items.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar grupos: " + ex.Message);
            }
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


        private void btnGenerarListas_Click_1(object sender, EventArgs e)
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

        private void panelApp_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnGenerarLisProf_Click_1(object sender, EventArgs e)
        {

            try
            {

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

        private void btn_capturaCalif_Click(object sender, EventArgs e)
        {
            Mod_capCal cap_calificacion = new Mod_capCal(rolUsuario);
            cap_calificacion.Show();
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

        private void btn_admaestros_Click(object sender, EventArgs e)
        {
            adm_maestros administrar_maestros = new adm_maestros();
            administrar_maestros.Show();
            this.Hide();
        }

        private void btnEnvioBoletas_Click(object sender, EventArgs e)
        {

        }



        private void cmbGrup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGrup.SelectedItem != null)
            {
                int idGrupo = (int)((ComboboxItem)cmbGrup.SelectedItem).Value;

                try
                {
                    using (MySqlConnection conn = new Conexion().GetConnection())
                    {
                        conn.Open();
                        string query = "SELECT AlumnoID, Nombre, ApellidoPaterno, ApellidoMaterno FROM alumnos WHERE id_grupo = @idGrupo";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@idGrupo", idGrupo);

                            using (MySqlDataReader dr = cmd.ExecuteReader())
                            {
   
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar alumnos: " + ex.Message);
                }
            }
        }




        private void cmbTrimestre_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnGenerarBoletas_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbGrup.SelectedItem == null)
                {
                    MessageBox.Show("Selecciona un grupo para generar la boleta grupal.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cmbTrimestreGrup.SelectedItem == null)
                {
                    MessageBox.Show("Selecciona un trimestre.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idGrupo = (int)((ComboboxItem)cmbGrup.SelectedItem).Value;
                string trimestre = cmbTrimestreGrup.SelectedItem.ToString();

                // Generar Boleta Grupal (Asumo que esta clase es la que creamos antes: GeneradorBoletaT)
                GeneradorBoletaT generador = new GeneradorBoletaT();
                generador.CrearBoletaGrupal(idGrupo, trimestre);

                MessageBox.Show("Boleta grupal generada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar la boleta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbGrupoPer_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbAlumno.Items.Clear();

            if (cbGrupoPer.SelectedItem != null)
            {
                int idGrupo = (int)((ComboboxItem)cbGrupoPer.SelectedItem).Value;

                try
                {
                    using (MySqlConnection conn = new Conexion().GetConnection())
                    {
                        conn.Open();
                        string query = "SELECT AlumnoID, Nombre, ApellidoPaterno, ApellidoMaterno FROM alumnos WHERE id_grupo = @idGrupo ORDER BY ApellidoPaterno";

                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@idGrupo", idGrupo);

                            using (MySqlDataReader dr = cmd.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    string nombreCompleto = $"{dr["ApellidoPaterno"]} {dr["ApellidoMaterno"]} {dr["Nombre"]}";

                                    cbAlumno.Items.Add(new ComboboxItem
                                    {
                                        Text = nombreCompleto,
                                        Value = dr["AlumnoID"]
                                    });
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar alumnos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LlenarComboTrimestres()
        {
            // Aseguramos que solo llenamos el ComboBox de la Boleta Personal
            cbTrimestrePer.Items.Clear();
            cbTrimestrePer.Items.AddRange(new string[] { "1er Trimestre", "2do Trimestre", "3er Trimestre" });
        }

        private void cbAlumno_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbTrimestrePer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cbGrupoPer.SelectedItem == null || cbAlumno.SelectedItem == null || cbTrimestrePer.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona Grupo, Alumno y Trimestre.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int idAlumno = (int)((ComboboxItem)cbAlumno.SelectedItem).Value;
                string trimestre = cbTrimestrePer.SelectedItem.ToString();

                // Crear el generador de boleta
                GeneradorBoletaP generador = new GeneradorBoletaP();

                // Genera la boleta personal del alumno seleccionado
                generador.CrearBoletaPersonal(idAlumno, trimestre);

                MessageBox.Show("Boleta generada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar la boleta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panelito4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmbGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
 
