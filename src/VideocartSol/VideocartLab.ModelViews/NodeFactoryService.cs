namespace VideocartLab.ModelViews;

/// <summary>
/// Информация о узле
/// </summary>
public struct NodeInfo
{
    /// <summary>
    /// Ширина узла
    /// </summary>
    public double Width { get; set; }
    /// <summary>
    /// Высота узла
    /// </summary>
    public double Height { get; set; }
    /// <summary>
    /// Имя узла
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// Информация о соединениях узла
    /// </summary>
    public ConnectionsInfos[] Connections { get; set; }

    /// <summary>
    /// Функция создания ModelView для узла
    /// </summary>
    public Func<ModelViewBase> GetVMFunc { get; set; }
}

/// <summary>
/// Информация о соединении узла
/// </summary>
public struct ConnectionsInfos
{
    /// <summary>
    /// Тип соединения
    /// </summary>
    public ConnectionType Type { get; set; }
    /// <summary>
    /// ID соединения
    /// </summary>
    public string? Id { get; set; }
}

/// <summary>
/// Фабрака узлов
/// </summary>
public class NodeFactoryService
{
    private Dictionary<Type, NodeInfo> nodeInnerContentDict = new();

    public NodeFactoryService()
    {
        InitDictionarty();
    }

    /// <summary>
    /// Иницилизация словаря
    /// </summary>
    private void InitDictionarty()
    {
        nodeInnerContentDict.Add(typeof(VRAMModelView), new NodeInfo()
        {
            Width = 420,
            Height = 300,
            Name = "VRAM",
            GetVMFunc = () => new VRAMModelView(),
            Connections = new ConnectionsInfos[1]
            //Connections = new ConnectionsInfos[64]
        });

        nodeInnerContentDict.Add(typeof(GPUContentModelView), new NodeInfo()
        {
            Width = 310,
            Height = 330,
            Name = "GPU",
            GetVMFunc = () => new GPUContentModelView(),
            Connections = new ConnectionsInfos[1]
        });

        nodeInnerContentDict.Add(typeof(ScreenInterfaceViewModel), new NodeInfo()
        {
            Width = 530,
            Height = 380,
            Name = "Порт подключения экрана",
            GetVMFunc = () => new ScreenInterfaceViewModel(),
            Connections = new ConnectionsInfos[1]
        });

        nodeInnerContentDict.Add(typeof(ConnectionInterfaceModelView), new NodeInfo()
        {
            Name = "Интерфейс подключени устройства",
            Width = 500,
            Height = 250,
            GetVMFunc = () => new ConnectionInterfaceModelView(),
            Connections = new ConnectionsInfos[1]
        });

        nodeInnerContentDict.Add(typeof(GPUControllerModelView), new NodeInfo()
        {
            Name = "Котнроллер GPU",
            Width = 600,
            Height = 400,
            GetVMFunc = () => new GPUControllerModelView(),
            Connections = new ConnectionsInfos[4]
        });
    }

    /// <summary>
    /// Словарь типов ViewModel и информацией о узле с таким содержанием
    /// </summary>
    internal Dictionary<Type, NodeInfo> NodeInfosDict => nodeInnerContentDict;

    /// <summary>
    /// Создание узла
    /// </summary>
    /// <param name="innerContentType">ViewModel, который содержится в узле</param>
    /// <param name="x">Координата X</param>
    /// <param name="y">Координата Y</param>
    /// <returns>ViewModel для узла</returns>
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

        //Определение соединений
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

