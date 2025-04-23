using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.ModelVIews
{
    public class NodeModelViewAddedArgs : EventArgs
    {
        private NodeModelView node;

        public NodeModelView Node => node;

        public NodeModelViewAddedArgs(NodeModelView node)
        {
            this.node = node;
        }
    }
}
