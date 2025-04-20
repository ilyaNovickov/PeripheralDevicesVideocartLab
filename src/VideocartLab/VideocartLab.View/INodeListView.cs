using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.View
{
    public class SelectedItemChagedArgs : EventArgs
    {
        public object? SelectedItem { get; private set; }

        public SelectedItemChagedArgs(object? item)
        {
            SelectedItem = item;
        }
    }

    public interface INodeListView
    {
        public IEnumerable? ItemsList { get; set; }

        public event EventHandler<SelectedItemChagedArgs> SelectedItemChanged;
    }
}
