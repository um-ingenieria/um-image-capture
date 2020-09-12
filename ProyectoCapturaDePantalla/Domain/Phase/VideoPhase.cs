using ProyectoCapturaDePantalla.Domain.Phase.stimulus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.Domain.Phase
{
    public class VideoPhase : PhaseBase
    {
        private List<DEVO> videos;
        public const string IAP_TYPE = "DEVO_TYPE";

        public VideoPhase(int id, string name, string description, string valenceArrousalQuadrant, List<DEVO> videos) : base(id, name, description, valenceArrousalQuadrant)
        {
            this.StimuliType = IAP_TYPE;
            this.videos = videos;
        }

        public List<DEVO> Videos { get => videos; set => videos = value; }
    }
}
