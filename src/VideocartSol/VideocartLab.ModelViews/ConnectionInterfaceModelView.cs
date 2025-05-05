using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            connectionInfos.Add(new ConnectionInterfaceInfo("PCI 32bit/33 MHz", PCI.PCI32bit33MHz));
            connectionInfos.Add(new ConnectionInterfaceInfo("PCI 32bit/66 MHz", PCI.PCI64bit33Mhz));
            connectionInfos.Add(new ConnectionInterfaceInfo("PCI 64bit/33 MHz", PCI.PCI64bit33Mhz));
            connectionInfos.Add(new ConnectionInterfaceInfo("PCI 64bit/66 MHz", PCI.PCI64bit66Mhz));

            connectionInfos.Add(new ConnectionInterfaceInfo("AGP 1.0x1", AGP.AGP1x1));
            connectionInfos.Add(new ConnectionInterfaceInfo("AGP 1.0x2", AGP.AGP1x2));
            connectionInfos.Add(new ConnectionInterfaceInfo("AGP 2.0", AGP.AGP2));
            connectionInfos.Add(new ConnectionInterfaceInfo("AGP 3.0", AGP.AGP3));

            connectionInfos.Add(new ConnectionInterfaceInfo("PCIe 1.0x1", new PCIe()));
            connectionInfos.Add(new ConnectionInterfaceInfo("PCIe 2.0x4", PCIe.PCIe2dot0x4));
            connectionInfos.Add(new ConnectionInterfaceInfo("PCIe 4.0x8", PCIe.PCIe4dot0x8));
            connectionInfos.Add(new ConnectionInterfaceInfo("PCIe 6.0x16", PCIe.PCIe6dot0x16));
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

        internal ConnectionInterfaceInfo(string name, ConnectionInterface @interface)
        {
            Name = name;
            Interface = @interface;
        }
    }
}
