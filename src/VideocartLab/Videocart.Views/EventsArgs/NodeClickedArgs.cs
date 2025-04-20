using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videocart.Views.EventsArgs
{
    public class NodeClickedArgs : EventArgs
    {
        public INodeView SenderNode { get; private set; }

        public double X { get; private set; }
        public double Y { get; private set; }

        public NodeClickedArgs(INodeView senderNode, double x, double y)
        {
            SenderNode = senderNode;
            X = x;
            Y = y;
        }
    }
}
