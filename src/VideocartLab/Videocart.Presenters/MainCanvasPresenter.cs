using Videocart.Models;
using Videocart.Views;

namespace Videocart.Presenters
{
    public enum WorkMode
    {
        None, Adding, Moving
    }

    public struct Point
    {
        public double X { get; set; }
        public double Y { get; set; }   


    }

    public class MainCanvasPresenter
    {
        private List<Node> nodes = new List<Node>();

        private IMainCanvasView mainCanvasView;

        private INodeView? selectedNode = null;

        internal Point prevPoint = new Point();

        public MainCanvasPresenter(IMainCanvasView mainCanvasView)
        {
            this.mainCanvasView = mainCanvasView;

            mainCanvasView.MousePressed += MainCanvasView_MousePressed;
            mainCanvasView.MouseMoved += MainCanvasView_MouseMoved;
            mainCanvasView.NodeSelected += MainCanvasView_NodeSelected;
        }

        private void MainCanvasView_MouseMoved(object? sender, Views.EventsArgs.MouseMovedArgs e)
        {
            if (selectedNode is null || Mode != WorkMode.Moving)
                return;

            selectedNode.X += (e.dX - prevPoint.X);
            selectedNode.Y += (e.dY - prevPoint.Y);

            prevPoint.X = e.dX;
            prevPoint.Y = e.dY;
        }

        private void MainCanvasView_NodeSelected(object? sender, Views.EventsArgs.NodeSelectedArgs e)
        {
            SelectedNode = e.Node;

            prevPoint.X = e.X;
            prevPoint.Y = e.Y;

            Mode = WorkMode.Moving;
        }

        public WorkMode Mode { get; private set; } = WorkMode.Adding;//= WorkMode.None;

        public INodeView? SelectedNode
        {
            get => selectedNode;
            private set => selectedNode = value;
        }

        private void MainCanvasView_MousePressed(object? sender, Views.EventsArgs.MousePressedArgs e)
        {
            switch (Mode)
            {
                default:
                case WorkMode.None:
                    return;
                case WorkMode.Adding:
                    //
                    var node = mainCanvasView.NodeFactory.CreateNode("string", e.X, e.Y);
                    NodePresenter nodePresenter = new NodePresenter(node, mainCanvasView);
                    mainCanvasView.AddNode(node);
                    break;
                case WorkMode.Moving:
                    //
                    
                    break;
            }


        }

    }
}
