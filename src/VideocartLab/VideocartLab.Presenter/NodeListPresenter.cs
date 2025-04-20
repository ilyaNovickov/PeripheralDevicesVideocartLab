using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideocartLab.View;

namespace VideocartLab.Presenter
{
    public class NodeType
    {
        public Type Type { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class NodeTypeSelectedArgs : EventArgs
    {
        public NodeType? NodeType { get; private set; }

        public NodeTypeSelectedArgs(NodeType? type)
        {
            NodeType = type;
        }
    }

    public class NodeListPresenter
    {
        private INodeListView nodeListView;
        private NodeType? selectedNodeType = null;

        private List<NodeType> nodesTypes = new List<NodeType>()
        {
            new NodeType()
            {
                Type = typeof(int), Name = "int32"
            },
            new NodeType()
            {
                Type = typeof(NodeListPresenter), Name = "Presenter!!!"
            }
        };

        public NodeListPresenter(INodeListView view) 
        {
            nodeListView = view;

            view.ItemsList = nodesTypes;
            view.SelectedItemChanged += View_SelectedItemChanged;
        }

        private void View_SelectedItemChanged(object? sender, SelectedItemChagedArgs e)
        {
            if (e.SelectedItem is not NodeType)
                return;

            SelectedNodeType = (NodeType)e.SelectedItem;
        }

        public NodeType? SelectedNodeType
        {
            get => selectedNodeType;
            set
            {
                selectedNodeType = value;
                NodeTypeSelectedChanged?.Invoke(this, new NodeTypeSelectedArgs(value));
            }
        }

        public event EventHandler<NodeTypeSelectedArgs>? NodeTypeSelectedChanged;

    }
}
