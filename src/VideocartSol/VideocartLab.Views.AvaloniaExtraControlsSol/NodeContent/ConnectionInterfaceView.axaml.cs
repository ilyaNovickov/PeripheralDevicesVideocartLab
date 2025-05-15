using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using VideocartLab.ModelViews;

namespace VideocartLab.Views.AvaloniaExtraControlsSol;

public partial class ConnectionInterfaceView : UserControl
{
    public ConnectionInterfaceView()
    {
        InitializeComponent();

        //DataContext = new ConnectionInterfaceModelView();
    }

    private void ComboBox_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        e.Handled = true;
    }
}