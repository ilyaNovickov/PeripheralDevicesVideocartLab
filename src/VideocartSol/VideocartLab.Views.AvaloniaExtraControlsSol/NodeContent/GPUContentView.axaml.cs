using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using VideocartLab.ModelViews;

namespace VideocartLab.Views.AvaloniaExtraControlsSol;

public partial class GPUContentView : UserControl
{
    public GPUContentView()
    {
        InitializeComponent();

        //DataContext = new GPUContentModelView();
    }
}