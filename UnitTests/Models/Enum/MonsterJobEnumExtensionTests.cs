using NUnit.Framework;

using Game.Models;

namespace UnitTests.Models
{
    [TestFixture]
    public class MonsterJobEnumExtensionTest
    {
        [Test]
        public void MonsterJobEnumExtensionsTests_Unknown_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = MonsterJobEnum.Unknown.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Player", result);
        }

        [Test]
        public void MonsterJobEnumExtensionsTests_Fighter_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = MonsterJobEnum.Bodyguard.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Bodyguard", result);
        }

        [Test]
        public void MonsterJobEnumExtensionsTests_Cleric_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = MonsterJobEnum.Mercenary.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Mercenary", result);
        }
    }
}
