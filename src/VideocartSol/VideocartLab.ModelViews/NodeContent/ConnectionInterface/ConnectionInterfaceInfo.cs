using VideocartLab.MainModelsProj.ConnectionInterface;

namespace VideocartLab.ModelViews
{
    /// <summary>
    /// Информаци о интерфейсе соединения с материнской платой
    /// </summary>
    public class ConnectionInterfaceInfo
    {
        //Словарь соотношения типа кодировки и его имени
        private static Dictionary<EncodingType, string> typesAndNames;

        static ConnectionInterfaceInfo()
        {
            typesAndNames = new Dictionary<EncodingType, string>()
            {
                { EncodingType.Another, "Another" },
                { EncodingType._8bOn10b, "8/10" },
                { EncodingType._128On130b, "128/130" },
                { EncodingType._64On66, "64/66" },
                { EncodingType._242On256, "242/256" },
            };
        }

        /// <summary>
        /// Имя интерфейса
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Экземпляр интерфейса соединения
        /// </summary>
        internal ConnectionInterface Interface { get; private set; }

        /// <summary>
        /// ModelView для этого интерфейса
        /// </summary>
        public ModelViewBase VM { get; private set; }

        /// <summary>
        /// Иницилизация информации о интерфейсе
        /// </summary>
        /// <param name="name">Имя интерфейса</param>
        /// <param name="interface">Экземпляр интерфейса</param>
        /// <param name="vm">ModelView для интерфейса</param>
        internal ConnectionInterfaceInfo(string name, ConnectionInterface @interface, ModelViewBase vm)
        {
            Name = name;
            Interface = @interface;
            VM = vm;
        }

        /// <summary>
        /// Иницилизация информации о интерфейсе
        /// </summary>
        /// <param name="name">Имя интерфейса</param>
        /// <param name="agp">Интерфейса типа AGP</param>
        /// <param name="vm">ModelView для интерфейса</param>
        internal ConnectionInterfaceInfo(string name, AGP agp, AGPViewModel vm)
        {
            Name = name;
            Interface = agp;

            vm.BitPerClock = agp.BitPerClock;
            vm.Frequency = agp.Frequency;
            vm.MemoryBusCapacity = agp.MemoryBusCapacity;

            VM = vm;
        }

        /// <summary>
        /// Иницилизация информации о интерфейсе
        /// </summary>
        /// <param name="name">Имя интерфейса</param>
        /// <param name="pci">Интерфейса типа PCI</param>
        /// <param name="vm">ModelView для интерфейса</param>
        internal ConnectionInterfaceInfo(string name, PCI pci, PCIViewModel vm)
        {
            Name = name;
            Interface = pci;

            vm.Frequency = pci.Frequency;
            vm.MemoryBusCapacity = pci.MemoryBusCapacity;

            VM = vm;
        }

        /// <summary>
        /// Иницилизация информации о интерфейсе
        /// </summary>
        /// <param name="name">Имя интерфейса</param>
        /// <param name="pcie">Интерфейса типа PCIe</param>
        /// <param name="vm">ModelView для интерфейса</param>
        internal ConnectionInterfaceInfo(string name, PCIe pcie, PCIExpressViewModel vm)
        {
            Name = name;
            Interface = pcie;

            vm.BitPerClock = pcie.BitPerClock;
            vm.Frequency = pcie.Frequency;
            vm.Lines = pcie.Lines;
            vm.Type = new EncodingTypeInfo(typesAndNames[pcie.Type], pcie.Type);

            VM = vm;
        }
    }
}
