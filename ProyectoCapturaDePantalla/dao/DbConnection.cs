using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.dao
{
    public class DbConnection
    {
        public static SqlConnection dbConnection = null;

        public static SqlConnection GetConnection()
        {
            if (dbConnection == null)
            {
                try
                {
                    //dbConnection = new SqlConnection("Data Source=192.168.0.4;User ID=sa;Password=Polopolo9;Initial Catalog=UM_NEUROSKY;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                    dbConnection = new SqlConnection("Data Source=DESKTOP-KQBRIL0\\SQLEXPRESS;Initial Catalog=UM_NEUROSKY;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Fallo la creacion de la conexión de la base");
                    Console.WriteLine(e.Message);
                }
            }
            return dbConnection;
        }

    }
}
