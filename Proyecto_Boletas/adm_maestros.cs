using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Boletas
{
    public partial class adm_maestros : Form
    {
        public adm_maestros()
        {
            InitializeComponent();
            // Limitar longitud de TextBox
            txtammaestro.MaxLength = 50;
            txtnombremaestro.MaxLength = 50;
            txtcorreomaestro.MaxLength = 50;
            txtapmaestro.MaxLength = 50;

            // Llenar ComboBox de grados
            LlenarComboGrados();

            // Mostrar maestros existentes
            MostrarMaestros();
        }


        

        private void btnVolver_Click(object sender, EventArgs e)
        {
            if (btnVolver.Enabled)
            {
                Menu_principal nuevoFormulario = new Menu_principal(); // creas una instancia del otro form
                nuevoFormulario.Show();              // lo muestras
                this.Hide();
            }
        }
        public class ComboboxItem
        {
            public string Text { get; set; }
            public string Value { get; set; }
            public override string ToString() => Text;
        }

        private void LlenarComboGrados()
        {
            grupo_asignado.Items.Clear();
            string[] grados = { "Primero", "Segundo", "Tercero", "Cuarto", "Quinto", "Sexto" };

            foreach (string g in grados)
            {
                ComboboxItem item = new ComboboxItem { Text = g, Value = g };
                grupo_asignado.Items.Add(item);
            }

            grupo_asignado.DisplayMember = "Text";
            grupo_asignado.ValueMember = "Value";

            if (grupo_asignado.Items.Count > 0)
                grupo_asignado.SelectedIndex = 0;
        }

        private void MostrarMaestros()
        {
            try
            {
                flowMaestros.Controls.Clear();
                Conexion conexion = new Conexion();
                using (MySqlConnection conn = conexion.GetConnection())
                {
                    conn.Open();
                    string query = @"
                        SELECT m.id_maestro, m.NombreMaestro, m.ApellidoPMaestro, m.ApellidoMMaestro, m.Correo_maestro,
                               g.nombre_grupo
                        FROM maestro m
                        LEFT JOIN grupo g ON g.id_maestro = m.id_maestro
                        ORDER BY m.id_maestro";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int idTemp = Convert.ToInt32(reader["id_maestro"]);
                        string nombreTemp = reader["NombreMaestro"].ToString();
                        string apPTemp = reader["ApellidoPMaestro"].ToString();
                        string apMTemp = reader["ApellidoMMaestro"].ToString();
                        string correoTemp = reader["Correo_maestro"].ToString();
                        string grupoTemp = reader["nombre_grupo"] == DBNull.Value ? "Sin grupo" : reader["nombre_grupo"].ToString();

                        string nombreCompleto = $"{nombreTemp} {apPTemp} {apMTemp}";

                        Panel card = new Panel
                        {
                            Width = 300,
                            Height = 120,
                            Margin = new Padding(10),
                            BackColor = Color.Bisque,
                            BorderStyle = BorderStyle.FixedSingle,
                            Cursor = Cursors.Hand
                        };

                        PictureBox picMaestro = new PictureBox
                        {
                            Image = Image.FromFile(Application.StartupPath + @"\Iconos\secretaria45.png"),
                            SizeMode = PictureBoxSizeMode.Zoom,
                            Location = new Point(10, 10),
                            Size = new Size(32, 32)
                        };

                        Label lblNombre = new Label
                        {
                            Text = nombreCompleto,
                            Font = new Font("Segoe UI", 10, FontStyle.Bold),
                            Location = new Point(50, 10),
                            AutoSize = true
                        };

                        PictureBox picCorreo = new PictureBox
                        {
                            Image = Image.FromFile(Application.StartupPath + @"\Iconos\correo45.png"),
                            SizeMode = PictureBoxSizeMode.Zoom,
                            Location = new Point(10, 50),
                            Size = new Size(32, 32)
                        };

                        Label lblCorreo = new Label
                        {
                            Text = correoTemp,
                            Font = new Font("Segoe UI", 9),
                            Location = new Point(50, 52),
                            AutoSize = true
                        };

                        PictureBox picGrupo = new PictureBox
                        {
                            Image = Image.FromFile(Application.StartupPath + @"\Iconos\grupo45.png"),
                            SizeMode = PictureBoxSizeMode.Zoom,
                            Location = new Point(10, 85),
                            Size = new Size(32, 32)
                        };

                        Label lblGrupo = new Label
                        {
                            Text = grupoTemp,
                            Font = new Font("Segoe UI", 9, FontStyle.Italic),
                            Location = new Point(50, 87),
                            AutoSize = true,
                            ForeColor = Color.DarkBlue
                        };

                        Button btnEditar = new Button
                        {
                            Size = new Size(30, 25),
                            Location = new Point(220, 10),
                            FlatStyle = FlatStyle.Flat,
                            BackColor = Color.SandyBrown,
                            BackgroundImageLayout = ImageLayout.Zoom,
                            BackgroundImage = Image.FromFile(Application.StartupPath + @"\Iconos\editor32.png")
                        };
                        btnEditar.Click += (s, e) => EditarMaestro(nombreTemp, apPTemp, apMTemp, correoTemp);

                        Button btnEliminar = new Button
                        {
                            Size = new Size(30, 25),
                            Location = new Point(260, 10),
                            FlatStyle = FlatStyle.Flat,
                            BackColor = Color.IndianRed,
                            BackgroundImageLayout = ImageLayout.Zoom,
                            BackgroundImage = Image.FromFile(Application.StartupPath + @"\Iconos\delete32.png")
                        };
                        btnEliminar.Click += (s, e) => EliminarMaestro(idTemp);

                        card.Controls.Add(picMaestro);
                        card.Controls.Add(lblNombre);
                        card.Controls.Add(picCorreo);
                        card.Controls.Add(lblCorreo);
                        card.Controls.Add(picGrupo);
                        card.Controls.Add(lblGrupo);
                        card.Controls.Add(btnEditar);
                        card.Controls.Add(btnEliminar);

                        flowMaestros.Controls.Add(card);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar maestros: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

 

        private void EditarMaestro(string nombre, string apP, string apM, string correo)
        {
            txtnombremaestro.Text = nombre;
            txtapmaestro.Text = apP;
            txtammaestro.Text = apM;
            txtcorreomaestro.Text = correo;
            MessageBox.Show($"Ahora puedes editar los datos de {nombre} {apP}.", "Editar", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void EliminarMaestro(int id)
        {
            if (MessageBox.Show($"¿Seguro que deseas eliminar este maestro?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Conexion conexion = new Conexion();
                using (MySqlConnection conn = conexion.GetConnection())
                {
                    conn.Open();
                    string query = "DELETE FROM maestro WHERE id_maestro=@id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Maestro eliminado correctamente.", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarMaestros();
            }
        }

        private bool DatoExistenteNombreCompleto(string nombreCompleto)
        {
            Conexion conexion = new Conexion();
            using (MySqlConnection conn = conexion.GetConnection())
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM maestro WHERE CONCAT(LOWER(NombreMaestro), ' ', LOWER(ApellidoPMaestro), ' ', LOWER(ApellidoMMaestro)) = @valor";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@valor", nombreCompleto);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        private bool DatoExistenteCorreo(string correo)
        {
            Conexion conexion = new Conexion();
            using (MySqlConnection conn = conexion.GetConnection())
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM maestro WHERE LOWER(Correo_maestro) = @correo";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@correo", correo);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            int borderRadius = 25;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, panel1.Width, panel1.Height);
            int diameter = borderRadius * 2;

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseAllFigures();

            panel1.Region = new Region(path);
        }

        private void btnAltaMaestros_Click_1(object sender, EventArgs e)
        {
            string nombre = txtnombremaestro.Text.Trim();
            string apellidoP = txtapmaestro.Text.Trim();
            string apellidoM = txtammaestro.Text.Trim();
            string correo = txtcorreomaestro.Text.Trim();

            // Validaciones
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellidoP) ||
                string.IsNullOrWhiteSpace(apellidoM) || string.IsNullOrWhiteSpace(correo))
            {
                MessageBox.Show("Completa todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(nombre, @"^[A-Za-zÁÉÍÓÚáéíóúÑñ ]{2,}$") ||
                !Regex.IsMatch(apellidoP, @"^[A-Za-zÁÉÍÓÚáéíóúÑñ ]{2,}$") ||
                !Regex.IsMatch(apellidoM, @"^[A-Za-zÁÉÍÓÚáéíóúÑñ ]{2,}$"))
            {
                MessageBox.Show("Los nombres y apellidos solo letras y mínimo 2 caracteres.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Correo inválido.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener grupo seleccionado de forma segura
            ComboboxItem grupoItem = grupo_asignado.SelectedItem as ComboboxItem;
            if (grupoItem == null)
            {
                MessageBox.Show("Selecciona un grupo.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string grupoSeleccionado = grupoItem.Value;

            string nombreCompleto = $"{nombre.ToLower()} {apellidoP.ToLower()} {apellidoM.ToLower()}";

            try
            {
                Conexion conexion = new Conexion();
                using (MySqlConnection conn = conexion.GetConnection())
                {
                    conn.Open();

                    // Validar duplicados por nombre completo
                    MySqlCommand cmdNombre = new MySqlCommand(
                        "SELECT COUNT(*) FROM maestro WHERE CONCAT(LOWER(NombreMaestro),' ',LOWER(ApellidoPMaestro),' ',LOWER(ApellidoMMaestro))=@nombre", conn);
                    cmdNombre.Parameters.AddWithValue("@nombre", nombreCompleto);
                    if (Convert.ToInt32(cmdNombre.ExecuteScalar()) > 0)
                    {
                        MessageBox.Show("Ya existe un maestro con ese nombre completo.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Validar duplicado por correo
                    MySqlCommand cmdCorreo = new MySqlCommand(
                        "SELECT COUNT(*) FROM maestro WHERE LOWER(Correo_maestro)=@correo", conn);
                    cmdCorreo.Parameters.AddWithValue("@correo", correo.ToLower());
                    if (Convert.ToInt32(cmdCorreo.ExecuteScalar()) > 0)
                    {
                        MessageBox.Show("Correo ya registrado.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Límite de 9 maestros
                    MySqlCommand cmdCount = new MySqlCommand("SELECT COUNT(*) FROM maestro", conn);
                    int cantidadMaestros = Convert.ToInt32(cmdCount.ExecuteScalar());
                    if (cantidadMaestros >= 9)
                    {
                        MessageBox.Show("Ya no puedes registrar más maestros. El límite es 9.", "Límite alcanzado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Insertar maestro
                    MySqlCommand cmdInsert = new MySqlCommand(
                        "INSERT INTO maestro (NombreMaestro, ApellidoPMaestro, ApellidoMMaestro, Correo_maestro) " +
                        "VALUES (@nombre, @apP, @apM, @correo); SELECT LAST_INSERT_ID();", conn);
                    cmdInsert.Parameters.AddWithValue("@nombre", nombre);
                    cmdInsert.Parameters.AddWithValue("@apP", apellidoP);
                    cmdInsert.Parameters.AddWithValue("@apM", apellidoM);
                    cmdInsert.Parameters.AddWithValue("@correo", correo);
                    int idMaestro = Convert.ToInt32(cmdInsert.ExecuteScalar());

                    // Asignar grupo
                    MySqlCommand cmdCheckGrupo = new MySqlCommand("SELECT id_grupo FROM grupo WHERE nombre_grupo=@grupo", conn);
                    cmdCheckGrupo.Parameters.AddWithValue("@grupo", grupoSeleccionado);
                    object result = cmdCheckGrupo.ExecuteScalar();
                    int idGrupo;

                    if (result == null)
                    {
                        // Crear grupo nuevo
                        MySqlCommand cmdInsertGrupo = new MySqlCommand(
                            "INSERT INTO grupo (nombre_grupo, id_maestro) VALUES (@grupo, @idMaestro); SELECT LAST_INSERT_ID();", conn);
                        cmdInsertGrupo.Parameters.AddWithValue("@grupo", grupoSeleccionado);
                        cmdInsertGrupo.Parameters.AddWithValue("@idMaestro", idMaestro);
                        idGrupo = Convert.ToInt32(cmdInsertGrupo.ExecuteScalar());
                    }
                    else
                    {
                        // Actualizar grupo existente
                        idGrupo = Convert.ToInt32(result);
                        MySqlCommand cmdUpdateGrupo = new MySqlCommand(
                            "UPDATE grupo SET id_maestro=@idMaestro WHERE id_grupo=@idGrupo", conn);
                        cmdUpdateGrupo.Parameters.AddWithValue("@idMaestro", idMaestro);
                        cmdUpdateGrupo.Parameters.AddWithValue("@idGrupo", idGrupo);
                        cmdUpdateGrupo.ExecuteNonQuery();
                    }

                    MessageBox.Show($"Maestro registrado y asignado al grupo {grupoSeleccionado}.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpiar campos
                    txtnombremaestro.Clear();
                    txtapmaestro.Clear();
                    txtammaestro.Clear();
                    txtcorreomaestro.Clear();
                    grupo_asignado.SelectedIndex = 0;

                    // Actualizar FlowLayoutPanel
                    MostrarMaestros();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar maestro: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    

    


        private void txtnombremaestro_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void grupo_asignado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}

