﻿using DevExpress.Diagram.Core;
using DevExpress.XtraDiagram;
using System.Drawing;

namespace TestTask
{
    class DevExpressVisualControl : IVisualControl
    {
        private DiagramControl diagramControl;

        public DevExpressVisualControl(MainForm link, Point loc, Size size)
        {
            //initialize and adding control on MainForm
            diagramControl = new DiagramControl()
            {
                Location = loc,
                Size = size
            };
            link.Controls.Add(diagramControl);            
        }

        public void Visualize(object obj)
        {
            var penBlack = Color.Black;
            var penGray = Color.Gray;
            var penGreen = Color.Green;
            var penRed = Color.Red;
            var penYellow = Color.Yellow;
            var penCornflowerBlue = Color.CornflowerBlue;
            var fontSerif8 = new Font(FontFamily.GenericSerif, 8);
            var fontSerif12 = new Font(FontFamily.GenericSerif, 12, FontStyle.Bold);
            var horzAlign = DevExpress.Utils.HorzAlignment.Near;
            var centerAlign = DevExpress.Utils.HorzAlignment.Center;

            if (obj.GetType() == typeof(HeatingPanel))
            {                
                var hp = (HeatingPanel)obj;

                //bound
                var boundItem = new DiagramShape(BasicShapes.RoundedRectangle, hp.Location[0], hp.Location[1], 100, 150);
                AdjustItem(boundItem, penCornflowerBlue, penBlack, penBlack, 2, null, horzAlign);

                //header
                var headerItem = new DiagramShape(BasicShapes.Rectangle, hp.Location[0] + 15, hp.Location[1] + 10, 70, 20, hp.Name);
                AdjustItem(headerItem, penCornflowerBlue, penBlack, penCornflowerBlue, 1, fontSerif12, horzAlign);                

                //labels
                DiagramShape[] labelsItem = new DiagramShape[5];
                for (int i = 0; i < 5; i++)
                {
                    labelsItem[i] = new DiagramShape(BasicShapes.Rectangle, hp.Location[0] + 10, hp.Location[1] + 30 + i * 15, 60, 10);
                    AdjustItem(labelsItem[i], penCornflowerBlue, penBlack, penCornflowerBlue, 1, fontSerif8, horzAlign);
                }
                labelsItem[0].Content = "Вв. автомат";
                labelsItem[1].Content = "Связь";
                labelsItem[2].Content = "Питание";
                labelsItem[3].Content = "На ИБП";
                labelsItem[4].Content = "Темп. ";

                //values of labels
                DiagramShape[]  circItem = new DiagramShape[4];
                for (int i = 0; i < 4; i++)
                {
                    circItem[i] = new DiagramShape(BasicShapes.Ellipse, hp.Location[0] + 80, hp.Location[1] + 30 + i*15, 10, 10);
                    circItem[i].Appearance.BorderColor = penBlack;
                }

                //temperature
                var tempItem = new DiagramShape(BasicShapes.Rectangle, hp.Location[0] + 40, hp.Location[1] + 90, 40, 10, hp.Temperature + " C");
                AdjustItem(tempItem, penCornflowerBlue, penBlack, penCornflowerBlue, 1, null, horzAlign);

                //alarm
                var alarmItem = new DiagramShape(BasicShapes.Rectangle, hp.Location[0] + 10, hp.Location[1] + 110, 80, 30, "Авария");
                AdjustItem(alarmItem, Color.Red, penBlack, penBlack, 2, null, centerAlign);

                //backgroung for value of labels
                int[] values = new int[4] {hp.IsEntryAutomateOn, hp.IsNetworkOn, hp.IsPowerOn, hp.IsOnUps};
                for(int i = 0; i < values.Length; i++)
                {                    
                    if (values[i] == -1)
                    {
                        circItem[i].Appearance.BackColor = penGray;
                    }
                    else if (values[i] == 0)
                    {
                        circItem[i].Appearance.BackColor = penRed;
                    }
                    else
                    {
                        circItem[i].Appearance.BackColor = penGreen;
                    }
                }
                
                //Add all items in one adjust container
                DiagramContainer container = new DiagramContainer();
                container.ItemsCanMove = false;
                container.ItemsCanSelect = false;
                container.Items.AddRange(boundItem, headerItem, labelsItem[0], labelsItem[1], labelsItem[2], labelsItem[3], labelsItem[4], circItem[0], circItem[1], circItem[2], circItem[3], tempItem);

                //show label of alarm              
                if (hp.IsInAlarm == true)
                    container.Items.Add(alarmItem);
                
                diagramControl.Items.Add(container);
            }
            else if (obj.GetType() == typeof(HeatingLine))
            {
                var hl = (HeatingLine)obj;

                var item1 = new DiagramShape(BasicShapes.Rectangle, hl.Location[0], hl.Location[1], 70, 40, hl.Temperature);
                AdjustItem(item1, penBlack, penBlack, penBlack, 2, null, centerAlign);

                //background of heatingLine
                if (hl.State == "GoodOrOff")
                {
                    item1.Appearance.BackColor = penGreen;
                }
                else if (hl.State == "Warning")
                {
                    item1.Appearance.BackColor = penYellow;
                }
                else
                {
                    item1.Appearance.BackColor = penRed;
                }
                
                diagramControl.Items.Add(item1);
            }
            else
            {
                var sensor = (Sensor)obj;

                var item1 = new DiagramShape(BasicShapes.RoundedRectangle, sensor.Location[0], sensor.Location[1], 70, 40, sensor.Temperature);
                AdjustItem(item1, penBlack, penBlack, penBlack, 2, null, centerAlign);

                //background of sensor
                if (sensor.State == "GoodOrOff")
                {
                    item1.Appearance.BackColor = penGreen;
                }
                else if (sensor.State == "Warning")
                {
                    item1.Appearance.BackColor = penYellow;
                }
                else
                {
                    item1.Appearance.BackColor = penRed;
                }
                
                diagramControl.Items.Add(item1);
            }            
        }

        private void AdjustItem(DiagramItem item, Color backC, Color foreC, Color borderC, int borderS, Font font, DevExpress.Utils.HorzAlignment horz)
        {
            item.Appearance.BackColor = backC;
            item.Appearance.ForeColor = foreC;
            item.Appearance.BorderColor = borderC;
            item.Appearance.BorderSize = borderS;
            item.Appearance.Font = font;
            item.Appearance.TextOptions.HAlignment = horz;
        }
    }
}
