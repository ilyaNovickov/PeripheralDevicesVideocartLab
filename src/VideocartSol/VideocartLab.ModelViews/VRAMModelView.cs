using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideocartLab.MainModelsProj.GPUMemory;
using VideocartLab.MainModelsProj;
using System.Collections.ObjectModel;

namespace VideocartLab.ModelViews
{
    public class VRAMModelView : ModelViewBase
    {
        //private GDDRType type = new GDDRType(GDDRTypes.GDDR);
        private int capacity = 1024;
        private int memoryBusCapacitiy = 8;
        private double realFrequency = 1000;

        /// <summary>
        /// Тип памяти GDDR
        /// </summary>
        //public GDDRType Type
        //{
        //    get => type;
        //    set
        //    {
        //        ValuesValidator.ValidUnnullObject(value);
        //        type = value;
        //    }
        //}

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
            get => realFrequency * (SelectedGDDR?.Type.EffectiveRatio);
            set
            {
                realFrequency = (SelectedGDDR?.Type.RealRatio * (value ?? 0d))!.Value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(RealFrequency));
                OnPropertyChanged(nameof(MemoryBandwidth));
            }
        }

        ObservableCollection<GDDRTypeInfo> gddrInfos = new();
        private GDDRTypeInfo? selectedType = null;

        public VRAMModelView()
        {
            InitList();
        }

        private void InitList()
        {
            #region addGDDRTypes
            gddrInfos.Add(new GDDRTypeInfo(GDDRType.GDDR, "GDDR"));
            gddrInfos.Add(new GDDRTypeInfo(GDDRType.GDDR2, "GDDR2"));
            gddrInfos.Add(new GDDRTypeInfo(GDDRType.GDDR3, "GDDR3"));
            gddrInfos.Add(new GDDRTypeInfo(GDDRType.GDDR4, "GDDR4"));
            gddrInfos.Add(new GDDRTypeInfo(GDDRType.GDDR5, "GDDR5"));
            gddrInfos.Add(new GDDRTypeInfo(GDDRType.GDDR5X, "GDDR5X"));
            gddrInfos.Add(new GDDRTypeInfo(GDDRType.GDDR6, "GDDR6"));
            gddrInfos.Add(new GDDRTypeInfo(GDDRType.GDDR6X, "GDDR6X"));
            #endregion

            SelectedGDDR = gddrInfos[0];
        }

        public ObservableCollection<GDDRTypeInfo> GDDRTypes
        {
            get => gddrInfos;
        }

        public GDDRTypeInfo? SelectedGDDR
        {
            get => selectedType;
            set
            {
                selectedType = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(EffectiveFrequency));
                OnPropertyChanged(nameof(MemoryBandwidth));
            }
        }
    }

    public class GDDRTypeInfo
    {
        internal GDDRType Type { get; private set; }
        public string Name { get; private set; }

        internal GDDRTypeInfo(GDDRType type, string? name) 
        {
            Type = type;
            Name = name ?? "";
        }
    }
}
