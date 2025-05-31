namespace VideocartLab.ModelViews
{
    /// <summary>
    /// Узлы-кандидаты на добавление
    /// </summary>
    public class NodeListItemModelView : ModelViewBase
    {
        private string name = "";
        private Type? node = null;

        /// <summary>
        /// Имя узла
        /// </summary>
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Тип внутренного ViewModel
        /// </summary>
        internal Type? NodeType
        {
            get => node;
            set
            {
                node = value;
                OnPropertyChanged();
            }
        }
    }

}
