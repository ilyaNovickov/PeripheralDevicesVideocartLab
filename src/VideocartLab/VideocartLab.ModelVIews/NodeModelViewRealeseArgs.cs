using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.ModelVIews
{

    public class NodeModelViewRealeseArgs : NodeModelViewClickedArgs
    {
        public NodeModelViewRealeseArgs(NodeModelView node, double x, double y) : base(node, x, y)
        {

        }
    }
}
