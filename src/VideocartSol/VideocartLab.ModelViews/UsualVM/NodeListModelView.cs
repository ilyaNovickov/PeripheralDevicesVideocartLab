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

        public NodeListModelView() 
        {
            items = new ObservableCollection<NodeListItemModelView>()
            {
                new NodeListItemModelView()
                {
                    Name = "foo1"
                },
                new NodeListItemModelView()
                {
                    Name = "foo2"
                }
            };
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
