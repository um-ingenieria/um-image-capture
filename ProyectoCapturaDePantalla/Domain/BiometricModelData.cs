using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.Domain
{
    class BiometricModelData
    {
        private DateTime timeStamp;
        private float hR;
        private float rR;
        private float hRV;
        private float microSiemens;
        private float sCR;
        private float sCR_MIN;
        private float arousal;
        private float valence;

        public DateTime TimeStamp { get => timeStamp; set => timeStamp = value; }
        public float HR { get => hR; set => hR = value; }
        public float RR { get => rR; set => rR = value; }
        public float HRV { get => hRV; set => hRV = value; }
        public float MicroSiemens { get => microSiemens; set => microSiemens = value; }
        public float SCR { get => sCR; set => sCR = value; }
        public float SCR_MIN { get => sCR_MIN; set => sCR_MIN = value; }
        public float Arousal { get => arousal; set => arousal = value; }
        public float Valence { get => valence; set => valence = value; }

        public BiometricModelData(DateTime timeStamp, float hR, float rR, float hRV, float microSiemens, float sCR, float sCR_MIN)
        {
            this.TimeStamp = timeStamp;
            HR = hR;
            RR = rR;
            HRV = hRV;
            MicroSiemens = microSiemens;
            SCR = sCR;
            SCR_MIN = sCR_MIN;
        }
    }
}
