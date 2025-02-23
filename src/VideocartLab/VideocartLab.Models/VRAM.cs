namespace VideocartLab.Models
{
    /// <summary>
    /// Класс видеопамяти видеокарты
    /// </summary>
    public class VRAM
    {
        private GDDRType type = new GDDRType(GDDRTypes.GDDR);
        private int capacity = 1024;
        private int memoryBusCapacitiy = 8;
        private double memoryBandwidth = 1;
        private double realFrequency = 1000d;
        private int effectiveFrequency = 1000;

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
        /// Изменение этого свойства изменяет эффективную частоту 
        /// </summary>
        public GDDRType Type
        {
            get => type;
            set 
            {
                type = value;
                OnChangeFrequencyAndBusCapcity(needToChangeFreq: true);
            } 
        }

        /// <summary>
        /// Объём памяти [МБ]
        /// </summary>
        public int Capacity
        {
            get => capacity;
            set => capacity = value;
        }

        /// <summary>
        /// Ширина шины памяти [бит]
        /// При изменении этого свойства меняется пропускная способность
        /// </summary>
        public int MemoryBusCapacity
        {
            get => memoryBusCapacitiy;
            set 
            {
                memoryBusCapacitiy = value;
                OnChangeFrequencyAndBusCapcity(needToChangeFreq: false);
            }
        }

        /// <summary>
        /// Пропускная способность памяти [ГБ/с]
        /// Высчитывается, как [Эффективная частота] * [Ширина шины] / 8 
        /// </summary>
        public double MemoryBandwidth
        {
            get => memoryBandwidth;
            private set => memoryBandwidth = value;
        }

        /// <summary>
        /// Реальаня частота памяти [МГц]
        /// Высчитывается, как [Эффективная частота] / [Множитель GDDR]
        /// При изменении этого свойства меняется и эффективная частота, 
        /// и пропускная способность
        /// </summary>
        public double RealFrequency
        {
            get => realFrequency;
            set
            {
                realFrequency = value;
                OnChangeFrequencyAndBusCapcity(needToChangeFreq: true);
            }
        }

        /// <summary>
        /// Эффективная частота памяти [МГц]
        /// Высчитывается, как [Реальная частота] * [Множитель GDDR]
        /// При изменении этого свойства меняется и реальная частота, и пропускная способность
        /// </summary>
        public int EffectiveFrequency
        {
            get => effectiveFrequency;
            set
            {
                effectiveFrequency = value;
                OnChangeFrequencyAndBusCapcity(needToChangeFreq: true, effectiveFreqChanged:true);
            }
        }

        /// <summary>
        /// Синхронизация изменений пропускной способности памяти и её частот
        /// </summary>
        /// <param name="needToChangeFreq">Указывает, что нужно изменить частоты памяты</param>
        /// <param name="effectiveFreqChanged">Указывает, то изменилась эффективная частота</param>
        private void OnChangeFrequencyAndBusCapcity(bool needToChangeFreq, bool effectiveFreqChanged = false)
        {
            if (!needToChangeFreq)
                goto Changed;

            if (effectiveFreqChanged)
            {
                this.realFrequency = EffectiveFrequency * Type.RealRatio;
            }
            else
            {
                this.effectiveFrequency = (int)(RealFrequency * Type.EffectiveRatio);
            }

            Changed:
            this.memoryBandwidth = MemoryBusCapacity * EffectiveFrequency / 8;
        }
    }
}
