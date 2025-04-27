using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videocart.ViewModel.Events
{
    public class NodeViewModelAddedArgs : EventArgs
    {
        public NodeViewModel NodeViewModel { get; private set; }

        public NodeViewModelAddedArgs(NodeViewModel nodeViewModel)
        {
            NodeViewModel = nodeViewModel;
        }
    }
}
