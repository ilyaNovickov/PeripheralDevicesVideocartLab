namespace VideocartLab.ModelViews
{
    /// <summary>
    /// Аргументы события удаления узла
    /// </summary>
    public class NodeRemovedArgs : EventArgs
    {
        /// <summary>
        /// Удалённый узел
        /// </summary>
        public NodeModelView RemovedNode { get; private set; }

        public NodeRemovedArgs(NodeModelView removedNode)
        {
            RemovedNode = removedNode;
        }
    }
}
