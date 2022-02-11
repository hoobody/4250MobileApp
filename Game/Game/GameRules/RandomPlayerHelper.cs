using System;
using System.Collections.Generic;
using System.Linq;

using Game.Helpers;
using Game.Models;
using Game.ViewModels;


namespace Game.GameRules
{
    public static class RandomPlayerHelper
    {
        public static List<string> ItemImageList = new List<string> {"sniper_item.png", "stinky_gun.png", "water_gun.png", "lazer_ring.png",
                                                                     "filler_speed_shoes.png", "filler_armor_shoes.png", "trench_coat.png", "stealth_suit.png",
                                                                     "fancy_hat.png", "multi_vision_goggles.png" };
        /// <summary>
        /// Get Health
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static int GetHealth(int level)
        {
            // Roll the Dice and reset the Health
            return DiceHelper.RollDice(level, 10);
        }

        /// <summary>
        /// Get A Random Difficulty
        /// </summary>
        /// <returns></returns>
        public static string GetMonsterUniqueItem()
        {
            var result = ItemIndexViewModel.Instance.Dataset.ElementAt(DiceHelper.RollDice(1, ItemIndexViewModel.Instance.Dataset.Count()) - 1).Id;

            return result;
        }

        /// <summary>
        /// Get A Random Difficulty
        /// </summary>
        /// <returns></returns>
        public static DifficultyEnum GetMonsterDifficultyValue()
        {
            var DifficultyList = DifficultyEnumHelper.GetListMonster;

            var RandomDifficulty = DifficultyList.ElementAt(DiceHelper.RollDice(1, DifficultyList.Count()) - 1);

            var result = DifficultyEnumHelper.ConvertStringToEnum(RandomDifficulty);

            return result;
        }

        /// <summary>
        /// Get Random Image
        /// </summary>
        /// <returns></returns>
        public static string GetMonsterImage()
        {

            List<String> FirstNameList = new List<String> { "filler_bodyguard.png", "filler_bounty_hunter.png", "filler_godfather.png", "filler_guard.png", "filler_henchman.png", "filler_hitman.png", "filler_mastermind.png" };

            var result = FirstNameList.ElementAt(DiceHelper.RollDice(1, FirstNameList.Count()) - 1);

            return result;
        }

        /// <summary>
        /// Get Random Image
        /// </summary>
        /// <returns></returns>
        public static string GetCharacterImage()
        {

            List<String> FirstNameList = new List<String> { "filler_assassin.png", "filler_detective.png", "filler_doubleo.png", "filler_hacker.png", "filler_saboteur.png", "filler_spy.png", "filler_survey_officer.png" };

            var result = FirstNameList.ElementAt(DiceHelper.RollDice(1, FirstNameList.Count()) - 1);

            return result;
        }

        /// <summary>
        /// Get Name
        /// 
        /// Return a Random Name
        /// </summary>
        /// <returns></returns>
        public static string GetMonsterName()
        {

            List<String> FirstNameList = new List<String> { "Bubbles", "Sir Pounce", "Lady Whiskers", "Taz", "Oreo", "Luna", "Paws", "Toby", "Nala", "Loki", "Doja",
                "Simba", "Tiger", "Tigger", "Boots", "Peanut", "Bagheera"};

            var result = FirstNameList.ElementAt(DiceHelper.RollDice(1, FirstNameList.Count()) - 1);

            return result;
        }

        /// <summary>
        /// Get Description
        /// 
        /// Return a random description
        /// </summary>
        /// <returns></returns>
        public static string GetMonsterDescription()
        {
            List<String> StringList = new List<String> { "Milk Drinker", "Yarn Thrower", "Mouse Catcher", "The Mouser",
                "Likes long naps", "Hates mondays", "On their last life" };

            var result = StringList.ElementAt(DiceHelper.RollDice(1, StringList.Count()) - 1);

            return result;
        }

        /// <summary>
        /// Get Name
        /// 
        /// Return a Random Name
        /// </summary>
        /// <returns></returns>
        public static string GetCharacterName()
        {

            List<String> FirstNameList = new List<String> { "Mike", "Doug", "Jea", "Sue", "Tim", "Daren", "Dani", "Mami", "Mari", "Ryu", "Hucky", "Peanut", "Sumi", "Apple", "Ami", "Honami", "Sonomi", "Pat", "Sakue", "Isamu" };

            var result = FirstNameList.ElementAt(DiceHelper.RollDice(1, FirstNameList.Count()) - 1);

            return result;
        }

        /// <summary>
        /// Get CodeName
        /// 
        /// Return a Random Name
        /// </summary>
        /// <returns></returns>
        public static string GetCharacterCodeName()
        {

            List<String> CodeNameList = new List<String> { "Roquefort", "Camembert", "Cotija", "Chèvre", "Feta", "Mozzarella", "Emmental", "Cheddar", "Gouda", "Taleggio", "Parmigiano", "Manchego", "HouseMouse", "Fondue", "Colby", "Swiss", "Stilton", "Mascarpone", "Pecorino", "Blue", "Beaufort", "Provolone", "Kraft", "Velveeta", "Cheezit", "Havarti", "Brie", "American" };

            var result = CodeNameList.ElementAt(DiceHelper.RollDice(1, CodeNameList.Count()) - 1);

            return result;
        }


        /// <summary>
        /// Get Description
        /// 
        /// Return a random description
        /// </summary>
        /// <returns></returns>
        public static string GetCharacterDescription()
        {
            List<String> StringList = new List<String> { "the terrible", "the awesome", "the lost", "the old", "the younger", "the quiet", "the loud", "the helpless", "the happy", "the sleepy", "the angry", "the clever" };

            var result = StringList.ElementAt(DiceHelper.RollDice(1, StringList.Count()) - 1);

            return result;
        }

        /// <summary>
        /// Returns a random job
        /// </summary>
        /// <returns></returns>
        public static string GetCharacterJob()
        {
            //Get all the jobs
            var JobList = CharacterJobEnumHelper.GetJobList;

            if (JobList.Count == 0)
            {
                return null;
            }

            //return a random item from the list
            var result = JobList.ElementAt(DiceHelper.RollDice(1, JobList.Count()) - 1);
            return result;
        }

        /// <summary>
        /// Returns a random monster job
        /// </summary>
        /// <returns></returns>
        public static string GetMonsterJob()
        {
            //Get all the jobs
            var JobList = MonsterJobEnumHelper.GetJobList;

            if (JobList.Count == 0)
            {
                return null;
            }

            //return a random item from the list
            var result = JobList.ElementAt(DiceHelper.RollDice(1, JobList.Count()) - 1);
            return result;
        }

        /// <summary>
        /// Get Random Ability Number
        /// </summary>
        /// <returns></returns>
        public static int GetAbilityValue()
        {
            // 0 to 9, not 1-10
            return DiceHelper.RollDice(1, 10) - 1;
        }

        /// <summary>
        /// Get a Random Level
        /// </summary>
        /// <returns></returns>
        public static int GetLevel()
        {
            // 1-20
            return DiceHelper.RollDice(1, 20);
        }

        /// <summary>
        /// Get a Random Item for the Location
        /// 
        /// Return the String for the ID
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static string GetItem(ItemLocationEnum location)
        {
            var ItemList = ItemIndexViewModel.Instance.GetLocationItems(location);
            if (ItemList.Count == 0)
            {
                return null;
            }

            // Add None to the list
            ItemList.Add(new ItemModel { Id = null, Name = "None" });

            var result = ItemList.ElementAt(DiceHelper.RollDice(1, ItemList.Count()) - 1).Id;
            return result;
        }

        /// <summary>
        /// Create Random Character for the battle
        /// </summary>
        /// <param name="MaxLevel"></param>
        /// <returns></returns>
        public static CharacterModel GetRandomCharacter(int MaxLevel)
        {
            // If there are no characters in the system, return a default one
            if (CharacterIndexViewModel.Instance.Dataset.Count == 0)
            {
                return new CharacterModel();
            }

            var rnd = DiceHelper.RollDice(1, CharacterIndexViewModel.Instance.Dataset.Count);

            var result = new CharacterModel(CharacterIndexViewModel.Instance.Dataset.ElementAt(rnd - 1))
            {
                Level = DiceHelper.RollDice(1, MaxLevel),

                // Randomize Name
                Name = GetCharacterName(),
                Description = GetCharacterDescription(),

                // Randomize the Attributes
                Attack = GetAbilityValue(),
                Speed = GetAbilityValue(),
                Defense = GetAbilityValue(),

                // Randomize an Item for Location
                Head = GetItem(ItemLocationEnum.Head),
                Necklass = GetItem(ItemLocationEnum.Necklass),
                PrimaryHand = GetItem(ItemLocationEnum.PrimaryHand),
                OffHand = GetItem(ItemLocationEnum.OffHand),
                RightFinger = GetItem(ItemLocationEnum.Finger),
                LeftFinger = GetItem(ItemLocationEnum.Finger),
                Feet = GetItem(ItemLocationEnum.Feet),

                ImageURI = GetCharacterImage()
            };

            result.MaxHealth = DiceHelper.RollDice(MaxLevel, 10);

            // Level up to the new level
            _ = result.LevelUpToValue(result.Level);

            // Enter Battle at full health
            result.CurrentHealth = result.MaxHealth;

            return result;
        }

        /// <summary>
        /// Create Random Character for the battle
        /// </summary>
        /// <param name="MaxLevel"></param>
        /// <returns></returns>
        public static MonsterModel GetRandomMonster(int MaxLevel, bool Items = false)
        {
            // If there are no Monsters in the system, return a default one
            if (MonsterIndexViewModel.Instance.Dataset.Count == 0)
            {
                return new MonsterModel();
            }

            var rnd = DiceHelper.RollDice(1, MonsterIndexViewModel.Instance.Dataset.Count);

            var result = new MonsterModel(MonsterIndexViewModel.Instance.Dataset.ElementAt(rnd - 1))
            {
                Level = DiceHelper.RollDice(1, MaxLevel),
                

                // Randomize Name
                Name = GetMonsterName(),
                Description = GetMonsterDescription(),

                // Randomize the Attributes
                Attack = GetAbilityValue(),
                Speed = GetAbilityValue(),
                Defense = GetAbilityValue(),

                ImageURI = GetMonsterImage(),

                Difficulty = GetMonsterDifficultyValue()
            };

            // Adjust values based on Difficulty
            result.Attack = result.Difficulty.ToModifier(result.Attack);
            result.Defense = result.Difficulty.ToModifier(result.Defense);
            result.Speed = result.Difficulty.ToModifier(result.Speed);
            result.Level = result.Difficulty.ToModifier(result.Level);

            // Get the new Max Health
            result.MaxHealth = DiceHelper.RollDice(result.Level, 10);

            // Adjust the health, If the new Max Health is above the rule for the level, use the original
            var MaxHealthAdjusted = result.Difficulty.ToModifier(result.MaxHealth);
            if (MaxHealthAdjusted < result.Level * 10)
            {
                result.MaxHealth = MaxHealthAdjusted;
            }

            int newLevel = result.Level;
            //level up monster one level if monster is a boss
            if (result.IsMonsterBoss())
            {
                newLevel++;
            }

            // Level up to the new level
            _ = result.LevelUpToValue(newLevel);
            

            // Set ExperienceRemaining so Monsters can both use this method
            result.ExperienceRemaining = LevelTableHelper.LevelDetailsList[result.Level + 1].Experience;

            // Enter Battle at full health
            result.CurrentHealth = result.MaxHealth;

            // Monsters can have weapons too....
            if (Items)
            {
                result.Head = GetItem(ItemLocationEnum.Head);
                result.Necklass = GetItem(ItemLocationEnum.Necklass);
                result.PrimaryHand = GetItem(ItemLocationEnum.PrimaryHand);
                result.OffHand = GetItem(ItemLocationEnum.OffHand);
                result.RightFinger = GetItem(ItemLocationEnum.Finger);
                result.LeftFinger = GetItem(ItemLocationEnum.Finger);
                result.Feet = GetItem(ItemLocationEnum.Feet);
            }

            return result;
        }
    }
}