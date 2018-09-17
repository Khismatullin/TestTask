using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    class View
    {
        private IImport import;
        private IVisualControl visualControl;

        public View(IImport i, IVisualControl v)
        {
            import = i;
            visualControl = v;
        }

        public void Show()
        {
            for (int i = 0; i < import.GetCount(); i++)
                visualControl.Visualize(import.Load(i));
        }
    }
}
