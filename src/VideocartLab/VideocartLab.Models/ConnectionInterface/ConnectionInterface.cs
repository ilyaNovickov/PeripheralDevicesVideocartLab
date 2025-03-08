using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.Models.ConnectionInterface
{
    /// <summary>
    /// Абстактный класс для интерфейсов подключения видеокарты
    /// </summary>
    public abstract class ConnectionInterface
    {
        /// <summary>
        /// Абстрактное свойство пропускной способности интерфейса подключения
        /// </summary>
        public abstract double Bandwidth
        {
            get;
        }
    }
}
