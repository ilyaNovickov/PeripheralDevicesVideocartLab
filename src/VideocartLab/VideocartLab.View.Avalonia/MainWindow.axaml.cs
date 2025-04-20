using Avalonia.Controls;
using VideocartLab.Presenter;

namespace VideocartLab.View.Avalonia
{
    public partial class MainWindow : Window
    {
        MainCanvasPresenter mainCanvasPresenter;
        NodeListPresenter nodeListPresenter;

        public MainWindow()
        {
            InitializeComponent();

            nodeListPresenter = new NodeListPresenter(this.nodeList);
        }
    }
}