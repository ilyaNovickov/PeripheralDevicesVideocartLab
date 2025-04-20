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
        //private List<Node> nodes = new List<Node>();

        private IMainCanvasView mainCanvasView;

        private INodeView? selectedNode = null;

        //Точка для перемещения
        private Point prevPoint = new Point();

        public MainCanvasPresenter(IMainCanvasView mainCanvasView)
        {
            this.mainCanvasView = mainCanvasView;

            mainCanvasView.MousePressed += MainCanvasView_MousePressed;
            mainCanvasView.MouseMoved += MainCanvasView_MouseMoved;
            mainCanvasView.MouseRelease += MainCanvasView_MouseRelease;
        }

        //Режим работы
        public WorkMode Mode { get; private set; } = WorkMode.Adding;//= WorkMode.None;

        //Выбранный узел
        public INodeView? SelectedNode
        {
            get => selectedNode;
            private set => selectedNode = value;
        }


        //Обработка нажатия мыши
        private void MainCanvasView_MousePressed(object? sender, Views.EventsArgs.MousePressedArgs e)
        {
            switch (Mode)
            {
                default:
                case WorkMode.None:
                    return;
                case WorkMode.Adding:
                    //Добавление узла
                    var node = mainCanvasView.NodeFactory.CreateNode("string", e.X, e.Y);
                    //node.Parent = mainCanvasView;
                    node.Clicked += Node_Clicked;//Указывает что делать при нажатии на узел
                    mainCanvasView.AddNode(node);//Добавление узла на view
                    break;
                case WorkMode.Moving:
                    //
                    break;
            }


        }

        //Обработка перемещения курсора
        private void MainCanvasView_MouseMoved(object? sender, Views.EventsArgs.MouseMovedArgs e)
        {
            if (SelectedNode is null || Mode != WorkMode.Moving)
                return;

            //Перемещение узла
            SelectedNode.X += (e.NewX - prevPoint.X);
            SelectedNode.Y += (e.NewY - prevPoint.Y);

            prevPoint.X = e.NewX;
            prevPoint.Y = e.NewY;
        }

        //Обработка отпускания мыши
        private void MainCanvasView_MouseRelease(object? sender, EventArgs e)
        {
            /*
             * TODO:
             * Изменить на None
             * когда будет готов выбор узла
             */
            Mode = WorkMode.Adding;
        }

        //Выбирает узел и начинает перемещать его
        private void Node_Clicked(object? sender, Views.EventsArgs.NodeClickedArgs e)
        {
            prevPoint.X = (e.X + e.SenderNode.X);
            prevPoint.Y = (e.Y + e.SenderNode.Y);

            SelectedNode = e.SenderNode;
            Mode = WorkMode.Moving;
        }
    }
}
/*
 * Остальное TODO:
 * 
 * Избавиться от NodePresenter
 * Подумать о размерах в Node
 * Подумать о других Node
 * Подумать о удалении Node \
 * о контектном меню \
 * о соединениях
 * 
 */