using System.Collections.ObjectModel;
using VideocartLab.ExtraAbstractions;
using VideocartLab.Models;

namespace VideocartLab.ModelVIews
{
    

    

    public class NodeModelView : ModelViewBase, IPlaceObject
    {
        private string name = "undef";

        private Node model;

        public NodeModelView()
        {
            this.Node = new Node();

            connections.CollectionChanged += Connections_CollectionChanged;
        }

        //Нажатие по узлу
        public event EventHandler<NodeModelViewClickedArgs>? Clicked;

        //Отжатие от узла
        public event EventHandler<NodeModelViewRealeseArgs>? Realesed;

        //Модель узла связанная с этим VM
        internal Node Node
        {
            get => model;
            private set
            {
                model = value;
            }
        }

        //Имя узла
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        #region ModelProperties
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

        public object? Content
        {
            get => model.Content;
            set
            {
                model.Content = value;
                OnPropertyChanged();
            }
        }
        #endregion

        
        public void Click(double x, double y)
        {
            Clicked?.Invoke(this, new NodeModelViewClickedArgs(this, x, y));
        }

        public void Realese(double x, double y)
        {
            Realesed?.Invoke(this, new NodeModelViewRealeseArgs(this, x, y));
        }



        private ObservableCollection<ConnectionModelView> connections = new();

        public ObservableCollection<ConnectionModelView> ConnectionModelViews
        {
            get => connections;
        }

        private void Connections_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    OnConnectorAdded((ConnectionModelView)e.NewItems![0]!);
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

        public event EventHandler<ConnectorAddedArgs>? ConnectorAdded;

        private void OnConnectorAdded(ConnectionModelView connector)
        {
            this.Node.Connectors.Add(connector.Model);
            ConnectorAdded?.Invoke(this, new ConnectorAddedArgs(connector));
        }
    }

    public class ConnectorAddedArgs : EventArgs
    {
        public ConnectionModelView Connector { get; private set; }

        public ConnectorAddedArgs(ConnectionModelView connector)
        {
            Connector = connector;
        }
    }
}
