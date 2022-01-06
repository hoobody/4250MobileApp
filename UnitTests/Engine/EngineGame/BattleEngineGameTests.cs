using NUnit.Framework;

using Game.Engine.EngineGame;
using Game.Models;
using Game.Engine.EngineModels;

namespace UnitTests.Engine.EngineGame
{
    [TestFixture]
    public class BattleEngineGameTests
    {
        #region TestSetup
        BattleEngine Engine;

        [SetUp]
        public void Setup()
        {
            Engine = new BattleEngine();
            Engine.Round = new RoundEngine();
            Engine.Round.Turn = new TurnEngine();
        }

        [TearDown]
        public void TearDown()
        {
        }
        #endregion TestSetup

        #region Constructor
        [Test]
        public void BattleEngine_Constructor_Valid_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        #endregion Constructor

        #region EngineSettings
        [Test]
        public void BattleEngine_EngineSettings_Valid_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine.EngineSettings = EngineSettingsModel.Instance;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }
        #endregion EngineSettings

        #region StartBattle
        [Test]
        public void BattleEngine_StartBattle_Valid_AutoModel_True_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine.StartBattle(true);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(true, Engine.EngineSettings.BattleScore.AutoBattle);
        }
        #endregion StartBattle

        #region EndBattle
        [Test]
        public void BattleEngine_EndBattle_Valid_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine.EndBattle();

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }
        #endregion EndBattle

        #region PopulateCharacterList
        [Test]
        public void BattleEngine_Valid_PopulateCharacterList_Should_Pass()
        {
            // Arrange
            var character = new CharacterModel();

            // Act
            var result = Engine.PopulateCharacterList(character);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }
        #endregion PopulateCharacterList

        #region SetBattleStateEnum
        [Test]
        public void BattleEngine_SetBattleStateEnum_Valid_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine.SetBattleStateEnum(BattleStateEnum.Battling);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }
        #endregion SetBattleStateEnum
    }
}