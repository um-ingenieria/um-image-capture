using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.Domain
{
    public class DimentionalMesurement
    {
        private float arousal;
        private float valence;
        private DateTime sinceDate;
        private DateTime toDate;

        public float Arousal { get => arousal; set => arousal = value; }
        public float Valence { get => valence; set => valence = value; }
        public DateTime SinceDate { get => sinceDate; set => sinceDate = value; }
        public DateTime ToDate { get => toDate; set => toDate = value; }
    }
}
