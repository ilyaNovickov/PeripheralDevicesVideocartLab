using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.ModelViews
{
    public partial class ProjectModelView : ModelViewBase
    {
        private struct Point
        {
            public double X { get; set; }
            public double Y { get; set; }
        }

        private Point prevPoint = new Point();

        private NodeModelView? selectedNode = null;

        private Type? candidatToAdd = null;
        private NodeFactoryService factoryService;
        private IModeBase mode;

        private IdleMode idle;
        private AddingNodeMode addNode;
        private MovingNodeMode move;


        private ObservableCollection<NodeModelView> nodes = new();

        #if DEBUG
        public ProjectModelView()
        {

        }
        #endif
        public ProjectModelView(NodeFactoryService factoryService)
        {
            this.factoryService = factoryService;

            idle = new IdleMode(this);
            addNode = new AddingNodeMode(this);
            move = new MovingNodeMode(this);

            mode = idle;

            nodes.CollectionChanged += Nodes_CollectionChanged;
        }

        private void Nodes_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    OnNodeAdded(new NodeAddedArgs((NodeModelView)e.NewItems![0]!));
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

        private void OnNodeAdded(NodeAddedArgs e)
        {
            e.AddedNode.NodePressed += OnNodePressed;

            NodeAdded?.Invoke(this, e);
        }

        private void OnNodePressed(object? sender, NodePressedArgs args)
        {
            if (Mode == move && args.Node == this.SelectedNode)
            {
                move.StartMove();
                return;
            }

            if (Mode != idle && args.Node == this.SelectedNode)
                return;

            this.SelectedNode = args.Node;

            if (args.Node != null)
            {
                Mode = move;
                move.StartMove();
            }
            else
                Mode = idle;
        }

        public event EventHandler<NodeAddedArgs>? NodeAdded;

        private NodeFactoryService NodeFactoryService { get { return factoryService; } }

        public Type? CandidateToAdd
        {
            get => candidatToAdd;
            set
            {
                candidatToAdd = value;

                
                if (candidatToAdd != null)
                    Mode = addNode;
                else
                    Mode = idle;
                
            }
        }

        public NodeModelView? SelectedNode
        {
            get => selectedNode;
            set
            {
                selectedNode = value;
            }
        }

        private IModeBase Mode
        {
            get => mode;
            set
            {
                mode = value;
            }
        }

        internal ObservableCollection<NodeModelView> Nodes => nodes;
        

        public void OnPointerPressed(double x, double y)
        {
            prevPoint.X = x;
            prevPoint.Y = y;

            Mode.OnPointerPressed();
        }

        public void OnPointerMoved(double newX, double newY)
        {
            double dx = newX - prevPoint.X;
            double dy = newY - prevPoint.Y;

            Mode.OnPointerMoved(dx, dy);

            prevPoint.X = newX;
            prevPoint.Y = newY;
        }

        public void OnPointerReleased()
        {
            Mode.OnPointerReleased();
        }

        public NodeModelView? AddNode()
        {
            if (CandidateToAdd == null)
                return null;

            var node = NodeFactoryService.CreateNode(CandidateToAdd, prevPoint.X, prevPoint.Y);

            return node;
        }
    }

    public class NodeAddedArgs : EventArgs
    {
        public NodeModelView AddedNode { get; private set; }

        public NodeAddedArgs(NodeModelView addedNode)
        {
            AddedNode = addedNode;
        }
    }
}
