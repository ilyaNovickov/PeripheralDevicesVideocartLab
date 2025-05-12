using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using VideocartLab.ModelViews;

namespace VideocartLab.Views.AvaloniaExtraControlsSol;

public partial class ProjectView : UserControl
{
    public static readonly StyledProperty<ProjectModelView?> ProjectVMProperty =
        StyledProperty<ProjectModelView?>.Register<ProjectView, ProjectModelView?>(nameof(ProjectVM));

    public ProjectView()
    {
        InitializeComponent();
    }

    public ProjectModelView? ProjectVM
    {
        get => GetValue(ProjectVMProperty);
        set
        {
            if (ProjectVM != null)
            {
                ProjectVM.NodeAdded -= Project_NodeAdded;
            }

            SetValue(ProjectVMProperty, value);

            if (value == null)
                return;

            value.NodeAdded += Project_NodeAdded;
        }
    }

    private void Project_NodeAdded(object? sender, NodeAddedArgs e)
    {
        var node = e.AddedNode;

        NodeView nodeView = new NodeView()
        {
            DataContext = node,
            NodeVM = node
        };

        mainCanvas.Children.Add(nodeView);
    }

    private void Canvas_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        if (e.Handled) return;

        var properties = e.GetCurrentPoint(mainCanvas).Properties;

        if (!properties.IsLeftButtonPressed)
            return;

        var p = e.GetPosition(mainCanvas);

        ProjectVM.OnPointerPressed(p.X, p.Y);
    }

    private void Canvas_PointerMoved(object? sender, Avalonia.Input.PointerEventArgs e)
    {
        if (e.Handled) return;


    }

    private void Canvas_PointerReleased(object? sender, Avalonia.Input.PointerReleasedEventArgs e)
    {
        if (e.Handled) return;


    }
}