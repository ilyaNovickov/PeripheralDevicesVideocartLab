namespace VideocartLab.Presenter
{

    public enum WorkMode
    {
        None, Adding
    }

    public class MainCanvasPresenter
    {
        private NodeType? nodeToAdd = null;

        public WorkMode Mode { get; private set; }

        public NodeType? NodeToAdd
        {
            get => nodeToAdd;
            set
            {
                nodeToAdd = value;
                if (nodeToAdd != null)
                {
                    Mode = WorkMode.Adding;
                }
            }
        }
    }
}
