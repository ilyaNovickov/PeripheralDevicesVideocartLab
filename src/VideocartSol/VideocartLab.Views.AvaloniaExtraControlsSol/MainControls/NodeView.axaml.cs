using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
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

        var vm = new NodeModelView()
        {
            Name = "Test",
            InnerContent = new VRAMModelView(),
        };

        vm.Connections.Add(new ConnectionModelView()
        {
            Id = "1",
            Type = ConnectionType.Duplex
        });
        vm.Connections.Add(new ConnectionModelView()
        {
            Id = "2",
            Type = ConnectionType.Getting
        });
        DataContext = vm;
    }

    public NodeModelView? NodeVM
    {
        get => GetValue(NodeVMProperty);
        set
        {
            SetValue(NodeVMProperty, value);
            BindProperties();
        }
    }

    private void BindProperties()
    {
        if (NodeVM == null)
            return;
        
        Binding xbing = new Binding();
        xbing.Source = NodeVM;
        xbing.Path = nameof(NodeVM.X);
        this.Bind(XProperty, xbing);

        Binding ybing = new Binding();
        ybing.Source = NodeVM;
        ybing.Path = nameof(NodeVM.Y);
        this.Bind(YProperty, ybing);

        Binding widthBing = new Binding();
        widthBing.Source = NodeVM;
        widthBing.Path = nameof(NodeVM.Width);
        //this.Bind(WidthProperty, widthBing);
        mainContentPanel.Bind(StackPanel.WidthProperty, widthBing);

        Binding heightBing = new Binding();
        heightBing.Source = NodeVM;
        heightBing.Path = nameof(NodeVM.Height);
        //this.Bind(HeightProperty, heightBing);
        mainContentPanel.Bind(StackPanel.HeightProperty, heightBing);

        Binding contentBing = new Binding();
        contentBing.Source = NodeVM;
        contentBing.Path = nameof(NodeVM.InnerContent);
        contentControl.Bind(ContentControl.ContentProperty, contentBing);

        Binding textBing = new Binding();
        textBing.Source = NodeVM;
        textBing.Path = nameof(NodeVM.Name);
        textBlock.Bind(TextBlock.TextProperty, textBing);

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

    private void UserControl_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        if (e.Handled) return;

        var properties = e.GetCurrentPoint(this).Properties;

        if (!properties.IsLeftButtonPressed)
            return;

        NodeVM.Clicked();
        e.Handled = true;
    }

    private void ComboBox_PointerPressed_1(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        e.Handled = true;
    }
}