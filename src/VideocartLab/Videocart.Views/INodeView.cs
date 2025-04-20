using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Videocart.Views.EventsArgs;

namespace Videocart.Views
{
    public interface INodeView
    {
        public double X { get; set; }
        public double Y { get; set; } 

        public object? InnerContent { get; set; }

        //public IMainCanvasView Parent { get; set; }

        public void SetContent(object? obj);

        public event EventHandler<NodeClickedArgs> Clicked;
    }
}
