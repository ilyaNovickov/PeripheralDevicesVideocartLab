namespace VideocartLab.MainModelsProj.ConnectionInterface
{
    /// <summary>
    /// Абстактный класс для интерфейсов подключения видеокарты
    /// </summary>
    public class ConnectionInterface : BaseModel
    {
        /// <summary>
        /// Абстрактное свойство пропускной способности интерфейса подключения [ГБ/с]
        /// </summary>
        public virtual double Bandwidth { get; }

        public virtual double Frequency { get; set; }
    }
}
