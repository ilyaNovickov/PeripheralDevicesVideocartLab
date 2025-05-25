using System.Text;
using VideocartLab.MainModelsProj;
using VideocartLab.ModelViews.Models;
using VideocartLab.ModelViews.Serializaion;

namespace VideocartLab.ModelViews;

/// <summary>
/// ModelView для основного окна
/// </summary>
public class MainWindowModelView : ModelViewBase
{
    private ProjectModelView? projectVM;
    private NodeListModelView? nodeListVM;
    private NodeFactoryService factoryService;
    private StringBuilder report = new StringBuilder();

    private RelayCommand removeSelectedNode;
    private GenericCommand<bool> removeNode;
    private RelayCommand startModeling;

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

    #region Properties
    /// <summary>
    /// Отчёт о симуляции
    /// </summary>
    public string Report => report.ToString();

    /// <summary>
    /// ProjectModelView
    /// </summary>
    public ProjectModelView? Project
    {
        get => projectVM;
        set
        {
            projectVM = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// NodeListModelView
    /// </summary>
    public NodeListModelView? NodeList
    {
        get => nodeListVM;
        set
        {
            nodeListVM = value;
            OnPropertyChanged();
        }
    }
    #endregion

    #region Commands
    /// <summary>
    /// Команда удаления выбранного узла
    /// </summary>
    public RelayCommand RemoveSelectedNodeCommand => removeSelectedNode;

    /// <summary>
    /// Команда перехода в режим удаления узла
    /// </summary>
    public GenericCommand<bool> RemoveNodeCommand => removeNode;

    /// <summary>
    /// Команда запуска ма=оделировани
    /// </summary>
    public RelayCommand StartModelingCommand => startModeling;

    /// <summary>
    /// ЗАпуск симуляции
    /// </summary>
    private void StartModeling()
    {
        modelingEnvironment.ProjectVM = Project!;
        modelingEnvironment.Start();
    }
    #endregion

    #region EventHandlers
    /// <summary>
    /// Обработка события формирования отчёта о симуляции
    /// </summary>
    /// <param name="sender">Источник события</param>
    /// <param name="e">Аргументы события</param>
    private void ModelingEnvironment_Report(object? sender, ReportArgs e)
    {
        report.AppendLine(e.Message);
        report.AppendLine();
        OnPropertyChanged(nameof(Report));
    }

    /// <summary>
    /// Обработка события формирования отчёта о симуляции
    /// </summary>
    /// <param name="sender">Источник события</param>
    /// <param name="e">Аргументы события</param>
    private void ProjectVM_NodeAdded(object? sender, NodeAddedArgs e)
    {
        nodeListVM!.SelectedItem = null;
    }

    /// <summary>
    /// Изменния выбранного узла на добавление
    /// </summary>
    /// <param name="sender">Источник события</param>
    /// <param name="e">Аргументы события</param>
    private void NodeListVM_SelectedItemChanged(object? sender, SelectedNodeItemChangedArgs e)
    {
        if (e.NewItem == null)
            projectVM!.CandidateToAdd = null;
        else
            projectVM!.CandidateToAdd = e.NewItem.NodeType;
    }
    #endregion

    public void ClearReport()
    {
        report.Clear();
        OnPropertyChanged(nameof(Report));
    }

    [Obsolete]
    public void SaveProject(string path)
    {
        ProjectConverter projectConverter = new();
        ProjectModel project = new ProjectModel();

        foreach (NodeModelView nodeVM in projectVM!.Nodes)
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

        using var _ = ProjectSerializer.SerializeToFileAsync(project, path);
    }

    [Obsolete]
    public async void LoadProject(string path)
    {
        var projectModelTask = await ProjectSerializer.DeserializeFromFileAsync(path);

        Project!.Nodes.Clear();

        Project.CandidateToAdd = null;
        Project.SetIdleMode();

        IEnumerable<NodeModelView> nodesVM = ProjectVMConverter.ConvertToModelView(projectModelTask!.Nodes);

        foreach (NodeModelView nodeVM in nodesVM)
        {
            Project.Nodes.Add(nodeVM);
        }
    }
}

