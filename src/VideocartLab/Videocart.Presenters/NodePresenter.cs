using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Videocart.Views;

namespace Videocart.Presenters
{
    public class NodePresenter
    {
        private INodeView nodeView;
        private IMainCanvasView canvasView;

        public NodePresenter(INodeView nodeView, IMainCanvasView canvasView)
        {
            this.nodeView = nodeView;

            nodeView.Clicked += NodeView_Clicked;
            this.canvasView = canvasView;
        }

        private void NodeView_Clicked(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
