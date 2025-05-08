using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;
using System;
using VideocartLab.ModelViews;
using Avalonia.Themes.Fluent;

namespace VideocartLab.Views.AvaloniaProj
{
    public partial class MainWindow : Window
    {
        private MainWindowModelView mainVM;

        public MainWindow()
        {
            InitializeComponent();

            mainVM = new MainWindowModelView();

            DataContext = mainVM;

            this.RequestedThemeVariant = ThemeVariant.Light;
        }
    }
}