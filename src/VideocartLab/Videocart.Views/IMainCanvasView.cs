using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Videocart.Views.EventsArgs;

namespace Videocart.Views
{
    public interface IMainCanvasView
    {
        public event EventHandler<MousePressedArgs> MousePressed;
        //public void OnMousePressed(MousePressedArgs e);

        public event EventHandler NodeSelected;

        public void SetSelectedNode(INodeView nodeView);
    }
}
