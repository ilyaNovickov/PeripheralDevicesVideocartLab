using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideocartLab.MainModelsProj;
using VideocartLab.MainModelsProj.ConnectionInterface;

namespace VideocartLab.ModelViews
{
    public class ConnectionInterfaceModelView : ModelViewBase
    {
        private ObservableCollection<ConnectionInterfaceInfo> connectionInfos = new();
        private ConnectionInterfaceInfo? selectedInfo = null;

        public ObservableCollection<ConnectionInterfaceInfo> ConnectionInfos => connectionInfos;

        public ConnectionInterfaceModelView()
        {
            InitList();
        }

        private void InitList()
        {
            #region InitList
            connectionInfos.Add(new ConnectionInterfaceInfo("PCI 32bit/33 MHz", PCI.PCI32bit33MHz, new PCIViewModel()));
            connectionInfos.Add(new ConnectionInterfaceInfo("PCI 32bit/66 MHz", PCI.PCI64bit33Mhz, new PCIViewModel()));
            connectionInfos.Add(new ConnectionInterfaceInfo("PCI 64bit/33 MHz", PCI.PCI64bit33Mhz, new PCIViewModel()));
            connectionInfos.Add(new ConnectionInterfaceInfo("PCI 64bit/66 MHz", PCI.PCI64bit66Mhz, new PCIViewModel()));

            connectionInfos.Add(new ConnectionInterfaceInfo("AGP 1.0x1", AGP.AGP1x1, new AGPViewModel()));
            connectionInfos.Add(new ConnectionInterfaceInfo("AGP 1.0x2", AGP.AGP1x2, new AGPViewModel()));
            connectionInfos.Add(new ConnectionInterfaceInfo("AGP 2.0", AGP.AGP2, new AGPViewModel()));
            connectionInfos.Add(new ConnectionInterfaceInfo("AGP 3.0", AGP.AGP3, new AGPViewModel()));

            connectionInfos.Add(new ConnectionInterfaceInfo("PCIe 1.0x1", new PCIe(), new PCIExpressViewModel()));
            connectionInfos.Add(new ConnectionInterfaceInfo("PCIe 2.0x4", PCIe.PCIe2dot0x4, new PCIExpressViewModel()));
            connectionInfos.Add(new ConnectionInterfaceInfo("PCIe 4.0x8", PCIe.PCIe4dot0x8, new PCIExpressViewModel()));
            connectionInfos.Add(new ConnectionInterfaceInfo("PCIe 6.0x16", PCIe.PCIe6dot0x16, new PCIExpressViewModel()));
            #endregion

            SelectedInterface = ConnectionInfos[0];
        }

        public ConnectionInterfaceInfo? SelectedInterface
        {
            get => selectedInfo;
            set
            {
                selectedInfo = value;
                OnPropertyChanged();
            }
        }


    }

    public class ConnectionInterfaceInfo
    {
        public string Name { get; private set; }
        internal ConnectionInterface Interface { get; private set; }
        public ModelViewBase VM { get; private set; }

        internal ConnectionInterfaceInfo(string name, ConnectionInterface @interface, ModelViewBase vm)
        {
            Name = name;
            Interface = @interface;
            VM = vm;
        }

        internal ConnectionInterfaceInfo(string name, AGP agp, AGPViewModel vm)
        {
            Name = name;
            Interface = agp;

            vm.BitPerClock = agp.BitPerClock;
            vm.Frequency = agp.Frequency;
            vm.MemoryBusCapacity = agp.MemoryBusCapacity;

            VM = vm;
        }

        internal ConnectionInterfaceInfo(string name, PCI pci, PCIViewModel vm)
        {
            Name = name;
            Interface = pci;

            vm.Frequency = pci.Frequency;
            vm.MemoryBusCapacity = pci.MemoryBusCapacity;

            VM = vm;
        }

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
    }

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

    public class PCIExpressViewModel : ModelViewBase
    {
        private double frequency = 2500;
        private int lines = 1;
        private double encodingType = 8d / 10d;
        private EncodingTypeInfo type = new EncodingTypeInfo("8/10", EncodingType._8bOn10b);
        private int bitPerClock = 1;

        /// <summary>
        /// Кол-во бит передаваемых за 1-н такт
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

        /// <summary>
        /// Тип кодирования передаваемых данных
        /// </summary>
        public EncodingTypeInfo Type
        {
            get => type;
            set
            {
                type = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Bandwidth));
            }
        }

        /// <summary>
        /// Частота работы интерфейса [МГц]
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
        /// Кол-во линий передачи [шт]
        /// </summary>
        public int Lines
        {
            get => lines;
            set
            {
                lines = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Bandwidth));
            }
        }

        /// <summary>
        /// Пропускная способность [ГБ/с]
        /// </summary>
        public double Bandwidth
        {
            get
            {
                return Lines * MatchEncodingType(Type.EncodingType) * BitPerClock * Frequency / 8d / 1000d;
            }
        }

        /// <summary>
        /// Пользовательский коэффициент передачи в пределах от 0 (строго больше) до 1 (включительно)
        /// </summary>
        public double UserEncodingType
        {
            get => encodingType;
            set
            {
                if (value <= 0 || value > 1d)
                    throw new Exception("User's encoding value has to be in range [0, 1]");

                encodingType = value;
            }
        }

        /// <summary>
        /// Сопоставление выбранного типа кодирования
        /// </summary>
        /// <param name="type">Тип кодирования</param>
        /// <returns>Значение от 0 до 1, обозначаещее коэффициент полезной длины сообщения</returns>
        /// <exception cref="Exception">Введён неизместный тип кодирования</exception>
        private double MatchEncodingType(EncodingType type)
        {
            switch (type)
            {
                case EncodingType.Another:
                    return encodingType;
                case EncodingType._8bOn10b:
                    return 8d / 10d;
                case EncodingType._64On66:
                    return 64d / 66d;
                case EncodingType._128On130b:
                    return 128d / 130d;
                case EncodingType._242On256:
                    return 242d / 256d;
                default:
                    throw new Exception("Unknown encoding type");
            }
        }
    }

    public class EncodingTypeInfo
    {
        public string Name { get; private set; }
        internal EncodingType EncodingType { get; private set; }

        public EncodingTypeInfo(string name, EncodingType type)
        {
            Name = name;
            EncodingType = type;
        }
    }
}
