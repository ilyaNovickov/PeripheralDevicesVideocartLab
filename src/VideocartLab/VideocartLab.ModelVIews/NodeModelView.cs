using VideocartLab.ExtraAbstractions;
using VideocartLab.Models;

namespace VideocartLab.ModelVIews
{
    public class NodeModelView : ModelViewBase, IPlaceObject
    {
        private double x;
        private double y;
        private double width;
        private double height;

        private Node model;

        public NodeModelView(Node model)
        {
            this.Node = model;
        }

        internal Node Node
        {
            get => model;
            private set
            {
                model = value;
                SetModelValues(model);
            }
        }

        private void SetModelValues(Node node)
        {
            this.X = node.X;
            this.Y = node.Y;
            this.Width = node.Width;
            this.Height = node.Height;
        }

        public double X
        {
            get => x;
            set
            {
                x = value;
                OnPropertyChanged();
            }
        }

        public double Y
        {
            get => y;
            set
            {
                y = value;
                OnPropertyChanged();
            }
        }

        public double Width
        {
            get => width;
            set
            {
                width = value;
                OnPropertyChanged();
            }
        }

        public double Height
        {
            get => height;
            set
            {
                height = value;
                OnPropertyChanged();
            }
        }
    }
}
