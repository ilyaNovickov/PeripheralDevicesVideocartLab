using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using VideocartLab.ModelViews.Models;
using System.Text.Json.Serialization.Metadata;
using VideocartLab.MainModelsProj;
using VideocartLab.MainModelsProj.GPUMemory;
using VideocartLab.MainModelsProj.Screen;
using VideocartLab.MainModelsProj.ConnectionInterface;

namespace VideocartLab.ModelViews;

public class MainWindowModelView : ModelViewBase
{
    private ProjectModelView? projectVM;
    private NodeListModelView? nodeListVM;
    private NodeFactoryService factoryService;
    private StringBuilder report = new StringBuilder();

    VideocartLab.ModelViews.Models.ModelingEnvironment modelingEnvironment;

    public MainWindowModelView()
    {
        factoryService = new NodeFactoryService();

        projectVM = new ProjectModelView(factoryService);
        nodeListVM = new NodeListModelView(factoryService);

        projectVM.NodeAdded += ProjectVM_NodeAdded;
        nodeListVM.SelectedItemChanged += NodeListVM_SelectedItemChanged;

        removeSelectedNode = new RelayCommand(projectVM.RemoveSelectedNode);
        removeNode = new GenericCommand<bool>(projectVM.ToggleRemoveMode);
        startModeling = new RelayCommand(this.StartModeling);

        modelingEnvironment = new();
        modelingEnvironment.Report += ModelingEnvironment_Report;
    }

    private void ModelingEnvironment_Report(object? sender, ReportArgs e)
    {
        report.AppendLine(e.Message);
        report.AppendLine();
        OnPropertyChanged(nameof(Report));
    }

    public string Report
    {
        get => report.ToString();
    }

    private void ProjectVM_NodeAdded(object? sender, NodeAddedArgs e)
    {
        nodeListVM!.SelectedItem = null;
    }

    private void NodeListVM_SelectedItemChanged(object? sender, SelectedNodeItemChangedArgs e)
    {
        if (e.NewItem == null)
            projectVM!.CandidateToAdd = null;
        else
            projectVM!.CandidateToAdd = e.NewItem.NodeType;
    }

    public ProjectModelView? Project
    {
        get => projectVM;
        set
        {
            projectVM = value;
            OnPropertyChanged();
        }
    }

    public NodeListModelView? NodeList
    {
        get => nodeListVM;
        set
        {
            nodeListVM = value;
            OnPropertyChanged();
        }
    }

    private RelayCommand removeSelectedNode;

    public RelayCommand RemoveSelectedNodeCommand => removeSelectedNode;

    private GenericCommand<bool> removeNode;

    public GenericCommand<bool> RemoveNodeCommand => removeNode;

    private RelayCommand startModeling;

    public RelayCommand StartModelingCommand => startModeling;

    private void StartModeling()
    {
        modelingEnvironment.ProjectVM = Project!;
        modelingEnvironment.Start();


    }

    public void SaveProject(string path)
    {
        ProjectConverter projectConverter = new();
        ProjectModel project = new ProjectModel();

        foreach (NodeModelView nodeVM in projectVM.Nodes)
        {
            NodeModel nodeModel = new NodeModel()
            {
                Name = nodeVM.Name,
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

        ProjectSerializer.SerializeToFileAsync(project, path);
    }

    public async void LoadProject(string path)
    {
        var projectModelTask = await ProjectSerializer.DeserializeFromFileAsync(path);

        Project.Nodes.Clear();

        Project.CandidateToAdd = null;
        Project.SetIdleMode();

        IEnumerable<NodeModelView> nodesVM = ProjectVMConverter.ConvertToModelView(projectModelTask!.Nodes);

        foreach (NodeModelView nodeVM in nodesVM)
        {
            Project.Nodes.Add(nodeVM);
        }
    }
}

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
            ConnectionModel conn = model as ConnectionModel;
            ConnectionModelView connVM = new()
            {
                Id = conn.Id,
                Type = conn.Type
            };
            return connVM;
        });
        #region Models
        modelViewDict.Add(typeof(GPU), (model) =>
        {
            GPU gpu = model as GPU;
            GPUContentModelView gpuVM = new()
            {
                Name = gpu.Name,
                Cores = gpu.Cores,
                Frequency = gpu.Frequency,
                TextureMappingUnits = gpu.TextureMappingUnits,
                RenderOutputPipelines = gpu.RenderOutputPipelines
            };
            return gpuVM;
        });
        modelViewDict.Add(typeof(VRAM), (model) => {
            VRAM? vram = model as VRAM;
            VRAMModelView vramVM = new VRAMModelView()
            {
                Capacity = vram.Capacity,
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
                ScreenHeight = screen.MaxHeight,
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

public static class ProjectSerializer
{
    private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
    {
        WriteIndented = true,
        // Если хочешь сериализовать object (InnerModel), стоит добавить:
         IncludeFields = true,
         ReferenceHandler = ReferenceHandler.Preserve,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public static string Serialize(ProjectModel project)
    {
        return JsonSerializer.Serialize(project, _options);
    }

    public static ProjectModel? Deserialize(string json)
    {
        return JsonSerializer.Deserialize<ProjectModel>(json, _options);
    }

    // Для сохранения в файл
    public static async Task SerializeToFileAsync(ProjectModel project, string filePath)
    {
        using var stream = File.Create(filePath);
        await JsonSerializer.SerializeAsync(stream, project, _options);
    }

    // Для загрузки из файла
    public static async Task<ProjectModel?> DeserializeFromFileAsync(string filePath)
    {
        using var stream = File.OpenRead(filePath);
        return await JsonSerializer.DeserializeAsync<ProjectModel>(stream, _options);
    }
}