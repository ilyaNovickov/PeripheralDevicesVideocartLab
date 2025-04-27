using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videocart.Models.Events
{
    public class NodeClickedArgs : EventArgs
    {
        public Node Node { get; private set; }

        public NodeClickedArgs(Node node)
        {
            Node = node;
        }
    }
}
