using System.ComponentModel;
using Videocart.Models.Events;

namespace Videocart.Models
{
    public class Node : PlaceObject
    {
        private object? content = null;
        private string name = "";

        public object? Content
        {
            get => content;
            set
            {
                content = value;
                OnPropertyChanged();
            }
        }

        public string? Name
        {
            get => name;
            set
            {
                if (value == null)
                    name = "";
                else
                    name = value;
                OnPropertyChanged();
            }
        }

        public Project? Project { get; set; }

        public event EventHandler<NodeMovedArgs>? NodeMoved;

        public event EventHandler<NodeClickedArgs>? NodeClicked;

        public void Clicked()
        {
            NodeClicked?.Invoke(this, new NodeClickedArgs(this));
        }

        public void Move(double dx, double dy)
        {
            this.X += dx;
            this.Y += dy;
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.PropertyName == nameof(X) || e.PropertyName == nameof(Y))
            {
                NodeMoved?.Invoke(this, new NodeMovedArgs(X, Y, this));
            }
        }
    }
}
