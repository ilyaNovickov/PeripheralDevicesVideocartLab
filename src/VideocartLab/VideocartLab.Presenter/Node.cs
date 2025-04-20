using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.Presenter
{
    public class NodeMovedArgs : EventArgs
    {
        public double dX { get; private set; }
        public double dY { get; private set; }

        public NodeMovedArgs(double dX, double dY)
        {
            this.dX = dX;
            this.dY = dY;
        }
    }

    public class Node
    {
        private double x;
        private double y;

        public double X
        {
            get => x;
            set
            {
                double old = x;
                x = value;
                OnNodeMoved(x - old, 0);
            }
        }

        public double Y
        {
            get => Y;
            set
            {
                double old = y;
                y = value;
                OnNodeMoved(0, y - old);
            }
        }

        public event EventHandler<NodeMovedArgs>? NodeMoved;

        public void Move(double dx, double dy)
        {
            this.X += dx;
            this.Y += dy;
        }

        private void OnNodeMoved(double dx, double dy)
        {
            NodeMoved?.Invoke(this, new NodeMovedArgs(dx, dy));
        }
    }
}
