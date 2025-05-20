using VideocartLab.MainModelsProj.ConnectionInterface;

namespace VideocartLab.ModelViews
{
    /// <summary>
    /// Информация о типе кодировки данных
    /// </summary>
    public class EncodingTypeInfo
    {
        /// <summary>
        /// Имя типа кодировки
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Тип кодировки данных
        /// </summary>
        internal EncodingType EncodingType { get; private set; }

        /// <summary>
        /// Иницилизация
        /// </summary>
        /// <param name="name">Имя типа</param>
        /// <param name="type">Тип</param>
        public EncodingTypeInfo(string name, EncodingType type)
        {
            Name = name;
            EncodingType = type;
        }
    }
}
