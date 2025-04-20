using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Videocart.Views;
using Avalonia.Input;
using System;
using Videocart.Views.EventsArgs;

namespace VideocartLab.Avalonia;

public partial class TextNode : UserControl, INodeView
{
    public static readonly StyledProperty<double> XProperty =
        Canvas.LeftProperty.AddOwner<TextNode>();

    public static readonly StyledProperty<double> YProperty =
        Canvas.TopProperty.AddOwner<TextNode>();

    public static readonly  StyledProperty<string?> TextProperty =
        TextBlock.TextProperty.AddOwner<TextNode>();

    public TextNode()
    {
        InitializeComponent();
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

    IMainCanvasView mainCanvasView = null;

    IMainCanvasView INodeView.Parent 
    {
        get => mainCanvasView;
        set => mainCanvasView = value;
    }

    private void StackPanel_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        e.Handled = true;

        if (mainCanvasView is not MainCanvas canvas)
            return;

        Clicked?.Invoke(this, new MousePressedArgs(e.GetPosition(canvas.canvas).X, e.GetPosition(canvas.canvas).Y,
            Videocart.Views.EventsArgs.MouseButton.Undef));
    }

    private string? str = null;

    object? INodeView.Content 
    {
        get => GetValue(TextProperty);// str;
        set
        {
            SetContent(value);
        }
    }

    public void SetContent(object? value)
    {
        if (value is not string)
            return;
        //str = (string)value;
        SetValue(TextProperty, value);
        textBlock.Text = (string)value;
    }

    public event EventHandler<MousePressedArgs> Clicked;

    public (double, double) GetPointFromParent()
    {
        //if (mainCanvasView is MainCanvas canvas)
        //    return equals.
        return (0d, 0d);
    }
}