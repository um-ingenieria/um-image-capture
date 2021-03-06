using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoCapturaDePantalla.Domain;

namespace ProyectoCapturaDePantalla.dao
{
    public class PulseDao
    {
        public void SavePulseMeasurement(PulseMeasurement pulseMeasurement, int sessionId)
        {
            SqlConnection dbConnection = DbConnection.GetConnection();

            try
            {
                dbConnection.Open(); //TODO: ver si es transacional
                foreach (PulseStatistic pulseStatistic in pulseMeasurement.PulseStatistics)
                {
                    SqlCommand command = createCommandForRecord(sessionId, dbConnection, pulseStatistic);
                    int recordsAffected = command.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("Fallo la escritura de pulso en la base");
                Console.WriteLine(e.Message);
            }
            finally
            {
                dbConnection.Close();
            }
        }

        private static SqlCommand createCommandForRecord(int sessionId, SqlConnection dbConnection, PulseStatistic pulseStatistic)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = dbConnection;
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT into PULSE_STATISTIC (session_id , relative_time, hr, rr, hrv, uniformity, absolute_time, score) " +
                        "VALUES (@session_id, @relative_time, @hr, @rr, @hrv, @uniformity, @absolute_time, @score)";
            command.Parameters.AddWithValue("@session_id", sessionId);
            command.Parameters.AddWithValue("@relative_time", pulseStatistic.RelativeTime);
            command.Parameters.AddWithValue("@hr", pulseStatistic.HR);
            command.Parameters.AddWithValue("@rr", pulseStatistic.RR);
            command.Parameters.AddWithValue("@hrv", pulseStatistic.HRV);
            command.Parameters.AddWithValue("@uniformity", pulseStatistic.Uniformity);
            command.Parameters.AddWithValue("@absolute_time", pulseStatistic.AbsoluteTime);
            command.Parameters.AddWithValue("@score", pulseStatistic.Score);
            return command;
        }
    }
}
