using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.Models.ConnectionInterface
{
    public class AGP : ConnectionInterface
    {
        private double frequency = 66;
        private int memoryBusCapacity = 32;
        private int bitPerClock = 1;

        /// <summary>
        /// Иницилизация интерфейса AGP с частатой 66 МГц, шириной шины 32 бита и передающая 1-н бит за такт
        /// </summary>
        public AGP()
        {

        }

        /// <summary>
        /// Иницилизирует экземпляр интерфейса подключения AGP с указанной частатой, 
        /// шириной шины и коэффициентом передачи данных
        /// </summary>
        /// <param name="frequency">Частота [МГц]</param>
        /// <param name="memoryBusCapacity">Ширина шины [бит]</param>
        /// <param name="bitPerClock">Кол-во бит, переданных за 1-н такт</param>
        public AGP(double frequency, int memoryBusCapacity, int bitPerClock)
        {
            Frequency = frequency;
            MemoryBusCapacity = memoryBusCapacity;
        }

        /// <summary>
        /// Пропускная способность интерфейса [ГБ/с]
        /// </summary>
        public override double Bandwidth => frequency * memoryBusCapacity * bitPerClock / 8d / 1000d;

        /// <summary>
        /// Частота [МГц]
        /// </summary>
        public double Frequency
        {
            get => frequency;
            set
            {
                ValuesValidator.ValidUnnegativeArgument(value);
                frequency = value;
            }
        }

        /// <summary>
        /// Ширина шины памяти [бит]
        /// </summary>
        public int MemoryBusCapacity
        {
            get => memoryBusCapacity;
            set
            {
                ValuesValidator.ValidUnnegativeArgument(value);
                memoryBusCapacity = value;
            }
        }

        /// <summary>
        /// Кол-во бит, переданных за 1-н такт [шт]
        /// </summary>
        public int BitPerClock
        {
            get => bitPerClock;
            set
            {
                ValuesValidator.ValidUnnegativeArgument(value);
                bitPerClock = value;
            }
        }
    }
}
