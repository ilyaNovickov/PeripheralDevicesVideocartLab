using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Videocart.Models;
using Videocart.ViewModel.Events;
using Videocart.ViewModel.Extra;
using Videocart.ViewModel.InnerContent;

namespace Videocart.ViewModel
{
    public class NodeViewModel : ViewModelBase
    {
        private Node model;
        private ViewModelBase? content = null;

        public NodeViewModel(Node node) 
        {
            Node = node;
        }

        public Node Node
        {
            get => model;
            private set
            {
                model = value;
                model.PropertyChanged += Model_PropertyChanged;
                ContentPropertyInit();
            }
        }

        private void Model_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(Node.Content))
                return;

            ContentPropertyInit();
        }

        private void ContentPropertyInit()
        {
            if (Node.Content is string str)
            {
                InnerContent = new StringContentViewModel()
                {
                    Str = str
                };
            }
        }

        public event EventHandler<NodeViewModelClickedArgs>? NodeClicked;
        public event EventHandler<NodeViewModelReleaseArgs>? NodeRealesed;

        internal ProjectViewModel? ProjectViewModel { get; set; }

        public double X
        {
            get => model.X;
            set
            {
                model.X = value;
                OnPropertyChanged();
            }
        }

        public double Y
        {
            get => model.Y;
            set
            {
                model.Y = value;
                OnPropertyChanged();
            }
        }

        public double Width
        {
            get => model.Width;
            set
            {
                model.Width = value;
                OnPropertyChanged();
            }
        }

        public double Height
        {
            get => model.Height;
            set
            {
                model.Height = value;
                OnPropertyChanged();
            }
        }

        public ViewModelBase? InnerContent
        {
            get => content;
            set
            {
                content = value;
                OnPropertyChanged();
            }
        }

        public void OnMousePressed(double x, double y, MouseButton button)
        {
            NodeClicked?.Invoke(this, new NodeViewModelClickedArgs(x, y, this, button));
        }

        public void OnMouseRealese()
        {
            NodeRealesed?.Invoke(this, new NodeViewModelReleaseArgs(this));
        }

        public void RemoveThisNode()
        {
            this.ProjectViewModel.RemoveNode(this);
        }
    }
}
