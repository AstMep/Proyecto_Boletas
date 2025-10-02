using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Boletas
{
    internal class Conexion
    {
        private string conexionString = "server=localhost;port=3306;user=root;password=;database=boletasescolares;";

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(conexionString);
        }
    }
}
