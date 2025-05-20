namespace VideocartLab.ModelViews
{
    public partial class ProjectModelView
    {
        /// <summary>
        /// Режим удаления узла
        /// </summary>
        private class RemoveNodeMode : IModeBase
        {
            public RemoveNodeMode(ProjectModelView parent)
            {
                Parent = parent;
            }

            public ProjectModelView Parent { get; private set; }

            public void OnPointerPressed()
            {
                return;
            }

            public void OnPointerMoved(double x, double y)
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
