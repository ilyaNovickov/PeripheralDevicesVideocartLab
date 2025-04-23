using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideocartLab.Models;
using static System.Net.Mime.MediaTypeNames;

namespace VideocartLab.ModelVIews
{
    public class TestModelView : ModelViewBase
    {
        private TestClass test;

        private NodeModelView node;

        public TestModelView(NodeModelView node)
        {
            this.node = node;
            test = (TestClass)node.Content!;
        }

        public int Count
        {
            get => test.Count;
            set
            {
                test.Count = value;
                OnPropertyChanged();
            }
        }

        public string Str
        {
            get => test.Str;
            set
            {
                test.Str = value;
                OnPropertyChanged();
            }
        }
    }
}
