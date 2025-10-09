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
    public partial class adm_Secretaria : Form
    {
        // Variables para controlar la edición
        private bool modoEdicion = false;
        private int idSecretariaEditar = 0;
        private string nombreOriginal = "";
            
        public adm_Secretaria()
        {
            InitializeComponent();
            txtUsuarioSecre.MaxLength = 100;
            txtCorreoSecre.MaxLength = 100;
            txtContrasenaSecre.MaxLength = 10;
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }
        private void adm_Secretaria_Load(object sender, EventArgs e)
        {
            MostrarSecretarias();
        }


        private void btnAltaSecretarias_Click(object sender, EventArgs e)
        {

            string nombre = txtUsuarioSecre.Text.Trim();
            string correo = txtCorreoSecre.Text.Trim();
            string contrasena = txtContrasenaSecre.Text.Trim();
            string rol = "Secretaria";
            DateTime fechaRegistro = DateTime.Now;


            nombre = System.Text.RegularExpressions.Regex.Replace(nombre, @"\s+", " ");
            correo = System.Text.RegularExpressions.Regex.Replace(correo, @"\s+", "");
            contrasena = System.Text.RegularExpressions.Regex.Replace(contrasena, @"\s+", "");


            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(contrasena))
            {
                MessageBox.Show("Por favor, completa todos los campos.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nombre.Length > 20)
            {
                MessageBox.Show("El nombre de usuario no puede tener más de 20 caracteres.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (!System.Text.RegularExpressions.Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Ingresa un correo electrónico válido (ejemplo: nombre@dominio.com).", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (correo.Length > 65)
            {
                MessageBox.Show("El correo no puede tener más de 65 caracteres.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (!System.Text.RegularExpressions.Regex.IsMatch(contrasena, @"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]).{8,}$"))
            {
                MessageBox.Show("La contraseña debe tener al menos:\n- 1 mayúscula\n- 1 número\n- 1 carácter especial\n- Mínimo 8 caracteres.", "Contraseña inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (DatoExistente("Nombre", nombre.ToLower()))
            {
                MessageBox.Show("El nombre de usuario ya existe. Usa otro.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            try
            {
                Conexion conexion = new Conexion();
                using (MySqlConnection conn = conexion.GetConnection())
                {
                    conn.Open();

                    // ⭐ MODO EDICIÓN
                    if (modoEdicion)
                    {
                        // Verificar si el nuevo nombre ya existe (excepto el actual)
                        if (nombre.ToLower() != nombreOriginal.ToLower() && DatoExistente("Nombre", nombre.ToLower()))
                        {
                            MessageBox.Show("El nombre de usuario ya existe. Usa otro.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Actualizar secretaria existente
                        string queryUpdate = "UPDATE usuarios SET Nombre=@nombre, Correo=@correo, Contrasena=@contrasena WHERE UsuarioID=@id";
                        MySqlCommand cmdUpdate = new MySqlCommand(queryUpdate, conn);
                        cmdUpdate.Parameters.AddWithValue("@nombre", nombre);
                        cmdUpdate.Parameters.AddWithValue("@correo", correo);
                        cmdUpdate.Parameters.AddWithValue("@contrasena", contrasena);
                        cmdUpdate.Parameters.AddWithValue("@id", idSecretariaEditar);
                        cmdUpdate.ExecuteNonQuery();

                        MessageBox.Show("Secretaria actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Salir del modo edición
                        CancelarEdicion();
                    }
                    // ⭐ MODO INSERCIÓN (NUEVA SECRETARIA)
                    else
                    {
                        // Verificar límite de secretarias
                        string queryCount = "SELECT COUNT(*) FROM usuarios WHERE Rol='Secretaria'";
                        MySqlCommand cmdCount = new MySqlCommand(queryCount, conn);
                        int cantidadSecretarias = Convert.ToInt32(cmdCount.ExecuteScalar());

                        if (cantidadSecretarias >= 3)
                        {
                            MessageBox.Show("Ya no puedes registrar más secretarias. El límite es 3.", "Límite alcanzado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Verificar duplicados
                        if (DatoExistente("Nombre", nombre.ToLower()))
                        {
                            MessageBox.Show("El nombre de usuario ya existe. Usa otro.", "Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Insertar nueva secretaria
                        string query = "INSERT INTO usuarios (Nombre, Correo, Contrasena, Rol, FechaRegistro) VALUES (@nombre, @correo, @contrasena, @rol, @fecha)";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@correo", correo);
                        cmd.Parameters.AddWithValue("@contrasena", contrasena);
                        cmd.Parameters.AddWithValue("@rol", rol);
                        cmd.Parameters.AddWithValue("@fecha", fechaRegistro);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Secretaria registrada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // Limpiar campos y recargar
                    txtUsuarioSecre.Clear();
                    txtCorreoSecre.Clear();
                    txtContrasenaSecre.Clear();
                    MostrarSecretarias();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar secretaria: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarSecretarias()
        {
            try
            {
                flowSecretarias.Controls.Clear();

                Conexion conexion = new Conexion();
                using (MySqlConnection conn = conexion.GetConnection())
                {
                    conn.Open();

                    string query = "SELECT UsuarioID, Nombre, Correo, Contrasena FROM usuarios WHERE Rol = 'Secretaria'";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int idTemp = Convert.ToInt32(reader["UsuarioID"]);
                        string nombreTemp = reader["Nombre"].ToString();
                        string correoTemp = reader["Correo"].ToString();


                        Panel card = new Panel
                        {
                            Width = 300,
                            Height = 250,
                            Margin = new Padding(10),
                            BackColor = Color.Bisque,
                            BorderStyle = BorderStyle.FixedSingle,
                            Cursor = Cursors.Hand
                        };


                        PictureBox picSecretaria = new PictureBox
                        {
                            Image = Image.FromFile(Application.StartupPath + @"\Iconos\secretaria45.png"),
                            SizeMode = PictureBoxSizeMode.Zoom,
                            Location = new Point(10, 50),
                            Size = new Size(32, 32)
                        };


                        Label lblNombre = new Label
                        {
                            Text = nombreTemp,
                            Font = new Font("Segoe UI", 10, FontStyle.Bold),
                            Location = new Point(50, 50),
                            AutoSize = true
                        };


                        PictureBox picCorreo = new PictureBox
                        {
                            Image = Image.FromFile(Application.StartupPath + @"\Iconos\correo45.png"),
                            SizeMode = PictureBoxSizeMode.Zoom,
                            Location = new Point(10, 85),
                            Size = new Size(32, 32)
                        };


                        Label lblCorreo = new Label
                        {
                            Text = correoTemp,
                            Font = new Font("Segoe UI", 9),
                            Location = new Point(50, 85),
                            AutoSize = true
                        };


                        Button btnEditar = new Button
                        {
                            Size = new Size(35, 35),
                            Location = new Point(195, 8),
                            FlatStyle = FlatStyle.Flat,
                            BackColor = Color.SandyBrown,
                            BackgroundImageLayout = ImageLayout.Zoom,
                            Cursor = Cursors.Hand
                        };
                        btnEditar.FlatAppearance.BorderSize = 0;
                        btnEditar.BackgroundImage = Image.FromFile(Application.StartupPath + @"\Iconos\editor32.png");
                        btnEditar.Click += (s, e) => EditarSecretaria(nombreTemp);


                        Button btnEliminar = new Button
                        {
                            Size = new Size(35, 35),
                            Location = new Point(235, 8),
                            FlatStyle = FlatStyle.Flat,
                            BackColor = Color.IndianRed,
                            BackgroundImageLayout = ImageLayout.Zoom,
                            Cursor = Cursors.Hand
                        };
                        btnEliminar.FlatAppearance.BorderSize = 0;
                        btnEliminar.BackgroundImage = Image.FromFile(Application.StartupPath + @"\Iconos\delete32.png");
                        btnEliminar.Click += (s, e) => EliminarSecretaria(nombreTemp);


                        card.Controls.Add(picSecretaria);
                        card.Controls.Add(lblNombre);
                        card.Controls.Add(picCorreo);
                        card.Controls.Add(lblCorreo);
                        card.Controls.Add(btnEditar);
                        card.Controls.Add(btnEliminar);


                        flowSecretarias.Controls.Add(card);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar secretarias: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EliminarSecretaria(string nombre)
        {
            if (MessageBox.Show($"¿Seguro que deseas eliminar a {nombre}?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    Conexion conexion = new Conexion();
                    using (MySqlConnection conn = conexion.GetConnection())
                    {
                        conn.Open();
                        string query = "DELETE FROM usuarios WHERE Nombre=@nombre AND Rol='Secretaria'";

                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Secretaria eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Si estábamos editando esta secretaria, cancelar edición
                    if (modoEdicion && nombreOriginal == nombre)
                    {
                        CancelarEdicion();
                    }

                    MostrarSecretarias();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void EditarSecretaria(string nombre)
        {
            try
            {
                Conexion conexion = new Conexion();
                using (MySqlConnection conn = conexion.GetConnection())
                {
                    conn.Open();

                    // Obtener todos los datos de la secretaria
                    string query = "SELECT UsuarioID, Nombre, Correo, Contrasena FROM usuarios WHERE Nombre=@nombre AND Rol='Secretaria'";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nombre", nombre);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Guardar datos para edición
                        idSecretariaEditar = Convert.ToInt32(reader["UsuarioID"]);
                        nombreOriginal = reader["Nombre"].ToString();

                        // Cargar datos en los campos
                        txtUsuarioSecre.Text = reader["Nombre"].ToString();
                        txtCorreoSecre.Text = reader["Correo"].ToString();
                        txtContrasenaSecre.Text = reader["Contrasena"].ToString();

                        // Activar modo edición
                        modoEdicion = true;

                        // Cambiar el texto del botón
                        btnAltaSecretarias.Text = "Actualizar Secretaria";
                        btnAltaSecretarias.BackColor = Color.Orange;

                        MessageBox.Show($"Editando datos de {nombre}.\n\nModifica los campos y haz clic en 'Actualizar Secretaria'.",
                            "Modo Edición", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos para editar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ⭐ MÉTODO PARA CANCELAR EDICIÓN
        private void CancelarEdicion()
        {
            modoEdicion = false;
            idSecretariaEditar = 0;
            nombreOriginal = "";

            // Restaurar el botón
            btnAltaSecretarias.Text = "Agregar Secretaria";
            btnAltaSecretarias.BackColor = SystemColors.Control;

            // Limpiar campos
            txtUsuarioSecre.Clear();
            txtCorreoSecre.Clear();
            txtContrasenaSecre.Clear();
        }

        private bool DatoExistente(string campo, string valor)
        {
            Conexion conexion = new Conexion();
            using (MySqlConnection conn = conexion.GetConnection())
            {
                conn.Open();
                string query = $"SELECT COUNT(*) FROM usuarios WHERE {campo} = @valor AND Rol = 'Secretaria'";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@valor", valor);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Menu_principal nuevoFormulario = new Menu_principal();
            nuevoFormulario.Show();
            this.Hide();
        }

        private void flowSecretarias_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            int borderRadius = 25; // puedes ajustar el radio a tu gusto

            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, panel1.Width, panel1.Height);
            int diameter = borderRadius * 2;

            // Esquinas redondeadas
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseAllFigures();

            panel1.Region = new Region(path);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
