using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Input;
using System;
using Videocart.Views;
using Videocart.Views.EventsArgs;
using VideocartLab.Avalonia.Factory;
using System.Threading;

namespace VideocartLab.Avalonia;

public partial class MainCanvas : UserControl, IMainCanvasView
{
    public MainCanvas()
    {
        InitializeComponent();
    }

    public INodeFactory NodeFactory { get; } = new NodeFactory();

    public event EventHandler<MousePressedArgs> MousePressed;

    public event EventHandler<NodeSelectedArgs> NodeSelected;

    public void SetSelectedNode(INodeView nodeView, double x, double y)
    {
        NodeSelected?.Invoke(this, new NodeSelectedArgs(nodeView, x, y));
    }

    private void Canvas_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (e.Handled)
            return;

        var properties = e.GetCurrentPoint(canvas).Properties;

        Videocart.Views.EventsArgs.MouseButton button;

        if (properties.IsLeftButtonPressed)
            button = Videocart.Views.EventsArgs.MouseButton.Left;
        else if (properties.IsRightButtonPressed)
            button = Videocart.Views.EventsArgs.MouseButton.Right;
        else if (properties.IsMiddleButtonPressed)
            button = Videocart.Views.EventsArgs.MouseButton.Middle;
        else
            button = Videocart.Views.EventsArgs.MouseButton.Undef;

        if (button == Videocart.Views.EventsArgs.MouseButton.Middle)
            return;

        Point point = e.GetPosition(canvas);

        MousePressed?.Invoke(this, new MousePressedArgs(point.X, point.Y, button));
    }

    public void AddNode(INodeView node)
    {
        if (node is Control control)
            canvas.Children.Add(control);
    }

    public event EventHandler<MouseMovedArgs> MouseMoved;

    private void Canvas_PointerMoved(object? sender, PointerEventArgs e)
    {
        Point p = e.GetPosition(canvas);

        MouseMoved?.Invoke(this, new MouseMovedArgs(p.X, p.Y));
    }
}