using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.Domain.Phase.stimulus
{
    public class IAP : DimensionalStimuli
    {
        public const string ALL_SUBJECTS = "IAPS_ALL_SUBJECTS";
        public const string MALE_SUBJECTS = "IAPS_MALE_SUBJECTS";
        public const string FEMALE_SUBJECTS = "IAPS_FEMALE_SUBJECTS";

        public const string IAP_TYPE = "IAP_TYPE";

        private int durationInMs;

        private int setId; //This value doesn't represent the TesSetId, it's the the default iaps set id

        public IAP(float idIaps, int setId, float valenceMean, float valenceStandardDerivation, float arousalMean, float arousalStandardDerivation, int durationInMs)
            : base (idIaps, arousalMean, arousalStandardDerivation, valenceMean , valenceStandardDerivation)
        {
            this.SetId = setId;
            this.type = IAP_TYPE;
            this.DurationInMs = durationInMs;
        }

        public int SetId { get => setId; set => setId = value; }
        public int DurationInMs { get => durationInMs; set => durationInMs = value; }
    }
}
