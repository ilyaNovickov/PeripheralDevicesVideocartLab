using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.ModelViews
{
    public class NodeModelView : ModelViewBase
    {
        #region Properties
        private double x;
        private double y;
        private double width;
        private double height;
        private ModelViewBase? innerContent = null;
        private string? name = null;

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

        public ModelViewBase? InnerContent
        {
            get => innerContent;
            set
            {
                innerContent = value;
                OnPropertyChanged();
            }
        }
        public string? Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public event EventHandler<NodePressedArgs>? NodePressed;

        public void Clicked()
        {
            NodePressed?.Invoke(this, new NodePressedArgs(this));
        }



        private ObservableCollection<ConnectionModelView> connections = new();

        public ObservableCollection<ConnectionModelView> Connections => connections;
    }

    public class NodePressedArgs : EventArgs
    {
        public NodeModelView Node { get; private set; }

        public NodePressedArgs(NodeModelView node)
        {
            Node = node;
        }
    }

    public enum ConnectionType
    {
        Undef, 
        Getting,
        Sending,
        Duplex
    }

    public class ConnectionModelView : ModelViewBase
    {
        private ConnectionType type = ConnectionType.Undef;
        private string? id = "";

        public ConnectionType Type
        {
            get => type;
            set
            {
                type = value;
                OnPropertyChanged();
            }
        }

        public string? Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        public ImmutableArray<ConnectionType> AvaibleConnectionTypes { get; } = (new List<ConnectionType>() 
        { 
            ConnectionType.Undef,
            ConnectionType.Getting,
            ConnectionType.Sending,
            ConnectionType.Duplex
        }).ToImmutableArray<ConnectionType>();
    }
}
