using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideocartLab.Models.ConnectionInterface;
using VideocartLab.Models.GPUMemory;

namespace VideocartLab.NUnitTest
{
    [TestFixture]
    public class ConnectionInterfaceTests
    {
        [Test]
        public void TestPCIe6x16Bandwidth()
        {
            double res = 121;

            PCIe val = PCIe.PCIe6dot0x16;

            Assert.AreEqual(val.Bandwidth, res);
        }

        [Test]
        public void testPCIe1x1Bandwidth2()
        {
            PCIe val = new PCIe();

            double res = 0.25d;

            Assert.AreEqual(val.Bandwidth, res);
        }

        [Test]
        public void testPCIe1x16Bandwidth3()
        {
            PCIe val = new PCIe();

            val.Lines = 16;

            double res = 4d;

            Assert.AreEqual(val.Bandwidth, res);
        }

        [Test]
        public void TestPCIeFrequencyChange()
        {
            PCIe val = new PCIe()
            {
                Frequency = 5000
            };

            double res = 0.25d * 2d;//5d;

            Assert.AreEqual(val.Bandwidth, res);
        }

        [Test]
        public void TestUserEncodingType()
        {
            PCIe val = new PCIe();

            val.Type = EncodingType.Another;

            val.UserEncodingType = 5d / 10d;

            double res = 1d * 2500d * 5d/10d * 1d / 8d / 1000d ;

            Assert.AreEqual(val.Bandwidth, res);
        }

        [Test]
        public void TestIgnoreUsersEncodingType()
        {
            PCIe val = new PCIe();

            //val.Type = EncodingType.Another;

            val.UserEncodingType = 5d / 10d;

            double res = 0.25d;

            Assert.AreEqual(val.Bandwidth, res);
        }
    }
}
