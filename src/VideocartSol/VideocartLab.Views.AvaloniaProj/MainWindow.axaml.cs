using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform.Storage;
using Avalonia.Styling;
using System;
using System.IO;
using VideocartLab.ModelViews;

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

            //nodeListView.SelectionChanged 
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

        private void Copy_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            this.textBox.Copy();
        }

        private void Clear_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            this.textBox.Clear();
        }

        //Сохранение отчёта в файл
        private async void SaveReport_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var file = await StorageProvider.SaveFilePickerAsync(new Avalonia.Platform.Storage.FilePickerSaveOptions()
            {
                FileTypeChoices = [FilePickerFileTypes.TextPlain],
                DefaultExtension = ".txt",
                Title = "Сохранение отчёта"
            });

            if (file is null)
                return;

            string p = file.Path.LocalPath;

            using (StreamWriter stream = new StreamWriter(p, new FileStreamOptions()
            {
                Access = FileAccess.Write,
                Mode = FileMode.Create,
            }))
            {
                stream.Write(textBox.Text);
            }


            file?.Dispose();
        }

        //Создание файла изображения проекта
        private async void CreateImage_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var file = await StorageProvider.SaveFilePickerAsync(new Avalonia.Platform.Storage.FilePickerSaveOptions()
            {
                FileTypeChoices = [FilePickerFileTypes.ImagePng],
                DefaultExtension = ".png",
                Title = "Сохранение изображения"
            });

            if (file is null)
                return;

            string p = file.Path.LocalPath;

            ExportToImage(projectView, p);

            file?.Dispose();
        }

        /// <summary>
        /// Экпорт элемента управления в изображение
        /// </summary>
        /// <param name="canvas">Элемент управления</param>
        /// <param name="filePath">Путь к файлу</param>
        private static void ExportToImage(Control canvas, string filePath)
        {
            var originalParent = canvas.Parent;

            var originalTransform = canvas.RenderTransform;

            try
            {
                canvas.RenderTransform = null;

                canvas.Measure(Size.Infinity);
                canvas.Arrange(new Rect(canvas.DesiredSize));

                var pixelWidth = (int)Math.Ceiling(canvas.Bounds.Width);
                var pixelHeight = (int)Math.Ceiling(canvas.Bounds.Height);

                using var rtb = new RenderTargetBitmap(new PixelSize(pixelWidth, pixelHeight));
                rtb.Render(canvas);

                using var fileStream = new FileStream(filePath, FileMode.Create);

                rtb.Save(fileStream);
            }
            finally
            {
                canvas.RenderTransform = originalTransform;

                if (originalParent is Panel panel && !panel.Children.Contains(canvas))
                {
                    panel.Children.Add(canvas);
                }
            }
        }

        [Obsolete]
        private async void SaveProject_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var file = await StorageProvider.SaveFilePickerAsync(new Avalonia.Platform.Storage.FilePickerSaveOptions()
            {
                FileTypeChoices = [new FilePickerFileType("JSON") { Patterns = ["*.json"] }],
                DefaultExtension = ".json",
                Title = "Сохранение изображения"
            });

            if (file is null)
                return;

            string path = file.Path.LocalPath;

            mainVM.SaveProject(path);

            file?.Dispose();
        }

        [Obsolete]
        private async void LoadProject_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var file = await StorageProvider.OpenFilePickerAsync(new Avalonia.Platform.Storage.FilePickerOpenOptions()
            {
                FileTypeFilter = [new FilePickerFileType("JSON") { Patterns = ["*.json"] }],
                AllowMultiple = false,
                Title = "Сохранение изображения"
            });

            if (file is null || file.Count != 1)
                return;

            string path = file[0].Path.LocalPath;

            mainVM.LoadProject(path);

            foreach (var item in file)
            {
                item.Dispose();
            }
            //file?.D();
        }

        private void ToggleButton_Click_1(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            if (removeToggleButton.IsChecked!.Value)
                nodeListView.SelectionChanged += NodeListView_SelectionChanged;
            else
                nodeListView.SelectionChanged -= NodeListView_SelectionChanged;
        }

        private void NodeListView_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            nodeListView.SelectionChanged -= NodeListView_SelectionChanged;

            removeToggleButton.IsChecked = false;
        }
    }
}