using MySql.Data.MySqlClient;
namespace Proyecto_Boletas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //G
        public class Conexion
        {
            private string conexionString = "server=localhost;port=3306;user=root;password=;database=boletasescolares;";

            public MySqlConnection GetConnection()
            {
                return new MySqlConnection(conexionString);
            }
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
                    command.Parameters.AddWithValue("@usuario", usuario_txtbox.Text);        // TextBox del nombre de usuario
                    command.Parameters.AddWithValue("@contrasena", contraseña_txtbox.Text); // TextBox de la contraseña

                    MySqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        MessageBox.Show("Inicio de sesión exitoso");

                        // Opcional: puedes obtener el rol o ID si lo necesitas
                        while (reader.Read())
                        {
                            string rol = reader["Rol"].ToString();
                            Console.WriteLine("Rol del usuario: " + rol);
                        }

                        Menu_principal menuPrincipal = new Menu_principal();
                        menuPrincipal.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Usuario o contraseña incorrectos");
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
    }
}
