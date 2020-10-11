using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.Domain.Phase.stimulus
{
    public class DEVO : DimensionalStimuli
    {
        public const string ALL_SUBJECTS = "DEVO_ALL_SUBJECTS";

        private float lengthInMs;
        private string title;
        private string description;
        private float impactMean;
        private float impactSD;

        public DEVO(float id, float lengthInMs, string title, string description, float valenceMean, float valenceStandardDerivation, float arousalMean, float arousalStandardDerivation, float impactMean, float impactStandardDerivation)
            : base(id, arousalMean, arousalStandardDerivation, valenceMean, valenceStandardDerivation)
        {
            this.LengthInMs = lengthInMs;
            this.Title = title;
            this.Description = description;
            this.ImpactMean = impactMean;
            this.ImpactSD = impactStandardDerivation;
            this.type = VideoPhase.DEVO_TYPE;
        }

        public DEVO(float id, float valenceMean, float valenceStandardDerivation, float arousalMean, float arousalStandardDerivation)
            : base(id, arousalMean, arousalStandardDerivation, valenceMean, valenceStandardDerivation)
        {
            this.type = VideoPhase.DEVO_TYPE;
        }

        public float LengthInMs { get => lengthInMs; set => lengthInMs = value; }
        public string Title { get => title; set => title = value; }
        public string Description { get => description; set => description = value; }
     
        public float ImpactMean { get => impactMean; set => impactMean = value; }
        public float ImpactSD { get => impactSD; set => impactSD = value; }
    }
}
