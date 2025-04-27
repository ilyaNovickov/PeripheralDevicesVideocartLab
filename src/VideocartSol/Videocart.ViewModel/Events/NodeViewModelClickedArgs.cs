using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Videocart.ViewModel.Extra;

namespace Videocart.ViewModel.Events
{
    public class NodeViewModelClickedArgs : EventArgs
    {
        public double X { get; private set; }
        public double Y { get; private set; }   
        public NodeViewModel NodeViewModel { get; private set; }
        public MouseButton Button { get; private set; }

        public NodeViewModelClickedArgs(double x, double y, NodeViewModel nodeViewModel, MouseButton button)
        {
            X = x;
            Y = y;
            NodeViewModel = nodeViewModel;
            Button = button;
        }
    }
}
