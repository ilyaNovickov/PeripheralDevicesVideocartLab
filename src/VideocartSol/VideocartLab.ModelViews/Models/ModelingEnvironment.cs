using System.Text;
using VideocartLab.MainModelsProj;
using VideocartLab.MainModelsProj.ConnectionInterface;
using VideocartLab.MainModelsProj.GPUMemory;
using VideocartLab.MainModelsProj.Screen;

namespace VideocartLab.ModelViews.Models;

/// <summary>
/// Среда для симуляции
/// </summary>
internal class ModelingEnvironment
{
    /// <summary>
    /// Информация о соединених узлов
    /// </summary>
    private struct NodeConnectionInfo
    {
        /// <summary>
        /// Исходный узел соединения
        /// </summary>
        public NodeModel SourseNode { get; set; }
        /// <summary>
        /// ID соединения
        /// </summary>
        public string? Id { get; set; }
        /// <summary>
        /// Тип соединения
        /// </summary>
        public ConnectionType Type { get; set; }
    }

    private ProjectConverter projectConverter = new();
    private ProjectModel? project = null;

    Queue<string> errors = new Queue<string>();
    Queue<string> warnings = new Queue<string>();

    public ModelingEnvironment()
    {

    }

    /// <summary>
    /// ProjectViewModel для симуляции
    /// </summary>
    public ProjectModelView ProjectVM
    {
        set
        {
            //Создания ProjectModel и конвертация NodeModelView с содержанием
            //к их моделям
            project = new ProjectModel();

            foreach (NodeModelView nodeVM in value.Nodes)
            {
                NodeModel nodeModel = new NodeModel()
                {
                    X = nodeVM.X,
                    Y = nodeVM.Y,
                    Width = nodeVM.Width,
                    Height = nodeVM.Height
                };
                nodeModel.InnerModel = (BaseModel)projectConverter.ConvertToModel(nodeVM.InnerContent!)!;
                nodeModel.Connections = new ConnectionModel[nodeVM.Connections.Count];

                for (int i = 0; i < nodeModel.Connections.Count(); i++)
                {
                    nodeModel.Connections[i] = (ConnectionModel)projectConverter.ConvertToModel(nodeVM.Connections[i])!;
                }

                project.Nodes.Add(nodeModel);
            }
        }
    }

    /// <summary>
    /// Событие вызова отчёта о действиях
    /// </summary>
    public event EventHandler<ReportArgs>? Report;

    /// <summary>
    /// Начало моделирования
    /// </summary>
    public void Start()
    {
        errors.Clear();
        warnings.Clear();
        Controller = null;

        BuildGPUEnvironment();

        StringBuilder reports = CreateErrorReport();

        Report?.Invoke(this, new ReportArgs(reports.ToString()));

        if (Controller == null)
        {
            Report?.Invoke(this, new ReportArgs("Моделирование не было запущено\n"));
            return;
        }

        //...
    }

    /// <summary>
    /// Контроллер GPU для моделирования
    /// </summary>
    private GPUController? Controller
    {
        get; set;
    }

    /// <summary>
    /// Формирование отчёта с предупреждениями и ошибками 
    /// </summary>
    /// <returns></returns>
    private StringBuilder CreateErrorReport()
    {
        StringBuilder sb = new();

        sb.AppendLine("=== Предупреждения ===");

        if (warnings.Count == 0)
            sb.AppendLine(" - Отсуствуют");
        else
            foreach (string warning in warnings)
            {
                sb.AppendLine($" - {warning}");
            }

        sb.AppendLine("======================");

        sb.AppendLine("=== Ошибки ===");

        if (errors.Count == 0)
            sb.AppendLine(" - Отсуствуют");
        else
            foreach (string error in errors)
            {
                sb.AppendLine($" - {error}");
            }

        sb.AppendLine("==============");

        return sb;
    }

    /// <summary>
    /// Иницилизация GPUController с компонентами
    /// </summary>
    private void BuildGPUEnvironment()
    {
        var controllers = project!.Nodes.FindAll((node) => node.InnerModel is GPUController);
        var vrams = project.Nodes.FindAll((node) => node.InnerModel is VRAM);
        var screenInterface = project.Nodes.FindAll((node) => node.InnerModel is ScreenInterface);
        var gpu = project.Nodes.FindAll((node) => node.InnerModel is GPU);
        var connectionInterface = project.Nodes.FindAll((node) => node.InnerModel is ConnectionInterface);

        //Проверка правильного кол-во узлов
        #region check1
        {
            List<(string, List<NodeModel>)> lists = new()
                {
                    ("Контроллеры GPU", controllers),
                    ("VRAM", vrams),
                    ("Интерфейсы подключения к монитору", screenInterface),
                    ("Ципы GPU", gpu),
                    ("Интерфейсы подключения к материнской плате", connectionInterface)
                };

            foreach ((string, List<NodeModel>) cortez in lists)
            {
                if (cortez.Item2.Count == 1)
                    continue;
                else if (cortez.Item2.Count > 1)
                {
                    if (cortez.Item2 == controllers)
                    {
                        errors.Enqueue("Контроллер GPU должен существовать в единственном экземпляре" +
                            $"Источник = \"{cortez.Item1}\"");
                        continue;
                    }
                    warnings.Enqueue("В проекте дублирующие друг друга узлы " +
                        $": Источник = \"{cortez.Item1}\"");
                    continue;
                }
                errors.Enqueue($"В проекте нет узлов необходимых для работы:" +
                    $" Источник = \"{cortez.Item1}\"");
            }
        }
        #endregion

        if (errors.Count != 0)
            return;

        #region attention

        GPUController controller = (controllers.First().InnerModel as GPUController)!;

        GPU? gpuCandidate = null;
        VRAM? vramCandidate = null;
        ConnectionInterface? connectionInterfaceCandidate = null;

        ScreenInterface? screenInterfaceCandidate = null;

        #endregion

        //Формирование списка соединений
        List<NodeConnectionInfo> conInfos = new(5);

        foreach (NodeModel node in project.Nodes)
        {
            foreach (ConnectionModel connection in node.Connections!)
            {
                conInfos.Add(new NodeConnectionInfo()
                {
                    SourseNode = node,
                    Id = connection.Id,
                    Type = connection.Type
                });
            }
        }

        //Список уже проверенных (исключённых) соединений
        List<NodeConnectionInfo> exceptionInfos = new(5);

        //Соединения контроллера
        var controllerConnections = conInfos.FindAll((info) =>
        {
            return info.SourseNode == controllers.First();
        });

        exceptionInfos.AddRange(controllerConnections);

        //Проверка соединений типа Duplex у контроллера 
        #region DuplexConnection


        var duplexConnections = controllerConnections.FindAll((info) => info.Type == ConnectionType.Duplex && (info.Id != null && info.Id != ""));

        if (duplexConnections.Count != 3)
        {
            errors.Enqueue("Неверное количество соединений для контроллера типа Duplex или ID соединений пусты");
            return;
        }

        foreach (NodeConnectionInfo info in duplexConnections)
        {
            var targetNodes = conInfos.FindAll((item) => item.Id == info.Id && item.SourseNode != info.SourseNode);

            if (targetNodes.Count > 1)
            {
                errors.Enqueue($"Слишком много соединений на один вывод : Источник = \"Контроллер | ID соединения : {info.Id}\"");
                return;
            }
            else if (targetNodes.Count < 1)
            {
                errors.Enqueue($"Нет соединений на вывод : Источник = \"Контроллер | ID соединения : {info.Id}\"");
                return;
            }

            if (exceptionInfos.Contains(targetNodes.First()))
            {
                errors.Enqueue($"Соединение на этот узел уже существует : Источник = \"ID соединения {info.Id}\"");
                return;
            }

            if (targetNodes.First().Type != ConnectionType.Duplex)
            {
                errors.Enqueue("Неверный тип соединения для целевого порта");
                return;
            }

            switch (targetNodes.First().SourseNode.InnerModel)
            {
                case VRAM:
                    vramCandidate = (VRAM)targetNodes.First().SourseNode.InnerModel!;
                    break;
                case GPU:
                    gpuCandidate = (GPU)targetNodes.First().SourseNode.InnerModel!;
                    break;
                case ConnectionInterface:
                    connectionInterfaceCandidate = (ConnectionInterface)targetNodes.First().SourseNode.InnerModel!;
                    break;
                case ScreenInterface:
                    errors.Enqueue("Нарушение правил : неверный тип соединения с экраном");
                    return;
                default:
                    break;
            }

            exceptionInfos.AddRange(targetNodes);
        }
        #endregion

        //Проверка соединений типа Sending у контроллера
        #region SendingConnection
        var sendingConnections = controllerConnections.FindAll((info) => info.Type == ConnectionType.Sending && (info.Id != null && info.Id != ""));

        if (sendingConnections.Count != 1)
        {
            errors.Enqueue("Неверное количество соединений для контроллера типа Duplex или ID соединений пусты");
            return;
        }

        foreach (NodeConnectionInfo info in sendingConnections)
        {
            var targetNodes = conInfos.FindAll((item) => item.Id == info.Id && item.SourseNode != info.SourseNode);

            if (targetNodes.Count > 1)
            {
                errors.Enqueue($"Слишком много соединений на один вывод : Источник = \"Контроллер | ID соединения : {info.Id}\"");
                return;
            }
            else if (targetNodes.Count < 1)
            {
                errors.Enqueue($"Нет соединений на вывод : Источник = \"Контроллер | ID соединения : {info.Id}\"");
                return;
            }

            if (exceptionInfos.Contains(targetNodes.First()))
            {
                errors.Enqueue($"Соединение на этот узел уже существует : Источник = \"ID соединения {info.Id}\"");
                return;
            }

            if (targetNodes.First().Type != ConnectionType.Getting)
            {
                errors.Enqueue("Неверный тип соединения для целевого порта");
                return;
            }

            switch (targetNodes.First().SourseNode.InnerModel)
            {
                case VRAM:
                    errors.Enqueue("Нарушение правил : неверный тип соединения с памятью");
                    return;
                case GPU:
                    errors.Enqueue("Нарушение правил : неверный тип соединения с ципом GPU");
                    return;
                case ConnectionInterface:
                    errors.Enqueue("Нарушение правил : неверный тип соединения с материнской платой");
                    return;
                case ScreenInterface:
                    screenInterfaceCandidate = (ScreenInterface)targetNodes.First().SourseNode.InnerModel!;
                    break;
                default:
                    break;
            }

            exceptionInfos.AddRange(targetNodes);
        }
        #endregion

        //Проверка на недостижимые узлы
        #region checkN-1
        exceptionInfos.AddRange(conInfos.FindAll((info) => info.Id == null || info.Id == ""));

        foreach (NodeConnectionInfo item in conInfos.Except(exceptionInfos))
        {
            exceptionInfos.Add(item);
        }
        #endregion

        //Кол-во проверенных (исключённых) узлов должен совпадать с общем кол-во
        if (exceptionInfos.Count != conInfos.Count)
            return;

        //Проверка кандидатов на моделирование
        #region checkN
        if (screenInterfaceCandidate != null)
            controller.ScreenInterface = screenInterfaceCandidate;
        else
            errors.Enqueue("Не обнаружено соединение контроллера и экрана");

        if (gpuCandidate != null)
            controller.GPU = gpuCandidate;
        else
            errors.Enqueue("Не обнаружено соединение контроллера и ципа GPU");

        if (connectionInterfaceCandidate != null)
            controller.ConnectionInterface = connectionInterfaceCandidate;
        else
            errors.Enqueue("Не обнаружено соединение контроллера и порта подключения видеокарты");

        if (vramCandidate != null)
            controller.VRAM = vramCandidate;
        else
            errors.Enqueue("Не обнаружено соединение контроллера и памяти");
        #endregion

        if (errors.Count != 0)
            return;
        //Всё правильно
        Controller = controller;
    }
}



