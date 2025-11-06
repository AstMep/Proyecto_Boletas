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

        private Conexion conexion = new Conexion();
        private const int LIMITE_TOTAL_MAESTROS = 6;
        private int idMaestroEditando = 0;

        public adm_maestros()
        {
            InitializeComponent();
            txtammaestro.MaxLength = 25;
            txtnombremaestro.MaxLength = 25;
            txtcorreomaestro.MaxLength = 65;
            txtapmaestro.MaxLength = 25;
            txtnombremaestro.CharacterCasing = CharacterCasing.Upper;
            txtapmaestro.CharacterCasing = CharacterCasing.Upper;
            txtammaestro.CharacterCasing = CharacterCasing.Upper;
            txtcorreomaestro.CharacterCasing = CharacterCasing.Lower;

            LlenarComboGrados();


            MostrarMaestros();
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

                        // Si id_maestro es NULL, nombre_grupo será NULL, por lo que muestra "Sin grupo".
                        string grupoTemp = reader["nombre_grupo"] == DBNull.Value ? "Sin grupo" : reader["nombre_grupo"].ToString();

                        string nombreCompleto = $"{nombreTemp} {apPTemp} {apMTemp}";

                        // Creación del Panel y controles
                        Panel card = new Panel
                        {
                            Width = 350,
                            Height = 120,
                            Margin = new Padding(10),
                            BackColor = Color.Bisque,
                            BorderStyle = BorderStyle.FixedSingle,
                            Cursor = Cursors.Hand
                        };

                        // Configuración de controles... (Pic, Labels, Botones)
                        // Ajuste de posiciones
                        int offsetX = 10;
                        int offsetY = 10;

                        PictureBox picMaestro = new PictureBox
                        {
                            Image = Image.FromFile(Application.StartupPath + @"\Iconos\secretaria45.png"),
                            SizeMode = PictureBoxSizeMode.Zoom,
                            Location = new Point(offsetX, offsetY),
                            Size = new Size(32, 32)
                        };

                        Label lblNombre = new Label
                        {
                            Text = nombreCompleto,
                            Font = new Font("Segoe UI", 10, FontStyle.Bold),
                            Location = new Point(offsetX + 40, offsetY + 5),
                            AutoSize = true,
                            MaximumSize = new Size(200, 0)
                        };

                        offsetY += 40;

                        PictureBox picCorreo = new PictureBox
                        {
                            Image = Image.FromFile(Application.StartupPath + @"\Iconos\correo45.png"),
                            SizeMode = PictureBoxSizeMode.Zoom,
                            Location = new Point(offsetX, offsetY),
                            Size = new Size(20, 20)
                        };

                        Label lblCorreo = new Label
                        {
                            Text = correoTemp,
                            Font = new Font("Segoe UI", 8),
                            Location = new Point(offsetX + 30, offsetY + 2),
                            AutoSize = true,
                            MaximumSize = new Size(250, 0)
                        };

                        offsetY += 25;

                        PictureBox picGrupo = new PictureBox
                        {
                            Image = Image.FromFile(Application.StartupPath + @"\Iconos\grupo45.png"),
                            SizeMode = PictureBoxSizeMode.Zoom,
                            Location = new Point(offsetX, offsetY),
                            Size = new Size(20, 20)
                        };

                        Label lblGrupo = new Label
                        {
                            Text = $"Grupo: {grupoTemp}",
                            Font = new Font("Segoe UI", 9, FontStyle.Italic),
                            Location = new Point(offsetX + 30, offsetY + 2),
                            AutoSize = true,
                            ForeColor = Color.DarkBlue
                        };

                        // Botones de acción
                        Button btnEditar = new Button
                        {
                            Size = new Size(30, 25),
                            Location = new Point(card.Width - 80, 10),
                            FlatStyle = FlatStyle.Flat,
                            BackColor = Color.SandyBrown,
                            BackgroundImageLayout = ImageLayout.Zoom,
                            BackgroundImage = Image.FromFile(Application.StartupPath + @"\Iconos\editor32.png")
                        };
                        // Se llama al método EditarMaestro con los datos del maestro y su ID
                        btnEditar.Click += (s, e) => EditarMaestro(idTemp, nombreTemp, apPTemp, apMTemp, correoTemp, grupoTemp);

                        Button btnEliminar = new Button
                        {
                            Size = new Size(30, 25),
                            Location = new Point(card.Width - 40, 10),
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



        private void EditarMaestro(int id, string nombre, string apP, string apM, string correo, string grupo)
        {
            // 2. Almacena el ID del maestro que se va a editar
            this.idMaestroEditando = id;

            txtnombremaestro.Text = nombre;
            txtapmaestro.Text = apP;
            txtammaestro.Text = apM;
            txtcorreomaestro.Text = correo;

            // Seleccionar el grupo actual del maestro en el ComboBox
            SeleccionarGrupoEnCombo(grupo);

            // Se asume que aquí habilitas un botón de "Guardar Cambios" y deshabilitas "Alta"
            // Ejemplo: btnAltaMaestros.Visible = false; btnGuardarCambios.Visible = true;
            MessageBox.Show($"Modo Edición: Puedes editar los datos de {nombre} {apP} y reasignar su grupo.", "Editar Maestro", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void SeleccionarGrupoEnCombo(string nombreGrupo)
        {
            if (nombreGrupo == "Sin grupo")
            {
                grupo_asignado.SelectedIndex = -1; // Deseleccionar o dejar el valor por defecto
                return;
            }

            foreach (ComboboxItem item in grupo_asignado.Items)
            {
                if (item.Value == nombreGrupo)
                {
                    grupo_asignado.SelectedItem = item;
                    return;
                }
            }
        }


        private void EliminarMaestro(int id)
        {
            string advertencia = "¿Seguro que deseas eliminar este maestro? ATENCIÓN: Esta acción eliminará al maestro, el grupo al que está asignado y a TODOS LOS ALUMNOS Y REGISTROS asociados a ese grupo. Esta acción es irreversible.";

            if (MessageBox.Show(advertencia, "Confirmar Eliminación Total", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                using (MySqlConnection conn = conexion.GetConnection())
                {
                    try
                    {
                        conn.Open();

                        // 1. OBTENER el ID del GRUPO asignado al maestro (Si existe)
                        string queryGetGrupo = "SELECT id_grupo FROM grupo WHERE id_maestro = @idMaestro";
                        MySqlCommand cmdGetGrupo = new MySqlCommand(queryGetGrupo, conn);
                        cmdGetGrupo.Parameters.AddWithValue("@idMaestro", id);
                        object result = cmdGetGrupo.ExecuteScalar();

                        int idGrupo = result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;

                        if (idGrupo > 0)
                        {
                            // 2. ELIMINAR LOS ALUMNOS (Tabla más dependiente)
                            string queryDeleteAlumnos = "DELETE FROM alumnos WHERE id_grupo = @idGrupo";
                            MySqlCommand cmdDeleteAlumnos = new MySqlCommand(queryDeleteAlumnos, conn);
                            cmdDeleteAlumnos.Parameters.AddWithValue("@idGrupo", idGrupo);
                            cmdDeleteAlumnos.ExecuteNonQuery();

                            // 3. ELIMINAR EL GRUPO
                            string queryDeleteGrupo = "DELETE FROM grupo WHERE id_grupo = @idGrupo";
                            MySqlCommand cmdDeleteGrupo = new MySqlCommand(queryDeleteGrupo, conn);
                            cmdDeleteGrupo.Parameters.AddWithValue("@idGrupo", idGrupo);
                            cmdDeleteGrupo.ExecuteNonQuery();
                        }

                        // 4. ELIMINAR EL MAESTRO.
                        string queryDeleteMaestro = "DELETE FROM maestro WHERE id_maestro=@idMaestro";
                        MySqlCommand cmdDelete = new MySqlCommand(queryDeleteMaestro, conn);
                        cmdDelete.Parameters.AddWithValue("@idMaestro", id);
                        cmdDelete.ExecuteNonQuery();

                        MessageBox.Show("Maestro, grupo y todos los alumnos asociados eliminados correctamente.", "Eliminación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MostrarMaestros();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error crítico al eliminar el registro: " + ex.Message, "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        private int ObtenerIdMaestroAsignadoAGrupo(string nombreGrupo)
        {
            using (MySqlConnection conn = conexion.GetConnection())
            {
                conn.Open();

                string query = "SELECT id_maestro FROM grupo WHERE nombre_grupo = @grupo AND id_maestro IS NOT NULL";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@grupo", nombreGrupo);
                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
                return 0;
            }
        }

        private bool DatoExistenteNombreCompleto(string nombreCompleto)
        {
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


        private void btnAltaMaestros_Click_1(object sender, EventArgs e)
        {

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

        private void InsertarNuevoMaestro(string nombre, string apellidoP, string apellidoM, string correo, string grupoSeleccionado, MySqlConnection conn)
        {

            MySqlCommand cmdInsert = new MySqlCommand(
                "INSERT INTO maestro (NombreMaestro, ApellidoPMaestro, ApellidoMMaestro, Correo_maestro) " +
                "VALUES (@nombre, @apP, @apM, @correo); SELECT LAST_INSERT_ID();", conn);
            cmdInsert.Parameters.AddWithValue("@nombre", nombre);
            cmdInsert.Parameters.AddWithValue("@apP", apellidoP);
            cmdInsert.Parameters.AddWithValue("@apM", apellidoM);
            cmdInsert.Parameters.AddWithValue("@correo", correo);
            int idMaestro = Convert.ToInt32(cmdInsert.ExecuteScalar());

            MySqlCommand cmdUpdateGrupo = new MySqlCommand(
                "UPDATE grupo SET id_maestro=@idMaestro WHERE nombre_grupo=@grupo", conn);
            cmdUpdateGrupo.Parameters.AddWithValue("@idMaestro", idMaestro);
            cmdUpdateGrupo.Parameters.AddWithValue("@grupo", grupoSeleccionado);
            cmdUpdateGrupo.ExecuteNonQuery();

            MessageBox.Show($"Maestro registrado y asignado al grupo {grupoSeleccionado}.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);


            LimpiarYRestablecerModo();
        }


        private void ActualizarMaestro(string nombre, string apellidoP, string apellidoM, string correo, string grupoSeleccionado, MySqlConnection conn)
        {

            MySqlCommand cmdDesasignar = new MySqlCommand(
                "UPDATE grupo SET id_maestro = NULL WHERE id_maestro = @idMaestroAntiguo", conn);
            cmdDesasignar.Parameters.AddWithValue("@idMaestroAntiguo", this.idMaestroEditando);
            cmdDesasignar.ExecuteNonQuery();

            MySqlCommand cmdUpdateMaestro = new MySqlCommand(
                "UPDATE maestro SET NombreMaestro=@nombre, ApellidoPMaestro=@apP, ApellidoMMaestro=@apM, Correo_maestro=@correo " +
                "WHERE id_maestro=@idMaestro", conn);
            cmdUpdateMaestro.Parameters.AddWithValue("@nombre", nombre);
            cmdUpdateMaestro.Parameters.AddWithValue("@apP", apellidoP);
            cmdUpdateMaestro.Parameters.AddWithValue("@apM", apellidoM);
            cmdUpdateMaestro.Parameters.AddWithValue("@correo", correo);
            cmdUpdateMaestro.Parameters.AddWithValue("@idMaestro", this.idMaestroEditando);
            cmdUpdateMaestro.ExecuteNonQuery();


            MySqlCommand cmdAsignar = new MySqlCommand(
                "UPDATE grupo SET id_maestro = @idMaestro WHERE nombre_grupo = @grupo", conn);
            cmdAsignar.Parameters.AddWithValue("@idMaestro", this.idMaestroEditando);
            cmdAsignar.Parameters.AddWithValue("@grupo", grupoSeleccionado);
            cmdAsignar.ExecuteNonQuery();

            MessageBox.Show("Maestro actualizado y reasignado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);


            LimpiarYRestablecerModo();
        }
        private void LimpiarYRestablecerModo()
        {
            txtnombremaestro.Clear();
            txtapmaestro.Clear();
            txtammaestro.Clear();
            txtcorreomaestro.Clear();
            grupo_asignado.SelectedIndex = 0;
            this.idMaestroEditando = 0; // Sale de modo edición

            MostrarMaestros();
        }
        private void adm_maestros_Load(object sender, EventArgs e)
        {

        }

        private void btnAltaMaestros_Click(object sender, EventArgs e)
        {

            // Validaciones iniciales (campos vacíos, formato)
            string nombre = txtnombremaestro.Text.Trim();
            string apellidoP = txtapmaestro.Text.Trim();
            string apellidoM = txtammaestro.Text.Trim();
            string correo = txtcorreomaestro.Text.Trim();

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
                using (MySqlConnection conn = conexion.GetConnection())
                {
                    conn.Open();


                    string queryNombreDuplicado =
                        "SELECT COUNT(*) FROM maestro WHERE CONCAT(LOWER(NombreMaestro),' ',LOWER(ApellidoPMaestro),' ',LOWER(ApellidoMMaestro))=@nombre AND id_maestro != @idActual";

                    MySqlCommand cmdNombre = new MySqlCommand(queryNombreDuplicado, conn);
                    cmdNombre.Parameters.AddWithValue("@nombre", nombreCompleto);
                    cmdNombre.Parameters.AddWithValue("@idActual", this.idMaestroEditando);
                    if (Convert.ToInt32(cmdNombre.ExecuteScalar()) > 0)
                    {
                        MessageBox.Show("Ya existe otro maestro con ese nombre completo.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }


                    string queryCorreoDuplicado =
                        "SELECT COUNT(*) FROM maestro WHERE LOWER(Correo_maestro)=@correo AND id_maestro != @idActual";

                    MySqlCommand cmdCorreo = new MySqlCommand(queryCorreoDuplicado, conn);
                    cmdCorreo.Parameters.AddWithValue("@correo", correo.ToLower());
                    cmdCorreo.Parameters.AddWithValue("@idActual", this.idMaestroEditando);
                    if (Convert.ToInt32(cmdCorreo.ExecuteScalar()) > 0)
                    {
                        MessageBox.Show("Correo ya registrado por otro maestro.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }


                    if (this.idMaestroEditando == 0)
                    {

                        MySqlCommand cmdCount = new MySqlCommand("SELECT COUNT(id_maestro) FROM maestro", conn);
                        int cantidadMaestros = Convert.ToInt32(cmdCount.ExecuteScalar());
                        if (cantidadMaestros >= LIMITE_TOTAL_MAESTROS)
                        {
                            MessageBox.Show($"Ya no puedes registrar más maestros. El límite es {LIMITE_TOTAL_MAESTROS}.", "Límite alcanzado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        int idMaestroExistente = ObtenerIdMaestroAsignadoAGrupo(grupoSeleccionado);
                        if (idMaestroExistente > 0)
                        {
                            MySqlCommand cmdName = new MySqlCommand(
                               "SELECT CONCAT(NombreMaestro, ' ', ApellidoPMaestro) FROM maestro WHERE id_maestro = @id", conn);
                            cmdName.Parameters.AddWithValue("@id", idMaestroExistente);
                            string nombreMaestroExistente = cmdName.ExecuteScalar()?.ToString() ?? "Maestro Desconocido";

                            MessageBox.Show(
                                $"El grupo '{grupoSeleccionado}' ya tiene un maestro asignado: {nombreMaestroExistente}.",
                                "Límite de Grupo Alcanzado",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            return;
                        }


                        InsertarNuevoMaestro(nombre, apellidoP, apellidoM, correo, grupoSeleccionado, conn);
                    }
                    else
                    {

                        int idMaestroOcupante = ObtenerIdMaestroAsignadoAGrupo(grupoSeleccionado);
                        if (idMaestroOcupante > 0 && idMaestroOcupante != this.idMaestroEditando)
                        {
                            MessageBox.Show($"El grupo '{grupoSeleccionado}' ya está asignado a otro maestro. Debes reasignarlo primero.", "Grupo Ocupado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }


                        ActualizarMaestro(nombre, apellidoP, apellidoM, correo, grupoSeleccionado, conn);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la validación o registro: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void grupo_asignado_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void flowMaestros_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_ingresar_Click(object sender, EventArgs e)
        {
            Form1 nuevoFormulario = new Form1();
            nuevoFormulario.Show();
            this.Close();
        }

        private void btn_inscripcion_Click(object sender, EventArgs e)
        {
            Mod_inscripcion inscripcion = new Mod_inscripcion("Director");
            inscripcion.Show();
            this.Hide();

        }

        private void btn_capturaCalif_Click(object sender, EventArgs e)
        {
            Mod_capCal cap_calificacion = new Mod_capCal("Director");
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

        private void btnEnvioBoletas_Click(object sender, EventArgs e)
        {
            CreacionPDF_Direc pdf_director = new CreacionPDF_Direc();
            pdf_director.Show();
            this.Hide();
        }

        private void btnEdicionDatos_Click(object sender, EventArgs e)
        {
            Mod_Modificacion modificacion = new Mod_Modificacion();
            modificacion.Show();
            this.Hide();
        }

        private void btnEstadisticas_Click(object sender, EventArgs e)
        {
            Mod_Estadisticas estadisticas = new Mod_Estadisticas("Director");
            estadisticas.Show();
            this.Hide();
        }
    }

}

