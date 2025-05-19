using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.ModelViews.Models
{
    internal class NodeModel
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public object? InnerModel { get; set; }
        public ConnectionModel[]? Connections { get; set; }
    }
}
