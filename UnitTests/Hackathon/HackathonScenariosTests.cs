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




namespace Scenario
{
    [TestFixture]
    public class HackathonScenarioTests
    {
        #region TestSetup
        readonly BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

        [SetUp]
        public void Setup()
        {
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


    }
}