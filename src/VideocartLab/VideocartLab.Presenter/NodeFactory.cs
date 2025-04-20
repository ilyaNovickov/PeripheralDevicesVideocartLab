using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.Presenter
{
    public class NodeFactory
    {
        public static Node CreateTextNode(string text, double x, double y)
        {
            return new Node()
            {
                X = x,
                Y = y,
                Content = text
            };
        }
    }
}
