using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VideocartLab.ModelViews.Models
{
    public class ProjectModel
    {
        private List<NodeModel> nodes = new List<NodeModel>();

        [JsonInclude]
        public List<NodeModel> Nodes
        {
            get => nodes;
            set => nodes = value;
        }
    }
}
