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
        private string rolUsuario;  // ⭐ Variable para almacenar el rol

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
            cmbTrimestre.Items.AddRange(new string[] { "1er Trimestre", "2do Trimestre", "3er Trimestre" });
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
                        while (dr.Read())
                        {
                            cmbGrup.Items.Add(new ComboboxItem
                            {
                                Text = dr["nombre_grupo"].ToString(),
                                Value = dr["id_grupo"]
                            });
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
            cmbAlumno.Items.Clear();

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
                                while (dr.Read())
                                {
                                    string nombreCompleto = $"{dr["ApellidoPaterno"]} {dr["ApellidoMaterno"]} {dr["Nombre"]}";
                                    cmbAlumno.Items.Add(new ComboboxItem
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
                    MessageBox.Show("Error al cargar alumnos: " + ex.Message);
                }
            }
        }

        private void cmbAlumno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAlumno.SelectedItem != null && cmbTrimestre.SelectedItem != null)
            {
                int idAlumno = (int)((ComboboxItem)cmbAlumno.SelectedItem).Value;
                string trimestre = cmbTrimestre.SelectedItem.ToString();

                GeneradorBoletaT generador = new GeneradorBoletaT();
                generador.CrearBoleta(idAlumno, trimestre); // Solo se llama sin asignar
            }
        }

        private void cmbTrimestre_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnGenerarBoletas_Click(object sender, EventArgs e)
        {
            try
            {
                // 🧩 Validar selección
                if (cmbAlumno.SelectedItem == null)
                {
                    MessageBox.Show("Selecciona un alumno.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cmbTrimestre.SelectedItem == null)
                {
                    MessageBox.Show("Selecciona un trimestre.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 🧩 Obtener datos seleccionados
                int idAlumno = Convert.ToInt32(((ComboboxItem)cmbAlumno.SelectedItem).Value);
                string trimestre = cmbTrimestre.SelectedItem.ToString();

                // 🧩 Generar la boleta
                GeneradorBoletaT generador = new GeneradorBoletaT();

                // ❌ ELIMINA CUALQUIER INTENTO DE ASIGNACIÓN COMO: 
                // string rutaArchivo = generador.CrearBoleta(idAlumno, trimestre);

                // ✅ SOLO SE LLAMA AL MÉTODO VOID
                generador.CrearBoleta(idAlumno, trimestre);

                // 🧩 Confirmar al usuario (El mensaje de éxito ya está dentro de CrearBoleta)
                // Puedes dejar este mensaje o eliminarlo si el mensaje dentro de CrearBoleta es suficiente.
                MessageBox.Show("Boleta generada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar la boleta: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
