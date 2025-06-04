using VideocartLab.MainModelsProj;
using VideocartLab.MainModelsProj.ConnectionInterface;
using VideocartLab.MainModelsProj.GPUMemory;
using VideocartLab.MainModelsProj.Screen;
using VideocartLab.ModelViews.Models;

namespace VideocartLab.ModelViews.Serializaion
{
    [Obsolete]
    public static class ProjectVMConverter
    {
        private static Dictionary<Type, Func<object, ModelViewBase>> modelViewDict;

        public static IEnumerable<NodeModelView> ConvertToModelView(IEnumerable<NodeModel> nodes)
        {
            List<NodeModelView> nodesVM = new(nodes.Count());

            foreach (NodeModel node in nodes)
            {
                NodeModelView nodeVM = new NodeModelView()
                {
                    Name = node.Name,
                    X = node.X,
                    Y = node.Y,
                    Width = node.Width,
                    Height = node.Height
                };

                if (node.InnerModel == null)
                    throw new Exception("У узла нет дочернего элемента");

                nodeVM.InnerContent = modelViewDict[node.InnerModel.GetType()].Invoke(node.InnerModel);

                if (node.Connections == null)
                    throw new Exception("У узла нет каких-либо соединений");

                foreach (ConnectionModel connection in node.Connections)
                {
                    ConnectionModelView? connVM = modelViewDict[connection.GetType()].Invoke(connection)
                        as ConnectionModelView;
                    nodeVM.Connections.Add(connVM!);
                }

                nodesVM.Add(nodeVM);
            }

            return nodesVM;
        }

        static ProjectVMConverter()
        {
            modelViewDict = new Dictionary<Type, Func<object, ModelViewBase>>();

            modelViewDict.Add(typeof(ConnectionModel), (model) =>
            {
                ConnectionModel? conn = model as ConnectionModel;
                ConnectionModelView connVM = new()
                {
                    Id = conn!.Id,
                    Type = conn.Type
                };
                return connVM;
            });
            #region Models
            modelViewDict.Add(typeof(GPU), (model) =>
            {
                GPU? gpu = model as GPU;
                GPUContentModelView gpuVM = new()
                {
                    Name = gpu!.Name ?? "",
                    Cores = gpu.Cores,
                    Frequency = gpu.Frequency,
                    TextureMappingUnits = gpu.TextureMappingUnits,
                    RenderOutputPipelines = gpu.RenderOutputPipelines
                };
                return gpuVM;
            });
            modelViewDict.Add(typeof(VRAM), (model) =>
            {
                VRAM? vram = model as VRAM;
                VRAMModelView vramVM = new VRAMModelView()
                {
                    Capacity = vram!.Capacity,
                    MemoryBusCapacity = vram.MemoryBusCapacity,
                };
                vramVM.SelectedGDDR = vramVM.GDDRTypes
                .ToList()
                .Find((item) => item.Type == vram.Type);
                vramVM.RealFrequency = vram.RealFrequency;
                //vramVM.EffectiveFrequency = vram.EffectiveFrequency;
                return vramVM;
            });
            modelViewDict.Add(typeof(GPUController), (model) =>
            {
                GPUController? controller = model as GPUController;
                GPUControllerModelView controllerVM = new GPUControllerModelView();
                //
                return controllerVM;
            });
            modelViewDict.Add(typeof(ScreenInterface), (model) =>
            {
                ScreenInterface? screen = model as ScreenInterface;
                ScreenInterfaceViewModel screenVM = new ScreenInterfaceViewModel()
                {
                    ScreenHeight = screen!.MaxHeight,
                    ScreenWidth = screen.MaxWidth,
                    BitPerPixel = screen.BitPerPixel,
                    Frequency = screen.Frequency,
                    Bandwidth = screen.Bandwidth,
                };
                return screenVM;
            });
            modelViewDict.Add(typeof(ConnectionInterface), (model) =>
            {
                ConnectionInterface? connection = model as ConnectionInterface;
                ConnectionInterfaceModelView connectionVM = new ConnectionInterfaceModelView();
                //connectionVM.ConnectionInfos[0].

                return connectionVM;
            });
            #endregion
        }
    }
}
