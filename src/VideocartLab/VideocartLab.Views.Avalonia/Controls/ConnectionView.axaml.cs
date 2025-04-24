using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using VideocartLab.ModelVIews;

namespace VideocartLab.Views.Avalonia;

public partial class ConnectionView : UserControl
{
    public static readonly StyledProperty<double> XProperty = Canvas.LeftProperty.AddOwner<ConnectionView>();
    public static readonly StyledProperty<double> YProperty = Canvas.TopProperty.AddOwner<ConnectionView>();

    private ConnectionModelView connectionModelView;

    public ConnectionView()
    {
        InitializeComponent();
    }

    public ConnectionView(ConnectionModelView connectionModelView) : this()
    {
        ConnectionModelView = connectionModelView;

        this.DataContext = ConnectionModelView;

        BindingProperties();
    }

    public ConnectionModelView ConnectionModelView
    {
        get => connectionModelView;
        set
        {
            connectionModelView = value;
        }
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

    public void BindingProperties()
    {
        //Свойство X
        Binding bindingX = new();
        bindingX.Source = ConnectionModelView;
        bindingX.Path = nameof(ConnectionModelView.X);

        this.Bind(XProperty, bindingX);

        //Свойство Y
        Binding bindingY = new();
        bindingY.Source = ConnectionModelView;
        bindingY.Path = nameof(ConnectionModelView.Y);

        this.Bind(YProperty, bindingY);
    }
}