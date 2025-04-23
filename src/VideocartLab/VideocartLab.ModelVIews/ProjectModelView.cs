using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideocartLab.Models;

namespace VideocartLab.ModelVIews
{
    public class NodeModelViewAddedArgs : EventArgs
    {
        private NodeModelView node;

        public NodeModelView Node => node;

        public NodeModelViewAddedArgs(NodeModelView node)
        {
            this.node = node;
        }
    }

    public class ProjectModelView : ModelViewBase
    {
        private Project project = new();

        private ObservableCollection<NodeModelView> nodes = new();

        public ProjectModelView()
        {
            project.NodeAdded += Project_NodeAdded;
        }

        public void AddNode(NodeModelView nodeModelView)
        {
            project.Nodes.Add(nodeModelView.Node);
        }

        public event EventHandler<NodeModelViewAddedArgs> NodeModelViewAdded;

        private void Project_NodeAdded(object? sender, NodeAddedArgs e)
        {
            foreach (Node node in e.Nodes)
            {
                NodeModelView nodeVM = new NodeModelView(node);
                nodes.Add(nodeVM);
                NodeModelViewAdded?.Invoke(this, new NodeModelViewAddedArgs(nodeVM));
            }
        }
    }
}
