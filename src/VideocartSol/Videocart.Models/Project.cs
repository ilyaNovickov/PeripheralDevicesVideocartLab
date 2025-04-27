using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videocart.Models
{
    public class Project
    {
        private ObservableCollection<Node> nodes = new();

        public Project()
        { 
            ConnectEvents();
        }

        private void ConnectEvents()
        {
            nodes.CollectionChanged += Nodes_CollectionChanged;
        }

        private void Nodes_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
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

        public ObservableCollection<Node> Nodes => nodes;

        public void AddNode(Node node)
        {
            nodes.Add(node);

            node.Project = this;
        }

        public void RemoveNode(Node node)
        {
            nodes.Remove(node);

            node.Project = null;
        }
    }
}
