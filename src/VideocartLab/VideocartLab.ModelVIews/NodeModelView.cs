using VideocartLab.ExtraAbstractions;
using VideocartLab.Models;

namespace VideocartLab.ModelVIews
{
    public class NodeModelViewClickedArgs : EventArgs
    {
        public NodeModelView Node { get; private set; }
        public double X { get; private set; }
        public double Y { get; private set; }

        public NodeModelViewClickedArgs(NodeModelView node, double x, double y)
        {
            Node = node;
            X = x;
            Y = y;
        }
    }

    public class NodeModelViewRealeseArgs : NodeModelViewClickedArgs
    {
        public NodeModelViewRealeseArgs(NodeModelView node, double x, double y) : base(node, x, y)
        {

        }
    }

    public class NodeModelView : ModelViewBase, IPlaceObject
    {
        private string name = "undef";

        private Node model;

        public NodeModelView()
        {
            this.Node = new Node();
        }

        public event EventHandler<NodeModelViewClickedArgs>? Clicked;

        public event EventHandler<NodeModelViewRealeseArgs>? Realesed;

        internal Node Node
        {
            get => model;
            private set
            {
                model = value;
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public double X
        {
            get => model.X;
            set => model.X = value;
        }

        public double Y
        {
            get => model.Y;
            set => model.Y = value;
        }

        public double Width
        {
            get => model.Width;
            set => model.Width = value;
        }

        public double Height
        {
            get => model.Height;
            set => model.Height = value;
        }

        public object? Content
        {
            get => model.Content;
            set
            {
                model.Content = value;
            }
        }

        public void Click(double x, double y)
        {
            Clicked?.Invoke(this, new NodeModelViewClickedArgs(this, x, y));
        }

        public void Realese(double x, double y)
        {
            Realesed?.Invoke(this, new NodeModelViewRealeseArgs(this, x, y));
        }
    }
}
