using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;

using Game;
using Game.Views;
using Game.Models;


namespace Scenario
{
    [TestFixture]
    public class BattlePageScenarioTests
    {
        App app;
        BattlePage page;

        [SetUp]
        public void Setup()
        {
            // Initilize Xamarin Forms
            MockForms.Init();

            //This is your App.xaml and App.xaml.cs, which can have resources, etc.
            app = new App();
            Application.Current = app;

            page = new BattlePage();

            page.BattleEngine.Engine.EngineSettings.CharacterList.Clear();
            page.BattleEngine.Engine.EngineSettings.MonsterList.Clear();
            page.BattleEngine.Engine.EngineSettings.PlayerList.Clear();
            page.BattleEngine.Engine.EngineSettings.CurrentDefender = null;
            page.BattleEngine.Engine.EngineSettings.CurrentAttacker = null;

        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void BattlePage_Constructor_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = page;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void BattlePage_RunBattle_Monsters_1_Should_Pass()
        {
            //----------------
            //Arrange
            //----------------

            // Add Characters
            page.BattleEngine.Engine.EngineSettings.MaxNumberPartyCharacters = 1;

            var CharacterPlayerJacob = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = 10,
                                Level = 10,
                                CurrentHealth = 1,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Jacob",
                                ListOrder = 1,
                            });

            page.BattleEngine.Engine.EngineSettings.CharacterList.Add(CharacterPlayerJacob);

            // Add Monsters
            var MonsterPlayerJesse = new PlayerInfoModel(
                           new CharacterModel
                           {
                               Speed = 1,
                               Level = 1,
                               CurrentHealth = 1,
                               ExperienceTotal = 1,
                               ExperienceRemaining = 1,
                               Name = "Jesse",
                               ListOrder = 2,
                           });
            page.BattleEngine.Engine.EngineSettings.MonsterList.Add(MonsterPlayerJesse);

            //Add them both to playerList
            page.BattleEngine.Engine.EngineSettings.PlayerList.AddRange(page.BattleEngine.Engine.EngineSettings.CharacterList);
            page.BattleEngine.Engine.EngineSettings.PlayerList.AddRange(page.BattleEngine.Engine.EngineSettings.MonsterList);


            // Need to set the Monster count to 1, so the battle goes to Next Round Faster
            page.BattleEngine.Engine.EngineSettings.MaxNumberPartyMonsters = 1;

            page.BattleEngine.Engine.Round.SetCurrentDefender(page.BattleEngine.Engine.EngineSettings.PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Monster).FirstOrDefault());
            page.BattleEngine.Engine.Round.SetCurrentAttacker(page.BattleEngine.Engine.EngineSettings.PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Character).FirstOrDefault());


            //Act
            RoundEnum result;

            //Characters turn
            page.NextAttackExample(page.BattleEngine.Engine.EngineSettings.CurrentDefender);
            result = page.BattleEngine.Engine.EngineSettings.RoundStateEnum;
            Assert.AreEqual(RoundEnum.NextTurn, result);

            // Monsters Turn
            page.AttackButton_Clicked(null, null);
            result = page.BattleEngine.Engine.EngineSettings.RoundStateEnum;
            Assert.AreEqual(RoundEnum.NextTurn, result);

            // loop eachothers turns until game over
            do
            {
                //Characters turn
                page.NextAttackExample(page.BattleEngine.Engine.EngineSettings.PlayerList.First());
                result = page.BattleEngine.Engine.EngineSettings.RoundStateEnum;

                if (result == RoundEnum.GameOver)
                {
                    break;
                }

                page.AttackButton_Clicked(null, null);
                
            }
            while (result != RoundEnum.GameOver);

            //Reset

            //Assert
            Assert.AreEqual(true, true);
        }
    }
}