using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideocartLab.View;

namespace VideocartLab.Presenter
{
    struct NodeType
    {
        public Type Type { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    public class NodeListPresenter
    {
        private INodeListView nodeListView;

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
        }



    }
}
