using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace VideocartLab.ModelViews
{
    public partial class ProjectModelView
    {
        private class IdleMode : IModeBase
        {
            public IdleMode(ProjectModelView parent) 
            {
                Parent = parent;
            }
            public ProjectModelView Parent { get; private set; }

            public void OnPointerPressed()
            {

            }
            public void OnPointerMoved()
            {

            }
            public void OnPointerReleased()
            {

            }
        }
    } 
}
