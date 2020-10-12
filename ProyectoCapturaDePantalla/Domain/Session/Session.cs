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
        private int testSet;


        public Session(string testName, int testSet)
        {
            TestName = testName;
            TestSet = testSet;
        }

        public int Id { get => id; set => id = value; }
        public string TestName { get => testName; set => testName = value; }
        public int TestSet { get => testSet; set => testSet = value; }
    }
}

