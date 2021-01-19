using ProyectoCapturaDePantalla.Domain.Phase;
using ProyectoCapturaDePantalla.Domain.Phase.stimulus;
using ProyectoCapturaDePantalla.Domain.Session;
using ProyectoCapturaDePantalla.utils;
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
        private static SessionEventDao instance = null;

        private SessionEventDao()
        {
        }

        public static SessionEventDao Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SessionEventDao();
                }
                return instance;
            }
        }

        public static void SaveSessionEvent(SessionEvent sessionEvent)
        {
            Instance.saveSessionEvent(sessionEvent);
        }

        public void saveSessionEvent(SessionEvent sessionEvent)
        {
            SqlConnection dbConnection = DbConnection.GetConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = dbConnection;
            command.CommandType = CommandType.Text;

            command.CommandText = "INSERT into SESSION_EVENT (SESSION_ID, TEST_NAME, TEST_EVENT, EVENT_DATE, STIMULI_ID, STIMULI_TYPE) " +
                "output INSERTED.ID VALUES (@session_id, @test_name, @test_event, @event_date, @stimuli_id, @stimuli_type)";
            command.Parameters.AddWithValue("@session_id", sessionEvent.SessionId);
            command.Parameters.AddWithValue("@test_name", sessionEvent.TestName);
            command.Parameters.AddWithValue("@test_event", sessionEvent.TestEvent);
            command.Parameters.AddWithValue("@event_date", sessionEvent.EventDate);
            if (sessionEvent.Stimuli == null)
            {
                command.Parameters.AddWithValue("@stimuli_id", (Object)DBNull.Value);
                command.Parameters.AddWithValue("@stimuli_type", (Object)DBNull.Value);
            } else
            {
                command.Parameters.AddWithValue("@stimuli_id", sessionEvent.Stimuli.Id == 0f ? (Object)DBNull.Value : sessionEvent.Stimuli.Id);
                command.Parameters.AddWithValue("@stimuli_type", string.IsNullOrEmpty(sessionEvent.Stimuli.Type) ? (Object)DBNull.Value : sessionEvent.Stimuli.Type);
            }

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
        public static List<SessionEvent> GetStimuliEventsBySessionId(int sessionId)
        {
            return Instance.getStimuliEventsBySessionId(sessionId);
        }
        public List<SessionEvent> getStimuliEventsBySessionId(int sessionId)
        {
            List<SessionEvent> events = new List<SessionEvent>();
            SqlConnection dbConnection = DbConnection.GetConnection();
          

            try
            {
                dbConnection.Open();
                
                SqlCommand cmd = new SqlCommand($@"SELECT se.event_date, se.test_name, se.test_event,
					CASE WHEN se.STIMULI_TYPE = 'IAP_TYPE' THEN iaps.arousal_mean WHEN se.STIMULI_TYPE = 'DEVO_TYPE' THEN devo.arousal_mean ELSE 0 END as arousal_mean,
                    CASE WHEN se.STIMULI_TYPE = 'IAP_TYPE' THEN iaps.arousal_sd WHEN se.STIMULI_TYPE = 'DEVO_TYPE' THEN devo.arousal_sd ELSE 0 END as arousal_sd,
                    CASE WHEN se.STIMULI_TYPE = 'IAP_TYPE' THEN iaps.valence_mean  WHEN se.STIMULI_TYPE = 'DEVO_TYPE' THEN devo.valence_mean ELSE 0 END as valence_mean,
                    CASE WHEN se.STIMULI_TYPE = 'IAP_TYPE' THEN iaps.valence_sd  WHEN se.STIMULI_TYPE = 'DEVO_TYPE' THEN devo.valence_sd ELSE 0 END as valence_sd,
                    CASE WHEN se.STIMULI_TYPE = 'IAP_TYPE' THEN iaps.set_id ELSE '' END as set_id,
					CASE WHEN se.test_event like '%SAM%' THEN '' else se.STIMULI_TYPE END as STIMULI_TYPE,
					CASE WHEN se.test_event like '%SAM%' THEN 0 else se.STIMULI_ID END as STIMULI_ID
                    FROM SESSION_EVENT se
                    LEFT JOIN IAPS_ALL_SUBJECTS iaps on se.STIMULI_ID = iaps.id_iaps
                    LEFT JOIN DEVO_ALL_SUBJECTS devo on se.STIMULI_ID = devo.id
                    WHERE se.session_id = {sessionId}
                    AND (se.test_event in ('INIT_STIMULI', 'END_STIMULI') OR se.test_event LIKE '%SAM%') order by se.event_date ASC", dbConnection);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    //DateTime eventDate = DateHelper.FormatDate((Convert.ToString(dr["event_date"])), DateHelper.FULL_DATE_HOUR_PERIOD);
                    DateTime eventDate = (System.DateTime)dr["event_date"];
                    string testName = Convert.ToString(dr["test_name"]);
                    float stimuliId= float.Parse(Convert.ToString(dr["STIMULI_ID"]));
                    string stimuliType = Convert.ToString(dr["STIMULI_TYPE"]);
                    string testEvent = Convert.ToString(dr["test_event"]);
                    float arousalMean = float.Parse(Convert.ToString(dr["arousal_mean"]));
                    float arousalSD = float.Parse(Convert.ToString(dr["arousal_sd"]));
                    float valenceMean = float.Parse(Convert.ToString(dr["valence_mean"]));
                    float valenceSD = float.Parse(Convert.ToString(dr["valence_sd"]));

                    DimensionalStimuli stimuli = null;

                    if (stimuliType == IAP.IAP_TYPE)
                    {
                        int setId = int.Parse(Convert.ToString(dr["set_id"]));
                        stimuli = new IAP(stimuliId, setId, valenceMean, valenceSD, arousalMean, arousalSD);
                    } else if (stimuliType == DEVO.DEVO_TYPE)
                    {
                        stimuli = new DEVO(stimuliId, valenceMean, valenceSD, arousalMean, arousalSD);
                    }

                    events.Add(new SessionEvent(sessionId, testName, testEvent, eventDate, stimuli));
                }
            }
            catch (Exception e)
            {
                string message = "Error recuperando los eventos de los estimulos";
                Console.WriteLine(message + e.Message);
                throw e;
            }
            finally
            {
                dbConnection.Close();
            }

            return events;
        }
    }
}
