using Av = Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using VideocartLab.ModelVIews;
using VideocartLab.Views.Avalonia.Helpers;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace VideocartLab.Views.Avalonia;

public partial class ProjectView : UserControl
{
    private ProjectModelView projectModelView;

    public ProjectView()
    {
        InitializeComponent();

        projectModelView = new ProjectModelView();
        projectModelView.Factory = new NodeFactory();
        this.DataContext = projectModelView;

        projectModelView.NodeModelViewAdded += ProjectModelView_NodeModelViewAdded;
        projectModelView.ConnectionClicked += ProjectModelView_ConnectionClicked;
    }

    Line? connectionLine = null;

    private void ProjectModelView_ConnectionClicked(object? sender, ConnectionClickedArgs e)
    {
        if (projectModelView.Mode == WorkingMode.AddConnection && e.Result == ConnectionClickedResult.ConnectionStart)
        {
            Line line = new Line();
            line.ZIndex = 0;
            line.IsHitTestVisible = false;
            line.StrokeThickness = 2d;
            line.Stroke = Brushes.Blue;
            line.StartPoint = new Av.Point(e.ConnectionModelView.X, e.ConnectionModelView.Y);
            line.EndPoint = new Av.Point(e.ConnectionModelView.X, e.ConnectionModelView.Y);
            this.connectionLine = line;
            this.canvas.Children.Add(line);

            //—войство X
            Binding bindingX = new();
            bindingX.Source = e.ConnectionModelView;
            bindingX.Path = nameof(e.ConnectionModelView.Position);
            bindingX.Converter = new foo2();
            bindingX.Mode = BindingMode.OneWay;
            bindingX.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            line.Bind(Line.StartPointProperty, bindingX);
        }

        switch (e.Result)
        {
            case ConnectionClickedResult.ConnectionStart:
                break;
            case ConnectionClickedResult.ConnectionAdded:
                //—войство X
                Binding bindingX = new();
                bindingX.Source = e.ConnectionModelView;
                bindingX.Path = nameof(e.ConnectionModelView.Position);
                bindingX.Converter = new foo2();
                bindingX.Mode = BindingMode.OneWay;
                bindingX.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

                connectionLine!.Bind(Line.EndPointProperty, bindingX);

                this.connectionLine = null;
                break;
            case ConnectionClickedResult.ConnectionReleased:
                this.canvas.Children.Remove(connectionLine!);
                this.connectionLine = null;
                break;
            default:
                break;
        }
    }

    //ќбработка добавлени€ узла в проект
    private void ProjectModelView_NodeModelViewAdded(object? sender, NodeModelViewAddedArgs e)
    {
        NodeModelView nodeModelView = e.Node;

        NodeView nodeView = new(nodeModelView);

        //nodeView.ZIndex = 1;

        canvas.Children.Add(nodeView);

        AddConnectorsForNode(nodeModelView);
    }

    //обработка нажати€ по холсту
    private void Canvas_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (e.Handled)
            return;

        VideocartLab.ModelVIews.MouseButton button = MouseButtonHelper.GetMouseButton(e.GetCurrentPoint(canvas));

        //—редн€€ кнопка мыши зарезервирована под перемещение
        if (button == VideocartLab.ModelVIews.MouseButton.Middle)
            return;

        var p = e.GetPosition(canvas);

        projectModelView.OnMousePressed(p.X, p.Y, button);

        //AddNode(factory.Create(p.X, p.Y, 100, 100, "HH"));
    }

    //ќбработка перемещени€ курсора мыши
    private void Canvas_PointerMoved(object? sender, PointerEventArgs e)
    {
        var p = e.GetPosition(canvas);

        projectModelView.OnMouseMoved(p.X, p.Y);

        if (projectModelView.Mode == WorkingMode.AddConnection)
        {
            connectionLine!.EndPoint = new Av.Point(p.X, p.Y);
        }
    }

    private void AddConnectorsForNode(NodeModelView node)
    {
        if (node.ConnectionModelViews.Count == 0)
            return;

        foreach (ConnectionModelView connector in node.ConnectionModelViews)
        {
            this.canvas.Children.Add(new ConnectionView(connector));
        }
    }
}

public class foo2 : IValueConverter
{
    object? IValueConverter.Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Point point)
            return new Av.Point(point.X, point.Y);
        return null;
        //throw new NotImplementedException();
    }

    object? IValueConverter.ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}