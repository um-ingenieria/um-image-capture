using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using ProyectoCapturaDePantalla.Domain;

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

        public static List<FaceValence> GetImages(int sessionId)
        {
            return Instance.getImages(sessionId);
        }

        private List<FaceValence> getImages(int sessionId)
        {
            SqlConnection dbConnection = DbConnection.GetConnection();
            List<FaceValence> images = new List<FaceValence>();

            try
            {
                dbConnection.Open();
                SqlCommand cmd = new SqlCommand($"SELECT [image_id], [VALENCE] FROM FACE_EMOTION WHERE session_id = {sessionId}", dbConnection);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    FaceValence img = new FaceValence();
                    img.Id = int.Parse(Convert.ToString(dr["image_id"]));
                    img.Valence = float.Parse(Convert.ToString(dr["VALENCE"]));
                    img.HasValence = (img.Valence > 0.5);
                    images.Add(img);
                }
            }
            catch (Exception e)
            {
                string message = "Error recuperando imagenes de la base de datos";
                Console.WriteLine(message + e.Message);
                throw e;
            }
            finally
            {
                dbConnection.Close();
            }

            return images;
        }

        public static void SaveEmotion(Emotion emotion, int sessionId, int image_id, double valence)
        {
            Instance.saveEmotion(emotion, sessionId, image_id, valence);
        }

        public void saveEmotion(Emotion emotion, int sessionId, int image_id, double valence)
        {
            SqlConnection dbConnection = DbConnection.GetConnection();
            SqlCommand command = new SqlCommand();
            command.Connection = dbConnection;          
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT into FACE_EMOTION (image_id, session_id, ANGER, CONTEMPT, DISGUST, FEAR, HAPPINESS, NEUTRAL, SADNESS, SURPRISE, VALENCE) " +
                "VALUES (@image_id , @session_id, @anger, @contempt, @disgust, @fear, @happiness, @neutral, @sadness, @surprise, @valence)";
            command.Parameters.AddWithValue("@image_id", image_id);
            command.Parameters.AddWithValue("@session_id", sessionId);
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
