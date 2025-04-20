namespace Videocart.Views
{
    public interface INodeFactory
    {
        public INodeView CreateNodeText(string name, double x, double y);
    }
}
