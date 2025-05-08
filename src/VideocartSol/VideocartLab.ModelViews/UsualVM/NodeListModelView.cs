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
        public NodeListModelView() 
        { 
            Strs = new ObservableCollection<string>()
            {
                "foo1", "foo2", "foo3"
            };
        }

        public ObservableCollection<string> Strs { get; set; }
    }
}
