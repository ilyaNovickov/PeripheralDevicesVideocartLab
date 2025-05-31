namespace VideocartLab.ModelViews
{
    public partial class ProjectModelView
    {
        /// <summary>
        /// Режим простоя
        /// </summary>
        private class IdleMode : IModeBase
        {
            public IdleMode(ProjectModelView parent)
            {
                Parent = parent;
            }
            public ProjectModelView Parent { get; private set; }

            public void OnPointerPressed()
            {
                return;
            }
            public void OnPointerMoved(double dx, double dy)
            {
                return;
            }
            public void OnPointerReleased()
            {
                return;
            }
        }
    }
}
