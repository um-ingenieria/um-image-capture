using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.Domain.Session
{
    public class Session
    {
        private int id;
        private string testName;

        public Session(string testName)
        {
            TestName = testName;
        }

        public int Id { get => id; set => id = value; }
        public string TestName { get => testName; set => testName = value; }
    }
}

