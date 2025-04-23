using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideocartLab.ExtraAbstractions;

namespace VideocartLab.Models
{
    public class NodeAddedArgs : EventArgs
    {
        private List<Node> nodes;

        public NodeAddedArgs(List<Node> nodes)
        {
            this.nodes = nodes;
        }

        public List<Node> Nodes => nodes;
    }

    public class Project : NotifyPropertyObject
    {
        private ObservableCollection<Node> nodes = new ObservableCollection<Node>();


        public Project()
        {
            nodes.CollectionChanged += Nodes_CollectionChanged;
        }

        public ObservableCollection<Node> Nodes
        {
            get => nodes;
        }

        public event EventHandler<NodeAddedArgs>? NodeAdded;

        private void Nodes_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    OnNodeAdded((List<Node>)e.NewItems!);
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

        private void OnNodeAdded(List<Node> nodes)
        {
            NodeAdded?.Invoke(this, new NodeAddedArgs(nodes));
        }
    }
}
