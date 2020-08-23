﻿using ProyectoCapturaDePantalla.Domain.Session;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.dao
{
    public class SessionEventDao
    {
        public void SaveSessionEvent(SessionEvent sessionEvent)
        {
            SqlConnection dbConnection = DbConnection.GetConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = dbConnection;
            command.CommandType = CommandType.Text;

            command.CommandText = "INSERT into SESSION_EVENT (SESSION_ID, TEST_NAME, TEST_EVENT, EVENT_DATE) " +
                "output INSERTED.ID VALUES (@session_id, @test_name, @test_event, @event_date)";
            command.Parameters.AddWithValue("@session_id", sessionEvent.SessionId);
            command.Parameters.AddWithValue("@test_name", sessionEvent.TestName);
            command.Parameters.AddWithValue("@test_event", sessionEvent.TestEvent);
            command.Parameters.AddWithValue("@event_date", sessionEvent.EventDate);

            try
            {
                dbConnection.Open();
                int recordsAffected = command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Falló la creación de evento de session en la base");
                Console.WriteLine(e.Message);
            }
            finally
            {
                dbConnection.Close();
            }
        }
    }
}