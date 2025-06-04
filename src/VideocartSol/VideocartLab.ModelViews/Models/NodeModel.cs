using VideocartLab.MainModelsProj;

namespace VideocartLab.ModelViews.Models
{
    /// <summary>
    /// Модель узла
    /// </summary>
    public class NodeModel
    {
        /// <summary>
        /// Имя узла
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Координата X
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// Координата Y
        /// </summary>
        public double Y { get; set; }
        /// <summary>
        /// Ширина узла
        /// </summary>
        public double Width { get; set; }
        /// <summary>
        /// Высота узла
        /// </summary>
        public double Height { get; set; }
        /// <summary>
        /// Модель внутри узла
        /// </summary>
        public BaseModel? InnerModel { get; set; }
        /// <summary>
        /// Соединения узла
        /// </summary>
        public ConnectionModel[]? Connections { get; set; }
    }
}
