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
        public static string PATH = "..\\..\\..\\resources\\csv\\skin\\";
        public static string FILE_NAME = "skin-min.csv";
        public static string CSV_KEY = "STATISTICS";

        private List<SkinStatistic> skinStatistics;

        public SkinMeasurement()
        {
            SkinStatistics = new List<SkinStatistic>();
        }

        public List<SkinStatistic> SkinStatistics { get => skinStatistics; set => skinStatistics = value; }

        public void AddSkinStatistic(SkinStatistic skinStatistic)
        {
            this.SkinStatistics.Add(skinStatistic);
        }

        public void AddSkinStatistics(List<SkinStatistic> skinStatistics)
        {
            this.SkinStatistics = skinStatistics;
        }
    }
}
