using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.ModelViews
{
    public class NodeListModelView : ModelViewBase
    {
        private ObservableCollection<NodeListItemModelView> items = new();
        private NodeListItemModelView? selectedItem = null;
        private NodeFactoryService factoryService;

        public NodeListModelView(NodeFactoryService factoryService) 
        {
            this.factoryService = factoryService;

            items = new ObservableCollection<NodeListItemModelView>();

            foreach (KeyValuePair<Type, NodeInfo> pair in this.factoryService.NodeInfosDict)
            {
                items.Add(new NodeListItemModelView()
                {
                    Name = pair.Value.Name ?? "undef name",
                    NodeType = pair.Key
                });
            }
        }

        public ObservableCollection<NodeListItemModelView> AvaibleNodes { get => items; }

        public event EventHandler<SelectedNodeItemChangedArgs>? SelectedItemChanged;

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
    }

    public class NodeListItemModelView : ModelViewBase
    {
        private string name = "";
        private Type? node = null;

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

    public class SelectedNodeItemChangedArgs : EventArgs
    {
        public NodeListItemModelView? NewItem { get; private set; }

        public SelectedNodeItemChangedArgs(NodeListItemModelView? item)
        {
            NewItem = item;
        }
    }
}
