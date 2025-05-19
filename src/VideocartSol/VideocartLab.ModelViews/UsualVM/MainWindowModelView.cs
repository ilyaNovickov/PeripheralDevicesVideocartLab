using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideocartLab.ModelViews.Models;

namespace VideocartLab.ModelViews
{
    public class MainWindowModelView : ModelViewBase
    {
        private ProjectModelView? projectVM;
        private NodeListModelView? nodeListVM;
        private NodeFactoryService factoryService;
        private StringBuilder report = new StringBuilder();

        ModelingEnvironment modelingEnvironment;

        public MainWindowModelView()
        {
            factoryService = new NodeFactoryService();

            projectVM = new ProjectModelView(factoryService);
            nodeListVM = new NodeListModelView(factoryService);

            projectVM.NodeAdded += ProjectVM_NodeAdded;
            nodeListVM.SelectedItemChanged += NodeListVM_SelectedItemChanged;

            removeSelectedNode = new RelayCommand(projectVM.RemoveSelectedNode);
            removeNode = new GenericCommand<bool>(projectVM.ToggleRemoveMode);
            startModeling = new RelayCommand(this.StartModeling);

            modelingEnvironment = new();
            modelingEnvironment.Report += ModelingEnvironment_Report;
        }

        private void ModelingEnvironment_Report(object? sender, ReportArgs e)
        {
            report.AppendLine(e.Message);
            report.AppendLine();
            OnPropertyChanged(nameof(Report));
        }

        public string Report
        {
            get => report.ToString();
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

        private GenericCommand<bool> removeNode;

        public GenericCommand<bool> RemoveNodeCommand => removeNode;

        private RelayCommand startModeling;

        public RelayCommand StartModelingCommand => startModeling;

        private void StartModeling()
        {
            modelingEnvironment.ProjectVM = Project!;
            modelingEnvironment.Start();


        }
    }
}
