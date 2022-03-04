using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

using Game.Models;
using Game.Engine.EngineInterfaces;
using Game.Engine.EngineModels;
using Game.Engine.EngineBase;
using Game.Helpers;
using Game.ViewModels;
using Game.GameRules;


namespace Game.Engine.EngineGame
{
    /* 
     * Need to decide who takes the next turn
     * Target to Attack
     * Should Move, or Stay put (can hit with weapon range?)
     * Death
     * Manage Round...
     * 
     */

    /// <summary>
    /// Engine controls the turns
    /// 
    /// A turn is when a Character takes an action or a Monster takes an action
    /// 
    /// </summary>
    public class TurnEngine : TurnEngineBase, ITurnEngineInterface
    {
        #region Algrorithm
        // Attack or Move
        // Roll To Hit
        // Decide Hit or Miss
        // Decide Damage
        // Death
        // Drop Items
        // Turn Over
        #endregion Algrorithm

        // Hold the BaseEngine
        public new EngineSettingsModel EngineSettings = EngineSettingsModel.Instance;

        /// <summary>
        /// CharacterModel Attacks...
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public override bool TakeTurn(PlayerInfoModel Attacker)
        {
            // Choose Action.  Such as Move, Attack etc.

            // INFO: Teams, if you have other actions they would go here.

            // If the action is not set, then try to set it or use Attact

            // Based on the current action...

            // Increment Turn Count so you know what turn number

            // Save the Previous Action off

            // Reset the Action to unknown for next time

            var result = false;

            // If the action is not set, then try to set it or use Attact
            if (EngineSettings.CurrentAction == ActionEnum.Unknown)
            {
                // Set the action if one is not set
                EngineSettings.CurrentAction = DetermineActionChoice(Attacker);

                // When in doubt, attack...
                if (EngineSettings.CurrentAction == ActionEnum.Unknown)
                {
                    EngineSettings.CurrentAction = ActionEnum.Attack;
                }
            }

            switch (EngineSettings.CurrentAction)
            {
                case ActionEnum.Attack:
                    result = Attack(Attacker);
                    break;

                case ActionEnum.Ability:
                    result = UseAbility(Attacker);
                    break;

                case ActionEnum.Move:
                    result = MoveAsTurn(Attacker);
                    break;
            }

            EngineSettings.BattleScore.TurnCount++;

            // Save the Previous Action off
            EngineSettings.PreviousAction = EngineSettings.CurrentAction;

            // Reset the Action to unknown for next time
            EngineSettings.CurrentAction = ActionEnum.Unknown;

            return result;
        }

        /// <summary>
        /// Determine what Actions to do
        /// </summary>
        /// <param name="Attacker"></param>
        /// <returns></returns>
        public override ActionEnum DetermineActionChoice(PlayerInfoModel Attacker)
        {
            // If it is the characters turn, and NOT auto battle, use what was sent into the engine

            /*
             * The following is Used for Monsters, and Auto Battle Characters
             * 
             * Order of Priority
             * If can attack Then Attack
             * Next use Ability or Move
             */

            // Assume Move if nothing else happens

            // Check to see if ability is avaiable

            // See if Desired Target is within Range, and if so attack away
            // If it is the characters turn, and NOT auto battle, use what was sent into the engine
            if (Attacker.PlayerType == PlayerTypeEnum.Character)
            {
                if (EngineSettings.BattleScore.AutoBattle == false)
                {
                    return EngineSettings.CurrentAction;
                }
            }

            /*
             * The following is Used for Monsters, and Auto Battle Characters
             * 
             * Order of Priority
             * If can attack Then Attack
             * Next use Ability or Move
             */

            // Assume Move if nothing else happens
            EngineSettings.CurrentAction = ActionEnum.Move;

            // Check to see if ability is avaiable
            if (ChooseToUseAbility(Attacker))
            {
                EngineSettings.CurrentAction = ActionEnum.Ability;
                return EngineSettings.CurrentAction;
            }

            // See if Desired Target is within Range, and if so attack away
            if (EngineSettings.MapModel.IsTargetInRange(Attacker, AttackChoice(Attacker)))
            {
                EngineSettings.CurrentAction = ActionEnum.Attack;
            }

            return EngineSettings.CurrentAction;
        }

        /// <summary>
        /// Find a Desired Target
        /// Move close to them
        /// Get to move the number of Speed
        /// </summary>
        public override bool MoveAsTurn(PlayerInfoModel Attacker)
        {

            /*
             * TODO: TEAMS Work out your own move logic if you are implementing move
             * 
             * Mike's Logic
             * The monster or charcter will move to a different square if one is open
             * Find the Desired Target
             * Jump to the closest space near the target that is open
             * 
             * If no open spaces, return false
             * 
             */

            //// If the Monster the calculate the options
            //if (Attacker.PlayerType == PlayerTypeEnum.Monster)
            //{
            //    // For Attack, Choose Who

            //    // Get X, Y for Defender

            //    // Get X, Y for the Attacker

            //    // Find Location Nearest to Defender that is Open.

            //    // Get the Open Locations

            //    // Format a message to show
            //    return false;
            //}

            if (Attacker.PlayerType == PlayerTypeEnum.Monster)
            {
                // For Attack, Choose Who
                EngineSettings.CurrentDefender = AttackChoice(Attacker);

                if (EngineSettings.CurrentDefender == null)
                {
                    return false;
                }

                // Get X, Y for Defender
                var locationDefender = EngineSettings.MapModel.GetLocationForPlayer(EngineSettings.CurrentDefender);
                if (locationDefender == null)
                {
                    return false;
                }

                var locationAttacker = EngineSettings.MapModel.GetLocationForPlayer(Attacker);
                if (locationAttacker == null)
                {
                    return false;
                }

                // Find Location Nearest to Defender that is Open.

                // Get the Open Locations
                var openSquare = EngineSettings.MapModel.ReturnClosestEmptyLocation(locationDefender);

                Debug.WriteLine(string.Format("{0} moves from {1},{2} to {3},{4}", locationAttacker.Player.Name, locationAttacker.Column, locationAttacker.Row, openSquare.Column, openSquare.Row));

                EngineSettings.BattleMessagesModel.TurnMessage = Attacker.Name + " moves closer to " + EngineSettings.CurrentDefender.Name;

                return EngineSettings.MapModel.MovePlayerOnMap(locationAttacker, openSquare);
            }

            return true;
        }

        /// <summary>
        /// Decide to use an Ability or not
        /// 
        /// Set the Ability
        /// </summary>
        public override bool ChooseToUseAbility(PlayerInfoModel Attacker)
        {
            // See if healing is needed.

            // If not needed, then role dice to see if other ability should be used
            // Choose the % chance
            // Select the ability

            // Don't try

            // See if healing is needed.
            EngineSettings.CurrentActionAbility = Attacker.SelectHealingAbility();
            if (EngineSettings.CurrentActionAbility != AbilityEnum.Unknown)
            {
                EngineSettings.CurrentAction = ActionEnum.Ability;
                return true;
            }

            // If not needed, then role dice to see if other ability should be used
            // <30% chance
            if (DiceHelper.RollDice(1, 10) < 3)
            {
                EngineSettings.CurrentActionAbility = Attacker.SelectAbilityToUse();

                if (EngineSettings.CurrentActionAbility != AbilityEnum.Unknown)
                {
                    // Ability can , switch to unknown to exit
                    EngineSettings.CurrentAction = ActionEnum.Ability;
                    return true;
                }

                // No ability available
                return false;
            }

            // Don't try
            return false;
        }

        /// <summary>
        /// Use the Ability
        /// </summary>
        public override bool UseAbility(PlayerInfoModel Attacker)
        {
            EngineSettings.BattleMessagesModel.TurnMessage = Attacker.Name + " Uses Ability " + EngineSettings.CurrentActionAbility.ToMessage();
            return (Attacker.UseAbility(EngineSettings.CurrentActionAbility));
        }

        /// <summary>
        /// Attack as a Turn
        /// 
        /// Pick who to go after
        /// 
        /// Determine Attack Score
        /// Determine DefenseScore
        /// 
        /// Do the Attack
        /// 
        /// </summary>
        public override bool Attack(PlayerInfoModel Attacker)
        {
            // INFO: Teams, AttackChoice will auto pick the target, good for auto battle

            // Manage autobattle

            // Do Attack
            // INFO: Teams, AttackChoice will auto pick the target, good for auto battle
            if (EngineSettings.BattleScore.AutoBattle)
            {
                // For Attack, Choose Who
                EngineSettings.CurrentDefender = AttackChoice(Attacker);

                if (EngineSettings.CurrentDefender == null)
                {
                    return false;
                }
            }

            //if (Attacker.Name == "Doug")
            //{
            //    _ = DiceHelper.EnableForcedRolls();
            //    _ = DiceHelper.SetForcedRollValue(1);
            //    // Do Attack
            //    _ = TurnAsAttack(Attacker, EngineSettings.CurrentDefender);
            //    _ = DiceHelper.DisableForcedRolls();
            //    return true;
            //}
            // Do Attack
            _ = TurnAsAttack(Attacker, EngineSettings.CurrentDefender);

            return true;
        }

        /// <summary>
        /// Decide which to attack
        /// </summary>
        public override PlayerInfoModel AttackChoice(PlayerInfoModel data)
        {
            switch (data.PlayerType)
            {
                case PlayerTypeEnum.Monster:
                    return SelectCharacterToAttack();

                case PlayerTypeEnum.Character:
                default:
                    return SelectMonsterToAttack();
            }
        }

        /// <summary>
        /// Pick the Character to Attack
        /// </summary>
        public override PlayerInfoModel SelectCharacterToAttack()
        {
            // Select first in the list

            if (EngineSettings.PlayerList == null)
            {
                return null;
            }

            if (EngineSettings.PlayerList.Count < 1)
            {
                return null;
            }

            // Prep for switch
            var d20 = DiceHelper.RollDice(1, 20);
            PlayerInfoModel Defender;
            
            //Get a list of living characters
            var livingCharacters = EngineSettings.PlayerList.Where(m => m.Alive && m.PlayerType == PlayerTypeEnum.Character);

            switch (d20)
            {
                case int n when (n < 3):
                    //Attack the lowest health character
                    var minHealth = EngineSettings.PlayerList.Min(m => m.CurrentHealth);
                    Defender = livingCharacters
                        .Where(m => m.CurrentHealth == minHealth).FirstOrDefault();
                    break;

                case int n when (n >= 3 && n < 5):
                    // Attack the highest health character
                    var maxHealth = EngineSettings.PlayerList.Max(m => m.CurrentHealth);
                    Defender = livingCharacters
                        .Where(m => m.CurrentHealth == maxHealth).FirstOrDefault();
                    break;

                case int n when (n >= 5 && n < 7):
                    // Attack the weakest character
                    var minAttack = EngineSettings.PlayerList.Min(m => m.GetAttackTotal);
                    Defender = livingCharacters
                        .Where(m => m.GetAttackTotal == minAttack).FirstOrDefault();
                    break;

                case int n when (n >= 7 && n < 11):
                    // Attack the Strongest character
                    var maxAttack = EngineSettings.PlayerList.Max(m => m.GetAttackTotal);
                    Defender = livingCharacters
                        .Where(m => m.GetAttackTotal == maxAttack).FirstOrDefault();
                    break;

                case int n when (n >= 11 && n < 16):
                    // Attack the slowest character
                    var minSpeed = EngineSettings.PlayerList.Min(m => m.GetSpeedTotal);
                    Defender = livingCharacters
                        .Where(m => m.GetSpeedTotal == minSpeed).FirstOrDefault();
                    break;

                default:
                    //Attack the first character
                    Defender = livingCharacters.FirstOrDefault();
                    break;
            }

            return Defender;
        }

        /// <summary>
        /// Pick the Monster to Attack
        /// </summary>
        public override PlayerInfoModel SelectMonsterToAttack()
        {
            // Select first one to hit in the list for now...
            // Attack the Weakness (lowest HP) MonsterModel first 

            // TODO: Teams, You need to implement your own Logic can not use mine.

            if (EngineSettings.PlayerList == null)
            {
                return null;
            }

            if (EngineSettings.PlayerList.Count < 1)
            {
                return null;
            }

            // Select first one to hit in the list for now...
            // Attack the Weakness (lowest HP) MonsterModel first 

            // TODO: Teams, You need to implement your own Logic can not use mine.

            var Defender = EngineSettings.PlayerList
                .Where(m => m.Alive && m.PlayerType == PlayerTypeEnum.Monster)
                .OrderBy(m => m.CurrentHealth).FirstOrDefault();

            return Defender;
        }

        /// <summary>
        /// // MonsterModel Attacks CharacterModel
        /// </summary>
        public override bool TurnAsAttack(PlayerInfoModel Attacker, PlayerInfoModel Target)
        {
            if (Attacker == null)
            {
                return false;
            }

            if (Target == null)
            {
                return false;
            }

            // Set Messages to empty
            _ = EngineSettings.BattleMessagesModel.ClearMessages();

            // Do the Attack
            _ = CalculateAttackStatus(Attacker, Target);

            // See if the Battle Settings Overrides the Roll
            EngineSettings.BattleMessagesModel.HitStatus = BattleSettingsOverride(Attacker);

            switch (EngineSettings.BattleMessagesModel.HitStatus)
            {
                case HitStatusEnum.Miss:
                    // It's a Miss

                    break;

                case HitStatusEnum.CriticalMiss:
                    // It's a Critical Miss, so Bad things may happen
                    _ = DetermineCriticalMissProblem(Attacker);

                    break;

                case HitStatusEnum.CriticalHit:
                case HitStatusEnum.Hit:
                    // It's a Hit

                    //Calculate Damage
                    EngineSettings.BattleMessagesModel.DamageAmount = Attacker.GetDamageRollValue();

                    // If critical Hit, double the damage
                    if (EngineSettings.BattleMessagesModel.HitStatus == HitStatusEnum.CriticalHit)
                    {
                        EngineSettings.BattleMessagesModel.DamageAmount *= 2;
                    }

                    // Apply the Damage
                    _ = ApplyDamage(Target);

                    EngineSettings.BattleMessagesModel.TurnMessageSpecial = EngineSettings.BattleMessagesModel.GetCurrentHealthMessage();

                    // Check if Dead and Remove
                    _ = RemoveIfDead(Target);

                    // If it is a character apply the experience earned
                    _ = CalculateExperience(Attacker, Target);

                    break;
            }

            EngineSettings.BattleMessagesModel.TurnMessage = Attacker.Name + EngineSettings.BattleMessagesModel.AttackStatus + Target.Name + EngineSettings.BattleMessagesModel.TurnMessageSpecial + EngineSettings.BattleMessagesModel.ExperienceEarned;
            Debug.WriteLine(EngineSettings.BattleMessagesModel.TurnMessage);

            return true;
        }

        /// <summary>
        /// See if the Battle Settings will Override the Hit
        /// Return the Override for the HitStatus
        /// </summary>
        public override HitStatusEnum BattleSettingsOverride(PlayerInfoModel Attacker)
        {
            if (Attacker.PlayerType == PlayerTypeEnum.Monster)
            {
                return BattleSettingsOverrideHitStatusEnum(EngineSettings.BattleSettingsModel.MonsterHitEnum);
            }

            return BattleSettingsOverrideHitStatusEnum(EngineSettings.BattleSettingsModel.CharacterHitEnum);
        }

        /// <summary>
        /// Return the Override for the HitStatus
        /// </summary>
        public override HitStatusEnum BattleSettingsOverrideHitStatusEnum(HitStatusEnum myEnum)
        {
            // Based on the Hit Status, establish a message

            switch (myEnum)
            {
                case HitStatusEnum.Hit:
                    EngineSettings.BattleMessagesModel.AttackStatus = " somehow Hit ";
                    return HitStatusEnum.Hit;

                case HitStatusEnum.CriticalHit:
                    EngineSettings.BattleMessagesModel.AttackStatus = " somehow Critical Hit ";
                    return HitStatusEnum.CriticalHit;

                case HitStatusEnum.Miss:
                    EngineSettings.BattleMessagesModel.AttackStatus = " somehow Missed ";
                    return HitStatusEnum.Miss;

                case HitStatusEnum.CriticalMiss:
                    EngineSettings.BattleMessagesModel.AttackStatus = " somehow Critical Missed ";
                    return HitStatusEnum.CriticalMiss;

                case HitStatusEnum.Unknown:
                case HitStatusEnum.Default:
                default:
                    // Return what it was
                    return EngineSettings.BattleMessagesModel.HitStatus;
            }
        }

        /// <summary>
        /// Apply the Damage to the Target
        /// </summary>
        public override bool ApplyDamage(PlayerInfoModel Target)
        {
            _ = Target.TakeDamage(EngineSettings.BattleMessagesModel.DamageAmount);
            EngineSettings.BattleMessagesModel.CurrentHealth = Target.GetCurrentHealthTotal;

            return true;
        }

        /// <summary>
        /// Calculate the Attack, return if it hit or missed.
        /// </summary>
        public override HitStatusEnum CalculateAttackStatus(PlayerInfoModel Attacker, PlayerInfoModel Target)
        {
            // Remember Current Player
            EngineSettings.BattleMessagesModel.PlayerType = PlayerTypeEnum.Monster;

            // Choose who to attack
            EngineSettings.BattleMessagesModel.TargetName = Target.Name;
            EngineSettings.BattleMessagesModel.AttackerName = Attacker.Name;

            // Set Attack and Defense
            var AttackScore = Attacker.Level + Attacker.GetAttack();
            var DefenseScore = Target.GetDefense() + Target.Level;

            EngineSettings.BattleMessagesModel.HitStatus = RollToHitTarget(AttackScore, DefenseScore);

            return EngineSettings.BattleMessagesModel.HitStatus;
        }

        /// <summary>
        /// Calculate Experience
        /// Level up if needed
        /// </summary>
        public override bool CalculateExperience(PlayerInfoModel Attacker, PlayerInfoModel Target)
        {
            if (Attacker.PlayerType == PlayerTypeEnum.Character)
            {
                var points = " points";

                var experienceEarned = Target.CalculateExperienceEarned(EngineSettings.BattleMessagesModel.DamageAmount);

                if (experienceEarned == 1)
                {
                    points = " point";
                }

                EngineSettings.BattleMessagesModel.ExperienceEarned = " Earned " + experienceEarned + points;

                var LevelUp = Attacker.AddExperience(experienceEarned);
                if (LevelUp)
                {
                    EngineSettings.BattleMessagesModel.LevelUpMessage = Attacker.Name + " is now Level " + Attacker.Level + " With Health Max of " + Attacker.GetMaxHealthTotal;
                    Debug.WriteLine(EngineSettings.BattleMessagesModel.LevelUpMessage);
                }

                // Add Experinece to the Score
                EngineSettings.BattleScore.ExperienceGainedTotal += experienceEarned;
            }

            return true;
        }

        /// <summary>
        /// If Dead process Target Died
        /// </summary>
        public override bool RemoveIfDead(PlayerInfoModel Target)
        {
            // Check for alive
            if (Target.Alive == false)
            {
                _ = TargetDied(Target);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Target Died
        /// 
        /// Process for death...
        /// 
        /// Returns the count of items dropped at death
        /// </summary>
        public override bool TargetDied(PlayerInfoModel Target)
        {
            // Mark Status in output

            // Removing the 

            // INFO: Teams, Hookup your Boss if you have one...

            // Using a switch so in the future additional PlayerTypes can be added (Boss...)
            // Add the Character to the killed list

            // Add one to the monsters killed count...

            // Add the MonsterModel to the killed list

            bool found;

            // Mark Status in output
            EngineSettings.BattleMessagesModel.TurnMessageSpecial = " and causes death. ";

            // Removing the 
            _ = EngineSettings.MapModel.RemovePlayerFromMap(Target);

            // INFO: Teams, Hookup your Boss if you have one...

            // Using a switch so in the future additional PlayerTypes can be added (Boss...)
            switch (Target.PlayerType)
            {
                case PlayerTypeEnum.Character:
                    // Add the Character to the killed list
                    EngineSettings.BattleScore.CharacterAtDeathList += Target.FormatOutput() + "\n";

                    EngineSettings.BattleScore.CharacterModelDeathList.Add(Target);

                    _ = DropItems(Target);

                    found = EngineSettings.CharacterList.Remove(EngineSettings.CharacterList.Find(m => m.Guid.Equals(Target.Guid)));
                    found = EngineSettings.PlayerList.Remove(EngineSettings.PlayerList.Find(m => m.Guid.Equals(Target.Guid)));

                    return true;

                case PlayerTypeEnum.Monster:
                default:
                    // Add one to the monsters killed count...
                    EngineSettings.BattleScore.MonsterSlainNumber++;

                    // Add the MonsterModel to the killed list
                    EngineSettings.BattleScore.MonstersKilledList += Target.FormatOutput() + "\n";

                    EngineSettings.BattleScore.MonsterModelDeathList.Add(Target);

                    _ = DropItems(Target);

                    found = EngineSettings.MonsterList.Remove(EngineSettings.MonsterList.Find(m => m.Guid.Equals(Target.Guid)));
                    found = EngineSettings.PlayerList.Remove(EngineSettings.PlayerList.Find(m => m.Guid.Equals(Target.Guid)));

                    return true;
            }
        }

        /// <summary>
        /// Drop Items
        /// </summary>
        public override int DropItems(PlayerInfoModel Target)
        {
            // Drop Items to ItemModel Pool

            // I feel generous, even when characters die, random drops happen :-)
            // If Random drops are enabled, then add some....

            // Add to ScoreModel

            var DroppedMessage = "\nItems Dropped : \n";

            // Drop Items to ItemModel Pool
            var myItemList = Target.DropAllItems();

            // I feel generous, even when characters die, random drops happen :-)
            // If Random drops are enabled, then add some....
            myItemList.AddRange(GetRandomMonsterItemDrops(EngineSettings.BattleScore.RoundCount));

            // Add to ScoreModel
            foreach (var ItemModel in myItemList)
            {
                EngineSettings.BattleScore.ItemsDroppedList += ItemModel.FormatOutput() + "\n";
                DroppedMessage += ItemModel.Name + "\n";
            }

            EngineSettings.ItemPool.AddRange(myItemList);

            if (myItemList.Count == 0)
            {
                DroppedMessage = " Nothing dropped. ";
            }

            EngineSettings.BattleMessagesModel.DroppedMessage = DroppedMessage;

            EngineSettings.BattleScore.ItemModelDropList.AddRange(myItemList);

            return myItemList.Count();
        }

        /// <summary>
        /// Roll To Hit
        /// </summary>
        /// <param name="AttackScore"></param>
        /// <param name="DefenseScore"></param>
        public override HitStatusEnum RollToHitTarget(int AttackScore, int DefenseScore)
        {
            var d20 = DiceHelper.RollDice(1, 20);

            if (d20 == 1)
            {
                EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.Miss;
                EngineSettings.BattleMessagesModel.AttackStatus = " rolls 1 to miss ";

                if (EngineSettings.BattleSettingsModel.AllowCriticalMiss)
                {
                    EngineSettings.BattleMessagesModel.AttackStatus = " rolls 1 to completly miss ";
                    EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.CriticalMiss;
                }

                return EngineSettings.BattleMessagesModel.HitStatus;
            }

            if (d20 == 20)
            {
                EngineSettings.BattleMessagesModel.AttackStatus = " rolls 20 for hit ";
                EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.Hit;

                if (EngineSettings.BattleSettingsModel.AllowCriticalHit)
                {
                    EngineSettings.BattleMessagesModel.AttackStatus = " rolls 20 for lucky hit ";
                    EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.CriticalHit;
                }
                return EngineSettings.BattleMessagesModel.HitStatus;
            }

            var ToHitScore = d20 + AttackScore;
            if (ToHitScore < DefenseScore)
            {
                EngineSettings.BattleMessagesModel.AttackStatus = " rolls " + d20 + " and misses ";

                // Miss
                EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.Miss;
                EngineSettings.BattleMessagesModel.DamageAmount = 0;
                return EngineSettings.BattleMessagesModel.HitStatus;
            }

            EngineSettings.BattleMessagesModel.AttackStatus = " rolls " + d20 + " and hits ";

            // Hit
            EngineSettings.BattleMessagesModel.HitStatus = HitStatusEnum.Hit;
            return EngineSettings.BattleMessagesModel.HitStatus;
        }

        /// <summary>
        /// Will drop between 1 and 4 items from the ItemModel set...
        /// </summary>
        public override List<ItemModel> GetRandomMonsterItemDrops(int round)
        {
            // You decide how to drop monster items, level, etc.

            // The Number drop can be Up to the Round Count, but may be less.  
            // Negative results in nothing dropped

            var levelToScaleTo = round;
            var NumberToDrop = (DiceHelper.RollDice(1, round) - 1);

            if (EngineSettings.CurrentDefender.isABoss)
            {
                NumberToDrop = (DiceHelper.RollDice(1, round) + 1);
                levelToScaleTo++;
            }
            
            var result = new List<ItemModel>();

            for (var i = 0; i < NumberToDrop; i++)
            {
                // Get a random Unique Item
                var data = ItemIndexViewModel.Instance.GetItem(RandomPlayerHelper.GetMonsterUniqueItem());
                data.ScaleLevel(levelToScaleTo);
                result.Add(data);
            }

            return result;
        }

        /// <summary>
        /// Critical Miss Problem
        /// </summary>
        public override bool DetermineCriticalMissProblem(PlayerInfoModel attacker)
        {
            return false;
            //var d10 = DiceHelper.RollDice(1, 10);

            //var droppedItem = new List<ItemModel>();

            //// Drop all Items
            //ItemModel myItem;


            //if (d10 == 1)
            //{
            //    attacker.RemoveItem(ItemLocationEnum.PrimaryHand); 
            //    return true;
            //}

            //if (d10 < 5)
            //{
            //    if (attacker.PlayerType == PlayerTypeEnum.Character)
            //    {
            //        myItem = attacker.RemoveItem(ItemLocationEnum.PrimaryHand);
            //        if (myItem != null)
            //        {
            //            droppedItem.Add(myItem);
            //        }

            //        EngineSettings.ItemPool.AddRange(droppedItem);
            //        return true;
            //    }

            //    myItem = attacker.RemoveItem(ItemLocationEnum.PrimaryHand);
            //    if (myItem != null)
            //    {
            //        droppedItem.Add(myItem);
            //    }

            //    if (myItem == null)
            //    {
            //        var data = ItemIndexViewModel.Instance.GetItem(RandomPlayerHelper.GetMonsterUniqueItem());
            //        droppedItem.Add(data);
            //    }

            //    EngineSettings.ItemPool.AddRange(droppedItem);
            //    return true;

            //}    

            //if (d10 < 7)
            //{
            //    var d7 = DiceHelper.RollDice(1, 7);

            //    bool foundItem = false;
            //    int originalNum = d7;
            //    int looped = 0;

            //    if (attacker.PlayerType == PlayerTypeEnum.Character)
            //    {
            //        while(!foundItem)
            //        {
            //            if (d7 == 1)
            //            {
            //                myItem = attacker.RemoveItem(ItemLocationEnum.Head);
            //                if (myItem != null)
            //                {
            //                    droppedItem.Add(myItem);
            //                    EngineSettings.ItemPool.AddRange(droppedItem);
            //                    return true;
            //                }
            //            }
            //            if(d7 == 2)
            //            {
            //                myItem = attacker.RemoveItem(ItemLocationEnum.Necklass);
            //                if (myItem != null)
            //                {
            //                    droppedItem.Add(myItem);
            //                    EngineSettings.ItemPool.AddRange(droppedItem);
            //                    return true;
            //                }
            //            }
            //            if (d7 == 3)
            //            {
            //                myItem = attacker.RemoveItem(ItemLocationEnum.PrimaryHand);
            //                if (myItem != null)
            //                {
            //                    droppedItem.Add(myItem);
            //                    EngineSettings.ItemPool.AddRange(droppedItem);
            //                    return true;
            //                }
            //            }
            //            if (d7 == 4)
            //            {
            //                myItem = attacker.RemoveItem(ItemLocationEnum.OffHand);
            //                if (myItem != null)
            //                {
            //                    droppedItem.Add(myItem);
            //                    EngineSettings.ItemPool.AddRange(droppedItem);
            //                    return true;
            //                }
            //            }
            //            if (d7 == 5)
            //            {
            //                myItem = attacker.RemoveItem(ItemLocationEnum.RightFinger);
            //                if (myItem != null)
            //                {
            //                    droppedItem.Add(myItem);
            //                    EngineSettings.ItemPool.AddRange(droppedItem);
            //                    return true;
            //                }
            //            }
            //            if (d7 == 6)
            //            {
            //                myItem = attacker.RemoveItem(ItemLocationEnum.LeftFinger);
            //                if (myItem != null)
            //                {
            //                    droppedItem.Add(myItem);
            //                    EngineSettings.ItemPool.AddRange(droppedItem);
            //                    return true;
            //                }
            //            }
            //            if (d7 == 7)
            //            {
            //                myItem = attacker.RemoveItem(ItemLocationEnum.Feet);
            //                if (myItem != null)
            //                {
            //                    droppedItem.Add(myItem);
            //                    EngineSettings.ItemPool.AddRange(droppedItem);
            //                    return true;
            //                }

            //                d7 = 1;
            //                looped = 1;
            //            }
            //            if(d7 == originalNum && looped == 1)
            //            {
            //                return true;
            //            }

            //            d7++;
            //        }
            //    }
            //    while (!foundItem)
            //    {
            //        if (d7 == 1)
            //        {
            //            myItem = attacker.RemoveItem(ItemLocationEnum.Head);
            //            if (myItem != null)
            //            {
            //                droppedItem.Add(myItem);
            //                EngineSettings.ItemPool.AddRange(droppedItem);
            //                return true;
            //            }
            //        }
            //        if (d7 == 2)
            //        {
            //            myItem = attacker.RemoveItem(ItemLocationEnum.Necklass);
            //            if (myItem != null)
            //            {
            //                droppedItem.Add(myItem);
            //                EngineSettings.ItemPool.AddRange(droppedItem);
            //                return true;
            //            }
            //        }
            //        if (d7 == 3)
            //        {
            //            myItem = attacker.RemoveItem(ItemLocationEnum.PrimaryHand);
            //            if (myItem != null)
            //            {
            //                droppedItem.Add(myItem);
            //                EngineSettings.ItemPool.AddRange(droppedItem);
            //                return true;
            //            }
            //        }
            //        if (d7 == 4)
            //        {
            //            myItem = attacker.RemoveItem(ItemLocationEnum.OffHand);
            //            if (myItem != null)
            //            {
            //                droppedItem.Add(myItem);
            //                EngineSettings.ItemPool.AddRange(droppedItem);
            //                return true;
            //            }
            //        }
            //        if (d7 == 5)
            //        {
            //            myItem = attacker.RemoveItem(ItemLocationEnum.RightFinger);
            //            if (myItem != null)
            //            {
            //                droppedItem.Add(myItem);
            //                EngineSettings.ItemPool.AddRange(droppedItem);
            //                return true;
            //            }
            //        }
            //        if (d7 == 6)
            //        {
            //            myItem = attacker.RemoveItem(ItemLocationEnum.LeftFinger);
            //            if (myItem != null)
            //            {
            //                droppedItem.Add(myItem);
            //                EngineSettings.ItemPool.AddRange(droppedItem);
            //                return true;
            //            }
            //        }
            //        if (d7 == 7)
            //        {
            //            myItem = attacker.RemoveItem(ItemLocationEnum.Feet);
            //            if (myItem != null)
            //            {
            //                droppedItem.Add(myItem);
            //                EngineSettings.ItemPool.AddRange(droppedItem);
            //                return true;
            //            }

            //            d7 = 1;
            //            looped = 1;
            //        }
            //        if (d7 == originalNum && looped == 1)
            //        {
            //            var data = ItemIndexViewModel.Instance.GetItem(RandomPlayerHelper.GetMonsterUniqueItem());
            //            droppedItem.Add(data);
            //            EngineSettings.ItemPool.AddRange(droppedItem);
            //            return true;
            //        }

            //        d7++;
            //    }
            //}
            //return true;
        }
    }
}