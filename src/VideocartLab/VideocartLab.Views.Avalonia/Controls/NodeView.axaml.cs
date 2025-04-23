using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using VideocartLab.ModelVIews;

namespace VideocartLab.Views.Avalonia;

public partial class NodeView : UserControl
{
    public static StyledProperty<double> XProperty = Canvas.LeftProperty.AddOwner<NodeView>();
    public static StyledProperty<double> YProperty = Canvas.TopProperty.AddOwner<NodeView>();

    public static StyledProperty<object?> InnerContentProperty = 
        StyledProperty<object?>.Register<NodeView, object?>(nameof(InnerContent));

    private NodeModelView? nodeModelView;

    public NodeView()
    {
        InitializeComponent();
    }

    public NodeView(NodeModelView nodeModelView) : this()
    {
        this.NodeModelView = nodeModelView;
        DataContext = this.nodeModelView;

        Binding bindingX = new();
        bindingX.Source = nodeModelView;
        bindingX.Path = nameof(nodeModelView.X);

        this.Bind(XProperty, bindingX);

        Binding bindingY = new();
        bindingY.Source = nodeModelView;
        bindingY.Path = nameof(nodeModelView.Y);

        this.Bind(YProperty, bindingY);

        Binding bindingContent = new();
        bindingContent.Source = nodeModelView;
        bindingContent.Path = nameof(nodeModelView.Content);

        this.Bind(InnerContentProperty, bindingContent);
    }

    private NodeModelView NodeModelView
    {
        get => nodeModelView;
        set
        {
            nodeModelView = value;

            panel2.Children.Add(new StringContentView());
        }
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

    public object? InnerContent
    {
        get => GetValue(InnerContentProperty);
        set
        {
            SetValue(InnerContentProperty, value);
            OnInnerContentChanged();
        }
    }

    private void OnInnerContentChanged()
    {

    }

    private void Panel_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        e.Handled = true;

        var p = e.GetPosition(canvas);

        this.NodeModelView.Click(p.X, p.Y);
    }

    private void Panel_PointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        e.Handled = true;

        var p = e.GetPosition(canvas);

        this.NodeModelView.Realese(p.X, p.Y);
    }
}