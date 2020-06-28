using CsvHelper;
using CsvHelper.Configuration;
using ProyectoCapturaDePantalla.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.parser
{
    public class ParserService
    {
        public PulseMeasurement ParseCsvPulseMeasurement(string path, string fileName)
        {
            PulseMeasurement pulseMeasurement = new PulseMeasurement();
            using (var reader = new StreamReader(path + fileName))
            using (var csv = new CsvReader(reader, CultureInfo.GetCultureInfo("en-GB")))
            {
                csv.Configuration.RegisterClassMap<PulseStatisticMap>();
                csv.Configuration.Delimiter = ";";
                csv.Configuration.MissingFieldFound = null;

                var pulseStatisticRecords = csv.GetRecords<PulseStatistic>();
                pulseMeasurement.AddPulseStatistics(pulseStatisticRecords.ToList());
            }
            return pulseMeasurement;
        }
    }
}
