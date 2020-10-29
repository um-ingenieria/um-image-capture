using ProyectoCapturaDePantalla.Domain.Phase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.Domain.TestSet
{
    public class TestSet
    {
        private int id;
        private string description;
        private List<Phase.Phase> phases;

        public TestSet(int id, string description, List<Phase.Phase> phases)
        {
            this.id = id;
            this.description = description;
            this.phases = phases;
        }

        public int Id { get => id; set => id = value; }
        public string Description { get => description; set => description = value; }
        public List<Phase.Phase> Phases { get => phases; set => phases = value; }
    }
}
