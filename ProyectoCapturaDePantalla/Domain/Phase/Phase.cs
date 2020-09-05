using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.Domain.Phase
{
    public class Phase
    {
        public Phase(string name, string[] images)
        {
            Name = name;
            Images = images;
        }

        public string Name { get; set; }
        public string[] Images { get; set; }
    }
}
