using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.Domain.Phase.stimulus
{
    public class DEVO
    {
        public const string ALL_SUBJECTS = "DEVO_ALL_SUBJECTS";

        private float id;
        private float lengthInMs;
        private string title;
        private string description;
        private float valenceMean;
        private float valenceStandardDerivation;
        private float arousalMean;
        private float arousalStandardDerivation;
        private float impactMean;
        private float impactStandardDerivation;

        public DEVO(float id, float lengthInMs, string title, string description, float valenceMean, float valenceStandardDerivation, float arousalMean, float arousalStandardDerivation, float impactMean, float impactStandardDerivation)
        {
            this.Id = id;
            this.LengthInMs = lengthInMs;
            this.Title = title;
            this.Description = description;
            this.ValenceMean = valenceMean;
            this.ValenceStandardDerivation = valenceStandardDerivation;
            this.ArousalMean = arousalMean;
            this.ArousalStandardDerivation = arousalStandardDerivation;
            this.ImpactMean = impactMean;
            this.ImpactStandardDerivation = impactStandardDerivation;
        }

        public float Id { get => id; set => id = value; }
        public float LengthInMs { get => lengthInMs; set => lengthInMs = value; }
        public string Title { get => title; set => title = value; }
        public string Description { get => description; set => description = value; }
        public float ValenceMean { get => valenceMean; set => valenceMean = value; }
        public float ValenceStandardDerivation { get => valenceStandardDerivation; set => valenceStandardDerivation = value; }
        public float ArousalMean { get => arousalMean; set => arousalMean = value; }
        public float ArousalStandardDerivation { get => arousalStandardDerivation; set => arousalStandardDerivation = value; }
        public float ImpactMean { get => impactMean; set => impactMean = value; }
        public float ImpactStandardDerivation { get => impactStandardDerivation; set => impactStandardDerivation = value; }
    }
}
