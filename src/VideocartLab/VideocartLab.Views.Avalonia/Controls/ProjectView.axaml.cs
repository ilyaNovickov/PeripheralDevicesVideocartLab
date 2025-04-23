using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using VideocartLab.ModelVIews;

namespace VideocartLab.Views.Avalonia;

public partial class ProjectView : UserControl
{
    private ProjectModelView projectModelView;

    public ProjectView()
    {
        InitializeComponent();

        projectModelView = new ProjectModelView();
        this.DataContext = projectModelView;

        projectModelView.NodeModelViewAdded += ProjectModelView_NodeModelViewAdded;

        AddNode(new NodeModelView(new Models.Node()
        {
            X = 100, Y = 100,
            Width = 150, Height = 150,
            Content = "Hello world"
        }));
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
}