using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.ModelVIews
{
    public class NodeFactory
    {
        public NodeModelView Create(double x, double y, double width, double height, object? content)
        {
            if (content is string str)
                return CreateStringNode(x, y, width, height, str);
            throw new Exception("!!!");
        }

        private NodeModelView CreateStringNode(double x, double y, double width, double height, string content)
        {
            return new NodeModelView()
            {
                X = x, Y = y, 
                Name = "Wolololo", 
                Width = 100, Height = 100, 
                Content = content
            };
        }
    }
}
