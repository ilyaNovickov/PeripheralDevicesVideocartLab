using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideocartLab.MainModelsProj.GPUMemory
{
    /// <summary>
    /// Класс видеопамяти видеокарты
    /// </summary>
    public class VRAM : BaseModel
    {
        private GDDRType type = new GDDRType(GDDRTypes.GDDR);
        private int capacity = 1024;
        private int memoryBusCapacitiy = 8;
        private double realFrequency = 1000;

        /// <summary>
        /// Создаёт экземпляр памяти с характеристиками:
        /// Объём = 1024 МБ;
        /// GDDR;
        /// Шина = 8 бит;
        /// Пропускная способность = 1 ГМБ/с;
        /// Реал. и Эфф. частоты = 1000 МГц;
        /// </summary>
        public VRAM()
        {

        }

        /// <summary>
        /// Тип памяти GDDR
        /// </summary>
        public GDDRType Type
        {
            get => type;
            set
            {
                ValuesValidator.ValidUnnullObject(value);
                type = value;
            }
        }

        /// <summary>
        /// Объём памяти [МБ]
        /// </summary>
        public int Capacity
        {
            get => capacity;
            set
            {
                ValuesValidator.ValidUnnegativeArgument(value);
                capacity = value;
            }
        }

        /// <summary>
        /// Ширина шины памяти [бит]
        /// </summary>
        public int MemoryBusCapacity
        {
            get => memoryBusCapacitiy;
            set
            {
                ValuesValidator.ValidUnnegativeArgument(value);
                memoryBusCapacitiy = value;
            }
        }

        /// <summary>
        /// Пропускная способность памяти [ГБ/с]
        /// Высчитывается, как [Эффективная частота] * [Ширина шины] / 8 
        /// </summary>
        public double MemoryBandwidth
        {
            get
            {
                return EffectiveFrequency * MemoryBusCapacity / 8d / 1000d;
            }
        }

        /// <summary>
        /// Реальаня частота памяти [МГц]
        /// Высчитывается, как [Эффективная частота] / [Множитель GDDR]
        /// </summary>
        public double RealFrequency
        {
            get => realFrequency;
            set
            {
                ValuesValidator.ValidUnnegativeArgument(value);

                realFrequency = value;
            }
        }

        /// <summary>
        /// Эффективная частота памяти [МГц]
        /// Высчитывается, как [Реальная частота] * [Множитель GDDR]
        /// </summary>
        public double EffectiveFrequency
        {
            get => realFrequency * type.EffectiveRatio;
            set
            {
                ValuesValidator.ValidUnnegativeArgument(value);

                realFrequency = type.RealRatio * value;
            }
        }

    }
}
