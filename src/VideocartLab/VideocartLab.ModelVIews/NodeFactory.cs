using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideocartLab.Models;

namespace VideocartLab.ModelVIews
{
    public class NodeFactory
    {
        public NodeModelView Create(double x, double y, double width, double height, object? content)
        {
            if (content is string str)
                return CreateStringNode(x, y, width, height, str);
            else if (content is TestClass test)
                return CreateTestNode(x, y, width,  height, test);
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
    
        private NodeModelView CreateTestNode(double x, double y, double width, double height, TestClass content)
        {
            return new NodeModelView()
            {
                X = x, Y = y, Width = width, Height = height,
                Content = content
            };
        }
    }
}
