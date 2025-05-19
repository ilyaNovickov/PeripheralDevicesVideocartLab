using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.ModelViews.Models
{
    internal class ProjectModel
    {
        private List<NodeModel> nodes = new List<NodeModel>();

        public List<NodeModel> Nodes => nodes;
    }
}
