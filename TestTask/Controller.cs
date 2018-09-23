using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    class Controller
    {
        private IImport _import;
        private IView _visualControl;

        public Controller(IImport i, IView v)
        {
            _import = i;
            _visualControl = v;
        }

        public Model LoadModel(int i)
        {
            return _import.Load(i);
        }

        public void Visualize(Model model)
        {
            _visualControl.AddingObject(model);
        }

        public void Show()
        {
            //Task showTask = new Task(() =>
            //{
                for (int i = 0; i < _import.GetCount(); i++)
                    Visualize(LoadModel(i));
            //});
            //showTask.Start();
        }

        public void Dispose()
        {
            _visualControl.Dispose();
        }
    }
}
