using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videocart.Views.EventsArgs
{
    public class NodeSelectedArgs : EventArgs
    {
        public INodeView Node { get; private set; }

        public double X { get; private set; }
        public double Y { get; private set; }   

        public NodeSelectedArgs(INodeView node, double x, double y)
        {
            Node = node;
            X = x;
            Y = y;
        }
    }
}
