using ProyectoCapturaDePantalla.Images;
using ProyectoCapturaDePantalla.utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.dao
{
    public class ImagesDao
    {
        public List<FaceImage> GetImages(int section)
        {
            List<FaceImage> images = new List<FaceImage>();
            SqlConnection dbConnection = DbConnection.GetConnection();

            try
            {
                dbConnection.Open();
                SqlCommand cmd = new SqlCommand($"SELECT [SECCION], [id], [DATE], [IMAGENWEBCAM] FROM NEUROSKY_IMAGENES WHERE SECCION = {section}", dbConnection);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    FaceImage img = new FaceImage();
                    img.Id = int.Parse(Convert.ToString(dr["id"]));
                    img.Section = int.Parse(Convert.ToString(dr["SECCION"]));
                    img.Date = DateHelper.FormatDate((Convert.ToString(dr["DATE"])), DateHelper.FULL_DATE_HOUR_PERIOD);
                    img.Path = Convert.ToString(dr["IMAGENWEBCAM"]);
                    images.Add(img);
                }

                dbConnection.Close();
            }
            catch (Exception e)
            {
                string message = "Error recuperando imagenes de la base de datos";
                Console.WriteLine(message + e.Message);
                throw e;
            }

            return images;
        }

        public void InsertImages(string testName, int section, int id, string desktopImagePath, string webcamImagePath)
        {
            SqlConnection dbConnection = DbConnection.GetConnection();
            try
            {
                dbConnection.Open();
                string SqlQuery = "INSERT INTO NEUROSKY_IMAGENES (test_name, SECCION, id, DATE, IMAGENESCRITORIO, IMAGENWEBCAM)VALUES('" + testName + "'," + section + "," + id + ",'" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff tt") + "','" + desktopImagePath + "', '" + webcamImagePath + "')";
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
