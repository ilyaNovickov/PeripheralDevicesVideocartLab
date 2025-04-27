using Avalonia;
using Av = Avalonia;
using Avalonia.Input;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using VideocartLab.ModelVIews;
using VideocartLab.Views.Avalonia.Helpers;
using Avalonia.Controls.Shapes;

namespace VideocartLab.Views.Avalonia;

public enum LinePoint
{
    Start, End
}

public partial class ConnectionView : UserControl
{
    public static readonly StyledProperty<double> XProperty = Canvas.LeftProperty.AddOwner<ConnectionView>();
    public static readonly StyledProperty<double> YProperty = Canvas.TopProperty.AddOwner<ConnectionView>();

    private ConnectionModelView connectionModelView;

    public ConnectionView()
    {
        InitializeComponent();
    }

    public Line? Line { get; set; }

    public LinePoint LinePoint { get; private set; }

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
        set
        {
            SetValue(XProperty, value);
            OnMoved();
        }
    }

    public double Y
    {
        get => GetValue(YProperty);
        set
        {
            SetValue(YProperty, value);
            OnMoved();
        }
    }

    public void BindingProperties()
    {
        //—войство X
        Binding bindingX = new();
        bindingX.Source = ConnectionModelView;
        bindingX.Path = nameof(ConnectionModelView.X);

        this.Bind(XProperty, bindingX);

        //—войство Y
        Binding bindingY = new();
        bindingY.Source = ConnectionModelView;
        bindingY.Path = nameof(ConnectionModelView.Y);

        this.Bind(YProperty, bindingY);
    }

    private void Ellipse_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (e.Handled)
            return;

        e.Handled = true;

        VideocartLab.ModelVIews.MouseButton button = MouseButtonHelper.GetMouseButton(e.GetCurrentPoint(this));

        //—редн€€ кнопка мыши зарезервирована под перемещение
        if (button == VideocartLab.ModelVIews.MouseButton.Middle)
            return;

        var p = e.GetPosition(this);

        this.ConnectionModelView.OnMousePressed(p.X, p.Y, button);
    }

    private void OnMoved()
    {
        if (Line == null)
            return;

        switch (LinePoint)
        {
            case LinePoint.Start:
                Line.StartPoint = new Av.Point(this.X, this.Y);
                break;
            case LinePoint.End:
                Line.EndPoint = new Av.Point(this.X, this.Y);
                break;
            default:
                break;
        }
    }
}