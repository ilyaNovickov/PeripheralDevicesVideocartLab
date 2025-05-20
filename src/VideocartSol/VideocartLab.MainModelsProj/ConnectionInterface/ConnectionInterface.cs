using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public virtual double Bandwidth
        {
            get;
        }

        public virtual double Frequency { get; set; }
    }
}
