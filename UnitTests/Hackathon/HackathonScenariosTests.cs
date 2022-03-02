using NUnit.Framework;

using Game.Models;
using Game;
using System.Threading.Tasks;
using Game.ViewModels;
using UnitTests.TestHelpers;
using Game.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Mocks;
using Game.Engine.EngineGame;

namespace Scenario
{
    [TestFixture]
    public class HackathonScenarioTests
    {
        #region TestSetup
        readonly BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

        BattleEngine Engine;

        [SetUp]
        public void Setup()
        {
            Engine = new();

            Engine.Round = new RoundEngine
            {
                Turn = new TurnEngine()
            };
            _ = Engine.Round.ClearLists();

            // Put seed data into the system for all tests
            _ = BattleEngineViewModel.Instance.Engine.Round.ClearLists();

            //Start the Engine in AutoBattle Mode
            _ = BattleEngineViewModel.Instance.Engine.StartBattle(false);

            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.CharacterHitEnum = HitStatusEnum.Default;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;

            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.AllowCriticalHit = false;
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.AllowCriticalMiss = false;
        }

        [TearDown]
        public void TearDown()
        {
        }
        #endregion TestSetup

        #region Scenario0
        [Test]
        public void HakathonScenario_Scenario_0_Valid_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      #
            *      
            * Description: 
            *      <Describe the scenario>
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      <List Files Changed>
            *      <List Classes Changed>
            *      <List Methods Changed>
            * 
            * Test Algrorithm:
            *      <Step by step how to validate this change>
            * 
            * Test Conditions:
            *      <List the different test conditions to make>
            * 
            * Validation:
            *      <List how to validate this change>
            *  
            */

            // Arrange

            // Act

            // Assert


            // Act
            var result = EngineViewModel;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }
        #endregion Scenario0

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

            EngineViewModel.Engine.EngineSettings.MaxNumberPartyCharacters = 1;

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

        #region Scenario2
        [Test]
        public async Task HackathonScenario_Scenario_2_Valid_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      2
            *      
            * Description: 
            *      Make a Character called Doug, who misses every attack
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      No Code changes requied 
            * 
            * Test Algrorithm:
            *      Create Character named Douge
            *      Set speed to -1 so he is really slow
            *      Set Current Health to 20 so he so he survives some hits, but still dies first round
            *  
            *      Startup Battle
            *      Run Auto Battle
            * 
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *      Verify Doug is not in the Player List
            *      Verify Turn Count is 1
            *  
            */

            //Arrange

            // Set Character Conditions

            EngineViewModel.Engine.EngineSettings.MaxNumberPartyCharacters = 1;

            var CharacterPlayerDoug = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = -1, // Will go last...
                                Level = 1,
                                CurrentHealth = 10,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Doug",
                            });

            EngineViewModel.Engine.EngineSettings.CharacterList.Add(CharacterPlayerDoug);

            // Set Monster Conditions

            // Auto Battle will add the monsters

            // Monsters always hit
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Hit;

            //Act
            var result = await EngineViewModel.AutoBattleEngine.RunAutoBattle();

            //Reset
            EngineViewModel.Engine.EngineSettings.BattleSettingsModel.MonsterHitEnum = HitStatusEnum.Default;

            //Assert
            Assert.AreEqual(null, EngineViewModel.Engine.EngineSettings.PlayerList.Find(m => m.Name.Equals("Doug")));
            Assert.AreEqual(1, EngineViewModel.Engine.EngineSettings.BattleScore.RoundCount);
        }
        #endregion Scenario2
        #region Scenario9
        [Test]
        public void HackathonScenario_Scenario_9_Valid_Default_Should_Pass()
        {
            /* 
            * Scenario Number:  
            *      9
            *      
            * Description: 
            *       Just in time delivery for better loot at the end of rounds
            * 
            * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
            *      RoundOverPage.xaml: Added a button to do the delivery
            *      RoundOverPage.xaml.cs:
            *       - MomAndPopShopInstantDelivery_Clicked(): Gets up to six extra items for the characters to select from directly from the HTTPS service
            *      
            * 
            * Test Algrorithm:
            *      Create app
            *      Create RoundOver page
            *      set up a mock http connection
            *      prepare the battle engine
            *      start the engine in autobattle mode
            *      made a new character
            *      Add them to the character list
            *      count the items in the pool
            *      click the delivery button
            *      count the items again
            *      reset the connection
            *      clean up the character list
            *      clear the app
            * 
            * Test Conditions:
            *      Default condition is sufficient
            * 
            * Validation:
            *      Verify the click worked by counting the number of items retrieved
            *  
            */

            //Arrange

            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            App app = new App();
            Application.Current = app;

            RoundOverPage page = new RoundOverPage();

            _ = TestBaseHelper.SetHttpClientToMock();
            ResponseMessage.SetHttpStatusCode(ResponseMessage.HttpStatusCodeSuccess);
            ResponseMessage.SetResponseMessageStringContent(JsonSampleData.StringContentItemPostDefault);

            _ = BattleEngineViewModel.Instance.Engine.Round.ClearLists();

            //Start the Engine in AutoBattle Mode
            _ = BattleEngineViewModel.Instance.Engine.StartBattle(false);

            var CharacterPlayer = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = -1, // Will go last...
                                Level = 1,
                                CurrentHealth = 1,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Jesse",
                                
                            });

            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Add(CharacterPlayer);

            var beforeItemCount = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList.Count;

            //Act
            page.MomAndPopShopInstantDelivery_Clicked(null, null);

            var afterItemCount = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore.ItemModelDropList.Count;

            //Reset
            ResponseMessage.SetResponseMessageStringContent(ResponseMessage.NullStringContent);
            _ = TestBaseHelper.SetHttpClientToReal();

            BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Remove(CharacterPlayer);

            Application.Current = null;

            // Assert
            Assert.IsTrue(afterItemCount > beforeItemCount); //We managed to get the items
        }
        #endregion Scenario1

        #region Scenario 14
        [Test]
        public void RoundEngine_AddMonstersToRound_Valid_Default_Should_Pass()
        {
            /* 
           * Scenario Number:  
           *      14
           *      
           * Description: 
           *      Every 5th round, the boss would spawn instead of the usual 6 monsters
           * 
           * Changes Required (Classes, Methods etc.)  List Files, Methods, and Describe Changes: 
           *      Added boss class and methods for them to be different from the regular monsters
           * 
           * Test Algrorithm:
           *      Start up to round 5
           *      On 5th round the boss would spawn
           *  
           *      Startup Battle
           *      Run Auto Battle
           * 
           * Test Conditions:
           *      Default condition is sufficient
           * 
           * Validation:
           *      Verify Monster Count is 1
           *  
           */

            // Arrange
            for (int i = 0; i < 5; i++)
            {
                Engine.Round.NewRound();
            }

            Engine.EngineSettings.MonsterList.Clear();
            // Act
            var result = Engine.Round.AddMonstersToRound();
            // Reset

            // Assert
            Assert.AreEqual(1, result);
        }
        #endregion Scenario 14

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
    }
}