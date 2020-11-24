using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.Domain.SAM
{
    public class SAM
    {
        const int MIN_HIGH_AROUSAL = 5;
        const int MIN_HIGH_VALENCE = 5;

        public SAM(int valence, int arousal)
        {
            this.Valence = valence;
            this.Arousal = arousal;
        }

        public SAM(string type, int valence, int arousal)
        {
            this.Valence = valence;
            this.Arousal = arousal;
            this.Type = type;
        }

        public int Valence { get; set; }
        public int Arousal { get; set; }
        public string Type { get; set; }

        public bool samMatchesPhaseValenceAndArousal() {
            if (Type.Contains("SAM_HA_PV"))
            {
                return /*Valence >= MIN_HIGH_AROUSAL &&*/ Arousal >= MIN_HIGH_AROUSAL;
            }

            if (Type.Contains("SAM_LA_PV"))
            {
                return /*Valence <= MIN_HIGH_AROUSAL &&*/ Arousal <= MIN_HIGH_AROUSAL;
            }

            if (Type.Contains("SAM_HA_NV"))
            {
                return /*Valence >= MIN_HIGH_AROUSAL &&*/ Arousal >= MIN_HIGH_AROUSAL;
            }

            if (Type.Contains("SAM_LA_NV"))
            {
                return /*Valence <= MIN_HIGH_AROUSAL &&*/ Arousal <= MIN_HIGH_AROUSAL;
            }

            return false;
        }
    }
}
