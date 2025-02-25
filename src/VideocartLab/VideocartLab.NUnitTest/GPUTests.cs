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
    }
}
