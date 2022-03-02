using System.Linq;
using System.Threading.Tasks;

using NUnit.Framework;

using Game.Engine.EngineGame;
using Game.Models;
using Game.ViewModels;

namespace UnitTests.Engine.EngineGame
{
    [TestFixture]
    public class RoundEngineGameTests
    {
        #region TestSetup
        BattleEngine Engine;

        readonly BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

        [SetUp]
        public void Setup()
        {
            Engine = new();

            Engine.Round = new RoundEngine
            {
                Turn = new TurnEngine()
            };
            _ = Engine.Round.ClearLists();

            //Start the Engine in AutoBattle Mode
            //Engine.StartBattle(true);   
        }

        [TearDown]
        public void TearDown()
        {
        }
        #endregion TestSetup

        #region Constructor
        [Test]
        public void RoundEngine_Constructor_Valid_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }
        #endregion Constructor

        #region OrderPlayListByTurnOrder
        //[Test]
        //public void RoundEngine_OrderPlayerListByTurnOrder_Valid_Speed_Higher_Should_Be_Z()
        //{
        //    // Arrange

        //    // Act
        //    var result = Engine.Round.OrderPlayerListByTurnOrder();

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(null, result);
        //}

        //[Test]
        //public void RoundEngine_OrderPlayerListByTurnOrder_Valid_Level_Higher_Should_Be_Z()
        //{
        //    // Arrange

        //    // Act
        //    var result = Engine.Round.OrderPlayerListByTurnOrder();

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(null, result);
        //}

        //[Test]
        //public void RoundEngine_OrderPlayerListByTurnOrder_Valid_Experience_Higher_Should_Be_Z()
        //{
        //    // Arrange

        //    // Act
        //    var result = Engine.Round.OrderPlayerListByTurnOrder();

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(null, result);
        //}

        //[Test]
        //public void RoundEngine_OrderPlayerListByTurnOrder_Valid_ListOrder_Should_Be_1()
        //{
        //    // Arrange

        //    // Act
        //    var result = Engine.Round.OrderPlayerListByTurnOrder();

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(null, result);
        //}

        //[Test]
        //public void RoundEngine_OrderPlayerListByTurnOrder_Valid_Name_A_Z_Should_Be_Z()
        //{
        //    // Arrange

        //    // Act
        //    var result = Engine.Round.OrderPlayerListByTurnOrder();

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(null, result);
        //}
        #endregion OrderPlayListByTurnOrder

        #region GetItemFromPoolIfBetter

        //[Test]
        //public async Task RoundEngine_GetItemFromPoolIfBetter_InValid_Pool_Empty_Should_Fail()
        //{
        //    Engine.EngineSettings.MonsterList.Clear();

        //    // Both need to be character to fall through to the Name Test
        //    // Arrange
        //    var Character = new CharacterModel
        //    {
        //        Speed = 20,
        //        Level = 1,
        //        CurrentHealth = 1,
        //        ExperienceTotal = 1,
        //        Name = "Z",
        //        ListOrder = 1,
        //        Guid = "me"
        //    };

        //    // Add each model here to warm up and load it.
        //    _ = Game.Helpers.DataSetsHelper.WarmUp();

        //    var item1 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 1, Location = ItemLocationEnum.Head };
        //    var item2 = new ItemModel { Attribute = AttributeEnum.Attack, Value = 20, Location = ItemLocationEnum.Head };

        //    _ = await ItemIndexViewModel.Instance.CreateAsync(item1);
        //    _ = await ItemIndexViewModel.Instance.CreateAsync(item2);

        //    //Engine.EngineSettings.ItemPool.Add(item1);
        //    //Engine.EngineSettings.ItemPool.Add(item2);

        //    // Put the Item on the Character
        //    _ = Character.AddItem(ItemLocationEnum.Head, item2.Id);

        //    var CharacterPlayer = new PlayerInfoModel(Character);
        //    Engine.EngineSettings.CharacterList.Clear();
        //    Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(Character));

        //    // Make the List
        //    Engine.EngineSettings.PlayerList = Engine.Round.MakePlayerList();

        //    // Act
        //    var result = Engine.Round.GetItemFromPoolIfBetter(CharacterPlayer, ItemLocationEnum.Head);

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(false, result);
        //}

        #endregion GetItemFromPoolIfBetter

        #region PickupItemsFromPool
        //[Test]
        //public void RoundEngine_PickupItemsFromPool_Valid_Default_Should_Pass()
        //{
        //    // Arrange
        //    var Character = new CharacterModel
        //    {
        //        Speed = 20,
        //        Level = 1,
        //        CurrentHealth = 1,
        //        ExperienceTotal = 1,
        //        Name = "Z",
        //        ListOrder = 1,
        //        Guid = "me"
        //    };

        //    // Add each model here to warm up and load it.
        //    _ = Game.Helpers.DataSetsHelper.WarmUp();

        //    var CharacterPlayer = new PlayerInfoModel(Character);
        //    Engine.EngineSettings.CharacterList.Clear();
        //    Engine.EngineSettings.CharacterList.Add(new PlayerInfoModel(Character));

        //    // Make the List
        //    Engine.EngineSettings.PlayerList = Engine.Round.MakePlayerList();

        //    // Act
        //    var result = Engine.Round.PickupItemsFromPool(CharacterPlayer);

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(false, result);
        //}
        #endregion PickupItemsFromPool

        #region EndRound
        [Test]
        public void RoundEngine_EndRound_Valid_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine.Round.EndRound();

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }
        #endregion EndRound

        #region RoundNextTurn
        //[Test]
        //public void RoundEngine_RoundNextTurn_Valid_No_Characters_Should_Return_GameOver()
        //{
        //    // Arrange

        //    // Act
        //    var result = Engine.Round.RoundNextTurn();

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(RoundEnum.Unknown, result);
        //}

        //[Test]
        //public void RoundEngine_RoundNextTurn_Valid_No_Monsters_Should_Return_NewRound()
        //{
        //    // Arrange

        //    // Act
        //    var result = Engine.Round.RoundNextTurn();

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(RoundEnum.Unknown, result);
        //}

        //[Test]
        //public void RoundEngine_RoundNextTurn_Valid_Characters_Monsters_Should_Return_NewRound()
        //{
        //    // Arrange

        //    // Act
        //    var result = Engine.Round.RoundNextTurn();

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(RoundEnum.Unknown, result);
        //}
        #endregion RoundNextTurn

        #region GetNextPlayerInList

        //[Test]
        //public void RoundEngine_GetNextPlayerInList_Valid_Sue_Should_Return_Monster()
        //{
        //    // Arrange

        //    // Act
        //    var result = Engine.Round.GetNextPlayerInList();

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(null, result);
        //}

        //[Test]
        //public void RoundEngine_GetNextPlayerInList_Valid_Monster_Should_Return_Mike()
        //{
        //    // Arrange

        //    // Act
        //    var result = Engine.Round.GetNextPlayerInList();

        //    // Reset


        //    // Assert
        //    Assert.AreEqual(null, result);
        //}

        #endregion GetNextPlayerInList

        #region PlayerList
        //[Test]
        //public void RoundEngine_PlayerList_Valid_Default_Should_Pass()
        //{
        //    // Act
        //    var result = Engine.Round.PlayerList();

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(false, result.Any());
        //}
        #endregion PlayerList

        #region SwapCharacterItem
        //[Test]
        //public void RoundEngine_SwapCharacterItem_Valid_Default_Should_Pass()
        //{
        //    // Arrange 

        //    // Act
        //    var result = Engine.Round.SwapCharacterItem(null, ItemLocationEnum.Head, null);

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(null, result);
        //}
        #endregion SwapCharacterItem

        #region GetItemFromPoolIfBetter
        [Test]
        public void RoundEngine_GetItemFromPoolIfBetter_Valid_Default_Should_Pass()
        {
            // Arrange 

            // Act
            var result = Engine.Round.GetItemFromPoolIfBetter(null, ItemLocationEnum.Head);

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }
        #endregion GetItemFromPoolIfBetter

        #region RemoveDeadPlayersFromList
        //[Test]
        //public void RoundEngine_RemoveDeadPlayersFromList_Valid_Default_Should_Pass()
        //{
        //    // Arrange 

        //    // Act
        //    var result = Engine.Round.RemoveDeadPlayersFromList();

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(null, result);
        //}
        #endregion RemoveDeadPlayersFromList

        #region PickupItemsFromPool
        //[Test]
        //public void RoundEngine_PickupItemsFromPool_Valid_Default_Should_Pass()
        //{
        //    // Arrange 

        //    // Act
        //    var result = Engine.Round.PickupItemsFromPool(null);

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(false, result);
        //}
        #endregion PickupItemsFromPool

        #region GetNextPlayerTurn
        //[Test]
        //public void RoundEngine_GetNextPlayerTurn_Valid_Default_Should_Pass()
        //{
        //    // Arrange 

        //    // Act
        //    var result = Engine.Round.GetNextPlayerTurn();

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(null, result);
        //}
        #endregion GetNextPlayerTurn

        #region HackathonUnitTests
        #region Scenario1
        [Test]
        public async Task HackathonScenario_Scenario_1_Valid_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      1
            *      
            * Description: 
            *      Make a Character called Mike, who dies in the first round
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      No Code changes requied 
            * 
            * Test Algrorithm:
            *      Create Character named Mike
            *      Set speed to -1 so he is really slow
            *      Set Max health to 1 so he is weak
            *      Set Current Health to 1 so he is weak
            *  
            *      Startup Battle
            *      Run Auto Battle
            * 
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *      Verify Battle Returned True
            *      Verify Mike is not in the Player List
            *      Verify Round Count is 1
            *  
            */

            //Arrange

            // Set Character Conditions

            Engine.EngineSettings.MaxNumberPartyCharacters = 1;

            var CharacterPlayerMike = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = -1, // Will go last...
                                Level = 1,
                                CurrentHealth = 1,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Mike",
                            });

            EngineViewModel.Engine.EngineSettings.CharacterList.Add(CharacterPlayerMike);

            // Set Monster Conditions

            // Auto Battle will add the monsters

            // Monsters always hit
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Hit;

            //Act
            var result = await EngineViewModel.AutoBattleEngine.RunAutoBattle();

            //Reset
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;

            //Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(null, EngineViewModel.Engine.EngineSettings.PlayerList.Find(m => m.Name.Equals("Mike")));
            Assert.AreEqual(1, EngineViewModel.Engine.EngineSettings.BattleScore.RoundCount);
        }
        #endregion Scenario1
        #region Scenario 4
        /* 
        * Scenario Number:  
        *      4
        *      
        * Description: 
        *      Added the possibility to critically hit
        * 
        * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
        *      None
        * 
        * Test Algrorithm:
        *      Set up results with each possibilities when attacking
        *      
        *  
        *      Startup Battle
        *      Run Auto Battle
        * 
        * Test Conditions:
        *      Default condition is sufficient
        * 
        * Validation:
        *      Verify Result matches with the Attack results
        *  
        */

        [Test]
                public void TurnEngine_BattleSettingsOverrideHitStatusEnum_Valid_Hit_Should_Pass()
                {
                    // Arrange

                    // Act
                    var result = Engine.Round.Turn.BattleSettingsOverrideHitStatusEnum(HitStatusEnum.Hit);

                    // Reset

                    // Assert
                    Assert.AreEqual(HitStatusEnum.Hit, result);
                }

                [Test]
                public void TurnEngine_BattleSettingsOverrideCriticalHitStatusEnum_Valid_CriticalHit_Should_Pass()
                {
                    // Arrange

                    // Act
                    var result = Engine.Round.Turn.BattleSettingsOverrideHitStatusEnum(HitStatusEnum.CriticalHit);

                    // Reset

                    // Assert
                    Assert.AreEqual(HitStatusEnum.CriticalHit, result);
                }

                [Test]
                public void TurnEngine_BattleSettingsOverrideCriticalMissStatusEnum_Valid_CriticalMiss_Should_Pass()
                {
                    // Arrange

                    // Act
                    var result = Engine.Round.Turn.BattleSettingsOverrideHitStatusEnum(HitStatusEnum.CriticalMiss);

                    // Reset

                    // Assert
                    Assert.AreEqual(HitStatusEnum.CriticalMiss, result);
                }

                [Test]
                public void TurnEngine_BattleSettingsOverrideMissStatusEnum_Valid_Miss_Should_Pass()
                {
                    // Arrange

                    // Act
                    var result = Engine.Round.Turn.BattleSettingsOverrideHitStatusEnum(HitStatusEnum.Miss);

                    // Reset

                    // Assert
                    Assert.AreEqual(HitStatusEnum.Miss, result);
                }
            #endregion Scenario 4
        #endregion HackathonUnitTests
    }
}