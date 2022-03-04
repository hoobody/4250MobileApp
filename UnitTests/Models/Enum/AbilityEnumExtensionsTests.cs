using NUnit.Framework;

using Game.Models;

namespace UnitTests.Models
{
    [TestFixture]
    public class AbilityEnumExtensionsTests
    {
        [Test]
        public void AbilityEnumExtensionsTests_Unknown_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AbilityEnum.Unknown.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("None", result);
        }

        [Test]
        public void AbilityEnumExtensionsTests_Bandage_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AbilityEnum.Bandage.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Apply Bandages", result);
        }

        [Test]
        public void AbilityEnumExtensionsTests_Barrier_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AbilityEnum.Barrier.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Barrier Defense", result);
        }

        [Test]
        public void AbilityEnumExtensionsTests_Curse_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AbilityEnum.Curse.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Shout Curse", result);
        }

        [Test]
        public void AbilityEnumExtensionsTests_Focus_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AbilityEnum.Focus.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Mental Focus", result);
        }

        [Test]
        public void AbilityEnumExtensionsTests_Heal_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AbilityEnum.Heal.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Heal Self", result);
        }

        [Test]
        public void AbilityEnumExtensionsTests_Nimble_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AbilityEnum.Nimble.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("React Quickly", result);
        }

        [Test]
        public void AbilityEnumExtensionsTests_Quick_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AbilityEnum.Quick.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Anticipate", result);
        }

        [Test]
        public void AbilityEnumExtensionsTests_Toughness_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AbilityEnum.Toughness.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Toughen Up", result);
        }


        [Test]
        public void AbilityEnumExtensionsTests_Assassin_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AbilityEnum.Assassin.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Large Attack Buff, minor Defense and Speed buff", result);
        }

        [Test]
        public void AbilityEnumExtensionsTests_Spy_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AbilityEnum.Spy.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Minor attack and Speed buff", result);
        }

        //////////////////////////////////////////////////////////////////////
        ///

        [Test]
        public void AbilityEnumExtensionsTests_Hacker_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AbilityEnum.Hacker.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Attack debuff, large defense buff, minor speed buff", result);
        }

        [Test]
        public void AbilityEnumExtensionsTests_SpecialAgent_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AbilityEnum.SpecialAgent.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Minor buffs to attack, defense, and speed", result);
        }

        [Test]
        public void AbilityEnumExtensionsTests_Sabateur_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AbilityEnum.Saboteur.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Attack debuff, minor defense and speed buffs", result);
        }

        [Test]
        public void AbilityEnumExtensionsTests_SurvOfficer_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AbilityEnum.SurveillanceOfficer.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Major speed buff, minor attack and defense buffs", result);
        }

        [Test]
        public void AbilityEnumExtensionsTests_Detective_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AbilityEnum.Detective.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Small attack buff, moderate defense buff, speed debuff", result);
        }

        [Test]
        public void AbilityEnumExtensionsTests_Double0_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = AbilityEnum.Double0.ToMessage();

            // Reset

            // Assert
            Assert.AreEqual("Buffs to attack, debuff to defense, and minor buff to speed", result);
        }


    }
}
