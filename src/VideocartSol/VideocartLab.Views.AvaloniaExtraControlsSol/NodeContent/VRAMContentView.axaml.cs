using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using VideocartLab.ModelViews;

namespace VideocartLab.Views.AvaloniaExtraControlsSol;

public partial class VRAMContentView : UserControl
{
    public VRAMContentView()
    {
        InitializeComponent();

        //DataContext = new VRAMModelView();
    }

    private void ComboBox_PointerPressed(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        e.Handled = true;
    }
}