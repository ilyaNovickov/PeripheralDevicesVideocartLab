using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using VideocartLab.ModelVIews;

namespace VideocartLab.Views.Avalonia;

public partial class ProjectView : UserControl
{
    private ProjectModelView projectModelView;

    private NodeFactory factory;

    public ProjectView()
    {
        InitializeComponent();

        projectModelView = new ProjectModelView();
        projectModelView.Factory = new NodeFactory();
        this.DataContext = projectModelView;

        projectModelView.NodeModelViewAdded += ProjectModelView_NodeModelViewAdded;

        
    }

    

    private void ProjectModelView_NodeModelViewAdded(object? sender, NodeModelViewAddedArgs e)
    {
        NodeModelView nodeModelView = e.Node;

        NodeView nodeView = new(nodeModelView);

        canvas.Children.Add(nodeView);
    }


    private void Canvas_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (e.Handled)
            return;

        var p = e.GetPosition(canvas);

        projectModelView.OnMousePressed(p.X, p.Y, MouseButtonHelper.GetMouseButton(e.GetCurrentPoint(canvas)));

        //AddNode(factory.Create(p.X, p.Y, 100, 100, "HH"));
    }

    private void Canvas_PointerMoved(object? sender, PointerEventArgs e)
    {
        var p = e.GetPosition(canvas);

        projectModelView.OnMouseMoved(p.X, p.Y);
    }
}

public static class MouseButtonHelper
{
    public static VideocartLab.ModelVIews.MouseButton GetMouseButton(PointerPoint pointer)
    {
        if (pointer.Properties.IsLeftButtonPressed)
            return VideocartLab.ModelVIews.MouseButton.Left;
        else if (pointer.Properties.IsRightButtonPressed)
            return VideocartLab.ModelVIews.MouseButton.Right;
        else if (pointer.Properties.IsMiddleButtonPressed)
            return VideocartLab.ModelVIews.MouseButton.Middle;
        else
            return VideocartLab.ModelVIews.MouseButton.Undef;
    }
}