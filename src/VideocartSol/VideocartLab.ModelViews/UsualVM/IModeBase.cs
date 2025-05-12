using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.ModelViews
{
    public partial class ProjectModelView
    {
        private interface IModeBase
        {
            ProjectModelView Parent { get; }
            void OnPointerPressed();
            void OnPointerMoved(double dx, double dy);
            void OnPointerReleased();
        }
    }
}
