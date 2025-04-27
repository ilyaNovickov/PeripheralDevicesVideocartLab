using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.ObjectModel;
using Videocart.Models;
using Videocart.ViewModel;
using Videocart.ViewModel.Extra;
using Videocart.Views.AvaloniaProj.Helpers;

namespace Videocart.Views.AvaloniaProj;

public partial class ProjectView : UserControl
{
    private ProjectViewModel? projectViewModel;

    public ProjectView()
    {
        InitializeComponent();

        InitContextMenu();
    }

    public ProjectViewModel? ProjectViewModel
    {
        get => projectViewModel;
        set
        {
            if (value == null && projectViewModel is not null)
            {
                projectViewModel.NodeAddedArgs -= ProjectViewModel_NodeAddedArgs;
            }   

            projectViewModel = value;
            DataContext = projectViewModel;

            if (projectViewModel is not null)
            {
                projectViewModel.NodeAddedArgs += ProjectViewModel_NodeAddedArgs;
            }
        }
    }

    private void ProjectViewModel_NodeAddedArgs(object? sender, ViewModel.Events.NodeViewModelAddedArgs e)
    {
        NodeView nodeView = new NodeView()
        {
            NodeViewModel = e.NodeViewModel
        };

        innerCanvas.Children.Add(nodeView);
    }

    private void InitContextMenu()
    {

        foreach (NodeFactoryAction action in NodeFactory.Actions)
        {
            MenuItem menuItem = new MenuItem();
            menuItem.Header = action.Name;
            menuItem.Click += (sender, e) =>
            {
                ProjectViewModel!.AddNode(action.Func);
            };
            contextMenu.Items.Add(menuItem);
        }
        
    }

    private void Canvas_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        if (e.Handled) return;

        var p = e.GetPosition(innerCanvas);
        MouseButton button = MouseHelper.GetButton(e.GetCurrentPoint(innerCanvas).Properties);

        ProjectViewModel!.OnMousePressed(p.X, p.Y, button);
    }

    private void Canvas_PointerMoved(object? sender, Avalonia.Input.PointerEventArgs e)
    {
        var p = e.GetPosition(innerCanvas);
        ProjectViewModel!.OnMouseMoved(p.X, p.Y);
    }
}