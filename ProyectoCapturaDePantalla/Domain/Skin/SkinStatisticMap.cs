using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.Domain.Skin
{
    public class SkinStatisticMap : ClassMap<SkinStatistic>
    {
        public SkinStatisticMap()
        {
            Map(m => m.RelativeTime).Name("SECOND");
            Map(m => m.MicroSiemens).Name("MICROSIEMENS");
            Map(m => m.AbsoluteTime).Name("TIMESTAMP");
            Map(m => m.SCR).Name("SCR");
            Map(m => m.SCR_MIN).Name("SCR/MIN");
        }
    }
}
