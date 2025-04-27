using System;
using System.Collections.ObjectModel;
using Videocart.Models;
using Videocart.ViewModel.Extra;

namespace Videocart.ViewModel
{
    public class ProjectViewModel : ViewModelBase
    {
        private Project project;

        public ProjectViewModel() 
        {
            project = new Project();
        }

        internal Project Project { get; }


        public MouseButton StandartMouseButton { get; set; } = MouseButton.Left;
        public MouseButton ContextMouseButton { get; set; } = MouseButton.Right;
        public MouseButton ExtraMouseButton { get; set; } = MouseButton.Middle;

        public void AddNode(Func<double, double, Node> creationNodeFunc)
        {

        }
    }
}
