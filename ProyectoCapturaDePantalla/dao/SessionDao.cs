using ProyectoCapturaDePantalla.Domain.Session;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.dao
{
    public class SessionDao
    {
        public Session SaveSession(Session session)
        {
            SqlConnection dbConnection = DbConnection.GetConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = dbConnection;
            command.CommandType = CommandType.Text;

            command.CommandText = "INSERT into SESSION (TEST_NAME, test_set) " +
                "output INSERTED.ID VALUES (@test_name, @test_set)";
            command.Parameters.AddWithValue("@test_name", session.TestName);
            command.Parameters.AddWithValue("@test_set", session.TestSet);

            try
            {
                dbConnection.Open();
                int modified = (int)command.ExecuteScalar();
                session.Id = modified;
                return session;
            }
            catch (SqlException e)
            {
                Console.WriteLine("Falló la creación de session en la base");
                Console.WriteLine(e.Message);
                throw e;
            }
            finally
            {
                dbConnection.Close();
            }
        }
    }
}
