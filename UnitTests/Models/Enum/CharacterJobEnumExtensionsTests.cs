using NUnit.Framework;

using Game.Models;

namespace UnitTests.Models
{
    [TestFixture]
    public class CharacterJobEnumExtensionsTests
    {
        [Test]
        public void CharacterJobEnumExtensionsTests_Unknown_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterJobEnum.Unknown.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Player", result);
        }

        [Test]
        public void CharacterJobEnumExtensionsTests_Fighter_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterJobEnum.Fighter.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Fighter", result);
        }

        [Test]
        public void CharacterJobEnumExtensionsTests_Cleric_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterJobEnum.Cleric.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Cleric", result);
        }

        [Test]
        public void CharacterJobEnumExtensionsTests_Assassin_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterJobEnum.Assassin.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Assassin", result);
        }

        [Test]
        public void CharacterJobEnumExtensionsTests_Spy_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterJobEnum.Spy.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Spy", result);
        }

        [Test]
        public void CharacterJobEnumExtensionsTests_Hacker_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterJobEnum.Hacker.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Hacker", result);
        }

        [Test]
        public void CharacterJobEnumExtensionsTests_SpecialAgent_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = CharacterJobEnum.SpecialAgent.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("SpecialAgent", result);
        }
    }
}
