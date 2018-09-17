using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestTask
{
    public partial class MainForm : Form
    {
        View view;

        public MainForm()
        {
            InitializeComponent();            
        }

        private string chooseFileXML()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "*.xml";
            ofd.Filter = "Xml(*.xml)|*.xml";
            ofd.Title = "Выберите документ для импорта данных";

            //user selected FileName
            if (ofd.ShowDialog() == DialogResult.OK)
                return ofd.FileName;

            return null;
        }

        private void загрузитьИзXmlфайлаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var res = chooseFileXML();
            if (res != null)
            {
                view = new View(new ImportXML(res), new DevExpressVisualControl(this, new Point(10, 30), new Size(830, 350)));
                view.Show();
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void подробнееToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Выполнил:  Хисматуллин А.И.\n"+
                "Почта: almir.khismatullin.job@mail.ru"
                , "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information
           );
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            int x, y, w, z;
            ThreadPool.GetMinThreads(out y, out x);
            ThreadPool.SetMinThreads(10, x);
            ThreadPool.GetMaxThreads(out w, out z);
            ThreadPool.SetMaxThreads(30, z);

            //handler of hot keys (need set propreties Form.KeyPreview as "true")
            KeyDown += new KeyEventHandler(HotKeys);
        }

        private void HotKeys(object sender, KeyEventArgs e)
        {
            //import data from file (CTRL + O)
            if (e.Control && e.KeyCode == Keys.O)
                загрузитьИзXmlфайлаToolStripMenuItem_Click(sender, e);
        }
    }
}
