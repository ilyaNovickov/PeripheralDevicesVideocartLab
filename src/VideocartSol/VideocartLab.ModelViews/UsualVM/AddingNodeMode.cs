using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.ModelViews
{
    public partial class ProjectModelView
    {
        private class AddingNodeMode : IModeBase
        {
            public AddingNodeMode(ProjectModelView parent) 
            {
                Parent = parent;
            }

            public ProjectModelView Parent { get; private set; }

            public void OnPointerPressed()
            {
                var node = Parent.AddNode();
                
                Parent.Nodes.Add(node!);
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
