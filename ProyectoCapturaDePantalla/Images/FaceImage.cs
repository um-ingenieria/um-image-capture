using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.Images
{
    public class FaceImage
    {
        private int id;
        private int sessionId;
        private DateTime date;
        private string name;
        private string path;

        public FaceImage() {}

        public FaceImage(int id, int section, DateTime date ,string name, string path)
        {
            this.id = id;
            this.SessionId = section; 
            this.date = date; 
            this.name = name;
            this.path = path;
        }

        public string Name { get => name; set => name = value; }
        public int Id { get => id; set => id = value; }
        public DateTime Date { get => date; set => date = value; }
        public string Path { get => path; set => path = value; }
        public int SessionId { get => sessionId; set => sessionId = value; }
    }
}
