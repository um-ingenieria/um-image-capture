using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.Domain
{
    public class PulseStatisticMap : ClassMap<PulseStatistic>
    {
        public PulseStatisticMap()
        {
            Map(m => m.RelativeTime).Name("SEGUNDO");
            Map(m => m.HR).Name("RITMO CARDIACO");
            Map(m => m.RR).Name("RR");
            Map(m => m.HRV).Name("AMPLITUD HRV");
            Map(m => m.Uniformity).Name("UNIFORMIDAD");
            Map(m => m.LF_HF).Name("LF + HF").ConvertUsing(NullFloatParser);
            Map(m => m.AbsoluteTime).Name("MARCAS DE TIEMPO").ConvertUsing(NullDateParser);
            Map(m => m.Score).Name("MARCADOR").ConvertUsing(NullIntParser);
        }

        public float NullFloatParser(IReaderRow row)
        {
            var rawValue = row.GetField(row.Context.CurrentIndex + 1);

            if (rawValue == "N.a." || rawValue == "")
                return 0;
            else
                return Convert.ToSingle(rawValue);
        }

        public int NullIntParser(IReaderRow row)
        {
            var rawValue = row.GetField(row.Context.CurrentIndex + 1);

            if (rawValue == "N.a." || rawValue == "")
                return 0;
            else
                return int.Parse(rawValue);
        }

        public DateTime NullDateParser(IReaderRow row)
        {
            var rawValue = row.GetField(row.Context.CurrentIndex + 1);
            DateTime dt = new DateTime();
            try
            {
                dt = DateTime.ParseExact(rawValue, "HH:mm:ss:fff", null);
            } catch (Exception e)
            {
                throw e;
            }
            return dt;
        }
    }
}
