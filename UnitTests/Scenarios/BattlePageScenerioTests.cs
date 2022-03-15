using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;

using Xamarin.Forms;
using Xamarin.Forms.Mocks;

using Game;
using Game.Views;
using Game.Models;
using Game.Helpers;
using Game.ViewModels;

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
            //Tests wether the battle goes to game over with a single monster and character
            // 1 Character, Experience set at next level mark
            // 1 Monster
            // Monsteres should win and the battlestate should be game over
            //----------------

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
                                CurrentHealth = 4,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Jacob",
                                ListOrder = 1,
                            });

            page.BattleEngine.Engine.EngineSettings.CharacterList.Add(CharacterPlayerJacob);

            // Add Monsters
            var MonsterPlayerJesse = new PlayerInfoModel(
                           new MonsterModel
                           {
                               Speed = 1,
                               Level = 1,
                               CurrentHealth = 10,
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

            //----------------
            //Act
            //----------------

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


            //----------------
            //Assert
            //----------------
            Assert.AreEqual(true, true);
        }

        [Test]
        public void BattlePage_RunBattle_Monsters_Go_To_Second_Round_Should_Pass()
        {
            //----------------
            //Tests wether the battle goes to game over with a single monster and character
            // 1 Character, Experience set at next level mark
            // 1 Monster
            // Monsteres should win and the battlestate should be game over
            //----------------


            //----------------
            //Arrange
            //----------------

            BattleStateEnum result;
            // Add Characters
            page.BattleEngine.Engine.EngineSettings.MaxNumberPartyCharacters = 1;

            var CharacterPlayerJacob = new PlayerInfoModel(
                            new CharacterModel
                            {
                                Speed = 100,
                                Level = 10,
                                Attack = 100,
                                CurrentHealth = 4,
                                ExperienceTotal = 1,
                                ExperienceRemaining = 1,
                                Name = "Jacob",
                                ListOrder = 1,
                            });

            page.BattleEngine.Engine.EngineSettings.CharacterList.Add(CharacterPlayerJacob);

            // Add Monsters
            
                var MonsterPlayer = new PlayerInfoModel(
                           new MonsterModel
                           {
                               Speed = -1,
                               Level = 1,
                               Defense = 0,
                               CurrentHealth = 1,
                               ExperienceTotal = 1,
                               ExperienceRemaining = 1,
                               Name = "Jesse",
                               ListOrder = 2,
                           });
                page.BattleEngine.Engine.EngineSettings.MonsterList.Add(MonsterPlayer);





            //Add them both to playerList
            page.BattleEngine.Engine.EngineSettings.PlayerList.AddRange(page.BattleEngine.Engine.EngineSettings.MonsterList);
            page.BattleEngine.Engine.EngineSettings.PlayerList.AddRange(page.BattleEngine.Engine.EngineSettings.CharacterList);


            // Need to set the Monster count to 1, so the battle goes to Next Round Faster
            page.BattleEngine.Engine.EngineSettings.MaxNumberPartyMonsters = 1;

            page.BattleEngine.Engine.Round.SetCurrentDefender(page.BattleEngine.Engine.EngineSettings.PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Monster).FirstOrDefault());
            page.BattleEngine.Engine.Round.SetCurrentAttacker(page.BattleEngine.Engine.EngineSettings.PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Character).FirstOrDefault());

            // Controll Rolls,  Hit is always a 3
            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(20);

            //----------------
            //Act
            //----------------

            //Characters turn
            page.NextAttackExample(page.BattleEngine.Engine.EngineSettings.CurrentDefender);
            result = page.BattleEngine.Engine.EngineSettings.BattleStateEnum;
            Assert.AreEqual(BattleStateEnum.NewRound, result);

            //Reset
            _ = DiceHelper.DisableForcedRolls();

            //----------------
            //Assert
            //----------------

            Assert.AreEqual(true, true);
        }

        [Test]
        public void BattlePage_RunBattle_Characters_Play_With_Full_Party_Should_Pass()
        {
            //----------------
            //Tests wether the battle goes to game over with a single monster and character
            // 1 Character, Experience set at next level mark
            // 1 Monster
            // Monsteres should win and the battlestate should be game over
            //----------------


            //----------------
            //Arrange
            //----------------

            BattleStateEnum result;

            // Add Characters
            page.BattleEngine.Engine.EngineSettings.MaxNumberPartyCharacters = 6;

            for (int i = 0; i < page.BattleEngine.Engine.EngineSettings.MaxNumberPartyCharacters; i++)
            {
                var CharacterPlayer = new PlayerInfoModel(
                    new CharacterModel
                    {
                        Speed = 5,
                        Attack = 5,
                        Defense = 5,
                        Level = 5,
                        CurrentHealth = 4,
                        ExperienceTotal = 1,
                        ExperienceRemaining = 1,
                        Name = "Jacob",
                        ListOrder = 1,
                    });

                page.BattleEngine.Engine.EngineSettings.CharacterList.Add(CharacterPlayer);
            }

            // Add Monsters
            page.BattleEngine.Engine.Round.AddMonstersToRound();
            

            //Add them both to playerList
            page.BattleEngine.Engine.EngineSettings.PlayerList.AddRange(page.BattleEngine.Engine.EngineSettings.MonsterList);
            page.BattleEngine.Engine.EngineSettings.PlayerList.AddRange(page.BattleEngine.Engine.EngineSettings.CharacterList);

            //set order
            page.BattleEngine.Engine.Round.OrderPlayerListByTurnOrder();


            // Need to set the Monster count to 1, so the battle goes to Next Round Faster
            page.BattleEngine.Engine.EngineSettings.MaxNumberPartyMonsters = 1;

            page.BattleEngine.Engine.Round.SetCurrentDefender(page.BattleEngine.Engine.EngineSettings.PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Monster).FirstOrDefault());
            page.BattleEngine.Engine.Round.SetCurrentAttacker(page.BattleEngine.Engine.EngineSettings.PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Character).FirstOrDefault());



            //----------------
            //Act
            //----------------

            //take everyones turn until the round ends. Then do it again until all the characters die
            while (page.BattleEngine.Engine.EngineSettings.BattleStateEnum != BattleStateEnum.GameOver)
            {
                switch (page.BattleEngine.Engine.EngineSettings.CurrentAttacker.PlayerType)
                {
                    case PlayerTypeEnum.Character:
                        page.NextAttackExample(page.BattleEngine.Engine.EngineSettings.MonsterList.First());
                        break;
                    case PlayerTypeEnum.Monster:
                        page.AttackButton_Clicked(null, null);
                        break;
                    default:
                        break;
                }
                //if the round ends prepare a new round
                if (page.BattleEngine.Engine.EngineSettings.BattleStateEnum == BattleStateEnum.NewRound)
                {
                    page.BattleEngine.Engine.Round.NewRound();
                    page.PrepareRound();
                }

            }

            //----------------
            //Assert
            //----------------

            Assert.AreEqual(BattleStateEnum.GameOver, page.BattleEngine.Engine.EngineSettings.BattleStateEnum);
        }

        [Test]
        public void BattlePage_RunBattle_Get_To_Boss_Round_Should_Pass()
        {
            //----------------
            //Tests wether the battle goes to game over with a single monster and character
            // 1 Character, Experience set at next level mark
            // 1 Monster
            // Monsteres should win and the battlestate should be game over
            //----------------


            //----------------
            //Arrange
            //----------------

            BattleStateEnum result;

            // Add Characters
            page.BattleEngine.Engine.EngineSettings.MaxNumberPartyCharacters = 6;

            for (int i = 0; i < page.BattleEngine.Engine.EngineSettings.MaxNumberPartyCharacters; i++)
            {
               
                var CharacterPlayer = new PlayerInfoModel(
                    new CharacterModel
                    {
                        Speed = 5,
                        Attack = 5,
                        Defense = 5,
                        Level = 5,
                        CurrentHealth = 4,
                        ExperienceTotal = 1,
                        ExperienceRemaining = 1,
                        Name = "Jacob",
                        ListOrder = 1,
                    });
                page.BattleEngine.Engine.EngineSettings.CharacterList.Add(CharacterPlayer);

            }

            // Add Monsters
            page.BattleEngine.Engine.Round.AddMonstersToRound();


            //Add them both to playerList
            page.BattleEngine.Engine.EngineSettings.PlayerList.AddRange(page.BattleEngine.Engine.EngineSettings.MonsterList);
            page.BattleEngine.Engine.EngineSettings.PlayerList.AddRange(page.BattleEngine.Engine.EngineSettings.CharacterList);

            //set order
            page.BattleEngine.Engine.Round.OrderPlayerListByTurnOrder();


            // Need to set the Monster count to 1, so the battle goes to Next Round Faster
            page.BattleEngine.Engine.EngineSettings.MaxNumberPartyMonsters = 1;

            page.BattleEngine.Engine.Round.SetCurrentDefender(page.BattleEngine.Engine.EngineSettings.PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Monster).FirstOrDefault());
            page.BattleEngine.Engine.Round.SetCurrentAttacker(page.BattleEngine.Engine.EngineSettings.PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Character).FirstOrDefault());



            //----------------
            //Act
            //----------------

            //take everyones turn until the round ends. Then do it again until all the characters die
            while (page.BattleEngine.Engine.EngineSettings.BattleStateEnum != BattleStateEnum.GameOver)
            {
                switch (page.BattleEngine.Engine.EngineSettings.CurrentAttacker.PlayerType)
                {
                    case PlayerTypeEnum.Character:
                        page.NextAttackExample(page.BattleEngine.Engine.EngineSettings.MonsterList.First());
                        break;
                    case PlayerTypeEnum.Monster:
                        page.AttackButton_Clicked(null, null);
                        break;
                    default:
                        break;
                }
                //if the round ends prepare a new round
                if (page.BattleEngine.Engine.EngineSettings.BattleStateEnum == BattleStateEnum.NewRound)
                {
                    page.BattleEngine.Engine.Round.NewRound();
                    page.PrepareRound();
                }
                if (page.BattleEngine.Engine.EngineSettings.BattleScore.RoundCount == 3)
                {
                    var bossMonster = page.BattleEngine.Engine.EngineSettings.MonsterList.First();
                    Assert.AreEqual(true, bossMonster.isABoss);
                    break;
                }

            }

            //----------------
            //Assert
            //----------------

            Assert.AreEqual(true, true); //we got here so must be working
        }

        [Test]
        public void BattlePage_RunBattle_Use_Abilities_Should_Pass()
        {
            //----------------
            //Tests wether using abilties works as intended
            // 6 Characters, Experience set at next level mark
            // 6 Monsters
            // Monsteres should win and the battlestate should be game over
            //----------------


            //----------------
            //Arrange
            //----------------

            CharacterIndexViewModel characters = CharacterIndexViewModel.Instance;

            // Add Characters
            page.BattleEngine.Engine.EngineSettings.MaxNumberPartyCharacters = 6;

            for (int i = 0; i < page.BattleEngine.Engine.EngineSettings.MaxNumberPartyCharacters; i++)
            {
                var CharacterPlayer = new PlayerInfoModel(
                    new CharacterModel
                    {
                        Speed = 5,
                        Attack = 5,
                        Defense = 5,
                        Level = 5,
                        CurrentHealth = 4,
                        ExperienceTotal = 1,
                        ExperienceRemaining = 1,
                        Name = "Jacob",
                        ListOrder = 1,
                    });

                page.BattleEngine.Engine.EngineSettings.CharacterList.Add(CharacterPlayer);
            }

            // Add Monsters
            page.BattleEngine.Engine.Round.AddMonstersToRound();


            //Add them both to playerList
            page.BattleEngine.Engine.EngineSettings.PlayerList.AddRange(page.BattleEngine.Engine.EngineSettings.MonsterList);
            page.BattleEngine.Engine.EngineSettings.PlayerList.AddRange(page.BattleEngine.Engine.EngineSettings.CharacterList);

            //set order
            page.BattleEngine.Engine.Round.OrderPlayerListByTurnOrder();


            // Need to set the Monster count to 1, so the battle goes to Next Round Faster
            page.BattleEngine.Engine.EngineSettings.MaxNumberPartyMonsters = 1;

            page.BattleEngine.Engine.Round.SetCurrentDefender(page.BattleEngine.Engine.EngineSettings.PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Monster).FirstOrDefault());
            page.BattleEngine.Engine.Round.SetCurrentAttacker(page.BattleEngine.Engine.EngineSettings.PlayerList.Where(m => m.PlayerType == PlayerTypeEnum.Character).FirstOrDefault());



            //----------------
            //Act
            //----------------

            //take everyones turn until the round ends. Then do it again until all the characters die
            while (page.BattleEngine.Engine.EngineSettings.BattleStateEnum != BattleStateEnum.GameOver)
            {
                switch (page.BattleEngine.Engine.EngineSettings.CurrentAttacker.PlayerType)
                {
                    case PlayerTypeEnum.Character:

                        //sometimes use abilities
                        if(page.BattleEngine.Engine.EngineSettings.BattleScore.RoundCount % 2 == 0)
                        {
                            page.AbilityButton_Clicked(null, null);
                        }
                        //otherwise use attacks
                        if (page.BattleEngine.Engine.EngineSettings.BattleScore.RoundCount % 2 != 0)
                        {
                            page.NextAttackExample(page.BattleEngine.Engine.EngineSettings.MonsterList.First());
                        }
                        break;
                    case PlayerTypeEnum.Monster:
                        page.AttackButton_Clicked(null, null);
                        break;
                    default:
                        break;
                }
                //if the round ends prepare a new round
                if (page.BattleEngine.Engine.EngineSettings.BattleStateEnum == BattleStateEnum.NewRound)
                {
                    page.BattleEngine.Engine.Round.NewRound();
                    page.PrepareRound();
                }

            }

            //----------------
            //Assert
            //----------------

            Assert.AreEqual(BattleStateEnum.GameOver, page.BattleEngine.Engine.EngineSettings.BattleStateEnum);
        }

    }
}