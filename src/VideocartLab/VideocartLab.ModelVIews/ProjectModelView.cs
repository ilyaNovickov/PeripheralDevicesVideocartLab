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

    public enum WorkingMode
    {
        None, AddNode, RemoveNode,
        MoveNode
    }

    public enum MouseButton
    {
        Left, Middle, Right, Undef
    }

    public class ProjectModelView : ModelViewBase
    {
        private Project project = new();

        private Point prevPoint = new Point();

        private ObservableCollection<NodeModelView> nodes = new();

        private NodeModelView? selectedNode = null;

        public ProjectModelView()
        {

        }

        public NodeFactory Factory { get; set; }

        public WorkingMode Mode
        {
            get; private set;
        } = WorkingMode.AddNode;

        //public void AddNode(NodeModelView nodeModelView)
        //{
        //    nodeModelView.Clicked += NodeModelView_Clicked;
        //    nodeModelView.Realesed += NodeModelView_Realesed;
        //    project.Nodes.Add(nodeModelView.Node);
        //    nodes.Add(nodeModelView);
        //    NodeModelViewAdded?.Invoke(this, new NodeModelViewAddedArgs(nodeModelView));
        //}
        public void AddNode(double x, double y)
        {
            NodeModelView nodeModelView = Factory.Create(x, y, 100, 100, "Test");

            nodeModelView.Clicked += NodeModelView_Clicked;
            nodeModelView.Realesed += NodeModelView_Realesed;

            project.Nodes.Add(nodeModelView.Node);
            nodes.Add(nodeModelView);

            NodeModelViewAdded?.Invoke(this, new NodeModelViewAddedArgs(nodeModelView));
        }

        private void NodeModelView_Realesed(object? sender, NodeModelViewRealeseArgs e)
        {
            Mode = WorkingMode.AddNode;
        }

        private void NodeModelView_Clicked(object? sender, NodeModelViewClickedArgs e)
        {
            SelectedNode = e.Node;
            Mode = WorkingMode.MoveNode;
            prevPoint.X = e.X + e.Node.X;
            prevPoint.Y = e.Y + e.Node.Y;
        }

        public NodeModelView? SelectedNode
        {
            get => selectedNode;
            set => selectedNode = value;
        }

        public event EventHandler<NodeModelViewAddedArgs>? NodeModelViewAdded;

        public void OnMousePressed(double x, double y, MouseButton button)
        {
            if (Mode == WorkingMode.AddNode)
                AddNode(x, y);
        }

        public void OnMouseMoved(double x, double y)
        {
            if (SelectedNode == null)
                return;

            if (Mode == WorkingMode.MoveNode)
            {
                double dx = x - prevPoint.X;
                double dy = y - prevPoint.Y;

                SelectedNode.X += dx;
                SelectedNode.Y += dy;

                prevPoint.X = x;
                prevPoint.Y = y;
            }
        }
    }
}
