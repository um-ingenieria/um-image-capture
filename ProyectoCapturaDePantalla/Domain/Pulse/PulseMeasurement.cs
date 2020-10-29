using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.Domain
{
    public class PulseMeasurement
    {
        public static string PATH = "..\\..\\..\\resources\\csv\\pulse\\";
        public static string FILE_NAME = "ePulse-min.csv";
        public static string CSV_KEY = "ESTADÍSTICAS";

        private List<PulseStatistic> pulseStatistics;

        public PulseMeasurement()
        {
            pulseStatistics = new List<PulseStatistic>();
        }

        public List<PulseStatistic> PulseStatistics { get => pulseStatistics; set => pulseStatistics = value; }

        public void AddPulseStatistic(PulseStatistic pulseStatistic)
        {
            pulseStatistics.Add(pulseStatistic);
        }

        public void AddPulseStatistics(List<PulseStatistic> pulseStatistics)
        {
            this.pulseStatistics = pulseStatistics;
        }
    }
}
