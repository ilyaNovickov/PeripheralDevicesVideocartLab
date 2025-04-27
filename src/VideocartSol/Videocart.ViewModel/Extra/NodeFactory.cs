using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Videocart.Models;

namespace Videocart.ViewModel.Extra
{
    public class NodeFactoryAction
    {
        public string Name { get; private set; }
        public Func<double, double, Node> Func { get; private set; }

        public NodeFactoryAction(string name, Func<double, double, Node> func)
        {
            Name = name;
            Func = func;
        }
    }

    public class NodeFactory : INodeFactory
    {
        private static ObservableCollection<NodeFactoryAction> actions = new();

        static NodeFactory()
        {
            actions.Add(new NodeFactoryAction("String Mode", CreateStringNode));
        }

        public Node Create(double x, double y, double width, double height, object? content)
        {
            Node node = new Node()
            {
                X = x, Y = y, Width = width, Height = height, Content = content
            };

            return node;
        }

        public static Node CreateStringNode(double x, double y)
        {
            return new Node()
            {
                X = x, Y = y, Width = 200, Height = 100, 
                Content = "Hello world",
                Name = "String Node"
            };
        }

        public static ObservableCollection<NodeFactoryAction> Actions => actions;
       
    }
}
