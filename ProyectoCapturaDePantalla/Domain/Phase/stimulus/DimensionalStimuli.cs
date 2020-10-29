using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.Domain.Phase.stimulus
{
    public abstract class DimensionalStimuli
    {
        private float id;
        private float arousalMean;
        private float arousalSD;
        private float valenceMean;
        private float valenceSD;
        protected string type;

        protected DimensionalStimuli(float id, float arousalMean, float arousalSD, float valenceMean, float valenceSD)
        {
            this.id = id;
            this.arousalMean = arousalMean;
            this.arousalSD = arousalSD;
            this.valenceMean = valenceMean;
            this.valenceSD = valenceSD;
        }

        public float Id { get => id; set => id = value; }
        public float ArousalMean { get => arousalMean; set => arousalMean = value; }
        public float ArousalSD { get => arousalSD; set => arousalSD = value; }
        public float ValenceMean { get => valenceMean; set => valenceMean = value; }
        public float ValenceSD { get => valenceSD; set => valenceSD = value; }
        public string Type { get => type; set => type = value; }
    }
}
