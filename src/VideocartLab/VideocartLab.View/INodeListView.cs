using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.View
{
    public interface INodeListView
    {
        public bool HasSelectedItem { get; }

        public Type? SelectedNodeType { get; }
    }
}
