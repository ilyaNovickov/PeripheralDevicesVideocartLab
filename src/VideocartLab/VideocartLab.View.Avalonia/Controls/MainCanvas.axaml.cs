using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using System;
using VideocartLab.Presenter;

namespace VideocartLab.View.Avalonia.Controls;

public partial class MainCanvas : UserControl, IMainCanvasView
{
    public MainCanvas()
    {
        InitializeComponent();
    }

    private void Canvas_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (e.Handled)
            return;

        Point p = e.GetPosition(canvas);
        var properties = e.GetCurrentPoint(canvas).Properties;
        MouseButton button;

        if (properties.IsLeftButtonPressed)
        {
            button = MouseButton.Left;
        }
        else if (properties.IsRightButtonPressed)
        {
            button = MouseButton.Right;
        }
        else if (properties.IsMiddleButtonPressed)
        {
            button = MouseButton.Middle;
        }
        else
            button = MouseButton.Undef;

        OnMousePressed(new MousePressedArgs(p.X, p.Y, button));
        
    }

    public event EventHandler<MousePressedArgs>? MousePressed;

    public void OnMousePressed(MousePressedArgs e)
    {
        if (e.Button == MouseButton.Middle)
            return;

        MousePressed?.Invoke(this, e);
    }

    public void UpdateNode(Node node)
    {
        
    }
}