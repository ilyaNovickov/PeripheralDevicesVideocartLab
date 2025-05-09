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
            SetValue(ProjectVMProperty, value);
        }
    }

    private void Canvas_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        if (e.Handled) return;

        var node = ProjectVM.AddNode();

        if (node == null) return;

        NodeView view = new NodeView();
        view.DataContext = node;
        view.NodeVM = node;

        mainCanvas.Children.Add(view);
    }
}