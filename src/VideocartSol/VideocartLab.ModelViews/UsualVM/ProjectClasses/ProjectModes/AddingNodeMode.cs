namespace VideocartLab.ModelViews
{
    public partial class ProjectModelView
    {
        /// <summary>
        /// Режим добавления узла
        /// </summary>
        private class AddingNodeMode : IModeBase
        {
            public AddingNodeMode(ProjectModelView parent)
            {
                Parent = parent;
            }

            public ProjectModelView Parent { get; private set; }

            public void OnPointerPressed()
            {
                var node = Parent.AddNode();

                Parent.Nodes.Add(node!);
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
