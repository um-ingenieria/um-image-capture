using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.Domain.Phase.stimulus
{
    public class IAP
    {
        public const string ALL_SUBJECTS = "IAPS_ALL_SUBJECTS";
        public const string MALE_SUBJECTS = "IAPS_MALE_SUBJECTS";
        public const string FEMALE_SUBJECTS = "IAPS_FEMALE_SUBJECTS";

        private float idIaps;
        private int setId; //This value doesn't represent the TesSetId, it's the the default iaps set id
        private float valenceMean;
        private float valenceStandardDerivation;
        private float arousalMean;
        private float arousalStandardDerivation;

        public IAP(float idIaps, int setId, float valenceMean, float valenceStandardDerivation, float arousalMean, float arousalStandardDerivation)
        {
            this.IdIaps = idIaps;
            this.SetId = setId;
            this.ValenceMean = valenceMean;
            this.ValenceStandardDerivation = valenceStandardDerivation;
            this.ArousalMean = arousalMean;
            this.ArousalStandardDerivation = arousalStandardDerivation;
        }

        public float IdIaps { get => idIaps; set => idIaps = value; }
        public int SetId { get => setId; set => setId = value; }
        public float ValenceMean { get => valenceMean; set => valenceMean = value; }
        public float ValenceStandardDerivation { get => valenceStandardDerivation; set => valenceStandardDerivation = value; }
        public float ArousalMean { get => arousalMean; set => arousalMean = value; }
        public float ArousalStandardDerivation { get => arousalStandardDerivation; set => arousalStandardDerivation = value; }
    }
}
