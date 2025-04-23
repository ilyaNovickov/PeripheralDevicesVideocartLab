using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideocartLab.ExtraAbstractions;

namespace VideocartLab.Models
{
    public class Node : PlaceObject
    {
        private object? content = null;
        private ObservableCollection<Connector?> connectors = new ObservableCollection<Connector?>();

        public Node()
        {
            connectors.CollectionChanged += Connectors_CollectionChanged;
        }

        public object? Content
        {
            get => content;
            set
            {
                content = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Connector?> Connectors
        {
            get => connectors;
        }

        public bool ConnectToNode(Node node, int connectionIndex)
        {
            this.Connectors.Add(node.Connectors[connectionIndex]);

            return true;
        }

        private void Connectors_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (Connector connector in e.NewItems!)
                    {
                        if (connector.Parent != null)
                            continue;

                        connector.Parent = this;
                    }
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
    }
}
