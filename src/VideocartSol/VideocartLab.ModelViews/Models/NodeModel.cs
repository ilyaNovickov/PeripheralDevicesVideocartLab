using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideocartLab.MainModelsProj;

namespace VideocartLab.ModelViews.Models
{
    public class NodeModel
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public BaseModel? InnerModel { get; set; }
        public ConnectionModel[]? Connections { get; set; }
    }
}
