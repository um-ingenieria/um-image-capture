using ProyectoCapturaDePantalla.Domain.Phase.stimulus;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.dao
{
    public class DEVODao
    {
        private static DEVODao instance = null;

        private DEVODao()
        {
        }

        public static DEVODao Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DEVODao();
                }
                return instance;
            }
        }

        public static List<DEVO> GetVideos(List<float> videosIds, string gender)
        {
            return Instance.getVideos(videosIds, gender);
        }
        private List<DEVO> getVideos(List<float> videosIds, string gender)
        {
            SqlConnection dbConnection = DbConnection.GetConnection();
            List<DEVO> videos;

            try
            {
                dbConnection.Open();
                SqlCommand cmd = DEVODao.getCommand(gender, videosIds, dbConnection);
                SqlDataReader dr = cmd.ExecuteReader();

                videos = new List<DEVO>();

                while (dr.Read())
                {
                    float id = float.Parse(Convert.ToString(dr["id"]));
                    int lengthInMs = int.Parse(Convert.ToString(dr["length_in_ms"]));
                    string title = Convert.ToString(dr["title"]);
                    string description = Convert.ToString(dr["description"]);
                    float valenceMean = float.Parse(Convert.ToString(dr["valence_mean"]));
                    float valenceStandardDeviation = float.Parse(Convert.ToString(dr["valence_sd"]));
                    float arousalMean = float.Parse(Convert.ToString(dr["arousal_mean"]));
                    float arousalStandardDeviation = float.Parse(Convert.ToString(dr["valence_sd"]));
                    float impactMean = float.Parse(Convert.ToString(dr["impact_mean"]));
                    float impactStandardDeviation = float.Parse(Convert.ToString(dr["impact_sd"]));

                    videos.Add(new DEVO(id, lengthInMs, title, description, valenceMean, valenceStandardDeviation, arousalMean, arousalStandardDeviation, impactMean, impactStandardDeviation));
                }
            }
            catch (Exception e)
            {
                string message = "Error recuperando las imagenes iaps de la base de datos";
                Console.WriteLine(message + e.Message);
                throw e;
            }
            finally
            {
                dbConnection.Close();
            }

            return videos;
        }

        private static SqlCommand getCommand(string gender, List<float> videosIds, SqlConnection dbConnection)
        {
            //TODO: ADD SWITCH
            return new SqlCommand($"SELECT TOP (2) [id], [length_in_ms], [title], [description], [valence_mean], [valence_sd], [arousal_mean], [arousal_sd], [impact_mean], [impact_sd], FROM {gender} WHERE id in ({string.Join(",", videosIds.Select(n => n.ToString()).ToArray())})", dbConnection); ;
        }
    }
}
