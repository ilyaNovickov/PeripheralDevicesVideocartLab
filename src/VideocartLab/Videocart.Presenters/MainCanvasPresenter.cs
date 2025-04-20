using Videocart.Models;
using Videocart.Views;

namespace Videocart.Presenters
{
    public enum WorkMode
    {
        None, Adding, Moving
    }

    public class MainCanvasPresenter
    {
        private List<Node> nodes = new List<Node>();

        private IMainCanvasView mainCanvasView;

        private Node? selectedNode = null;

        public MainCanvasPresenter(IMainCanvasView mainCanvasView)
        {
            this.mainCanvasView = mainCanvasView;

            mainCanvasView.MousePressed += MainCanvasView_MousePressed;
        }

        public WorkMode Mode { get; private set; } = WorkMode.None;

        private void MainCanvasView_MousePressed(object? sender, Views.EventsArgs.MousePressedArgs e)
        {
            switch (Mode)
            {
                default:
                case WorkMode.None:
                    return;
                case WorkMode.Adding:
                    //
                    break;
                case WorkMode.Moving:
                    //
                    break;
            }
        }

    }
}
