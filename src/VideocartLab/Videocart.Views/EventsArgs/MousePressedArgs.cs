using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videocart.Views.EventsArgs
{
    public enum MouseButton
    {
        Left, Right, Middle, Undef
    }

    public class MousePressedArgs : EventArgs
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public MouseButton MouseButton { get; private set; }    

        public MousePressedArgs(double x, double y, MouseButton mouseButton)
        {
            X = x;
            Y = y;
            MouseButton = mouseButton;
        }
    }
}
