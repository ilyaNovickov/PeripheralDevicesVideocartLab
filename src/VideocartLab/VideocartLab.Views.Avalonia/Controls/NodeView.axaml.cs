using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using VideocartLab.ModelVIews;
using Avalonia.Data.Converters;
using System;
using System.Globalization;

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

        BindProperties();
    }

    private void BindProperties()
    {

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

        //bindingContent.Converter = new Foo();

        this.Bind(InnerContentProperty, bindingContent);
    }

    private NodeModelView NodeModelView
    {
        get => nodeModelView;
        set
        {
            nodeModelView = value;

            InnerContent = nodeModelView.Content;
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
        if (InnerContent is string str)
        {
            innerPanel.Children.Clear();
            innerPanel.Children.Add(new StringContentView()
            {
                DataContext = new StringContentModelView(this.NodeModelView)
                {
                    Content = str
                }
            });
        }
    }

    private void Panel_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        e.Handled = true;

        var p = e.GetPosition(canvas);

        this.NodeModelView.Click(p.X, p.Y);

        InnerContent = "Hwew";
    }

    private void Panel_PointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        e.Handled = true;

        var p = e.GetPosition(canvas);

        this.NodeModelView.Realese(p.X, p.Y);
    }
}

public class Foo : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}