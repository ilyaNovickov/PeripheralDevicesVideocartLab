namespace VideocartLab.ModelViews
{
    /// <summary>
    /// Аргументы события нажатия на узел
    /// </summary>
    public class NodePressedArgs : EventArgs
    {
        /// <summary>
        /// Нажатый узел
        /// </summary>
        public NodeModelView Node { get; private set; }

        public NodePressedArgs(NodeModelView node)
        {
            Node = node;
        }
    }
}
