using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Videocart.Models.Events;

namespace Videocart.Models
{
    public interface INodeFactory
    {
        //public event EventHandler<NodeCreatedArgs>? NodeCreated;

        public Node Create(double x, double y, double width, double height, object? content)
        {
            Node node = new Node()
            {
                X = x,
                Y = y,
                Width = width,
                Height = height,
                Content = content
            };

            return node;
        }
    }
}
