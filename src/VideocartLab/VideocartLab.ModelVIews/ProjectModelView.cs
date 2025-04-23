using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideocartLab.Models;

namespace VideocartLab.ModelVIews
{
    public class ProjectModelView : ModelViewBase
    {
        private Project project = new();

        private Point prevPoint = new Point();

        private ObservableCollection<NodeModelView> nodes = new();

        private NodeModelView? selectedNode = null;

        public ProjectModelView()
        {

        }

        public NodeFactory Factory { get; set; }

        public WorkingMode Mode
        {
            get; private set;
        } = WorkingMode.AddNode;

        //Добавление нового узла по координатам со стандартным содержанием
        public void AddNode(double x, double y)
        {
            //NodeModelView nodeModelView = Factory.Create(x, y, 100, 100, "Test");
            NodeModelView nodeModelView = Factory.Create(x, y, 100, 100, new TestClass()
            {
                Str = "Hello world", Count = 5
            });
            

            nodeModelView.Clicked += NodeModelView_Clicked;
            nodeModelView.Realesed += NodeModelView_Realesed;

            project.Nodes.Add(nodeModelView.Node);
            nodes.Add(nodeModelView);

            NodeModelViewAdded?.Invoke(this, new NodeModelViewAddedArgs(nodeModelView));
        }

        //Обрабатывает отпускание мыши на узле
        private void NodeModelView_Realesed(object? sender, NodeModelViewRealeseArgs e)
        {
            Mode = WorkingMode.AddNode;
        }

        //Обрабатывает нажатие по узлу
        private void NodeModelView_Clicked(object? sender, NodeModelViewClickedArgs e)
        {
            SelectedNode = e.Node;//Нажатыый узел - выбранный
            Mode = WorkingMode.MoveNode;
            prevPoint.X = e.X + e.Node.X;//Точка отсчёта перемещения
            prevPoint.Y = e.Y + e.Node.Y;
        }

        //Выбарнный узел
        public NodeModelView? SelectedNode
        {
            get => selectedNode;
            set => selectedNode = value;
        }

        //Событие добавления узла
        public event EventHandler<NodeModelViewAddedArgs>? NodeModelViewAdded;

        //При нажатии мыши по холсту
        public void OnMousePressed(double x, double y, MouseButton button)
        {
            if (Mode == WorkingMode.AddNode)
                AddNode(x, y);
        }

        //При перемещении курсора мыши
        public void OnMouseMoved(double x, double y)
        {
            if (SelectedNode == null)
                return;

            //если перемещеение узла
            if (Mode == WorkingMode.MoveNode)
            {
                double dx = x - prevPoint.X;
                double dy = y - prevPoint.Y;

                SelectedNode.X += dx;
                SelectedNode.Y += dy;

                prevPoint.X = x;
                prevPoint.Y = y;
            }
        }
    }
}
