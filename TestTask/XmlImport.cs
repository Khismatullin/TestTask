using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Xml;

namespace TestTask
{
    class XmlImport : IImport
    {
        private int _сount;
        private string _dir;
        private XmlDocument _doc;

        public XmlImport(string d)
        {
            _dir = d;

            //get xml-file
            _doc = new XmlDocument();
            _doc.Load(_dir);           

            //set count of all objects
            _сount = _doc.DocumentElement.LastChild.ChildNodes.Count;
        }

        public int GetCount()
        {
            return _сount;
        }

        public Model Load(int i)
        {
            var node = _doc.DocumentElement.LastChild.ChildNodes[i];
            Model model;

            if (node.Name == "HeatingPanel")
            {
                //read HeatingPanel
                model = new HeatingPanel(
                    int.Parse(node.Attributes["Id"].Value),
                    node.Attributes["Name"].Value,
                    (node.Attributes["Location"].Value).Split(';').Select(float.Parse).ToArray(),
                    node.Attributes["Temperature"].Value,
                    bool.Parse(node.Attributes["IsInAlarm"].Value),
                    int.Parse(node.Attributes["IsEntryAutomateOn"].Value),
                    int.Parse(node.Attributes["IsNetworkOn"].Value),
                    int.Parse(node.Attributes["IsPowerOn"].Value),
                    int.Parse(node.Attributes["IsOnUps"].Value)
                    );
            }
            else if (node.Name == "HeatingLine")
            {
                //read HeatingLine
                model = new HeatingLine(
                    int.Parse(node.Attributes["Id"].Value),
                    int.Parse(node.Attributes["ParentId"].Value),
                    node.Attributes["Name"].Value,
                    (node.Attributes["Location"].Value).Split(';').Select(float.Parse).ToArray(),
                    node.Attributes["Temperature"].Value,
                    node.Attributes["State"].Value
                    );
            }
            else
            {
                //read Sensor
                model = new Sensor(
                    int.Parse(node.Attributes["Id"].Value),
                    int.Parse(node.Attributes["ParentId"].Value),
                    node.Attributes["Name"].Value,
                    (node.Attributes["Location"].Value).Split(';').Select(float.Parse).ToArray(),
                    node.Attributes["Temperature"].Value,
                    node.Attributes["State"].Value
                    );
            }

            //save model locally
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(model.GetType() + i.ToString() + ".txt", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, model);
            }

            return model;
        }
    }
}
