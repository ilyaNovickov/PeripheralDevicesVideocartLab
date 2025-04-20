namespace Videocart.Views
{
    public interface INodeFactory
    {
        public INodeView CreateNode(object? content, double x, double y);
    }
}
