using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VideocartLab.Presenter;

namespace VideocartLab.View
{
    public enum MouseButton
    {
        Left, Right, Middle, Undef
    }

    public class MousePressedArgs : EventArgs
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public MouseButton Button { get; private set; }

        public MousePressedArgs(double x, double y, MouseButton button)
        {
            X = x;
            Y = y;
            Button = button;
        }
    }

    public interface IMainCanvasView
    {
        public event EventHandler<MousePressedArgs> MousePressed;

        public void OnMousePressed(MousePressedArgs e);

        public void UpdateNode(Node node);
    }
}
