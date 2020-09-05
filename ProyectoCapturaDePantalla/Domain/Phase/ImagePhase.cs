using ProyectoCapturaDePantalla.Domain.Phase.stimulus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.Domain.Phase
{
    public class ImagePhase : PhaseBase
    {
        private List<IAP> iaps;
        public const string IAP_TYPE = "IAP_TYPE";

        public ImagePhase(int id, string name, string description, string valenceArrousalQuadrant, List<IAP> iaps) : base(id, name, description, valenceArrousalQuadrant)
        {
            this.StimuliType = IAP_TYPE;
            this.Iaps = iaps;
        }

        public List<IAP> Iaps { get => iaps; set => iaps = value; }
    }
}
