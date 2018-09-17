using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    class HeatingLine
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public float[] Location { get; set; }
        public string Temperature { get; set; }
        public string State { get; set; }

        public HeatingLine()
        {

        }

        public HeatingLine(int id, int parId, string name, float[] loc, string temp, string state)
        {
            Id = id;
            ParentId = parId;
            Name = name;
            Location = loc;
            Temperature = temp;
            State = state;
        }
    }
}
