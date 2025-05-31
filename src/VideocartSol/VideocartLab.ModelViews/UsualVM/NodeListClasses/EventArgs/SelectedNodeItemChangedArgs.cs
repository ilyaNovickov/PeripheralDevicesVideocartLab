namespace VideocartLab.ModelViews
{

    /// <summary>
    /// Аргументы события изменения выбранного кандидата
    /// </summary>
    public class SelectedNodeItemChangedArgs : EventArgs
    {
        /// <summary>
        /// Кандидат
        /// </summary>
        public NodeListItemModelView? NewItem { get; private set; }

        public SelectedNodeItemChangedArgs(NodeListItemModelView? item)
        {
            NewItem = item;
        }
    }
}
