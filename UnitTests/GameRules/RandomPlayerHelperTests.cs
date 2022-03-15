using NUnit.Framework;

using Game.Helpers;
using Game.Models;
using Game.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using Game.GameRules;

namespace UnitTests.Helpers
{
    [TestFixture]
    public class RandomPlayerHelperTests
    {
        [Test]
        public void RandomPlayerHelper_GetAbilityValue_2_Should_Return_2()
        {
            // Arrange
            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetAbilityValue();

            // Reset
            _ = DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(2 - 1, result);
        }

        [Test]
        public void RandomPlayerHelper_GetLevel_2_Should_Return_2()
        {
            // Arrange
            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetLevel();

            // Reset
            _ = DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(2, result);
        }

        [Test]
        public void RandomPlayerHelper_GetHealth_2_Should_Return_2()
        {
            // Arrange
            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetHealth(1);

            // Reset
            _ = DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(2, result);
        }

        [Test]
        public void RandomPlayerHelper_GetMonsterName_2_Should_Return_2()
        {
            // Arrange
            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetMonsterName();

            // Reset
            _ = DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual("Sir Pounce", result);
        }

        [Test]
        public void RandomPlayerHelper_GetMonsterDescription_2_Should_Return_2()
        {
            // Arrange
            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetMonsterDescription();

            // Reset
            _ = DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual("Yarn Thrower", result);
        }

        [Test]
        public void RandomPlayerHelper_GetCharacterDescription_2_Should_Return_2()
        {
            // Arrange
            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetCharacterDescription();

            // Reset
            _ = DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual("the awesome", result);
        }

        [Test]
        public void RandomPlayerHelper_GetCharacterName_2_Should_Return_2()
        {
            // Arrange
            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetCharacterName();

            // Reset
            _ = DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual("Doug", result);
        }

        [Test]
        public void RandomPlayerHelper_GetItem_InValid_No_Item_Should_Return_Null()
        {
            // Arrange

            // Act
            var result = RandomPlayerHelper.GetItem(Game.Models.ItemLocationEnum.Unknown);

            // Reset

            // Assert
            Assert.AreEqual(null, result);
        }

        [Test]
        public void RandomPlayerHelper_GetItem_2_Should_Return_2()
        {
            // Arrange
            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetItem(Game.Models.ItemLocationEnum.Feet);

            // Reset
            _ = DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreNotEqual(null, result);
        }

        [Test]
        public void RandomPlayerHelper_GetMonsterDifficultyValue_Should_Pass()
        {
            // Arrange
            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetMonsterDifficultyValue();

            // Reset
            _ = DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(DifficultyEnum.Average, result);
        }

        [Test]
        public void RandomPlayerHelper_GetMonsterImage_2_Should_Return_2()
        {
            // Arrange
            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetMonsterImage();

            // Reset
            _ = DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual("monster_bountyhunter.png", result);
        }

        [Test]
        public void RandomPlayerHelper_GetCharacterImage_2_Should_Return_2()
        {
            // Arrange
            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetCharacterImage();

            // Reset
            _ = DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual("character_detective.png", result);
        }

        [Test]
        public void RandomPlayerHelper_GetRandomCharacter_InValid_Empty_CharacterList_Should_Return_New()
        {
            // Arrange
            CharacterIndexViewModel.Instance.Dataset.Clear();

            // Act
            var result = RandomPlayerHelper.GetRandomCharacter(1);

            // Reset

            // Assert
            Assert.AreEqual(true, result.Name.Contains("Mister Mouse"));
        }

        [Test]
        public async Task RandomPlayerHelper_GetRandomCharacter_Valid_CharacterList_1_Should_Return_1()
        {
            // Arrange
            CharacterIndexViewModel.Instance.Dataset.Clear();
            _ = await CharacterIndexViewModel.Instance.CreateAsync(new CharacterModel { UniqueItem = "1" });

            // Arrange
            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(1);

            // Act
            var result = RandomPlayerHelper.GetRandomCharacter(1);

            // Reset
            _ = DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(true, result.UniqueItem.Equals("1"));
        }

        [Test]
        public async Task RandomPlayerHelper_GetRandomCharacter_Valid_CharacterList_3_Should_Return_2()
        {
            // Arrange
            CharacterIndexViewModel.Instance.Dataset.Clear();
            _ = await CharacterIndexViewModel.Instance.CreateAsync(new CharacterModel { UniqueItem = "1" });
            _ = await CharacterIndexViewModel.Instance.CreateAsync(new CharacterModel { UniqueItem = "2" });
            _ = await CharacterIndexViewModel.Instance.CreateAsync(new CharacterModel { UniqueItem = "3" });

            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetRandomCharacter(1);

            // Reset
            _ = DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(true, result.UniqueItem.Equals("2"));
        }

        [Test]
        public async Task RandomPlayerHelper_GetRandomCharacter_Valid_Health_Should_Be_Correct()
        {
            // Arrange
            CharacterIndexViewModel.Instance.Dataset.Clear();
            _ = await CharacterIndexViewModel.Instance.CreateAsync(new CharacterModel { UniqueItem = "1" });
            _ = await CharacterIndexViewModel.Instance.CreateAsync(new CharacterModel { UniqueItem = "2" });
            _ = await CharacterIndexViewModel.Instance.CreateAsync(new CharacterModel { UniqueItem = "3" });

            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(2);

            // Act
            var result = RandomPlayerHelper.GetRandomCharacter(1);

            // Reset
            _ = DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual(result.MaxHealth, result.CurrentHealth);
        }

        [Test]
        public void RandomPlayerHelper_GetRandomMonster_InValid_Empty_List_Should_Return_New_Monster()
        {
            // Arrange
            var save = MonsterIndexViewModel.Instance.Dataset;
            MonsterIndexViewModel.Instance.Dataset.Clear();

            // Act
            var result = RandomPlayerHelper.GetRandomMonster(1);

            // Reset
            MonsterIndexViewModel.Instance.Dataset = save;

            // Assert
            Assert.AreEqual(PlayerTypeEnum.Monster, result.PlayerType);
        }

        [Test]
        public void RandomPlayerHelper_GetRandomMonster_Valid_Should_Return_New_Monster()
        {
            // Arrange

            // Act
            var result = RandomPlayerHelper.GetRandomMonster(1);

            // Reset

            // Assert
            Assert.AreEqual(PlayerTypeEnum.Monster, result.PlayerType);
        }

        [Test]
        public async Task RandomPlayerHelper_GetRandomMonster_Valid_Items_True_Should_Return_New_Monster()
        {
            // Arrange
            MonsterIndexViewModel.Instance.Dataset.Clear();
            _ = await MonsterIndexViewModel.Instance.CreateAsync(new MonsterModel { UniqueItem = "1" });
            _ = await MonsterIndexViewModel.Instance.CreateAsync(new MonsterModel { UniqueItem = "2" });
            _ = await MonsterIndexViewModel.Instance.CreateAsync(new MonsterModel { UniqueItem = "3" });

            // Act
            var result = RandomPlayerHelper.GetRandomMonster(1, true);

            // Reset

            // Assert
            Assert.AreEqual(PlayerTypeEnum.Monster, result.PlayerType);
        }

        [Test]
        public void RandomPlayerHelper_GetRandomMonster_Valid_Items_False_Should_Return_New_Monster()
        {
            // Arrange

            // Act
            var result = RandomPlayerHelper.GetRandomMonster(1, false);

            // Reset

            // Assert
            Assert.AreEqual(PlayerTypeEnum.Monster, result.PlayerType);
        }

        [Test]
        public void RandomPlayerHelper_GetCharacterJob_Should_Return_Spy()
        {
            // Arrange
            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(11);

            // Act
            var result = RandomPlayerHelper.GetCharacterJob();

            // Reset
            _ = DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual("Spy", result);
        }

        [Test]
        public void RandomPlayerHelper_GetMonsterJob_Should_Return_Godfather()
        {
            // Arrange
            _ = DiceHelper.EnableForcedRolls();
            _ = DiceHelper.SetForcedRollValue(3);

            // Act
            var result = RandomPlayerHelper.GetMonsterJob();

            // Reset
            _ = DiceHelper.DisableForcedRolls();

            // Assert
            Assert.AreEqual("Godfather", result);
        }


    }
}