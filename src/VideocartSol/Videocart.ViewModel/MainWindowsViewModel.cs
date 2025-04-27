using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Videocart.ViewModel.Extra;

namespace Videocart.ViewModel
{
    public class MainWindowsViewModel
    {
        private NodeFactory factory;
        private ProjectViewModel projectViewModel;

        public MainWindowsViewModel()
        {
            factory = new NodeFactory();
            projectViewModel = new ProjectViewModel();
        }

        public NodeFactory Factory => factory;
        public ProjectViewModel Project => projectViewModel;
    }
}
