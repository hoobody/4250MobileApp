
using NUnit.Framework;

using Game.Engine.EngineGame;
using Game.Models;
using Game.ViewModels;
using System.Linq;
using Game.Helpers;

namespace UnitTests.Engine.EngineGame
{
    [TestFixture]
    public class TurnEngineGameTests
    {
        #region TestSetup
        BattleEngine Engine;

        [SetUp]
        public void Setup()
        {
            Engine = new BattleEngine();
            Engine.Round = new RoundEngine();
            Engine.Round.Turn = new TurnEngine();
            Engine.StartBattle(true);   // Start engine in auto battle mode
        }

        [TearDown]
        public void TearDown()
        {
        }
        #endregion TestSetup

        #region Constructor
        [Test]
        public void TurnEngine_Constructor_Valid_Default_Should_Pass()
        {
            // Arrange

            // Act
            var result = Engine;

            // Reset

            // Assert
            Assert.IsNotNull(result);
        }
        #endregion Constructor

        #region BattleSettings
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

        [Test]
        public void TurnEngine_DetermineCriticalMissProblem_InValid_Null_Should_Fail()
        {
            // Arrange

            // Act
            var result = Engine.Round.Turn.DetermineCriticalMissProblem(null);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void TurnEngine_DetermineCriticalMissProblem_Valid_Monster_Drops_Random_Should_Pass()
        {
            // Arrange
            var MonsterPlayer = new PlayerInfoModel(new MonsterModel());

            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(6);

            // Act
            var result = Engine.Round.Turn.DetermineCriticalMissProblem(MonsterPlayer);

            // Reset
            _ = DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(true, result);
        }
        #endregion BattleSettings

        #region MoveAsTurn
        [Test]
        public void RoundEngine_MoveAsTurn_Valid_Default_Should_Pass()
        {
            // Arrange 

            // Act
            var result = Engine.Round.Turn.MoveAsTurn(new PlayerInfoModel());

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void RoundEngine_MoveAsTurn_Valid_Monster_Default_Should_Pass()
        {
            // Arrange 

            // Act
            var result = Engine.Round.Turn.MoveAsTurn(new PlayerInfoModel(new MonsterModel()));

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }
        #endregion MoveAsTurn

        #region ApplyDamage
        [Test]
        public void RoundEngine_ApplyDamage_Valid_Default_Should_Pass()
        {
            // Arrange 

            // Act
            var result = Engine.Round.Turn.ApplyDamage(new PlayerInfoModel(new MonsterModel()));

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }
        #endregion ApplyDamage

        #region Attack
        [Test]
        public void TurnEngine_Attack_Valid_Empty_Monster_List_Should_Fail()
        {
            // Arrange
            var PlayerInfo = new PlayerInfoModel(new CharacterModel());

            // Act
            var result = Engine.Round.Turn.Attack(PlayerInfo);

            // Reset
            _ = Engine.StartBattle(false);   // Clear the Engine

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void TurnEngine_Attack_InValid_Empty_Character_List_Should_Fail()
        {
            // Arrange
            var PlayerInfo = new PlayerInfoModel(new MonsterModel());

            // Cause an error by making the list empty
            Engine.EngineSettings.CharacterList.Clear();
            Engine.EngineSettings.CurrentDefender = null;
            Engine.EngineSettings.CurrentAttacker = null;

            _ = Engine.StartBattle(true);   // Clear the Engine

            // Act
            var result = Engine.Round.Turn.Attack(PlayerInfo);

            // Reset
            _ = Engine.StartBattle(false);   // Clear the Engine

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void TurnEngine_Attack_Valid_Correct_List_Should_Pass()
        {
            // Arrange
            var PlayerInfo = new PlayerInfoModel();
            Engine.EngineSettings.MonsterList.Add(new PlayerInfoModel(new MonsterModel()));

            // Act
            var result = Engine.Round.Turn.Attack(PlayerInfo);

            // Reset
            _ = Engine.StartBattle(false);   // Clear the Engine

            // Assert
            Assert.AreEqual(true, result);
        }
        #endregion Attack

        #region AttackChoice
        //[Test]
        //public void RoundEngine_AttackChoice_Valid_Default_Should_Pass()
        //{
        //    // Arrange 

        //    // Act
        //    var result = Engine.Round.Turn.AttackChoice(new PlayerInfoModel());

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(null, result);
        //}
        #endregion AttackChoice

        #region SelectCharacterToAttack
        //[Test]
        //public void RoundEngine_SelectCharacterToAttack_Valid_Default_Should_Pass()
        //{
        //    // Arrange 

        //    // Act
        //    var result = Engine.Round.Turn.SelectCharacterToAttack();

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(null, result);
        //}
        #endregion SelectCharacterToAttack

        #region UseAbility
        //[Test]
        //public void RoundEngine_UseAbility_Valid_Default_Should_Pass()
        //{
        //    // Arrange 

        //    // Act
        //    var result = Engine.Round.Turn.UseAbility(null);

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(false, result);
        //}
        #endregion UseAbility

        #region BattleSettingsOverrideHitStatusEnum
        //[Test]
        //public void RoundEngine_BattleSettingsOverrideHitStatusEnum_Valid_Default_Should_Pass()
        //{
        //    // Arrange 

        //    // Act
        //    var result = Engine.Round.Turn.BattleSettingsOverrideHitStatusEnum(HitStatusEnum.Unknown);

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(HitStatusEnum.Unknown, result);
        //}
        //#endregion BattleSettingsOverrideHitStatusEnum

        //#region BattleSettingsOverride
        //[Test]
        //public void RoundEngine_BattleSettingsOverride_Valid_Default_Should_Pass()
        //{
        //    // Arrange 

        //    // Act
        //    var result = Engine.Round.Turn.BattleSettingsOverride(new PlayerInfoModel());

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(HitStatusEnum.Unknown, result);
        //}
        #endregion BattleSettingsOverride

        #region CalculateExperience
        //[Test]
        //public void RoundEngine_CalculateExperience_Valid_Default_Should_Pass()
        //{
        //    // Arrange 

        //    // Act
        //    var result = Engine.Round.Turn.CalculateExperience(new PlayerInfoModel(), new PlayerInfoModel());

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(false, result);
        //}
        #endregion CalculateExperience

        #region CalculateAttackStatus
        //[Test]
        //public void RoundEngine_CalculateAttackStatus_Valid_Default_Should_Pass()
        //{
        //    // Arrange 

        //    // Act
        //    var result = Engine.Round.Turn.CalculateAttackStatus(new PlayerInfoModel(), new PlayerInfoModel());

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(HitStatusEnum.Unknown, result);
        //}
        #endregion CalculateAttackStatus

        #region RemoveIfDead
        [Test]
        public void RoundEngine_RemoveIfDead_Valid_Default_Should_Pass()
        {
            // Arrange 

            // Act
            var result = Engine.Round.Turn.RemoveIfDead(new PlayerInfoModel());

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }
        #endregion RemoveIfDead

        #region ChooseToUseAbility
        [Test]
        public void RoundEngine_ChooseToUseAbility_Valid_Default_Should_Pass()
        {
            // Arrange 

            // Act
            var result = Engine.Round.Turn.ChooseToUseAbility(new PlayerInfoModel());

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }
        #endregion ChooseToUseAbility

        #region SelectMonsterToAttack
        //[Test]
        //public void RoundEngine_SelectMonsterToAttack_Valid_Default_Should_Pass()
        //{
        //    // Arrange 

        //    // Act
        //    var result = Engine.Round.Turn.SelectMonsterToAttack();

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(null, result);
        //}
        #endregion SelectMonsterToAttack

        #region DetermineActionChoice
        [Test]
        public void TurnEngine_DetermineActionChoice_Valid_Monster_Should_Return_CurrentAction()
        {
            // Arrange
            var MonsterPlayer = new PlayerInfoModel(new MonsterModel());

            MonsterPlayer.CurrentHealth = 1;
            MonsterPlayer.MaxHealth = 1000;

            Engine.EngineSettings.CurrentAction = ActionEnum.Unknown;

            // Act
            var result = Engine.Round.Turn.DetermineActionChoice(MonsterPlayer);

            // Reset

            // Assert
            Assert.AreEqual(ActionEnum.Ability, result);
        }

        [Test]
        public void TurnEngine_DetermineActionChoice_Valid_Character_Should_Return_CurrentAction()
        {
            // Arrange
            var CharacterPlayer = new PlayerInfoModel(new CharacterModel());

            CharacterPlayer.CurrentHealth = 1;
            CharacterPlayer.MaxHealth = 1000;

            Engine.EngineSettings.CurrentAction = ActionEnum.Unknown;
            Engine.EngineSettings.BattleScore.AutoBattle = true;

            // Act
            var result = Engine.Round.Turn.DetermineActionChoice(CharacterPlayer);

            // Reset

            // Assert
            Assert.AreEqual(ActionEnum.Ability, result);
        }

        [Test]
        public void TurnEngine_DetermineActionChoice_Valid_Character_Range_Should_Return_Attack()
        {
            // Arrange

            var CharacterPlayer = new PlayerInfoModel(new CharacterModel());

            // Get the longest range weapon in stock.
            var weapon = ItemIndexViewModel.Instance.Dataset.Where(m => m.Range > 1).ToList().OrderByDescending(m => m.Range).FirstOrDefault();
            CharacterPlayer.PrimaryHand = weapon.Id;
            Engine.EngineSettings.PlayerList.Add(CharacterPlayer);

            var Monster = new MonsterModel();
            Engine.EngineSettings.PlayerList.Add(new PlayerInfoModel(Monster));
            Engine.EngineSettings.PlayerList.Add(new PlayerInfoModel(Monster));

            _ = Engine.EngineSettings.MapModel.PopulateMapModel(Engine.EngineSettings.PlayerList);

            Engine.EngineSettings.CurrentAction = ActionEnum.Unknown;
            Engine.EngineSettings.BattleScore.AutoBattle = true;

            // Act
            var result = Engine.Round.Turn.DetermineActionChoice(CharacterPlayer);

            // Reset

            // Assert
            Assert.AreEqual(ActionEnum.Attack, result);
        }
        #endregion DetermineActionChoice

        #region TurnAsAttack
        [Test]
        public void TurnEngine_TurnAsAttack_Valid_Character_Attacks_Null_Should_Fail()
        {
            // Arrange
            var Character = new CharacterModel();
            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.EngineSettings.CharacterList.Add(CharacterPlayer);

            var Monster = new MonsterModel();
            var MonsterPlayer = new PlayerInfoModel(Monster);
            Engine.EngineSettings.MonsterList.Add(MonsterPlayer);

            // Act
            var result = Engine.Round.Turn.TurnAsAttack(CharacterPlayer, null);

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void TurnEngine_TurnAsAttack_Valid_Null_Attacks_Character_Should_Fail()
        {
            // Arrange
            var Character = new CharacterModel();
            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.EngineSettings.CharacterList.Add(CharacterPlayer);

            var Monster = new MonsterModel();
            var MonsterPlayer = new PlayerInfoModel(Monster);
            Engine.EngineSettings.MonsterList.Add(MonsterPlayer);

            // Act
            var result = Engine.Round.Turn.TurnAsAttack(null, CharacterPlayer);

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void TurnEngine_TurnAsAttack_Valid_Character_Attacks_Monster_Miss_Should_Pass()
        {
            // Arrange
            var Character = new CharacterModel();
            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.EngineSettings.CharacterList.Add(CharacterPlayer);

            var Monster = new MonsterModel();
            var MonsterPlayer = new PlayerInfoModel(Monster);
            Engine.EngineSettings.MonsterList.Add(MonsterPlayer);

            // Forece a Miss
            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(1);

            // Act
            var result = Engine.Round.Turn.TurnAsAttack(CharacterPlayer, MonsterPlayer);

            // Reset
            _ = DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void TurnEngine_TurnAsAttack_Valid_Character_Attacks_Monster_Hit_Should_Pass()
        {
            // Arrange
            var Character = new CharacterModel();
            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.EngineSettings.CharacterList.Add(CharacterPlayer);

            var Monster = new MonsterModel();
            var MonsterPlayer = new PlayerInfoModel(Monster);
            Engine.EngineSettings.MonsterList.Add(MonsterPlayer);

            // Forece a Miss
            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(20);

            // Act
            var result = Engine.Round.Turn.TurnAsAttack(CharacterPlayer, MonsterPlayer);

            // Reset
            _ = DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void TurnEngine_TurnAsAttack_Valid_Character_Attacks_Monster_Hit_Death_Should_Pass()
        {
            // Arrange
            var Character = new CharacterModel();
            Character.CurrentHealth = 1;
            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.EngineSettings.CharacterList.Add(CharacterPlayer);

            var Monster = new MonsterModel();
            var MonsterPlayer = new PlayerInfoModel(Monster);
            Engine.EngineSettings.MonsterList.Add(MonsterPlayer);

            // Forece a Miss
            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(20);

            // Act
            var result = Engine.Round.Turn.TurnAsAttack(CharacterPlayer, MonsterPlayer);

            // Reset
            _ = DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void TurnEngine_TurnAsAttack_Valid_Monster_Attacks_Character_Miss_Should_Pass()
        {
            // Arrange
            var Monster = new MonsterModel();
            var MonsterPlayer = new PlayerInfoModel(Monster);
            Engine.EngineSettings.MonsterList.Add(MonsterPlayer);

            var Character = new CharacterModel();
            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.EngineSettings.CharacterList.Add(CharacterPlayer);

            // Forece a Miss
            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(1);

            // Act
            var result = Engine.Round.Turn.TurnAsAttack(MonsterPlayer, CharacterPlayer);

            // Reset
            _ = DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void TurnEngine_TurnAsAttack_Valid_Monster_Attacks_Character_Hit_Should_Pass()
        {
            // Arrange
            var Monster = new MonsterModel();
            var MonsterPlayer = new PlayerInfoModel(Monster);
            Engine.EngineSettings.MonsterList.Add(MonsterPlayer);

            var Character = new CharacterModel();
            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.EngineSettings.CharacterList.Add(CharacterPlayer);

            // Forece a Miss
            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(20);

            // Act
            var result = Engine.Round.Turn.TurnAsAttack(MonsterPlayer, CharacterPlayer);

            // Reset
            _ = DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void TurnEngine_TurnAsAttack_Valid_Force_Critical_Hit_Monster_Attacks_Character_Hit_Should_Pass()
        {
            // Arrange
            var Monster = new MonsterModel();
            var MonsterPlayer = new PlayerInfoModel(Monster);
            Engine.EngineSettings.MonsterList.Add(MonsterPlayer);

            var Character = new CharacterModel();
            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.EngineSettings.CharacterList.Add(CharacterPlayer);

            // Forece a Miss
            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(20);

            var oldSetting = Engine.EngineSettings.BattleSettingsModel.AllowCriticalHit;
            Engine.EngineSettings.BattleSettingsModel.AllowCriticalHit = true;

            // Act
            var result = Engine.Round.Turn.TurnAsAttack(MonsterPlayer, CharacterPlayer);

            // Reset
            _ = DiceHelper.DisableForcedRolls();
            Engine.EngineSettings.BattleSettingsModel.AllowCriticalHit = oldSetting;

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void TurnEngine_TurnAsAttack_Valid_Force_Critical_Miss_Monster_Attacks_Character_Hit_Should_Pass()
        {
            // Arrange
            var Monster = new MonsterModel();
            var MonsterPlayer = new PlayerInfoModel(Monster);
            Engine.EngineSettings.MonsterList.Add(MonsterPlayer);

            var Character = new CharacterModel();
            var CharacterPlayer = new PlayerInfoModel(Character);
            Engine.EngineSettings.CharacterList.Add(CharacterPlayer);

            // Forece a Miss
            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(1);

            var oldSetting = Engine.EngineSettings.BattleSettingsModel.AllowCriticalMiss;
            Engine.EngineSettings.BattleSettingsModel.AllowCriticalMiss = true;

            // Act
            var result = Engine.Round.Turn.TurnAsAttack(MonsterPlayer, CharacterPlayer);

            // Reset
            _ = DiceHelper.DisableForcedRolls();
            Engine.EngineSettings.BattleSettingsModel.AllowCriticalMiss = oldSetting;

            // Assert
            Assert.AreEqual(true, result);
        }

        public void TurnEngine_TurnAsAttack_Valid_Character_Attacks_Monster_Levels_Up_Should_Pass()
        {
            // Arrange
            var Monster = new MonsterModel();
            var MonsterPlayer = new PlayerInfoModel(Monster);
            Engine.EngineSettings.MonsterList.Add(MonsterPlayer);

            var Character = new CharacterModel();

            var CharacterPlayer = new PlayerInfoModel(Character);

            CharacterPlayer.ExperienceTotal = 300;    // Enough for next level

            Engine.EngineSettings.CharacterList.Add(CharacterPlayer);

            // Forece a Miss
            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(20);

            // Act
            var result = Engine.Round.Turn.TurnAsAttack(CharacterPlayer, MonsterPlayer);

            // Reset
            _ = DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(2, CharacterPlayer.Level);
        }
        #endregion TurnAsAttack

        #region TargetDied
        //[Test]
        //public void RoundEngine_TargetDied_Valid_Default_Should_Pass()
        //{
        //    // Arrange 

        //    // Act
        //    var result = Engine.Round.Turn.TargetDied(new PlayerInfoModel());

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(false, result);
        //}
        #endregion TargetDied

        #region TakeTurn
        //[Test]
        //public void RoundEngine_TakeTurn_Valid_Default_Should_Pass()
        //{
        //    // Arrange 

        //    // Act
        //    var result = Engine.Round.Turn.TakeTurn(new PlayerInfoModel());

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(false, result);
        //}
        #endregion TakeTurn

        #region RollToHitTarget
        //[Test]
        //public void RoundEngine_RollToHitTarget_Valid_Default_Should_Pass()
        //{
        //    // Arrange 

        //    // Act
        //    var result = Engine.Round.Turn.RollToHitTarget(1,1);

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(HitStatusEnum.Unknown, result);
        //}
        #endregion RollToHitTarget

        #region GetRandomMonsterItemDrops
        //[Test]
        //public void RoundEngine_GetRandomMonsterItemDrops_Valid_Default_Should_Pass()
        //{
        //    // Arrange 

        //    // Act
        //    var result = Engine.Round.Turn.GetRandomMonsterItemDrops(1);

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(null, result);
        //}
        #endregion GetRandomMonsterItemDrops

        #region DetermineCriticalMissProblem
        [Test]
        public void RoundEngine_DetermineCriticalMissProblem_Valid_Default_Should_Pass()
        {
            // Arrange 

            // Act
            var result = Engine.Round.Turn.DetermineCriticalMissProblem(new PlayerInfoModel());

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }
        #endregion DetermineCriticalMissProblem

        #region DropItems
        //[Test]
        //public void RoundEngine_DropItems_Valid_Default_Should_Pass()
        //{
        //    // Arrange 

        //    // Act
        //    var result = Engine.Round.Turn.DropItems(new PlayerInfoModel());

        //    // Reset

        //    // Assert
        //    Assert.AreEqual(0, result);
        //}
        #endregion DropItems
    }
}