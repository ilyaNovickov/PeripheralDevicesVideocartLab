using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using VideocartLab.ModelVIews;

namespace VideocartLab.Views.Avalonia;

public partial class NodeView : UserControl
{
    public static StyledProperty<double> XProperty = Canvas.LeftProperty.AddOwner<NodeView>();
    public static StyledProperty<double> YProperty = Canvas.TopProperty.AddOwner<NodeView>();

    private NodeModelView? nodeModelView;

    public NodeView()
    {
        InitializeComponent();
    }

    public NodeView(NodeModelView nodeModelView) : this()
    {
        this.nodeModelView = nodeModelView;
        DataContext = this.nodeModelView;

        Binding bindingX = new();
        bindingX.Source = nodeModelView;
        bindingX.Path = nameof(nodeModelView.X);

        this.Bind(XProperty, bindingX);

        Binding bindingY = new();
        bindingY.Source = nodeModelView;
        bindingY.Path = nameof(nodeModelView.Y);

        this.Bind(YProperty, bindingY);
    }

    public double X
    {
        get => GetValue(XProperty);
        set
        {
            SetValue(XProperty, value);
        }
    }

    public double Y
    {
        get => GetValue(YProperty);
        set
        {
            SetValue(YProperty, value);
        }
    }

    private void Panel_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        e.Handled = true;
    }
}