using ProyectoCapturaDePantalla.Domain.Phase.stimulus;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.dao
{
    public class IAPSDao
    {
        private static IAPSDao instance = null;

        private IAPSDao()
        {
        }

        public static IAPSDao Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new IAPSDao();
                }
                return instance;
            }
        }

        public static List<IAP> GetIAPS(List<float> iapsIds, string gender)
        {
            return Instance.getIAPS(iapsIds, gender);
        }

        private List<IAP> getIAPS(List<float> iapsIds, string gender)
        {
            SqlConnection dbConnection = DbConnection.GetConnection();
            List<IAP> iapsList;

            try
            {
                dbConnection.Open();
                SqlCommand cmd = new SqlCommand($"SELECT TOP (2) [id_iaps], [valence_mean], [valence_sd], [arousal_mean], [arousal_sd], [set_id] FROM {gender} WHERE id_iaps in ({string.Join(",", iapsIds.Select(n => n.ToString()).ToArray())})", dbConnection);
                SqlDataReader dr = cmd.ExecuteReader();

                iapsList = new List<IAP>();

                while (dr.Read())
                {
                    float id = float.Parse(Convert.ToString(dr["id_iaps"]));
                    float valenceMean = float.Parse(Convert.ToString(dr["valence_mean"]));
                    float valenceStandardDeviation = float.Parse(Convert.ToString(dr["valence_sd"]));
                    float arousalMean = float.Parse(Convert.ToString(dr["arousal_mean"]));
                    float arousalStandardDeviation = float.Parse(Convert.ToString(dr["valence_sd"]));
                    int setId = int.Parse(Convert.ToString(dr["set_id"]));

                    iapsList.Add(new IAP(id, setId, valenceMean, valenceStandardDeviation, arousalMean, arousalStandardDeviation));
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

            return iapsList;
        }
    }
}
