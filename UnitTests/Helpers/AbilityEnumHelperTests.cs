using System;
using System.Linq;

using NUnit.Framework;

using Game.Models;

namespace UnitTests.Helpers
{
    [TestFixture]
    class AbilityEnumHelperTests
    {
        [Test]
        public void AbilityEnumHelper_GetFullList_Should_Pass()
        {
            // Arrange

            // Act
            var result = AbilityEnumHelper.GetFullList;

            // Assert
            Assert.AreEqual(18, result.Count);

            // Assert
        }

        [Test]
        public void AbilityEnumHelper_GetListFighter_Should_Pass()
        {
            // Arrange

            // Act
            var result = AbilityEnumHelper.GetListFighter;

            // Assert
            Assert.AreEqual(4, result.Count);
        }

        [Test]
        public void AbilityEnumHelper_GetListCleric_Should_Pass()
        {
            // Arrange

            // Act
            var result = AbilityEnumHelper.GetListCleric;

            // Assert
            Assert.AreEqual(5, result.Count);
        }

        [Test]
        public void AbilityEnumHelper_GetListAssassin_Should_Pass()
        {
            // Arrange

            // Act
            var result = AbilityEnumHelper.GetListCleric;

            // Assert
            Assert.AreEqual(5, result.Count);
        }

        [Test]
        public void AbilityEnumHelper_GetListDetective_Should_Pass()
        {
            // Arrange

            // Act
            var result = AbilityEnumHelper.GetListDetective;

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void AbilityEnumHelper_GetListDouble0_Should_Pass()
        {
            // Arrange

            // Act
            var result = AbilityEnumHelper.GetListDouble0;

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void AbilityEnumHelper_GetListHacker_Should_Pass()
        {
            // Arrange

            // Act
            var result = AbilityEnumHelper.GetListHacker;

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void AbilityEnumHelper_GetListSabateur_Should_Pass()
        {
            // Arrange

            // Act
            var result = AbilityEnumHelper.GetListSaboteur;

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void AbilityEnumHelper_GetListSpecialAgent_Should_Pass()
        {
            // Arrange

            // Act
            var result = AbilityEnumHelper.GetListSpecialAgent;

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void AbilityEnumHelper_GetListSpy_Should_Pass()
        {
            // Arrange

            // Act
            var result = AbilityEnumHelper.GetListSpy;

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void AbilityEnumHelper_GetListSurvOfficer_Should_Pass()
        {
            // Arrange

            // Act
            var result = AbilityEnumHelper.GetListSurveillanceOfficer;

            // Assert
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void AbilityEnumHelper_GetListOthers_Should_Pass()
        {
            // Arrange

            // Act
            var result = AbilityEnumHelper.GetListOthers;

            // Assert
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void AbilityEnumHelper_ConvertStringToEnum_Should_Pass()
        {
            // Arrange

            var myList = Enum.GetNames(typeof(AbilityEnum)).ToList();

            AbilityEnum myActual;
            AbilityEnum myExpected;

            // Act

            foreach (var item in myList)
            {
                myActual = AbilityEnumHelper.ConvertStringToEnum(item);
                myExpected = (AbilityEnum)Enum.Parse(typeof(AbilityEnum), item);

                // Assert
                Assert.AreEqual(myExpected, myActual, "string: " + item + TestContext.CurrentContext.Test.Name);
            }
        }
    }
}

