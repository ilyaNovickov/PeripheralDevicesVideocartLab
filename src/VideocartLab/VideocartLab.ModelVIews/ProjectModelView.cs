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
            NodeModelView nodeModelView = Factory.Create(x, y, 200, 100, new TestClass()
            {
                Str = "Hello world", Count = 5
            });

            nodeModelView.Node.Moved += Node_Moved;

            nodeModelView.Clicked += NodeModelView_Clicked;
            nodeModelView.Realesed += NodeModelView_Realesed;

            foreach (ConnectionModelView connectionVM in nodeModelView.ConnectionModelViews)
            {
                connectionVM.Clicked += ConnectionVM_Clicked;
            }

            project.Nodes.Add(nodeModelView.Node);
            nodes.Add(nodeModelView);

            NodeModelViewAdded?.Invoke(this, new NodeModelViewAddedArgs(nodeModelView));
        }

        private void Node_Moved(object? sender, NodeMovedArgs e)
        {
            //throw new NotImplementedException();
        }

        ConnectionModelView? selectedConnector = null;

        private void ConnectionVM_Clicked(object? sender, ConnectorViewModelClickedArgs e)
        {
            ConnectionClickedResult res;

            if (Mode == WorkingMode.AddConnection)
            {
                if (selectedConnector == e.ConnectionModelView)
                {
                    res = ConnectionClickedResult.ConnectionReleased;
                    goto release;
                }

                res = ConnectionClickedResult.ConnectionAdded;
                selectedConnector!.Model.TargetConnections.Add(e.ConnectionModelView.Model);
release:
                Mode = WorkingMode.AddNode;
            }
            else
            {
                res = ConnectionClickedResult.ConnectionStart;
                this.Mode = WorkingMode.AddConnection;
                this.selectedConnector = e.ConnectionModelView;
                this.SelectedNode = (from node in nodes 
                                     where node.Node == e.ConnectionModelView.Model.Parent 
                                     select node).First();
            }

            ConnectionClicked?.Invoke(this, new ConnectionClickedArgs(e.ConnectionModelView, res));
        }

        public event EventHandler<ConnectionClickedArgs>? ConnectionClicked;

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

                //NodeMoved?.Invoke(this, new NodeMovedArgs(dx, dy, SelectedNode));
            }
        }

        //public event EventHandler<NodeMovedArgs>? NodeMoved;
    }

    //public class NodeMovedArgs : EventArgs
    //{
    //    public double dX { get; private set; }
    //    public double dY { get; private set; }
    //    public NodeModelView NodeModelView { get; private set; }

    //    public NodeMovedArgs(double dX, double dY, NodeModelView nodeModelView)
    //    {
    //        this.dX = dX;
    //        this.dY = dY;
    //        NodeModelView = nodeModelView;
    //    }
    //}

    public enum ConnectionClickedResult
    {
        ConnectionStart,
        ConnectionAdded, ConnectionReleased
    }

    public class ConnectionClickedArgs : ConnectorViewModelClickedArgs
    {
        public ConnectionClickedResult Result { get; private set; }

        public ConnectionClickedArgs(ConnectionModelView connectionModelView, ConnectionClickedResult res) : base(connectionModelView)
        {
            Result = res;
        }
    }
}
