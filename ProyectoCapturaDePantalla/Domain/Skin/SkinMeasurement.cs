using ProyectoCapturaDePantalla.Domain.Skin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.Domain
{
    public class SkinMeasurement
    {
        public static string PATH = "C:\\emotions\\skin\\";
        public static string FILE_NAME = "skin-min.csv";
        public static string CSV_KEY = "STATISTICS";

        public List<SkinStatistic> skinStatistics;

        public SkinMeasurement()
        {
            skinStatistics = new List<SkinStatistic>();
        }

        public void AddSkinStatistic(SkinStatistic skinStatistic)
        {
            this.skinStatistics.Add(skinStatistic);
        }

        public void AddSkinStatistics(List<SkinStatistic> skinStatistics)
        {
            this.skinStatistics = skinStatistics;
        }
    }
}
