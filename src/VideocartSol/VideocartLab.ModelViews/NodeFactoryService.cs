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
                Width = 420 * 2d,
                Height = 300 * 2d,
                Name = "VRAM",
                GetVMFunc = () => new VRAMModelView(),
                Connections = new ConnectionsInfos[2]
            });

            nodeInnerContentDict.Add(typeof(GPUContentModelView), new NodeInfo()
            {
                Width = 310 * 2d,
                Height = 330 * 2d,
                Name = "GPU",
                GetVMFunc = () => new GPUContentModelView(),
                Connections = new ConnectionsInfos[2]
            });

            nodeInnerContentDict.Add(typeof(ScreenInterfaceViewModel), new NodeInfo()
            {
                Width = 500,
                Height = 500,
                Name = "Порт подключения экрана",
                GetVMFunc = () => new ScreenInterfaceViewModel(),
                Connections = new ConnectionsInfos[1]
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
                X = x - info.Width / 2d,
                Y = y - info.Height / 2d
            };

            foreach (ConnectionsInfos connectionsInfo in info.Connections)
            {
                node.Connections.Add(new ConnectionModelView()
                {
                    Type = connectionsInfo.Type,
                    Id = connectionsInfo.Id == null ? null : (string)connectionsInfo.Id.Clone()
                });
            }

            return node;
        }
    }

    public struct NodeInfo
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public string? Name { get; set; }
        public ConnectionsInfos[] Connections { get; set; }

        public Func<ModelViewBase> GetVMFunc { get; set; }
    }

    public struct ConnectionsInfos
    {
        public ConnectionType Type { get; set; }
        public string? Id { get; set; }
    }
}
