using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideocartLab.Models;

namespace VideocartLab.ModelVIews
{
    //Фабрика для добавления узлов
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
            var nodeVM = new NodeModelView()
            {
                X = x,
                Y = y,
                Width = width,
                Height = height,
                Content = content,
            };

            var conn1 = new ConnectionModelView()
            {
                Width = 10,
                Height = 10,
                X = x - 5,
                Y = y + height / 2d - 5,
            };
            conn1.Model.Parent = nodeVM.Node;
            nodeVM.ConnectionModelViews.Add(conn1);

            var conn2 = new ConnectionModelView()
            {
                Width = 10,
                Height = 10,
                X = x + width - 5,
                Y = y + height / 2d - 5
            };
            conn2.Model.Parent = nodeVM.Node;
            nodeVM.ConnectionModelViews.Add(conn2);

            return nodeVM;
        }
    }
}
