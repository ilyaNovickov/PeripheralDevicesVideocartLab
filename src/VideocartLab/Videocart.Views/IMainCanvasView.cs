using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Videocart.Views.EventsArgs;

namespace Videocart.Views
{
    public interface IMainCanvasView
    {
        //Файбрика для создания узлов
        public INodeFactory NodeFactory { get; }

        public event EventHandler<MousePressedArgs> MousePressed;
        public event EventHandler<MouseMovedArgs> MouseMoved;
        public event EventHandler MouseRelease;
        //public void OnMousePressed(MousePressedArgs e);

        //Добавления узла в представления
        public void AddNode(INodeView node);
    }
}
