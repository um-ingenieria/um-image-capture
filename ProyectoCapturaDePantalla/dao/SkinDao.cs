using ProyectoCapturaDePantalla.Domain;
using ProyectoCapturaDePantalla.Domain.Skin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.dao
{
    public class SkinDao
    {
        public void SaveSkinMeasurement(SkinMeasurement skinMeasurement, int sessionId)
        {
            SqlConnection dbConnection = DbConnection.GetConnection();
            try
            {
                dbConnection.Open(); //TODO: ver si es transacional
                foreach (SkinStatistic skinStatistic in skinMeasurement.SkinStatistics)
                {
                    SqlCommand command = createCommandForRecord(sessionId, dbConnection, skinStatistic);
                    int recordsAffected = command.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("Fallo la escritura de piel en la base");
                Console.WriteLine(e.Message);
            }
            finally
            {
                dbConnection.Close();
            }
        }

        private static SqlCommand createCommandForRecord(int sessionId, SqlConnection dbConnection, SkinStatistic skinStatistic)
        {
            SqlCommand command = new SqlCommand
            {
                Connection = dbConnection,
                CommandType = CommandType.Text,
                CommandText = "INSERT into SKIN_STATISTIC (session_id, relative_time, micro_siemens, absolute_time, scr, scr_min) " +
                        "VALUES (@session_id, @relative_time, @micro_siemens, @absolute_time, @scr, @scr_min)"
            };
            command.Parameters.AddWithValue("@session_id", sessionId);
            command.Parameters.AddWithValue("@relative_time", skinStatistic.RelativeTime);
            command.Parameters.AddWithValue("@micro_siemens", skinStatistic.MicroSiemens);
            command.Parameters.AddWithValue("@absolute_time", skinStatistic.AbsoluteTime);
            command.Parameters.AddWithValue("@scr", skinStatistic.SCR);
            command.Parameters.AddWithValue("@scr_min", skinStatistic.SCR_MIN);
            return command;
        }
    }
}
