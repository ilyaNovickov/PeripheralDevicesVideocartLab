using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Input;
using System;
using Videocart.Views;
using Videocart.Views.EventsArgs;
using VideocartLab.Avalonia.Factory;
using System.Threading;
using Av = Avalonia;

namespace VideocartLab.Avalonia;

public partial class MainCanvas : UserControl, IMainCanvasView
{
    public MainCanvas()
    {
        InitializeComponent();
    }

    //������� ��� �������� �����
    public INodeFactory NodeFactory { get; } = new NodeFactory();

    //�������� ���� �� �����
    public void AddNode(INodeView node)
    {
        if (node is Control control)
            canvas.Children.Add(control);
    }

    public event EventHandler<MousePressedArgs>? MousePressed;

    public event EventHandler<MouseMovedArgs>? MouseMoved;

    public event EventHandler? MouseRelease;

    //����� ������� ������� ����
    private void Canvas_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (e.Handled)
            return;

        var properties = e.GetCurrentPoint(canvas).Properties;

        Videocart.Views.EventsArgs.MouseButton button;

        //����������� ������� ������
        if (properties.IsLeftButtonPressed)
            button = Videocart.Views.EventsArgs.MouseButton.Left;
        else if (properties.IsRightButtonPressed)
            button = Videocart.Views.EventsArgs.MouseButton.Right;
        else if (properties.IsMiddleButtonPressed)
            button = Videocart.Views.EventsArgs.MouseButton.Middle;
        else
            button = Videocart.Views.EventsArgs.MouseButton.Undef;

        //�� ������� ������ ����������� �� ������
        if (button == Videocart.Views.EventsArgs.MouseButton.Middle)
            return;

        Av.Point point = e.GetPosition(canvas);

        MousePressed?.Invoke(this, new MousePressedArgs(point.X, point.Y, button));
    }

    //����� ������� ����������� �� ������
    private void Canvas_PointerMoved(object? sender, PointerEventArgs e)
    {
        Av.Point p = e.GetPosition(canvas);

        MouseMoved?.Invoke(this, new MouseMovedArgs(p.X, p.Y));
    }

    //����� ������� ������� ������� ����
    private void Canvas_PointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        //e.Handled = true;

        MouseRelease?.Invoke(this, EventArgs.Empty);
    }
}