using Videocart.Models;
using Videocart.Views;

namespace Videocart.Presenters
{
    public class MainCanvasPresenter
    {
        private List<Node> nodes = new List<Node>();

        private IMainCanvasView mainCanvasView;

        public MainCanvasPresenter(IMainCanvasView mainCanvasView)
        {
            this.mainCanvasView = mainCanvasView;
        }


    }
}
