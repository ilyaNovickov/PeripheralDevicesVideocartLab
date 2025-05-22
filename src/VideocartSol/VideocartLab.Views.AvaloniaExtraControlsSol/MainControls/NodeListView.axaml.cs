using Avalonia;
using Avalonia.Controls;
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

    public event EventHandler<SelectionChangedEventArgs>? SelectionChanged;

    private void ListBox_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
    {
        SelectionChanged?.Invoke(this, e);
    }
}