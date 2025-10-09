using MySql.Data.MySqlClient;
namespace Proyecto_Boletas
{
    public partial class Form1 : Form
    {
        private bool contrasenaVisible = false;
        public Form1()
        {
            InitializeComponent();
            txtbox_usuario.MaxLength = 100;
            txtbox_contrasena.MaxLength = 10;
            txtbox_contrasena.UseSystemPasswordChar = true;

            btnMostrarContrasena.Text = "👁️";
        }

        public void login()
        {




            Conexion conexion = new Conexion();

            using (MySqlConnection connection = conexion.GetConnection())
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM usuarios WHERE Nombre = @usuario AND Contrasena = @contrasena";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@usuario", txtbox_usuario.Text);
                    command.Parameters.AddWithValue("@contrasena", txtbox_contrasena.Text);

                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {

                        string rolDisplay = "";
                        string nombreUsuario = "";


                        while (reader.Read())
                        {

                            rolDisplay = reader["Rol"].ToString();
                            nombreUsuario = reader["Nombre"].ToString();

                            string rol = reader["Rol"].ToString();

                            if (rol == "Secretaria")
                            {
                                Form_Secretaria formSecretaria = new Form_Secretaria();
                                formSecretaria.Show();
                                this.Hide();

                            }
                            else if (rol == "Director")
                            {
                                Menu_principal formDirector = new Menu_principal();
                                formDirector.Show();
                                this.Hide();
                            }
                            else
                            {

                                MessageBox.Show("Rol no reconocido");
                            }
                        }


                        MessageBox.Show($"¡Bienvenido {nombreUsuario}!\nRol: {rolDisplay}",
                                       "Inicio de sesión exitoso",
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Usuario o contraseña incorrectos");
                        txtbox_usuario.Clear();
                        txtbox_contrasena.Clear();
                        txtbox_usuario.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectar a la base de datos: " + ex.Message);
                }
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        //comentario prueba git javier
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void tb_usuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_ingresar_Click(object sender, EventArgs e)
        {
            login();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void txtbox_usuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnMostrarContrasena_Click(object sender, EventArgs e)
        {
            if (contrasenaVisible)
            {
                // Estado: Contraseña VISIBLE → Cambiar a OCULTA
                txtbox_contrasena.UseSystemPasswordChar = true;
                btnMostrarContrasena.Text = "👁️"; // Ojo para "mostrar"
                contrasenaVisible = false;
            }
            else
            {
                // Estado: Contraseña OCULTA → Cambiar a VISIBLE
                txtbox_contrasena.UseSystemPasswordChar = false;
                txtbox_contrasena.PasswordChar = '\0';
                btnMostrarContrasena.Text = "🔒"; // Candado para "ocultar"
                contrasenaVisible = true;
            }
        }

        private void txtbox_contrasena_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
