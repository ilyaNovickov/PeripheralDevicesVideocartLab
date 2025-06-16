using Avalonia.Controls;
using VideocartLab.ModelViews;

namespace VideocartLab.Views.AvaloniaExtraControlsSol;

public partial class GPUControllerView : UserControl
{
    public GPUControllerView()
    {
        InitializeComponent();

        lab.DoubleTapped += (s, e) => 
        {
            if (this.DataContext is null || this.DataContext is not GPUControllerModelView gpuControllerVM)
                return;

            gpuControllerVM.SetRightListCommand.Execute(null);
        };
    }

    private void ListBox_PointerWheelChanged(object? sender, Avalonia.Input.PointerWheelEventArgs e)
    {
        e.Handled = true;
    }
}