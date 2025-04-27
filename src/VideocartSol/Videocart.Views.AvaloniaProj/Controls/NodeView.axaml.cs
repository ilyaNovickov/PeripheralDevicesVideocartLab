using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using Videocart.ViewModel;
using Videocart.ViewModel.InnerContent;

namespace Videocart.Views.AvaloniaProj;

public partial class NodeView : UserControl
{
    //Связывание свойств положения узла с холстом
    public static StyledProperty<double> XProperty = Canvas.LeftProperty.AddOwner<NodeView>();
    public static StyledProperty<double> YProperty = Canvas.TopProperty.AddOwner<NodeView>();

    //Свойство внутренного содержания узла
    public static StyledProperty<object?> InnerContentProperty =
        StyledProperty<object?>.Register<NodeView, object?>(nameof(InnerContent));

    private NodeViewModel? nodeViewModel;

    public NodeView()
    {
        InitializeComponent();
    }

    //Связывание свойств узла и VM
    private void BindProperties()
    {
        //Свойство X
        Binding bindingX = new();
        bindingX.Source = NodeViewModel;
        bindingX.Path = nameof(NodeViewModel.X);

        this.Bind(XProperty, bindingX);

        //Свойство Y
        Binding bindingY = new();
        bindingY.Source = NodeViewModel;
        bindingY.Path = nameof(NodeViewModel.Y);

        this.Bind(YProperty, bindingY);

        //Свойство Width
        Binding bindingWidth = new();
        bindingWidth.Source = NodeViewModel;
        bindingWidth.Path = nameof(NodeViewModel.Width);

        this.Bind(WidthProperty, bindingWidth);

        //Свойство Height
        Binding bindingHeight = new();
        bindingHeight.Source = NodeViewModel;
        bindingHeight.Path = nameof(NodeViewModel.Height);

        this.Bind(HeightProperty, bindingHeight);

        //Свойство Content
        Binding bindingContent = new();
        bindingContent.Source = NodeViewModel;
        bindingContent.Path = nameof(NodeViewModel.InnerContent);

        //bindingContent.Converter = new Foo();

        this.Bind(InnerContentProperty, bindingContent);
    }

    public NodeViewModel? NodeViewModel
    {
        get => nodeViewModel;
        set 
        {
            nodeViewModel = value;
            BindProperties();
            OnInnerContentChanged();
            DataContext = value;
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
        if (InnerContent is StringContentViewModel strVM)
        {
            StringContentView strV = new StringContentView();
            strV.StringContentViewModel = strVM;
            innerPanel.Children.Add(strV);
        }
    }
}