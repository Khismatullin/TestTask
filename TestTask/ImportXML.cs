using DevExpress.Diagram.Core;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace TestTask
{
    class ImportXML : IImport
    {
        private HeatingPanel[] heatingPanels;
        private HeatingLine[] heatingLines;
        private Sensor[] sensors;
        private int Count;
        private string dir;
        XmlDocument doc;

        public ImportXML(string d)
        {
            heatingPanels = new HeatingPanel[0];
            heatingLines = new HeatingLine[0];
            sensors = new Sensor[0];
            dir = d;

            //get xml-file
            doc = new XmlDocument();
            doc.Load(dir);           

            //set count of all objects
            Count = doc.DocumentElement.LastChild.ChildNodes.Count;
        }

        public int GetCount()
        {
            return Count;
        }

        public object Load(int i)
        {
            var node = doc.DocumentElement.LastChild.ChildNodes[i];

            if (node.Name == "HeatingPanel")
            {
                //read HeatingPanel
                Array.Resize(ref heatingPanels, heatingPanels.Length + 1);
                int last = heatingPanels.Length - 1;
                heatingPanels[last] = new HeatingPanel();

                heatingPanels[last].Id = int.Parse(node.Attributes["Id"].Value);
                heatingPanels[last].Name = node.Attributes["Name"].Value;
                heatingPanels[last].Location = (node.Attributes["Location"].Value).Split(';').Select(float.Parse).ToArray();
                heatingPanels[last].Temperature = node.Attributes["Temperature"].Value;
                heatingPanels[last].IsInAlarm = bool.Parse(node.Attributes["IsInAlarm"].Value);
                heatingPanels[last].IsEntryAutomateOn = int.Parse(node.Attributes["IsEntryAutomateOn"].Value);
                heatingPanels[last].IsNetworkOn = int.Parse(node.Attributes["IsNetworkOn"].Value);
                heatingPanels[last].IsPowerOn = int.Parse(node.Attributes["IsPowerOn"].Value);
                heatingPanels[last].IsOnUps = int.Parse(node.Attributes["IsOnUps"].Value);

                return heatingPanels[last];
            }
            else if (node.Name == "HeatingLine")
            {
                //read HeatingLine
                Array.Resize(ref heatingLines, heatingLines.Length + 1);
                int last = heatingLines.Length - 1;
                heatingLines[last] = new HeatingLine();

                heatingLines[last].Id = int.Parse(node.Attributes["Id"].Value);
                heatingLines[last].ParentId = int.Parse(node.Attributes["ParentId"].Value);
                heatingLines[last].Name = node.Attributes["Name"].Value;
                heatingLines[last].Location = (node.Attributes["Location"].Value).Split(';').Select(float.Parse).ToArray();
                heatingLines[last].Temperature = node.Attributes["Temperature"].Value;
                heatingLines[last].State = node.Attributes["State"].Value;

                return heatingLines[last];
            }
            else
            {
                //read Sensor
                Array.Resize(ref sensors, sensors.Length + 1);
                int last = sensors.Length - 1;
                sensors[last] = new Sensor();

                sensors[last].Id = int.Parse(node.Attributes["Id"].Value);
                sensors[last].ParentId = int.Parse(node.Attributes["ParentId"].Value);
                sensors[last].Name = node.Attributes["Name"].Value;
                sensors[last].Location = (node.Attributes["Location"].Value).Split(';').Select(float.Parse).ToArray();
                sensors[last].Temperature = node.Attributes["Temperature"].Value;
                sensors[last].State = node.Attributes["State"].Value;

                return sensors[last];
            }
        }
    }
}
