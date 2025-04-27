using Avalonia.Controls;
using System.ComponentModel;
using Videocart.ViewModel;

namespace Videocart.Views.AvaloniaProj
{
    public partial class MainWindow : Window
    {
        private MainWindowsViewModel MainWindowsViewModel { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            MainWindowsViewModel = new MainWindowsViewModel();
            this.DataContext = MainWindowsViewModel;

            this.projectView.ProjectViewModel = MainWindowsViewModel.Project;
        }
    }
}