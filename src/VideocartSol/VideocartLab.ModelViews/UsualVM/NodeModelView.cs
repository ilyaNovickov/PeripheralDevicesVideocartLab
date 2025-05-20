using System.Collections.ObjectModel;

namespace VideocartLab.ModelViews
{
    /// <summary>
    /// Узел
    /// </summary>
    public class NodeModelView : ModelViewBase
    {
        private ObservableCollection<ConnectionModelView> connections = new();

        /// <summary>
        /// Соединения узла
        /// </summary>
        public ObservableCollection<ConnectionModelView> Connections => connections;

        #region StandartProperties
        private double x;
        private double y;
        private double width;
        private double height;
        private ModelViewBase? innerContent = null;
        private string? name = null;

        /// <summary>
        /// Координата X
        /// </summary>
        public double X
        {
            get => x;
            set
            {
                x = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Координата Y
        /// </summary>
        public double Y
        {
            get => y;
            set
            {
                y = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Ширина узла
        /// </summary>
        public double Width
        {
            get => width;
            set
            {
                width = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Высота узла
        /// </summary>
        public double Height
        {
            get => height;
            set
            {
                height = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Внутренний ModelView узла
        /// </summary>
        public ModelViewBase? InnerContent
        {
            get => innerContent;
            set
            {
                innerContent = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Имя узла
        /// </summary>
        public string? Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        #endregion

        /// <summary>
        /// Событие нажатия на узел
        /// </summary>
        public event EventHandler<NodePressedArgs>? NodePressed;

        /// <summary>
        /// Вызов события нажатия на узел
        /// </summary>
        public void Clicked() => NodePressed?.Invoke(this, new NodePressedArgs(this));
    }
}
