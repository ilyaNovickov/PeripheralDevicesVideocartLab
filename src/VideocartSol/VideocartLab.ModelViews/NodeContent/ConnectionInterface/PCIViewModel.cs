namespace VideocartLab.ModelViews
{
    /// <summary>
    /// Интерфейс типа PCI
    /// </summary>
    public class PCIViewModel : ModelViewBase
    {
        private double frequency = 33;
        private int memoryBusCapacity = 32;

        /// <summary>
        /// Пропускная способность интерфейса [ГБ/с]
        /// </summary>
        public double Bandwidth => frequency * memoryBusCapacity / 8d / 1000d;

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
    }
}
