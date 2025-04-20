using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videocart.Views.EventsArgs
{
    public class MouseMovedArgs : EventArgs
    {
        public double dX { get; private set; }
        public double dY { get; private set; }

        public MouseMovedArgs(double dX, double dY)
        {
            this.dX = dX;
            this.dY = dY;
        }
    }
}
