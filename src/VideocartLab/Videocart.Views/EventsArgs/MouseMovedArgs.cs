using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Videocart.Views.EventsArgs
{
    public class MouseMovedArgs : EventArgs
    {
        public double NewX { get; private set; }
        public double NewY { get; private set; }

        public MouseMovedArgs(double dX, double dY)
        {
            this.NewX = dX;
            this.NewY = dY;
        }
    }
}
