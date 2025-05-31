using System.Collections.ObjectModel;
using VideocartLab.MainModelsProj.ConnectionInterface;

namespace VideocartLab.ModelViews
{
    /// <summary>
    /// Интерфейс подключения к материнской плате 
    /// </summary>
    public class ConnectionInterfaceModelView : ModelViewBase
    {
        private ObservableCollection<ConnectionInterfaceInfo> connectionInfos = new();
        private ConnectionInterfaceInfo? selectedInfo = null;

        /// <summary>
        /// Доступные стандратные соединения
        /// </summary>
        public ObservableCollection<ConnectionInterfaceInfo> ConnectionInfos => connectionInfos;

        public ConnectionInterfaceModelView()
        {
            InitList();
        }

        /// <summary>
        /// Заполнение стандратных соединений
        /// </summary>
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
            connectionInfos.Add(new ConnectionInterfaceInfo("PCIe 1.0x16", PCIe.PCEe1dot0x16, new PCIExpressViewModel()));
            connectionInfos.Add(new ConnectionInterfaceInfo("PCIe 2.0x4", PCIe.PCIe2dot0x4, new PCIExpressViewModel()));
            connectionInfos.Add(new ConnectionInterfaceInfo("PCIe 3.0x8", new PCIe(8, 8000, 1, EncodingType._128On130b), new PCIExpressViewModel()));
            connectionInfos.Add(new ConnectionInterfaceInfo("PCIe 2.0x16", new PCIe(16, 5000, 1, EncodingType._8bOn10b), new PCIExpressViewModel()));
            connectionInfos.Add(new ConnectionInterfaceInfo("PCIe 3.0x4", new PCIe(4, 8000, 1, EncodingType._128On130b), new PCIExpressViewModel()));
            connectionInfos.Add(new ConnectionInterfaceInfo("PCIe 4.0x4", new PCIe(4, 16000, 1, EncodingType._128On130b), new PCIExpressViewModel()));
            connectionInfos.Add(new ConnectionInterfaceInfo("PCIe 4.0x8", PCIe.PCIe4dot0x8, new PCIExpressViewModel()));
            connectionInfos.Add(new ConnectionInterfaceInfo("PCIe 6.0x16", PCIe.PCIe6dot0x16, new PCIExpressViewModel()));
            #endregion

            SelectedInterface = ConnectionInfos[0];
        }

        /// <summary>
        /// Выбранный интерфейс соединения
        /// </summary>
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
}
