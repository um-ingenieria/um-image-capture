using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCapturaDePantalla.Domain.Session
{
    public class SessionEvent
    {
        private int id;
        private int sessionId;
        private string testName;
        private string testEvent;
        private DateTime eventDate;
        private float stimulId;
        private string stimuliType;


        public SessionEvent(int sessionId, string testName, string testEvent, DateTime eventDate)
        {
            this.SessionId = sessionId;
            this.TestName = testName;
            this.TestEvent = testEvent;
            this.EventDate = eventDate;
        }

        public SessionEvent(int sessionId, string testName, string testEvent, DateTime eventDate, float stimulId, string stimuliType)
        {
            this.SessionId = sessionId;
            this.TestName = testName;
            this.TestEvent = testEvent;
            this.EventDate = eventDate;
            this.stimulId = stimulId;
            this.stimuliType = stimuliType;
        }

        public int Id { get => id; set => id = value; }
        public int SessionId { get => sessionId; set => sessionId = value; }
        public string TestName { get => testName; set => testName = value; }
        public string TestEvent { get => testEvent; set => testEvent = value; }
        public DateTime EventDate { get => eventDate; set => eventDate = value; }
        public float StimulId { get => stimulId; set => stimulId = value; }
        public string StimuliType { get => stimuliType; set => stimuliType = value; }
    }
}
 