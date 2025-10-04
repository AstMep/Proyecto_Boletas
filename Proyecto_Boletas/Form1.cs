using MySql.Data.MySqlClient;
namespace Proyecto_Boletas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
                        
               

                        // Declarar la variable rol fuera del while
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

                        // AQUÍ MUESTRAS EL MENSAJE CON EL ROL
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
                        txtbox_usuario.Focus(); // Pone el cursor en el campo de usuario
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
    }
}
