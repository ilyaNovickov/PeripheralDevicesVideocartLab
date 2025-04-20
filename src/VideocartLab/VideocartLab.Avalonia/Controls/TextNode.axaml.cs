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

    //public static readonly  StyledProperty<string?> TextProperty =
    //    TextBlock.TextProperty.AddOwner<TextNode>();

    //InnerContent

    public static readonly AvaloniaProperty<object?> InnerProperty =
        AvaloniaProperty<object?>.Register<TextNode, object?>(nameof(InnerContent));

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

    //IMainCanvasView mainCanvasView = null;

    //IMainCanvasView INodeView.Parent 
    //{
    //    get => mainCanvasView;
    //    set => mainCanvasView = value;
    //}

    private void StackPanel_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        e.Handled = true;

        Clicked?.Invoke(this, new NodeClickedArgs(this, e.GetPosition(this).X, e.GetPosition(this).Y));
    }

    //private string? str = null;

    public object? InnerContent 
    {
        get => GetValue(InnerProperty);//textBlock.Text; //GetValue(TextProperty);// str;
        set
        {
            SetValue(InnerProperty, value);
            SetContent(value);
        }
    }

    public void SetContent(object? value)
    {
        if (value is not string)
            return;
        //str = (string)value;
        //SetValue(TextProperty, value);
        textBlock.Text = (string)value;
    }

    public event EventHandler<NodeClickedArgs>? Clicked;

}