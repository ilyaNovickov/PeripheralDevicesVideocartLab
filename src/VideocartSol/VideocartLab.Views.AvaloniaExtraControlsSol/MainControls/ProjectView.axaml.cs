using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using VideocartLab.ModelViews;

namespace VideocartLab.Views.AvaloniaExtraControlsSol;

public partial class ProjectView : UserControl
{
    public static readonly StyledProperty<ProjectModelView?> ProjectViewModelProperty =
        StyledProperty<ProjectModelView?>.Register<ProjectView, ProjectModelView?>(nameof(ProjectViewModel));

    public ProjectView()
    {
        InitializeComponent();
    }

    public ProjectModelView? ProjectViewModel
    {
        get => GetValue(ProjectViewModelProperty);
        set
        {
            SetValue(ProjectViewModelProperty, value);
            DataContext = value;
        }
    }


}