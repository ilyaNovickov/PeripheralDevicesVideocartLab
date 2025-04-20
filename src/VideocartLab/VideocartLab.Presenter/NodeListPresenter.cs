using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideocartLab.View;

namespace VideocartLab.Presenter
{
    internal class NodeListPresenter
    {
        private INodeListView nodeListView;

        public NodeListPresenter(INodeListView view) 
        {
            nodeListView = view;
        }


    }
}
