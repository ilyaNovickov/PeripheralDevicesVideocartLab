using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using VideocartLab.ModelViews;

namespace VideocartLab.Views.AvaloniaExtraControlsSol;

public partial class ScreenInterfaceView : UserControl
{
    public ScreenInterfaceView()
    {
        InitializeComponent();

        DataContext = new ScreenInterfaceViewModel();
    }
}