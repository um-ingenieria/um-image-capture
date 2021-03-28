using ProyectoCapturaDePantalla.Domain;
using ProyectoCapturaDePantalla.Images;
using ProyectoCapturaDePantalla.utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.dao
{
    public class ImagesDao
    {
        public List<FaceImage> GetImages(int sessionId, List<string> imagesName)
        {
            List<FaceImage> images = new List<FaceImage>();
            SqlConnection dbConnection = DbConnection.GetConnection();

            var sb = new StringBuilder();
            if (imagesName.Count > 0)
            {
                sb.Append($"[IMAGENWEBCAM] LIKE '%{imagesName[0]}.jpg'");
            }

            foreach (String image in imagesName)
            {
                sb.Append($" OR [IMAGENWEBCAM] LIKE '%{image}.jpg'");
            }

            try
            {
                dbConnection.Open();
                SqlCommand cmd = new SqlCommand($"SELECT [session_id], [id], [DATE], [IMAGENWEBCAM] FROM NEUROSKY_IMAGENES WHERE session_id = {sessionId} AND ({sb})", dbConnection);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    FaceImage img = new FaceImage();
                    img.Id = int.Parse(Convert.ToString(dr["id"]));
                    img.SessionId = int.Parse(Convert.ToString(dr["session_id"]));
                    img.Date = DateHelper.FormatDate((Convert.ToString(dr["DATE"])), DateHelper.FULL_DATE_HOUR_PERIOD);
                    img.Path = Convert.ToString(dr["IMAGENWEBCAM"]);
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

        public List<FaceValence> GetImagesNames(int sessionId, List<FaceValence> imagesValence)
        {
            if(imagesValence.Count == 0)
            {
                return imagesValence;
            }
            List<FaceImage> images = new List<FaceImage>();
            SqlConnection dbConnection = DbConnection.GetConnection();

            List<int> imagesIds = imagesValence.Select(it => it.Id).ToList();

            try
            {
                dbConnection.Open();
                SqlCommand cmd = new SqlCommand($"SELECT [id], [IMAGENWEBCAM] , [DATE] FROM NEUROSKY_IMAGENES WHERE session_id = {sessionId} AND [id] IN ({string.Join(",", imagesValence.Select(it => it.Id).ToList())})", dbConnection);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    int id = int.Parse(Convert.ToString(dr["id"]));
                    String name = Convert.ToString(dr["IMAGENWEBCAM"]);
                    string[] nameArray = name.Split('\\');
                    imagesValence.First(it => it.Id == id).Name = nameArray[nameArray.Count() - 1];
                    imagesValence.First(it => it.Id == id).Date = DateHelper.FormatDate((Convert.ToString(dr["DATE"])), DateHelper.FULL_DATE_HOUR_PERIOD);
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

            return imagesValence;
        }

        public void InsertImages(string testName, int sessionId, int id, string desktopImagePath, string webcamImagePath)
        {
            SqlConnection dbConnection = DbConnection.GetConnection();
            try
            {
                dbConnection.Open();
                string SqlQuery = "INSERT INTO NEUROSKY_IMAGENES (test_name, session_id, id, DATE, IMAGENESCRITORIO, IMAGENWEBCAM)VALUES('" + testName + "'," + sessionId + "," + id + ",'" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff tt") + "','" + desktopImagePath + "', '" + webcamImagePath + "')";
                SqlCommand cmd = new SqlCommand(SqlQuery, dbConnection);


                int N = cmd.ExecuteNonQuery();
                dbConnection.Close();
            }
            catch (Exception e)
            {

            }
        }
    }
}
