using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.Domain.Phase
{
    public abstract class PhaseBase
    {
        private int id;
        private string name;
        private string description;
        private string valenceArrousalQuadrant;
        private string stimuliType;

        protected PhaseBase(int id, string name, string description, string valenceArrousalQuadrant)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            ValenceArrousalQuadrant = valenceArrousalQuadrant;
        }

       
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public string ValenceArrousalQuadrant { get => valenceArrousalQuadrant; set => valenceArrousalQuadrant = value; }
        public string StimuliType { get => stimuliType; set => stimuliType = value; }
    }
}
