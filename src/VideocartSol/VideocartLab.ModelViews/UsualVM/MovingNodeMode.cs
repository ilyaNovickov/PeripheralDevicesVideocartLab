using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.ModelViews
{
    public partial class ProjectModelView
    {
        private class MovingNodeMode : IModeBase
        {
            public MovingNodeMode(ProjectModelView parent)
            {
                Parent = parent;
            }

            public ProjectModelView Parent { get; private set; }

            private bool moving = false;

            public void OnPointerPressed()
            {
                //StartMove();
                this.Parent.SelectedNode = null;
            }

            public void OnPointerMoved(double dx, double dy)
            {
                if (Parent.SelectedNode == null || !moving)
                    return;

                Parent.SelectedNode.X += dx;
                Parent.SelectedNode.Y += dy;
            }

            public void OnPointerReleased()
            {
                moving = false;
            }

            public void StartMove()
            {
                moving = true;
            }
        }
    }
}
