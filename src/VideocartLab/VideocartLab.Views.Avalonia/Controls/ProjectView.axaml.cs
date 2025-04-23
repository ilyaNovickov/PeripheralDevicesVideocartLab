using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using VideocartLab.ModelVIews;
using VideocartLab.Views.Avalonia.Helpers;

namespace VideocartLab.Views.Avalonia;

public partial class ProjectView : UserControl
{
    private ProjectModelView projectModelView;

    public ProjectView()
    {
        InitializeComponent();

        projectModelView = new ProjectModelView();
        projectModelView.Factory = new NodeFactory();
        this.DataContext = projectModelView;

        projectModelView.NodeModelViewAdded += ProjectModelView_NodeModelViewAdded;
    }

    //ќбработка добавлени€ узла в проект
    private void ProjectModelView_NodeModelViewAdded(object? sender, NodeModelViewAddedArgs e)
    {
        NodeModelView nodeModelView = e.Node;

        NodeView nodeView = new(nodeModelView);

        canvas.Children.Add(nodeView);
    }

    //обработка нажати€ по холсту
    private void Canvas_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (e.Handled)
            return;

        VideocartLab.ModelVIews.MouseButton button = MouseButtonHelper.GetMouseButton(e.GetCurrentPoint(canvas));

        //—редн€€ кнопка мыши зарезервирована под перемещение
        if (button == VideocartLab.ModelVIews.MouseButton.Middle)
            return;

        var p = e.GetPosition(canvas);

        projectModelView.OnMousePressed(p.X, p.Y, button);

        //AddNode(factory.Create(p.X, p.Y, 100, 100, "HH"));
    }

    //ќбработка перемещени€ курсора мыши
    private void Canvas_PointerMoved(object? sender, PointerEventArgs e)
    {
        var p = e.GetPosition(canvas);

        projectModelView.OnMouseMoved(p.X, p.Y);
    }
}

