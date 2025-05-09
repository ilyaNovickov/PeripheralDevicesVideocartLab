using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.ModelViews
{
    public class NodeFactoryService
    {
        private Dictionary<Type, NodeInfo> nodeInnerContentDict;

        public NodeFactoryService() 
        {
            InitDictionarty();
        }

        private void InitDictionarty()
        {
            nodeInnerContentDict = new Dictionary<Type, NodeInfo>();

            nodeInnerContentDict.Add(typeof(VRAMModelView), new NodeInfo()
            {
                Width = 420,
                Height = 300,
                Name = "VRAM",
                GetVMFunc = () => new VRAMModelView()
            });

            nodeInnerContentDict.Add(typeof(GPUContentModelView), new NodeInfo()
            {
                Width = 310,
                Height = 330,
                Name = "GPU",
                GetVMFunc = () => new GPUContentModelView()
            });
        }

        internal Dictionary<Type, NodeInfo> NodeInfosDict => nodeInnerContentDict;

        public NodeModelView CreateNode(Type innerContentType, double x, double y)
        {
            NodeInfo info = nodeInnerContentDict[innerContentType];

            ModelViewBase vm = info.GetVMFunc();
            NodeModelView node = new NodeModelView()
            {
                InnerContent = vm,
                Width = info.Width,
                Height = info.Height,
                Name = info.Name,
                X = x,
                Y = y
            };

            return node;
        }
    }

    public struct NodeInfo
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public string? Name { get; set; }

        public Func<ModelViewBase> GetVMFunc { get; set; }
    }
}
