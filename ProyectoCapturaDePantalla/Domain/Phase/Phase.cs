using ProyectoCapturaDePantalla.Domain.Phase.stimulus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.Domain.Phase
{
    public class Phase
    {
        private int id;
        private string name;
        private string description;
        private string valenceArrousalQuadrant;
        private List<DimensionalStimuli> stimulis;

        public Phase(int id, string name, string description, string valenceArrousalQuadrant)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
            ValenceArrousalQuadrant = valenceArrousalQuadrant;
            this.stimulis = new List<DimensionalStimuli>();
        }
       
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public string ValenceArrousalQuadrant { get => valenceArrousalQuadrant; set => valenceArrousalQuadrant = value; }
        public List<DimensionalStimuli> Stimulis { get => stimulis; set => stimulis = value; }
    }
}
