using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.Domain
{
    public class FaceValence
    {
        private int id;
        private string name;
        private Boolean hasValence;
        private float valence;
        private DateTime date;

        public float Valence { get => valence; set => valence = value; }
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public bool HasValence { get => hasValence; set => hasValence = value; }
        public DateTime Date { get => date; set => date = value; }
    }
}
