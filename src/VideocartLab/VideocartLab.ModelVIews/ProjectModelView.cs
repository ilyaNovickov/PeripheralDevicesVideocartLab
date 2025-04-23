using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideocartLab.Models;

namespace VideocartLab.ModelVIews
{
    public class ProjectModelView : ModelViewBase
    {
        private Project project = new();

        private ObservableCollection<NodeModelView> nodes = new();

        public ProjectModelView()
        {
            project.NodeAdded += Project_NodeAdded;
        }

        private void Project_NodeAdded(object? sender, NodeAddedArgs e)
        {
            foreach (Node node in e.Nodes)
            {
                nodes.Add(new NodeModelView(node));
            }
        }
    }
}
