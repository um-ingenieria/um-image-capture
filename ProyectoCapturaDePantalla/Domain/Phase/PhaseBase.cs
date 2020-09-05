using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.Domain.Phase
{
    public abstract class PhaseBase
    {
        public int id;
        public string name;
        public string description;
        public string ValenceArrousalQuadrant;
        public string StimuliType;

        protected PhaseBase(int id, string name, string description, string valenceArrousalQuadrant)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            ValenceArrousalQuadrant = valenceArrousalQuadrant;
        }
    }
}
