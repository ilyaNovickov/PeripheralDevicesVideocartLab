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
        private NodeListModelView? nodeListVM;

        public MainWindowModelView()
        {
            projectVM = new ProjectModelView();
            nodeListVM = new NodeListModelView();
        }

        public ProjectModelView? Project
        {
            get => projectVM;
            set
            {
                projectVM = value;
                OnPropertyChanged();
            }
        }

        public NodeListModelView? NodeList
        {
            get => nodeListVM;
            set
            {
                nodeListVM = value;
                OnPropertyChanged();
            }
        }
    }
}
