using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videocart.Models.Events
{
    public class NodeMovedArgs : NodeClickedArgs
    {
        public double Xnew { get; private set; }
        public double Ynew { get; private set; }

        public NodeMovedArgs(double xnew, double ynew, Node node) : base(node)
        {
            Xnew = xnew;
            Ynew = ynew;
        }
    }
}
