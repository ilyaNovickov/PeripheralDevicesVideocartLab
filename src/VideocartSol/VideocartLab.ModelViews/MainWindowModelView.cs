using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.ModelViews
{
    public class MainWindowModelView : ModelViewBase
    {
        private ProjectModelView? projectVM;

        public MainWindowModelView()
        {
            projectVM = new ProjectModelView();
        }

        public ProjectModelView? ProjectModelView
        {
            get => projectVM;
            set
            {
                projectVM = value;
                OnPropertyChanged();
            }
        }
    }
}
