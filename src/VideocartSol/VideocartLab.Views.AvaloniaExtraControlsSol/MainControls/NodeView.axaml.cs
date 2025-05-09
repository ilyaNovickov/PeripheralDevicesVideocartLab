using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using VideocartLab.ModelViews;

namespace VideocartLab.Views.AvaloniaExtraControlsSol;

public partial class NodeView : UserControl
{
    public static readonly StyledProperty<NodeModelView?> NodeVMProperty =
        StyledProperty<NodeModelView?>.Register<NodeView, NodeModelView?>(nameof(NodeVM));

    public static readonly StyledProperty<double> XProperty = Canvas.LeftProperty.AddOwner<NodeView>();
    public static readonly StyledProperty<double> YProperty = Canvas.TopProperty.AddOwner<NodeView>();

    public NodeView()
    {
        InitializeComponent();

        DataContext = new NodeModelView()
        {
            Name = "FOOO1"
        };
    }

    public NodeModelView? NodeVM
    {
        get => GetValue(NodeVMProperty);
        set
        {
            SetValue(NodeVMProperty, value);
        }
    }

    public double X
    {
        get => GetValue(XProperty);
        set => SetValue(XProperty, value);
    }

    public double Y
    {
        get => GetValue(YProperty);
        set => SetValue(YProperty, value);
    }
}