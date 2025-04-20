using Avalonia.Controls;
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
            //TextNode textNode = new TextNode();
            INodeView textNode = ChooseControl(content);

            textNode.X = x;
            textNode.Y = y;
            textNode.SetContent(content);

            return textNode;
        }

        private INodeView ChooseControl(object? content)
        {
            switch (content)
            {
                case string:
                    return new TextNode();
                default:
                    throw new Exception("??");
                    //return null;
            }
        }
    }
}
