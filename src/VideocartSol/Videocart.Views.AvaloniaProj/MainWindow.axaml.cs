using Avalonia.Controls;
using System.ComponentModel;
using Videocart.ViewModel;

namespace Videocart.Views.AvaloniaProj
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainWindowsViewModel();
        }
    }
}