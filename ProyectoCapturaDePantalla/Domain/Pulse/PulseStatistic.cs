using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.Domain
{
    public class PulseStatistic
    {
        public float RelativeTime { get; set; }
        public float HR { get; set; }
        public float RR { get; set; }
        public float HRV { get; set; }
        public float Uniformity { get; set; }
        //public float LF_HF { get; set; }
        public DateTime AbsoluteTime { get; set; }
        public int Score { get; set; }
    }
}

