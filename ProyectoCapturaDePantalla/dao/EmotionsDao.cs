using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;

namespace ProyectoCapturaDePantalla.dao
{
    public class EmotionsDao
    {

        private static EmotionsDao instance = null;

        private EmotionsDao()
        {
        }

        public static EmotionsDao Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EmotionsDao();
                }
                return instance;
            }
        }

        public static void SaveEmotion(Emotion emotion, int seccion, int id, double valence)
        {
            Instance.saveEmotion(emotion, seccion, id, valence);
        }

        public void saveEmotion(Emotion emotion, int seccion, int id, double valence)
        {
            SqlConnection dbConnection = DbConnection.GetConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = dbConnection;          
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT into FACE_EMOTION (SECCION , image_id , ANGER, CONTEMPT, DISGUST, FEAR, HAPPINESS, NEUTRAL, SADNESS, SURPRISE, VALENCE) " +
                "VALUES (@id , @seccion, @anger, @contempt, @disgust, @fear, @happiness, @neutral, @sadness, @surprise, @valence)";
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@seccion", seccion);
            command.Parameters.AddWithValue("@anger", emotion.Anger);
            command.Parameters.AddWithValue("@contempt", emotion.Contempt);
            command.Parameters.AddWithValue("@disgust", emotion.Disgust);
            command.Parameters.AddWithValue("@fear", emotion.Fear);
            command.Parameters.AddWithValue("@happiness", emotion.Happiness);
            command.Parameters.AddWithValue("@neutral", emotion.Neutral);
            command.Parameters.AddWithValue("@sadness", emotion.Sadness);
            command.Parameters.AddWithValue("@surprise", emotion.Surprise);
            command.Parameters.AddWithValue("@valence", valence);

            try
            {
                dbConnection.Open();
                int recordsAffected = command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine("Fallo la escritura de emociones en la base");
                Console.WriteLine(e.Message);
            }
            finally
            {
                dbConnection.Close();
            }
        }
    }
}
