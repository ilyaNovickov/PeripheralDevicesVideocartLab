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

    public struct Point
    {
        public double X { get; set; }
        public double Y { get; set; }
    }

    public class ProjectModelView : ModelViewBase
    {
        private Project project = new();
        

        private ObservableCollection<NodeModelView> nodes = new();

        public ProjectModelView()
        {

        }

        public void AddNode(NodeModelView nodeModelView)
        {
            project.Nodes.Add(nodeModelView.Node);
            nodes.Add(nodeModelView);
            NodeModelViewAdded?.Invoke(this, new NodeModelViewAddedArgs(nodeModelView));
        }

        public event EventHandler<NodeModelViewAddedArgs>? NodeModelViewAdded;


    }
}
