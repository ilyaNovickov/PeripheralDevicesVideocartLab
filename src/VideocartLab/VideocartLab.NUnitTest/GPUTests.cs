using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideocartLab.Models;

namespace VideocartLab.NUnitTest
{
    [TestFixture]
    public class GPUTests
    {
        [Test]
        public void TestValidUnnegativeInt()
        { 
            Assert.DoesNotThrow(() => ValuesValidator.ValidUnnegativeArgument(10));
        }

        [Test]
        public void TestValidNegativeInt()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => ValuesValidator.ValidUnnegativeArgument(-10));
        }

        [Test]
        public void TestPFR()
        {
            int result = 16;

            GPU gpu = new GPU()
            {
                Frequency = 1000,
                RenderOutputPipelines = 16
            };

            Assert.AreEqual(result, gpu.PixelFillRate);
        }

        [Test]
        public void TestTFR()
        {
            int result = 12;

            GPU gpu = new GPU()
            {
                Frequency = 1000,
                TextureMappingUnits = 12
            };

            Assert.AreEqual(result, gpu.TextureFillRate);
        }
    }
}
