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
        private NodeFactoryService factoryService;

        public MainWindowModelView()
        {
            factoryService = new NodeFactoryService();

            projectVM = new ProjectModelView();
            nodeListVM = new NodeListModelView(factoryService);
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
