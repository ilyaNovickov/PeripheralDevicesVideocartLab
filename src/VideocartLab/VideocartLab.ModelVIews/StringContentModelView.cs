using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.ModelVIews
{
    public class StringContentModelView : ModelViewBase
    {
        private string _content = "";

        public NodeModelView NodeModelView { get; private set; }

        public StringContentModelView(NodeModelView node)
        {
            NodeModelView = node;
            _content = (string)node.Content!;
        }

        public string Content
        {
            get => _content;
            set
            {
                _content = value;
                NodeModelView.Content = _content;
                OnPropertyChanged();
            }
        }
    }
}
