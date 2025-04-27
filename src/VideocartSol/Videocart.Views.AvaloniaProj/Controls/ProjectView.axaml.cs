using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.ObjectModel;
using Videocart.Models;
using Videocart.ViewModel;
using Videocart.ViewModel.Extra;

namespace Videocart.Views.AvaloniaProj;

public partial class ProjectView : UserControl
{
    public static readonly StyledProperty<ProjectViewModel?> ProjectViewModelProperty =
        AvaloniaProperty<ProjectViewModel>.Register<ProjectView, ProjectViewModel?>(nameof(ProjectViewModel));


    private ObservableCollection<NodeFactoryAction>? actions = null;

    public ProjectView()
    {
        InitializeComponent();

        InitContextMenu();
    }

    public ProjectViewModel? ProjectViewModel
    {
        get => GetValue(ProjectViewModelProperty);
        set
        {
            SetValue(ProjectViewModelProperty, value);
        }
    }

    private void InitContextMenu()
    {

        foreach (NodeFactoryAction action in NodeFactory.Actions)
        {
            MenuItem menuItem = new MenuItem();
            menuItem.Header = action.Name;
            menuItem.Click += (sender, e) =>
            {
                ProjectViewModel.AddNode(action.Func);
            };
            contextMenu.Items.Add(menuItem);
        }
        
    }

    private void Canvas_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {

    }
}