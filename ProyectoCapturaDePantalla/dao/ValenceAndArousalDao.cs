using ProyectoCapturaDePantalla.Domain.SAM;
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
        public List<SAM> GetSAMsBySessionId(int sessionId)
        {
            SqlConnection dbConnection = DbConnection.GetConnection();
            List<SAM> samList;

            try
            {
                dbConnection.Open();
                SqlCommand cmd = new SqlCommand($"SELECT TIPO, EXCITACION, VALENCIA FROM EXCITACION_VALENCIA WHERE session_id = {sessionId}", dbConnection);
                SqlDataReader dr = cmd.ExecuteReader();

                samList = new List<SAM>();

                while (dr.Read())
                {
                    string type = Convert.ToString(dr["TIPO"]);
                    int arousal = int.Parse(Convert.ToString(dr["EXCITACION"]));
                    int valence = int.Parse(Convert.ToString(dr["VALENCIA"]));

                    samList.Add(new SAM(type, valence, arousal));
                }
            }
            catch (Exception e)
            {
                string message = "Error recuperando los valores de las encuestas SAM";
                Console.WriteLine(message + e.Message);
                throw e;
            }
            finally
            {
                dbConnection.Close();
            }

            return samList;
        }
        public void InsertExcitementAndArousal(string name, string periodicity, int valence, int arousal, int sessionId)
        {
            SqlConnection dbConnection = DbConnection.GetConnection();
            SqlCommand cmd;

            try
            {
                dbConnection.Open();
                string SqlQuery1 = "INSERT INTO Excitacion_VALENCIA (test_name, session_id, TIPO, EXCITACION, VALENCIA) VALUES('" + name + "'," + sessionId + ",'" + periodicity + "'," + valence + "," + arousal + ")";
                cmd = new SqlCommand(SqlQuery1, dbConnection);
                int N1 = cmd.ExecuteNonQuery();
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
