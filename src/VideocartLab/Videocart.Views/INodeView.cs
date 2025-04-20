using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videocart.Views
{
    public interface INodeView
    {
        public double X { get; set; }
        public double Y { get; set; } 

        public object? Content { get; set; }

        public event EventHandler Clicked;
    }
}
