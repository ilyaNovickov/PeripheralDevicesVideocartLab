using VideocartLab.View;

namespace VideocartLab.Presenter
{
    public enum WorkMode
    {
        None, Adding
    }

    public class MainCanvasPresenter
    {
        private NodeType? nodeToAdd = null;

        private List<Node> nodes = new List<Node>();

        private IMainCanvasView mainCanvasView;

        public MainCanvasPresenter(IMainCanvasView view)
        {
            mainCanvasView = view;

            mainCanvasView.MousePressed += MainCanvasView_MousePressed;
        }

        private void MainCanvasView_MousePressed(object? sender, MousePressedArgs e)
        {
            switch (Mode)
            {
                case WorkMode.None:
                    break;
                case WorkMode.Adding:
                    AddNode(e.X, e.Y);
                    break;
                default:
                    break;
            }
        }

        public void AddNode(double x, double y)
        {
            Node node = NodeFactory.CreateTextNode($"Node #{nodes.Count}", x, y);

            nodes.Add(node);

            mainCanvasView.UpdateNode(node);
        }

        public WorkMode Mode { get; private set; }

        public NodeType? NodeToAdd
        {
            get => nodeToAdd;
            set
            {
                nodeToAdd = value;
                if (nodeToAdd != null)
                {
                    Mode = WorkMode.Adding;
                }
            }
        }
    }
}
