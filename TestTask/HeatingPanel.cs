using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    class HeatingPanel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float[] Location { get; set; }
        public string Temperature { get; set; }
        public bool IsInAlarm { get; set; }
        public int IsEntryAutomateOn { get; set; }
        public int IsNetworkOn { get; set; }
        public int IsPowerOn { get; set; }
        public int IsOnUps { get; set; }

        public HeatingPanel()
        {

        }

        public HeatingPanel(int id, string name, float[] loc, string temp, bool alarm, int entryAuto, int network, int power, int ups)
        {
            Id = id;
            Name = name;
            Location = loc;
            Temperature = temp;
            IsInAlarm = alarm;
            IsEntryAutomateOn = entryAuto;
            IsNetworkOn = network;
            IsPowerOn = power;
            IsOnUps = ups;
        }
    }
}
