using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.utils
{
    public class DateHelper
    {
        public static string FULL_DATE_HOUR_PERIOD = "dd/MM/yyyy hh:mm:ss.fff tt";

        public static DateTime FormatDate(string date, string format)
        {
            DateTime parsedDate;

            try
            {
                parsedDate = DateTime.ParseExact(date, format, System.Globalization.CultureInfo.InvariantCulture);
                return parsedDate;
            } catch (Exception e)
            {
                Console.WriteLine("couldn't parse the date in " + format + " format");
                throw e;
            }
        }
    }
}
