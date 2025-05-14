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
}