using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.ModelVIews
{
    public class NodeModelViewClickedArgs : EventArgs
    {
        public NodeModelView Node { get; private set; }
        public double X { get; private set; }
        public double Y { get; private set; }

        public NodeModelViewClickedArgs(NodeModelView node, double x, double y)
        {
            Node = node;
            X = x;
            Y = y;
        }
    }
}
