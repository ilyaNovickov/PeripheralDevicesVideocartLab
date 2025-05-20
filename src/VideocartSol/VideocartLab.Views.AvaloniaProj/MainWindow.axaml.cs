using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;
using System;
using System.IO;
using VideocartLab.ModelViews;
using Avalonia.Themes.Fluent;
using Avalonia.Platform.Storage;
using Avalonia.Media.Imaging;

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

        private void Copy_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            this.textBox.Copy();
        }

        private void Clear_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            this.textBox.Clear();
        }

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

        private static void ExportToImage(Control canvas, string filePath, string format = "png")
        {
            if (canvas == null)
                throw new ArgumentNullException(nameof(canvas));

            // Сохраняем оригинального родителя
            var originalParent = canvas.Parent;

            // Сохраняем оригинальный Transform (масштаб)
            var originalTransform = canvas.RenderTransform;

            try
            {
                // Временно удалим трансформации (масштаб)
                canvas.RenderTransform = null;

                // Получаем желаемый размер (без учёта масштаба)
                canvas.Measure(Size.Infinity);
                canvas.Arrange(new Rect(canvas.DesiredSize));

                var pixelWidth = (int)Math.Ceiling(canvas.Bounds.Width);
                var pixelHeight = (int)Math.Ceiling(canvas.Bounds.Height);

                if (pixelWidth == 0 || pixelHeight == 0)
                    throw new InvalidOperationException("Canvas has zero size.");

                // Рендерим в изображение
                using var rtb = new RenderTargetBitmap(new PixelSize(pixelWidth, pixelHeight));
                rtb.Render(canvas);

                using var fileStream = new FileStream(filePath, FileMode.Create);

                if (format.ToLower() == "png")
                {
                    rtb.Save(fileStream); // PNG по умолчанию
                }
                else
                {
                    throw new ArgumentException("Unsupported format. Use 'png' or 'jpeg'.");
                }
            }
            finally
            {
                // Возвращаем трансформацию обратно
                canvas.RenderTransform = originalTransform;

                // Возвращаем Canvas обратно в визуальное дерево, если нужно
                if (originalParent is Panel panel && !panel.Children.Contains(canvas))
                {
                    panel.Children.Add(canvas);
                }
            }
        }

        private async void SaveProject_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var file = await StorageProvider.SaveFilePickerAsync(new Avalonia.Platform.Storage.FilePickerSaveOptions()
            {
                FileTypeChoices = [new FilePickerFileType("JSON") { Patterns=["*.json"]}],
                DefaultExtension = ".json",
                Title = "Сохранение изображения"
            });

            if (file is null)
                return;

            string path = file.Path.LocalPath;

            mainVM.SaveProject(path);

            file?.Dispose();
        }

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
    }
}