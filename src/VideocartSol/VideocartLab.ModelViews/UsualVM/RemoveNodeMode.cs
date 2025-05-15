using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.ModelViews
{
    public partial class ProjectModelView
    {
        private class RemoveNodeMode : IModeBase
        {
            public RemoveNodeMode(ProjectModelView parent)
            {
                Parent = parent;
            }

            public ProjectModelView Parent { get; private set; }

            public void OnPointerPressed()
            {

            }

            public void OnPointerMoved(double x, double y)
            {

            }

            public void OnPointerReleased()
            {

            }
        }
    } 
}
