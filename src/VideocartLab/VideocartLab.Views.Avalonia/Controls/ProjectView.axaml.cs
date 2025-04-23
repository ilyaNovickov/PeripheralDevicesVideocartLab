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
        this.DataContext = projectModelView;

        factory = new NodeFactory();

        projectModelView.NodeModelViewAdded += ProjectModelView_NodeModelViewAdded;

        
    }

    public void AddNode(NodeModelView node)
    {
        projectModelView.AddNode(node);
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

        //AddNode(new NodeModelView()
        //{
        //    X = p.X, Y = p.Y, Height = 100, Width = 100, Content = "HH"
        //});
        AddNode(factory.Create(p.X, p.Y, 100, 100, "HH"));
    }
}