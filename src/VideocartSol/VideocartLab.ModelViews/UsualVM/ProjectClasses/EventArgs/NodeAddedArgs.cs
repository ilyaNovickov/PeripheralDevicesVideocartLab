namespace VideocartLab.ModelViews
{
    /// <summary>
    /// Аргументы события добавленяи узла
    /// </summary>
    public class NodeAddedArgs : EventArgs
    {
        /// <summary>
        /// Добавленный узел
        /// </summary>
        public NodeModelView AddedNode { get; private set; }

        public NodeAddedArgs(NodeModelView addedNode)
        {
            AddedNode = addedNode;
        }
    }
}
