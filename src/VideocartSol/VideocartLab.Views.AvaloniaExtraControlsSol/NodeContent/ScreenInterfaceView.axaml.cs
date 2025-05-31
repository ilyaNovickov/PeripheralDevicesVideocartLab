using Avalonia.Controls;

namespace VideocartLab.Views.AvaloniaExtraControlsSol;

public partial class ScreenInterfaceView : UserControl
{
    public ScreenInterfaceView()
    {
        InitializeComponent();

        //DataContext = new ScreenInterfaceViewModel();
    }

    private void ComboBox_PointerPressed_2(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        e.Handled = true;
    }
}