namespace VideocartLab.ModelViews
{
    public partial class ProjectModelView
    {
        /// <summary>
        /// Интерфейс режима проекта
        /// </summary>
        private interface IModeBase
        {
            ProjectModelView Parent { get; }
            /// <summary>
            /// При событии нажатия кнопки мыши
            /// </summary>
            void OnPointerPressed();
            /// <summary>
            /// При событии перемещения курсора мыши
            /// </summary>
            /// <param name="dx">Изменение в координатах X</param>
            /// <param name="dy">Изменение в координатах Y</param>
            void OnPointerMoved(double dx, double dy);
            /// <summary>
            /// При событии отжатия кнопки мыши
            /// </summary>
            void OnPointerReleased();
        }
    }
}
