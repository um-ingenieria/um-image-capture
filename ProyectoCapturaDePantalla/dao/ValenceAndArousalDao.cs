using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.dao
{
    public class ValenceAndArousalDao
    {
        public void InsertExcitementAndArousal(string name, string periodicity, int valence, int arousal, int sessionId)
        {
            SqlConnection dbConnection = DbConnection.GetConnection();
            SqlCommand cmd;

            try
            {
                dbConnection.Open();
                string SqlQuery1 = "INSERT INTO Excitacion_VALENCIA (test_name, session_id, TIPO, EXCITACION, VALENCIA) VALUES('" + name + "'," + sessionId + ",'" + periodicity + "'," + valence + ", NULL)";
                cmd = new SqlCommand(SqlQuery1, dbConnection);
                int N1 = cmd.ExecuteNonQuery();
                dbConnection.Close();

                dbConnection.Open();
                string SqlQuery2 = "INSERT INTO Excitacion_VALENCIA (test_name, session_id, TIPO, EXCITACION, VALENCIA) VALUES('" + name + "'," + sessionId + ",'" + periodicity + "', NULL ," + arousal + ")";
                cmd = new SqlCommand(SqlQuery2, dbConnection);
                int N2 = cmd.ExecuteNonQuery();
                dbConnection.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Fallo la escritura de excitación y valencia en la base");
                Console.WriteLine(e.Message);
            }
            finally
            {
                dbConnection.Close();
            }
        }
    }
}
