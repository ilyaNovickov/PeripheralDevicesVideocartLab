using System.Text.Json.Serialization;

namespace VideocartLab.ModelViews.Models
{
    /// <summary>
    /// Модель проекта
    /// </summary>
    public class ProjectModel
    {
        private List<NodeModel> nodes = new List<NodeModel>();

        /// <summary>
        /// Список узлов в проекте
        /// </summary>
        [JsonInclude]
        public List<NodeModel> Nodes
        {
            get => nodes;
            set => nodes = value;
        }
    }
}
