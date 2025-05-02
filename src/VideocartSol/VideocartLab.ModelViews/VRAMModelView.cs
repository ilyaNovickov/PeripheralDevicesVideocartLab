using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideocartLab.MainModelsProj.GPUMemory;
using VideocartLab.MainModelsProj;

namespace VideocartLab.ModelViews
{
    public class VRAMModelView : ModelViewBase
    {
        private GDDRType type = new GDDRType(GDDRTypes.GDDR);
        private int capacity = 1024;
        private int memoryBusCapacitiy = 8;
        private double realFrequency = 1000;

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
        public int? Capacity
        {
            get => capacity;
            set
            {
                capacity = value ?? 0;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Ширина шины памяти [бит]
        /// </summary>
        public int? MemoryBusCapacity
        {
            get => memoryBusCapacitiy;
            set
            {
                memoryBusCapacitiy = value ?? 0;
                OnPropertyChanged();
                OnPropertyChanged(nameof(MemoryBandwidth));
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
                return EffectiveFrequency!.Value * MemoryBusCapacity!.Value / 8d / 1000d;
            }
        }

        /// <summary>
        /// Реальаня частота памяти [МГц]
        /// Высчитывается, как [Эффективная частота] / [Множитель GDDR]
        /// </summary>
        public double? RealFrequency
        {
            get => realFrequency;
            set
            {
                realFrequency = value ?? 0d;
                OnPropertyChanged();
                OnPropertyChanged(nameof(EffectiveFrequency));
                OnPropertyChanged(nameof(MemoryBandwidth));
            }
        }

        /// <summary>
        /// Эффективная частота памяти [МГц]
        /// Высчитывается, как [Реальная частота] * [Множитель GDDR]
        /// </summary>
        public double? EffectiveFrequency
        {
            get => realFrequency * type.EffectiveRatio;
            set
            {
                realFrequency = type.RealRatio * (value ?? 0d);
                OnPropertyChanged();
                OnPropertyChanged(nameof(RealFrequency));
                OnPropertyChanged(nameof(MemoryBandwidth));
            }
        }
    }
}
