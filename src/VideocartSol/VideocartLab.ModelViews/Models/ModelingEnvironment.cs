using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using VideocartLab.MainModelsProj;
using VideocartLab.MainModelsProj.ConnectionInterface;
using VideocartLab.MainModelsProj.GPUMemory;
using VideocartLab.MainModelsProj.Screen;

namespace VideocartLab.ModelViews.Models;

internal class ModelingEnvironment
{
    private ProjectConverter projectConverter = new();
    private ProjectModel? project = null;

    Queue<string> errors = new Queue<string>();
    Queue<string> warnings = new Queue<string>();

    public ModelingEnvironment()
    {

    }

    public ProjectModelView ProjectVM
    {
        set
        {
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
                nodeModel.InnerModel = projectConverter.ConvertToModel(nodeVM.InnerContent!);
                nodeModel.Connections = new ConnectionModel[nodeVM.Connections.Count];

                for (int i = 0; i < nodeModel.Connections.Count(); i++)
                {
                    nodeModel.Connections[i] = (ConnectionModel)projectConverter.ConvertToModel(nodeVM.Connections[i])!;
                }

                project.Nodes.Add(nodeModel);
            }
        }
    }

    public event EventHandler<ReportArgs>? Report;

    public void Start()
    {
        errors.Clear();
        warnings.Clear();
        Controller = null;

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        BuildGPUEnvironment();
        stopwatch.Stop();

        var reports = CreateErrorReport();

        Report?.Invoke(this, new ReportArgs(reports.ToString()));

        if (Controller == null)
        {
            Report?.Invoke(this, new ReportArgs("Моделирование не было запущено\n"));
            return;
        }
    }

    private void BuildGPUEnvironment()
    {
        var controllers = project!.Nodes.FindAll((node) => node.InnerModel is GPUController);
        var vrams = project.Nodes.FindAll((node) => node.InnerModel is VRAM);
        var screenInterface = project.Nodes.FindAll((node) => node.InnerModel is ScreenInterface);
        var gpu = project.Nodes.FindAll((node) => node.InnerModel is GPU);
        var connectionInterface = project.Nodes.FindAll((node) => node.InnerModel is ConnectionInterface);

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

        if (errors.Count != 0)
            return;

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

        List<NodeConnectionInfo> exceptionInfos = new(5);

        GPUController controller = (controllers.First().InnerModel as GPUController)!;

        var controllerConnections = conInfos.FindAll((info) =>
        {
            return info.SourseNode == controllers.First();
        });

        exceptionInfos.AddRange(controllerConnections);

        //duplex
        #region DuplexConnection
        GPU? gpuCandidate = null;
        VRAM? vramCandidate = null;
        ConnectionInterface? connectionInterfaceCandidate = null;

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

        //sending
        #region SendingConnection
        ScreenInterface? screenInterfaceCandidate = null;

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

        exceptionInfos.AddRange(conInfos.FindAll((info) => info.Id == null || info.Id == ""));

        foreach (NodeConnectionInfo item in conInfos.Except(exceptionInfos))
        {
            exceptionInfos.Add(item);
        }

        if (exceptionInfos.Count != conInfos.Count)
            return;

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

        if (errors.Count != 0)
            return;

        Controller = controller;
    }

    private GPUController? Controller
    {
        get; set;
    }

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
}

internal struct NodeConnectionInfo
{
    public NodeModel SourseNode { get; set; }
    public string? Id { get; set; }
    public  ConnectionType Type { get; set; }
}

internal class ReportArgs : EventArgs
{
    public string Message { get; private set; }

    public ReportArgs(string message)
    {
        Message = message;
    }
}