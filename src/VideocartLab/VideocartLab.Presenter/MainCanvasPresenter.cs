namespace VideocartLab.Presenter
{

    public enum WorkMode
    {
        None, Adding
    }

    public class MainCanvasPresenter
    {
        private NodeListPresenter listPresenter;

        public WorkMode Mode { get; private set; }
    }
}
