using CsvHelper;
using CsvHelper.Configuration;
using ProyectoCapturaDePantalla.Domain;
using ProyectoCapturaDePantalla.Domain.Skin;
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
        public PulseMeasurement ParseCsvPulseMeasurement(string path, string fileName, string csvKey)
        {
            PulseMeasurement pulseMeasurement = new PulseMeasurement();

            try
            {
                using (var reader = new StreamReader(path + fileName))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Configuration.RegisterClassMap<PulseStatisticMap>();
                    csv.Configuration.Delimiter = ";";
                    csv.Configuration.MissingFieldFound = null;

                    skipCsvResumeUntilKey(csv, csvKey);

                    var pulseStatisticRecords = csv.GetRecords<PulseStatistic>();
                    pulseMeasurement.AddPulseStatistics(pulseStatisticRecords.ToList());
                }
            } catch (Exception e)
            {
                Console.WriteLine("Ocurrio un error en el procesamiento de las mediciones de pulso");
                Console.WriteLine(e.Message);
                throw e;
            }
            return pulseMeasurement;
        }
        
        public SkinMeasurement ParseCsvSkinMeasurement(string path, string fileName, string csvKey)
        {
            SkinMeasurement skinMeasurement = new SkinMeasurement();
            try
            {
                using (var reader = new StreamReader(path + fileName))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Configuration.RegisterClassMap<SkinStatisticMap>();
                    csv.Configuration.Delimiter = ";";
                    csv.Configuration.MissingFieldFound = null;

                    skipCsvResumeUntilKey(csv, csvKey);

                    var skinStatisticRecords = csv.GetRecords<SkinStatistic>();
                    skinMeasurement.AddSkinStatistics(skinStatisticRecords.ToList());
                }
            } catch(Exception e)
            {
                Console.WriteLine("Ocurrio un error en el procesamiento de las mediciones de conductancia");
                Console.WriteLine(e.Message);
                throw e;
            }
            return skinMeasurement;
        }

        private static void skipCsvResumeUntilKey(CsvReader csv, string csvKey)
        {
            while (csv.Read())
            {
                if (csv.Context.Record[0].StartsWith(csvKey))
                {
                    csv.Read();
                    csv.ReadHeader();
                    break;
                }
            }
        }
    }
}
