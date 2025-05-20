namespace VideocartLab.ModelViews
{
    /// <summary>
    /// Подключение по AGP
    /// </summary>
    public class AGPViewModel : ModelViewBase
    {
        private double frequency = 66;
        private int memoryBusCapacity = 32;
        private int bitPerClock = 1;

        /// <summary>
        /// Пропускная способность интерфейса [ГБ/с]
        /// </summary>
        public double Bandwidth => frequency * memoryBusCapacity * bitPerClock / 8d / 1000d;

        /// <summary>
        /// Частота [МГц]
        /// </summary>
        public double Frequency
        {
            get => frequency;
            set
            {
                frequency = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Bandwidth));
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
                memoryBusCapacity = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Bandwidth));
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
                bitPerClock = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Bandwidth));
            }
        }
    }
}
