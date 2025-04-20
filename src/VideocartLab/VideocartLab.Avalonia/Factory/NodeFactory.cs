using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Videocart.Views;

namespace VideocartLab.Avalonia.Factory
{
    internal class NodeFactory : INodeFactory
    {
        public INodeView CreateNode(object? content, double x, double y)
        {
            TextNode textNode = new TextNode();

            textNode.X = x;
            textNode.Y = y;
            textNode.SetContent(content);

            return textNode;
        }
    }
}
