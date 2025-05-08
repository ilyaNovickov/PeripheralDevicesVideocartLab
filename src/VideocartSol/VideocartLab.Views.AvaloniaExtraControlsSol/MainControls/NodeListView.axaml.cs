using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using VideocartLab.ModelViews;

namespace VideocartLab.Views.AvaloniaExtraControlsSol;

public partial class NodeListView : UserControl
{
    public static readonly StyledProperty<NodeListModelView?> NodeListVMProperty =
        StyledProperty<NodeListModelView?>.Register<ProjectView, NodeListModelView?>(nameof(NodeListVM));

    public NodeListView()
    {
        InitializeComponent();
    }

    public NodeListModelView? NodeListVM
    {
        get => GetValue(NodeListVMProperty);
        set
        {
            SetValue(NodeListVMProperty, value);
        }
    }
}