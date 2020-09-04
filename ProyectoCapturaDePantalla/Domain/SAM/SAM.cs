using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.Domain.SAM
{
    public class SAM
    {

        public SAM(int valence, int arousal)
        {
            this.Valence = valence;
            this.Arousal = arousal;
        }

        public int Valence { get; set; }
        public int Arousal { get; set; }
    }
}
