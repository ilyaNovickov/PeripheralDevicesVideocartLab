using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using VideocartLab.ModelViews;

namespace VideocartLab.Views.AvaloniaExtraControlsSol;

public partial class NodeListView : UserControl
{
    public static readonly StyledProperty<NodeListModelView?> NodeListModelViewProperty =
        StyledProperty<NodeListModelView?>.Register<ProjectView, NodeListModelView?>(nameof(NodeListModelView));

    public NodeListView()
    {
        InitializeComponent();
    }

    public NodeListModelView? NodeListModelView
    {
        get => GetValue(NodeListModelViewProperty);
        set
        {
            SetValue(NodeListModelViewProperty, value);
            DataContext = value;
        }
    }
}