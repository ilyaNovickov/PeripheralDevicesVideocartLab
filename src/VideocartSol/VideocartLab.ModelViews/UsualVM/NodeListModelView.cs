using System.Collections.ObjectModel;

namespace VideocartLab.ModelViews
{
    /// <summary>
    /// Список узлов-кандидатов на добавление 
    /// </summary>
    public class NodeListModelView : ModelViewBase
    {
        private ObservableCollection<NodeListItemModelView> items = new();
        private NodeListItemModelView? selectedItem = null;
        private NodeFactoryService factoryService;

        public NodeListModelView(NodeFactoryService factoryService)
        {
            this.factoryService = factoryService;

            items = new ObservableCollection<NodeListItemModelView>();

            //Заполнение списка кандидатов в соотведствии с содержимой фабрики
            foreach (KeyValuePair<Type, NodeInfo> pair in this.factoryService.NodeInfosDict)
            {
                items.Add(new NodeListItemModelView()
                {
                    Name = pair.Value.Name ?? "undef name",
                    NodeType = pair.Key
                });
            }
        }

        /// <summary>
        /// Список доступных узлов на добавление
        /// </summary>
        public ObservableCollection<NodeListItemModelView> AvaibleNodes { get => items; }

        /// <summary>
        /// Выбранный на добавление узел
        /// </summary>
        public NodeListItemModelView? SelectedItem
        {
            get => selectedItem;
            set
            {
                selectedItem = value;
                OnPropertyChanged();
                SelectedItemChanged?.Invoke(this, new SelectedNodeItemChangedArgs(selectedItem));
            }
        }

        /// <summary>
        /// Событие изменения кандидата на добавления
        /// </summary>
        public event EventHandler<SelectedNodeItemChangedArgs>? SelectedItemChanged;
    }

}
