using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using VideocartLab.Models;

namespace VideocartLab.NUnitTest
{

    public class Tests
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

        /*
        [Test]
        public void IsPalindrome_ShouldReturnFalse_ForNonPalindrome()
        {
            // Arrange
            var input = "hello";

            // Act
            var result = _stringUtilities.IsPalindrome(input);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsPalindrome_ShouldReturnFalse_ForEmptyString()
        {
            // Arrange
            var input = "";

            // Act
            var result = _stringUtilities.IsPalindrome(input);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsPalindrome_ShouldReturnFalse_ForNull()
        {
            // Arrange
            string input = null;

            // Act
            var result = _stringUtilities.IsPalindrome(input);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsPalindrome_ShouldIgnoreCase()
        {
            // Arrange
            var input = "Level";

            // Act
            var result = _stringUtilities.IsPalindrome(input);

            // Assert
            Assert.IsTrue(result);
        }

        [TestCase("madam", true)] // Тестовый случай
        [TestCase("step on no pets", false)] // Не обрабатываем пробелы
        [TestCase("racecar", true)]
        [TestCase("12321", true)]
        [TestCase("12345", false)]
        public void IsPalindrome_ShouldHandleMultipleCases(string input, bool expected)
        {
            // Act
            var result = _stringUtilities.IsPalindrome(input);

            // Assert
            Assert.AreEqual(expected, result);
        }
        */
    }
}