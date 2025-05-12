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

            projectView.ProjectVM = mainVM.Project;
            nodeListView.NodeListVM = mainVM.NodeList;

        }

        private void ComboBox_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
        {
            switch ((string)((ComboBoxItem)e.AddedItems[0]!).Content!)
            {
                case "Light":
                    this.RequestedThemeVariant = ThemeVariant.Light;
                    break;
                case "Dark":
                    this.RequestedThemeVariant = ThemeVariant.Dark;
                    break;
            }
        }
    }
}