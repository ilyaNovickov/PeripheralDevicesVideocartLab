using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using VideocartLab.MainModelsProj;

namespace VideocartLab.ModelViews;

/// <summary>
/// Дествие контроллера
/// </summary>
public class GPUAction
{
    /// <summary>
    /// Действие 
    /// </summary>
    public GPUActions Action { get; private set; }

    /// <summary>
    /// Имя действия
    /// </summary>
    public string Name { get; private set; }

    public GPUAction(GPUActions action, string name)
    {
        Action = action;
        Name = name;
    }
}

/// <summary>
/// Контроллер GPU
/// </summary>
public class GPUControllerModelView : ModelViewBase
{
    private ObservableCollection<GPUAction> actions;
    private GPUAction? selectedAction = null;
    private RelayCommand moveUp;
    private RelayCommand moveDown;

    public GPUControllerModelView()
    {
        InitList();

        moveUp = new RelayCommand(MoveItemUp);
        moveDown = new RelayCommand(MoveItemDown);
    }

    /// <summary>
    /// Иницилизация списка действий контроллера
    /// </summary>
    private void InitList()
    {
        List<GPUAction> actions = new(30);

        actions.Add(new GPUAction(GPUActions.Init, "Инициализация устройства"));
        //Согласование с экраном
        actions.Add(new GPUAction(GPUActions.HandshakeWithScreenStart, "НАЧАЛО согласования с экраном"));
        actions.Add(new GPUAction(GPUActions.DesicionSolution, "Согласование с монитором разрешения экрана"));
        actions.Add(new GPUAction(GPUActions.DesicionColorDepth, "Согласование с монитором глубины цвета"));
        actions.Add(new GPUAction(GPUActions.DesicionFrameRate, "Согласование с монитором частоты кадров"));
        actions.Add(new GPUAction(GPUActions.HandshakeWithScreenEnd, "КОНЕЦ согласования с экраном"));

        actions.Add(new GPUAction(GPUActions.CPUSentsData, "Процессор посылает данные на обработку"));

        //Размещение данных в памяти
        actions.Add(new GPUAction(GPUActions.ControllerPlaceDataInVRAMStart, "НАЧАЛО контроллер GPU размещает данные в памяти VRAM"));
        actions.Add(new GPUAction(GPUActions.PlaceModels, "Размещение информации о моделях (вершинах)"));
        actions.Add(new GPUAction(GPUActions.PlaceTextures, "Размещение текстур"));
        actions.Add(new GPUAction(GPUActions.PlaceSceneInfo, "Размещение информации о сцене"));
        actions.Add(new GPUAction(GPUActions.ReservingPlaceForImage, "Резервирования места под формируемое изображение"));
        actions.Add(new GPUAction(GPUActions.ControllerPlaceDataInVRAMEnd, "КОНЕЦ контроллер GPU размещает данные в памяти VRAM"));

        //Обработка данных
        actions.Add(new GPUAction(GPUActions.GPUCalculateDataStart, "НАЧАЛО обработки данных"));
        actions.Add(new GPUAction(GPUActions.TransformModels, "Трансформация вершин 3D-моделей"));
        actions.Add(new GPUAction(GPUActions.ProjectionModelsTo2DScreen, "Проецирование моделей на 2D плоскость"));
        actions.Add(new GPUAction(GPUActions.UseTextureAndShaders, "Применение текстур, фильтрации и шейдеров"));
        actions.Add(new GPUAction(GPUActions.RasterizationAndCreationImange, "Растеризация и запись изображения в память"));
        actions.Add(new GPUAction(GPUActions.GPUCalculateDataEnd, "КОНЕЦ обработки данных"));

        //actions.Add(new GPUAction(GPUActions.ControllerPlaceImageInVRAM, "Контроллер размещает изображение в памяти VRAM"));
        actions.Add(new GPUAction(GPUActions.ControllerSentImageToScreen, "Отправка изображения на экран"));
        actions.Add(new GPUAction(GPUActions.ControllerFreeDataInVRAM, "Освобождение данных из памяти"));

#if DEBUG

#else
        //Перетасовка элементов списка
        Random.Shared.Shuffle(CollectionsMarshal.AsSpan(actions));
#endif

        this.actions = new ObservableCollection<GPUAction>(actions);
    }


    /// <summary>
    /// Список действий контроллера
    /// </summary>
    public ObservableCollection<GPUAction> GpuActions => actions;

    /// <summary>
    /// Выбранное действие из списка
    /// </summary>
    public GPUAction? SelectedAction
    {
        get => selectedAction;
        set
        {
            selectedAction = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Команда перемещение выбранного элемента списка вверх
    /// </summary>
    public RelayCommand MoveUpCommand => moveUp;

    /// <summary>
    /// Команда перемещение выбранного элемента списка вниз
    /// </summary>
    public RelayCommand MoveDownCommand => moveDown;

    /// <summary>
    /// Перемещение выбранного элемента вверх списка
    /// </summary>
    private void MoveItemUp()
    {
        if (SelectedAction == null)
            return;

        int index = GpuActions.IndexOf(SelectedAction);

        if (index == 0)
            return;

        var selectedAction = SelectedAction;
        (GpuActions[index - 1], GpuActions[index]) = (GpuActions[index], GpuActions[index - 1]);
        SelectedAction = selectedAction;
    }

    /// <summary>
    /// Перемещение выбранного элемента списка сниз
    /// </summary>
    private void MoveItemDown()
    {
        if (SelectedAction == null)
            return;

        int index = GpuActions.IndexOf(SelectedAction);

        if (index == GpuActions.Count - 1)
            return;

        var selectedAction = SelectedAction;
        (GpuActions[index], GpuActions[index + 1]) = (GpuActions[index + 1], GpuActions[index]);
        SelectedAction = selectedAction;
    }
}

