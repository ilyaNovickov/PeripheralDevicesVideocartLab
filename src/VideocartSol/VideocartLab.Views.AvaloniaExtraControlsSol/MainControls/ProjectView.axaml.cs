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


}