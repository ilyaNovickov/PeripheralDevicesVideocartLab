using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.MainModelsProj.ConnectionInterface
{
    public class PCI : ConnectionInterface
    {
        /// <summary>
        /// Экземпляр интерфейса PCI с частатой 33 МГц и шириной шины 32 бита
        /// </summary>
        public static PCI PCI32bit33MHz => new PCI(33, 32);
        /// <summary>
        /// Экземпляр интерфейса PCI с частатой 33 МГц и шириной шины 64 бита 
        /// (эквивалент PCI с 64 МГц и 32 бита)
        /// </summary>
        public static PCI PCI64bit33Mhz => new PCI(33, 64);
        /// <summary>
        /// Экземпляр интерфейса PCI с частатой 66 МГц и шириной шины 64 бита
        /// </summary>
        public static PCI PCI64bit66Mhz => new PCI(66, 64);

        private double frequency = 33;
        private int memoryBusCapacity = 32;

        /// <summary>
        /// Иницилизация интерфейса PCI с частатой 33 МГц и шириной шины 32 бита
        /// </summary>
        public PCI()
        {

        }

        /// <summary>
        /// Иницилизирует экземпляр интерфейса подключения PCI с указанной частатой и шириной шины
        /// </summary>
        /// <param name="frequency">Частота [МГц]</param>
        /// <param name="memoryBusCapacity">Ширина шины [бит]</param>
        public PCI(double frequency, int memoryBusCapacity)
        {
            Frequency = frequency;
            MemoryBusCapacity = memoryBusCapacity;
        }

        /// <summary>
        /// Пропускная способность интерфейса [ГБ/с]
        /// </summary>
        public override double Bandwidth => frequency * memoryBusCapacity / 8d / 1000d;

        /// <summary>
        /// Частота [МГц]
        /// </summary>
        public override double Frequency
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
    }
}
