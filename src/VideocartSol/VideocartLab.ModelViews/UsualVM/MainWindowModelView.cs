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

            projectVM = new ProjectModelView(factoryService);
            nodeListVM = new NodeListModelView(factoryService);

            projectVM.NodeAdded += ProjectVM_NodeAdded;
            nodeListVM.SelectedItemChanged += NodeListVM_SelectedItemChanged;

            removeSelectedNode = new RelayCommand(projectVM.RemoveSelectedNode);
        }

        private void ProjectVM_NodeAdded(object? sender, NodeAddedArgs e)
        {
            nodeListVM!.SelectedItem = null;
        }

        private void NodeListVM_SelectedItemChanged(object? sender, SelectedNodeItemChangedArgs e)
        {
            if (e.NewItem == null)
                projectVM!.CandidateToAdd = null;
            else
                projectVM!.CandidateToAdd = e.NewItem.NodeType;
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

        private RelayCommand removeSelectedNode;

        public RelayCommand RemoveSelectedNodeCommand => removeSelectedNode;
    }
}
