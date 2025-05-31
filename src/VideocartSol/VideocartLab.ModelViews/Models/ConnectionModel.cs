namespace VideocartLab.ModelViews.Models
{
    /// <summary>
    /// Модель соединения
    /// </summary>
    public class ConnectionModel
    {
        /// <summary>
        /// ID соединения
        /// </summary>
        public string? Id { get; set; }
        /// <summary>
        /// Тип соединения
        /// </summary>
        public ConnectionType Type { get; set; }
    }
}
