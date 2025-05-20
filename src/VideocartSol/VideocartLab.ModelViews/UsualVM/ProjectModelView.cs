using System.Collections.ObjectModel;

namespace VideocartLab.ModelViews
{
    /// <summary>
    /// Проект
    /// </summary>
    public partial class ProjectModelView : ModelViewBase
    {
        private struct Point
        {
            public double X { get; set; }
            public double Y { get; set; }
        }

        private Point prevPoint = new Point();

        private NodeModelView? selectedNode = null;

        private Type? candidatToAdd = null;
        private NodeFactoryService factoryService;
        private IModeBase mode;

        private IdleMode idle;
        private AddingNodeMode addNode;
        private MovingNodeMode move;
        private RemoveNodeMode remove;


        private ObservableCollection<NodeModelView> nodes = new();

#if DEBUG
        public ProjectModelView()
        {

        }
#endif

        public ProjectModelView(NodeFactoryService factoryService)
        {
            this.factoryService = factoryService;

            idle = new IdleMode(this);
            addNode = new AddingNodeMode(this);
            move = new MovingNodeMode(this);
            remove = new RemoveNodeMode(this);

            mode = idle;

            nodes.CollectionChanged += Nodes_CollectionChanged;
        }

        ~ProjectModelView()
        {
            nodes.CollectionChanged -= Nodes_CollectionChanged;
        }

        /// <summary>
        /// Обработка события изменения списка узлов
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void Nodes_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    OnNodeAdded(new NodeAddedArgs((NodeModelView)e.NewItems![0]!));
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    OnNodeRemoved(new NodeRemovedArgs((NodeModelView)e.OldItems![0]!));
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Replace:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Move:
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    NodeListCleared?.Invoke(this, new EventArgs());
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Обработка события добавления узла
        /// </summary>
        /// <param name="e"></param>
        private void OnNodeAdded(NodeAddedArgs e)
        {
            e.AddedNode.NodePressed += OnNodePressed;

            NodeAdded?.Invoke(this, e);
        }

        /// <summary>
        /// Обработка события удаления узла
        /// </summary>
        /// <param name="e"></param>
        private void OnNodeRemoved(NodeRemovedArgs e)
        {
            e.RemovedNode.NodePressed -= OnNodePressed;

            NodeRemoved?.Invoke(this, e);
        }

        /// <summary>
        /// Событие очистки списка узлов
        /// </summary>
        public event EventHandler? NodeListCleared;

        /// <summary>
        /// Событие удаления узла
        /// </summary>
        public event EventHandler<NodeRemovedArgs>? NodeRemoved;

        /// <summary>
        /// Событие добавления узла
        /// </summary>
        public event EventHandler<NodeAddedArgs>? NodeAdded;

        /// <summary>
        /// Обработка события нажатия на узел
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnNodePressed(object? sender, NodePressedArgs args)
        {
            if (Mode == remove)
            {
                RemoveNode(args.Node);
                return;
            }

            if (Mode == addNode)
                return;

            if (Mode == move)
            {
                if (args.Node == this.SelectedNode)
                {
                    move.StartMove();
                    return;
                }
            }

            if (Mode != idle && args.Node == this.SelectedNode)
                return;

            this.SelectedNode = args.Node;

            if (args.Node != null)
            {
                Mode = move;
                move.StartMove();
            }
            else
                Mode = idle;
        }



        private NodeFactoryService NodeFactoryService { get { return factoryService; } }

        /// <summary>
        /// Кандидат на добавление
        /// </summary>
        public Type? CandidateToAdd
        {
            get => candidatToAdd;
            set
            {
                candidatToAdd = value;


                if (candidatToAdd != null)
                    Mode = addNode;
                else
                    Mode = idle;

            }
        }

        /// <summary>
        /// Выбранный узел
        /// </summary>
        public NodeModelView? SelectedNode
        {
            get => selectedNode;
            set
            {
                selectedNode = value;
            }
        }

        /// <summary>
        /// Режим проекта
        /// </summary>
        private IModeBase Mode
        {
            get => mode;
            set
            {
                mode = value;
            }
        }

        /// <summary>
        /// Список узлов проекта
        /// </summary>
        internal ObservableCollection<NodeModelView> Nodes => nodes;

        /// <summary>
        /// Нажатие на проект мышью
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        public void OnPointerPressed(double x, double y)
        {
            prevPoint.X = x;
            prevPoint.Y = y;

            Mode.OnPointerPressed();
        }

        /// <summary>
        /// Перемещение мыши
        /// </summary>
        /// <param name="newX">Новая координата X</param>
        /// <param name="newY">Новая координата Y</param>
        public void OnPointerMoved(double newX, double newY)
        {
            double dx = newX - prevPoint.X;
            double dy = newY - prevPoint.Y;

            Mode.OnPointerMoved(dx, dy);

            prevPoint.X = newX;
            prevPoint.Y = newY;
        }

        /// <summary>
        /// Отпускание мыши
        /// </summary>
        public void OnPointerReleased()
        {
            Mode.OnPointerReleased();
        }

        /// <summary>
        /// Добавление узла
        /// </summary>
        /// <returns></returns>
        public NodeModelView? AddNode()
        {
            if (CandidateToAdd == null)
                return null;

            var node = NodeFactoryService.CreateNode(CandidateToAdd, prevPoint.X, prevPoint.Y);

            return node;
        }

        /// <summary>
        /// Удаление выбранного узла
        /// </summary>
        public void RemoveSelectedNode()
        {
            if (SelectedNode == null)
                return;

            var node = SelectedNode;
            SelectedNode = null;

            nodes.Remove(node);
        }

        /// <summary>
        /// Удаление узла
        /// </summary>
        /// <param name="node">Узел на удаление</param>
        public void RemoveNode(NodeModelView node)
        {
            if (SelectedNode == node)
            {
                SelectedNode = null;
            }

            nodes.Remove(node);
        }

        /// <summary>
        /// Переключение в режим удаление
        /// </summary>
        /// <param name="turnOn"></param>
        public void ToggleRemoveMode(bool turnOn)
        {
            Mode = turnOn ? remove : idle;
        }

        /// <summary>
        /// Установка в режи простоя
        /// </summary>
        public void SetIdleMode()
        {
            this.Mode = idle;
        }
    }
}
