using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Videocart.ViewModel.InnerContent;

namespace Videocart.Views.AvaloniaProj;

public partial class StringContentView : UserControl
{
    private StringContentViewModel? strVM;

    public StringContentView()
    {
        InitializeComponent();
    }

    public StringContentViewModel? StringContentViewModel
    {
        get => strVM;
        set
        {
            strVM = value;
            this.DataContext = value;
        }
    }
}