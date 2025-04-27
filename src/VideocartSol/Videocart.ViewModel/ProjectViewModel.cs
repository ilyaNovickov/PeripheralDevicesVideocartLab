using System;
using System.Collections.ObjectModel;
using Videocart.Models;
using Videocart.ViewModel.Extra;
using Videocart.ViewModel.Events;

namespace Videocart.ViewModel
{
    public class ProjectViewModel : ViewModelBase
    {
        private Project project;
        private ObservableCollection<NodeViewModel> nodeViewModels = new();
        private Point prevPoint = Point.Empty;

        public ProjectViewModel() 
        {
            project = new Project();
            nodeViewModels.CollectionChanged += NodeViewModels_CollectionChanged;
            project.NodeAdded += Project_NodeAdded;
        }

        private void NodeViewModels_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    break;
                default:
                    break;
            }
        }

        public event EventHandler<NodeViewModelAddedArgs> NodeAddedArgs;

        internal Project Project { get; }

        public WorkingMode Mode { get; private set; } = WorkingMode.None;

        public MouseButton StandartMouseButton { get; set; } = MouseButton.Left;
        public MouseButton ContextMouseButton { get; set; } = MouseButton.Right;
        public MouseButton ExtraMouseButton { get; set; } = MouseButton.Middle;

        public NodeViewModel? SelectedNode { get; private set; }

        private void Project_NodeAdded(object? sender, Models.Events.NodeAddedArgs e)
        {
            NodeViewModel nodeViewModel = new NodeViewModel(e.Node);
            nodeViewModel.ProjectViewModel = this;
            nodeViewModel.NodeClicked += NodeViewModel_NodeClicked;
            nodeViewModel.NodeRealesed += NodeViewModel_NodeRealesed;
            nodeViewModels.Add(nodeViewModel);
            NodeAddedArgs?.Invoke(this, new NodeViewModelAddedArgs(nodeViewModel));
        }

        private void NodeViewModel_NodeRealesed(object? sender, NodeViewModelReleaseArgs e)
        {
            Mode = WorkingMode.None;
            SelectedNode = null;
        }

        private void NodeViewModel_NodeClicked(object? sender, NodeViewModelClickedArgs e)
        {
            prevPoint = new Point(e.X + e.NodeViewModel.X, e.Y + e.NodeViewModel.Y);

            Mode = WorkingMode.MoveNode;
            SelectedNode = e.NodeViewModel;
        }

        public void OnMousePressed(double x, double y, MouseButton button)
        {
            switch (Mode)
            {
                case WorkingMode.None:
                    break;
                case WorkingMode.AddNode:
                    break;
                case WorkingMode.MoveNode:
                    break;
                case WorkingMode.RemoveNode:
                    break;
                default:
                    break;
            }

            prevPoint.X = x;
            prevPoint.Y = y;
        }

        public void OnMouseMoved(double newX, double newY)
        {
            if (Mode != WorkingMode.MoveNode)
                return;

            double dx = newX - prevPoint.X;
            double dy = newY - prevPoint.Y;

            SelectedNode.X += dx;
            SelectedNode.Y += dy;

            prevPoint.X = newX;
            prevPoint.Y = newY;
        }

        public void AddNode(Func<double, double, Node> creationNodeFunc)
        {
            Node node = creationNodeFunc(prevPoint.X, prevPoint.Y);
            project.AddNode(node);
        }
    }
}
