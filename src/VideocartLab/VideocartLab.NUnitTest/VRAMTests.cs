using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using VideocartLab.Models.GPUMemory;

namespace VideocartLab.NUnitTest
{
    [TestFixture]
    public class VRAMTests
    {
        private VRAM vram;

        [SetUp] // Выполняется перед каждым тестом
        public void Setup()
        {
            
        }

        [Test] // Обычный тестовый метод
        public void IsEffectiveFrequencyChangeRealRight()
        {
            double right = (10010 * GDDRType.GDDR5X.RealRatio);

            VRAM vRAM = new VRAM();

            vRAM.Type = GDDRType.GDDR5X;

            vRAM.EffectiveFrequency = 10010;


            Assert.AreEqual(right, vRAM.RealFrequency);
        }

        [Test]
        public void IsRealFrequencyChangedEfective()
        {
            int right = 10010;

            VRAM vRAM = new VRAM();

            vRAM.Type = GDDRType.GDDR5X;

            vRAM.RealFrequency = (10010 * GDDRType.GDDR5X.RealRatio);

            Assert.AreEqual(right, vRAM.EffectiveFrequency);
        }

        [Test]
        public void IsGDDRTypeChangedFrequency()
        {
            int right = 10010;

            VRAM vRAM = new VRAM();

            vRAM.RealFrequency = 10010 * GDDRType.GDDR5X.RealRatio;

            vRAM.Type = GDDRType.GDDR5X;

            Assert.AreEqual(right, vRAM.EffectiveFrequency);
        }

        [Test]
        public void ChangeGDDRRation()
        {
            GDDRType type = new(GDDRTypes.GDDR);

            type.Type = GDDRTypes.GDDR6X;

            Assert.AreEqual(16, type.EffectiveRatio);
        }

        [Test]
        public void Test1080Bandwidth()
        {
            VRAM ram = new VRAM()
            {
                MemoryBusCapacity = 256,
                EffectiveFrequency = 10000
            };

            double res = 320;

            Assert.AreEqual(res, ram.MemoryBandwidth);
        }

        [Test]
        public void TestChangeEffectiveFreq()
        {
            VRAM ram = new VRAM();

            ram.EffectiveFrequency = 2000;

            ram.Type = GDDRType.GDDR4;

            double res = 2000d * 2d;

            Assert.AreEqual(res, ram.EffectiveFrequency);
        }

        [Test]
        public void TestChangeEffectiveFreq2()
        {
            VRAM ram = new VRAM();

            ram.Type = GDDRType.GDDR4;

            ram.EffectiveFrequency = 2000;

            double res = 2000d / 2d;

            Assert.AreEqual(res, ram.RealFrequency);
        }
    }
}