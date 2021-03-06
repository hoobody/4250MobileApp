using System.Collections.Generic;

using Game.Models;
using Game.ViewModels;

namespace Game.GameRules
{
    public static class DefaultData
    {
        /// <summary>
        /// Load the Default data
        /// </summary>
        /// <returns></returns>
        public static List<ItemModel> LoadData(ItemModel temp)
        {
            var datalist = new List<ItemModel>()
            {
                new ItemModel {
                    Name = "Sniper",
                    Description = "Range is your friend",
                    ImageURI = "sniper_item.png",
                    Range = 10,
                    Damage = 8,
                    Value = 8,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Silent Stinky Cheese Gun",
                    Description = "Silent, but deadly",
                    ImageURI = "stinky_gun.png",
                    Range = 5,
                    Damage = 3,
                    Value = 3,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Water Gun",
                    Description = "A potent liquid solution",
                    ImageURI = "water_gun.png",
                    Range = 4,
                    Damage = 6,
                    Value = 6,
                    Location = ItemLocationEnum.PrimaryHand,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Lazer Ring",
                    Description = "Excellent distraction tool",
                    ImageURI = "lazer_ring.png",
                    Range = 1,
                    Damage = 1,
                    Value = 1,
                    Location = ItemLocationEnum.Finger,
                    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Speedy Shoes",
                    Description = "Gotta go fast",
                    ImageURI = "filler_speed_shoes.png",
                    Range = 0,
                    Damage = 0,
                    Value = 4,
                    Location = ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Armored Boots",
                    Description = "These boots have been reinforced",
                    ImageURI = "filler_armor_shoes.png",
                    Range = 0,
                    Damage = 0,
                    Value = 2,
                    Location = ItemLocationEnum.Feet,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Trench Coat",
                    Description = "Some say you can stack 3 mice inside and pass off as a cat",
                    ImageURI = "trench_coat.png",
                    Range = 0,
                    Damage = 0,
                    Value = 2,
                    Location = ItemLocationEnum.Necklass,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Stealth Suit",
                    Description = "Hard to focus when looking directly at it",
                    ImageURI = "stealth_suit.png",
                    Range = 0,
                    Damage = 0,
                    Value = 8,
                    Location = ItemLocationEnum.Necklass,
                    Attribute = AttributeEnum.Speed},

                new ItemModel {
                    Name = "Hat",
                    Description = "This is a fancy hat",
                    ImageURI = "fancy_hat.png",
                    Range = 0,
                    Damage = 0,
                    Value = 1,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Defense},

                new ItemModel {
                    Name = "Multi-Vision Goggles",
                    Description = "Night, infrared, and heat vision. Sam Fisher would be proud ",
                    ImageURI = "multi_vision_goggles.png",
                    Range = 0,
                    Damage = 0,
                    Value = 6,
                    Location = ItemLocationEnum.Head,
                    Attribute = AttributeEnum.Speed},

                //new ItemModel {
                //    Name = "Spy watch",
                //    Description = "What the cool spy kids wear these days",
                //    ImageURI = "item.png",
                //    Range = 0,
                //    Damage = 0,
                //    Value = 3,
                //    Location = ItemLocationEnum.OffHand,
                //    Attribute = AttributeEnum.Speed},

                //new ItemModel {
                //    Name = "Exploding Pen",
                //    Description = "Click 3 times and then run fast",
                //    ImageURI = "item.png",
                //    Range = 1,
                //    Damage = 2,
                //    Value = 2,
                //    Location = ItemLocationEnum.OffHand,
                //    Attribute = AttributeEnum.Attack},

                new ItemModel {
                    Name = "Brass Knuckles",
                    Description = "Brass. Knuckles.",
                    ImageURI = "brass_knuckles.png",
                    Range = 1,
                    Damage = 2,
                    Value = 2,
                    Location = ItemLocationEnum.OffHand,
                    Attribute = AttributeEnum.Attack},

                //new ItemModel {
                //    Name = "Dagger",
                //    Description = "watch out",
                //    ImageURI = "sword3.png",
                //    Range = 0,
                //    Damage = 6,
                //    Value = 9,
                //    Location = ItemLocationEnum.PrimaryHand,
                //    Attribute = AttributeEnum.Attack},

                //new ItemModel {
                //    Name = "Strong Sword",
                //    Description = "watch out",
                //    ImageURI = "sword4.png",
                //    Range = 0,
                //    Damage = 12,
                //    Value = 9,
                //    Location = ItemLocationEnum.PrimaryHand,
                //    Attribute = AttributeEnum.Attack},

                //new ItemModel {
                //    Name = "Wand",
                //    Description = "watch out",
                //    ImageURI = "sword5.png",
                //    Range = 0,
                //    Damage = 4,
                //    Value = 9,
                //    Location = ItemLocationEnum.PrimaryHand,
                //    Attribute = AttributeEnum.Defense},

                //new ItemModel {
                //    Name = "Mace",
                //    Description = "watch out",
                //    ImageURI = "sword6.png",
                //    Range = 0,
                //    Damage = 6,
                //    Value = 9,
                //    Location = ItemLocationEnum.PrimaryHand,
                //    Attribute = AttributeEnum.Speed},

                //new ItemModel {
                //    Name = "Mace of Health",
                //    Description = "Feeling Good",
                //    ImageURI = "sword7.png",
                //    Range = 0,
                //    Damage = 6,
                //    Value = 9,
                //    Location = ItemLocationEnum.PrimaryHand,
                //    Attribute = AttributeEnum.CurrentHealth},

                //new ItemModel {
                //    Name = "Arrows",
                //    Description = "Poke your eye out",
                //    ImageURI = "sword8.png",
                //    Range = 10,
                //    Damage = 10,
                //    Value = 9,
                //    Location = ItemLocationEnum.PrimaryHand,
                //    Attribute = AttributeEnum.Attack},

                //new ItemModel {
                //    Name = "Boxing",
                //    Description = "watch out",
                //    ImageURI = "sword9.png",
                //    Range = 0,
                //    Damage = 6,
                //    Value = 9,
                //    Location = ItemLocationEnum.PrimaryHand,
                //    Attribute = AttributeEnum.Attack},

                //new ItemModel {
                //    Name = "Bow",
                //    Description = "Fast Bow",
                //    ImageURI = "sword10.png",
                //    Range = 10,
                //    Damage = 10,
                //    Value = 9,
                //    Location = ItemLocationEnum.PrimaryHand,
                //    Attribute = AttributeEnum.Attack},

                //new ItemModel {
                //    Name = "Fire Bow",
                //    Description = "Fast Bow",
                //    ImageURI = "sword11.png",
                //    Range = 10,
                //    Damage = 10,
                //    Value = 9,
                //    Location = ItemLocationEnum.PrimaryHand,
                //    Attribute = AttributeEnum.Attack},

                //new ItemModel {
                //    Name = "Strong Shield",
                //    Description = "Enough to hide behind",
                //    ImageURI = "shield1.png",
                //    Range = 0,
                //    Damage = 0,
                //    Value = 9,
                //    Location = ItemLocationEnum.OffHand,
                //    Attribute = AttributeEnum.Defense},

                //new ItemModel {
                //    Name = "Fancy Shield",
                //    Description = "Enough to hide behind",
                //    ImageURI = "shield2.png",
                //    Range = 0,
                //    Damage = 0,
                //    Value = 9,
                //    Location = ItemLocationEnum.OffHand,
                //    Attribute = AttributeEnum.Defense},

                //new ItemModel {
                //    Name = "Health Shield",
                //    Description = "Enough to hide behind",
                //    ImageURI = "shield3.png",
                //    Range = 0,
                //    Damage = 0,
                //    Value = 9,
                //    Location = ItemLocationEnum.OffHand,
                //    Attribute = AttributeEnum.MaxHealth},

                //new ItemModel {
                //    Name = "Lucky Shield",
                //    Description = "Do you feel lucky punk?",
                //    ImageURI = "shield4.png",
                //    Range = 0,
                //    Damage = 0,
                //    Value = 9,
                //    Location = ItemLocationEnum.OffHand,
                //    Attribute = AttributeEnum.Attack},

                //new ItemModel {
                //    Name = "Bunny Hat",
                //    Description = "Pink hat with fluffy ears",
                //    ImageURI = "hat1.png",
                //    Range = 0,
                //    Damage = 0,
                //    Value = 9,
                //    Location = ItemLocationEnum.Head,
                //    Attribute = AttributeEnum.Speed},

                //new ItemModel {
                //    Name = "Horned Hat",
                //    Description = "Where's the Rabbit?",
                //    ImageURI = "hat2.png",
                //    Range = 0,
                //    Damage = 0,
                //    Value = 9,
                //    Location = ItemLocationEnum.Head,
                //    Attribute = AttributeEnum.Defense},

                //new ItemModel {
                //    Name = "Fast Necklass",
                //    Description = "And Tasty",
                //    ImageURI = "neck1.png",
                //    Range = 0,
                //    Damage = 0,
                //    Value = 9,
                //    Location = ItemLocationEnum.Necklass,
                //    Attribute = AttributeEnum.Speed},

                //new ItemModel {
                //    Name = "Feel the Power",
                //    Description = "Love this one",
                //    ImageURI = "neck2.png",
                //    Range = 0,
                //    Damage = 0,
                //    Value = 9,
                //    Location = ItemLocationEnum.Necklass,
                //    Attribute = AttributeEnum.Attack},

                //new ItemModel {
                //    Name = "Horned Hat",
                //    Description = "Where's the Rabbit?",
                //    ImageURI = "hat2.png",
                //    Range = 0,
                //    Damage = 0,
                //    Value = 9,
                //    Location = ItemLocationEnum.Head,
                //    Attribute = AttributeEnum.Defense},

                //new ItemModel {
                //    Name = "Ring of Power",
                //    Description = "The wearer has all the power",
                //    ImageURI = "ring1.png",
                //    Range = 0,
                //    Damage = 0,
                //    Value = 9,
                //    Location = ItemLocationEnum.Finger,
                //    Attribute = AttributeEnum.Speed},

                //new ItemModel {
                //    Name = "Strong Ring",
                //    Description = "Watch out",
                //    ImageURI = "ring2.png",
                //    Range = 0,
                //    Damage = 0,
                //    Value = 9,
                //    Location = ItemLocationEnum.Finger,
                //    Attribute = AttributeEnum.Attack},

                //new ItemModel {
                //    Name = "Warm Shoes",
                //    Description = "Strong Shoes",
                //    ImageURI = "feet1.png",
                //    Range = 0,
                //    Damage = 0,
                //    Value = 9,
                //    Location = ItemLocationEnum.Feet,
                //    Attribute = AttributeEnum.Attack},

                //new ItemModel {
                //    Name = "Cute Shoes",
                //    Description = "really fast",
                //    ImageURI = "feet2.png",
                //    Range = 0,
                //    Damage = 0,
                //    Value = 9,
                //    Location = ItemLocationEnum.Feet,
                //    Attribute = AttributeEnum.Speed},
            };

            return datalist;
        }

        /// <summary>
        /// Load Example Scores
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static List<ScoreModel> LoadData(ScoreModel temp)
        {
            var datalist = new List<ScoreModel>()
            {
                new ScoreModel {
                    Name = "Jesse",
                    Description = "Test Data",
                    ScoreTotal=1000000,
                    RoundCount=500,
                },

                new ScoreModel {
                    Name = "Caroline",
                    Description = "Test Data",
                    ScoreTotal=900000,
                    RoundCount=400,
                },

                new ScoreModel {
                    Name = "Jacob",
                    Description = "Test Data",
                    ScoreTotal=600000,
                    RoundCount=300,
                },

                new ScoreModel {
                    Name = "Elizabeth",
                    Description = "Test Data",
                    ScoreTotal=400000,
                    RoundCount=200,
                },

                new ScoreModel {
                    Name = "Jonathan",
                    Description = "Test Data",
                    ScoreTotal=200000,
                    RoundCount=100,
                },
            };

            return datalist;
        }

        /// <summary>
        /// Load Characters
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static List<CharacterModel> LoadData(CharacterModel temp)
        {
            var HeadString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Head);
            var NecklassString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Necklass);
            var PrimaryHandString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.PrimaryHand);
            var OffHandString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.OffHand);
            var FeetString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Feet);
            var RightFingerString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Finger);
            var LeftFingerString = ItemIndexViewModel.Instance.GetDefaultItemId(ItemLocationEnum.Finger);

            var datalist = new List<CharacterModel>()
            {
                new CharacterModel {
                    Name = "James Boccini",
                    Description = "The mouse who loved me",
                    CodeName = "00Cheese",
                    Level = 6,
                    MaxHealth = 45,
                    ImageURI = "character_doubleO.png",
                    HeadshotImageURI = "character_doubleO_headshot.png",
                    HeadshotGIFURI = "character_doubleO_headshot.gif",
                    Job = CharacterJobEnum.Double0,
                    Attack = 4,
                    Defense = 1,
                    Speed = 2,
                },

                new CharacterModel {
                    Name = "Sherlock Holes",
                    Description = "Indubitably",
                    CodeName = "Swiss",
                    Level = 4,
                    MaxHealth = 33,
                    ImageURI = "character_detective.png",
                    HeadshotImageURI = "character_detective_headshot.png",
                    HeadshotGIFURI = "character_detective_headshot.png",
                    Job = CharacterJobEnum.Detective,
                    Attack = 1,
                    Defense = 3,
                    Speed = 3,
                },

                new CharacterModel {
                    Name = "Feta vision",
                    Description = "He's got his eyes on you",
                    CodeName = "Feta",
                    Level = 2,
                    MaxHealth = 13,
                    ImageURI = "character_surveillanceofficer.png",
                    HeadshotImageURI = "character_surveillanceofficer_headshot.png",
                    HeadshotGIFURI = "character_surveillanceofficer_headshot.gif",
                    Job = CharacterJobEnum.SurveillanceOfficer,
                    Attack = 2,
                    Defense = 2,
                    Speed = 2,
                },

                new CharacterModel {
                    Name = "Cheddar Kills-a-lot",
                    Description = "It's in the name",
                    CodeName = "Cheddar",
                    Level = 5,
                    MaxHealth = 39,
                    ImageURI = "character_assassin.png",
                    HeadshotImageURI = "character_assassin_headshot.png",
                    HeadshotGIFURI = "character_assassin_headshot.gif",
                    Job = CharacterJobEnum.Assassin,
                    Attack = 4,
                    Defense = 0,
                    Speed = 2,
                },

                new CharacterModel {
                    Name = "Moonlight Gouda",
                    Description = "There's always a backdoor",
                    CodeName = "Gouda",
                    Level = 3,
                    MaxHealth = 29,
                    ImageURI = "character_hacker.png",
                    HeadshotImageURI = "character_hacker_headshot.png",
                    HeadshotGIFURI = "character_hacker_headshot.gif",
                    Job = CharacterJobEnum.Hacker,
                    Attack = 0,
                    Defense = 2,
                    Speed = 4,
                },

                new CharacterModel {
                    Name = "Perry 'Problems' Parmesan",
                    Description = "Always up to no good",
                    CodeName = "Parmesan",
                    Level = 1,
                    MaxHealth = 7,
                    ImageURI = "character_saboteur.png",
                    HeadshotImageURI = "character_saboteur_headshot.png",
                    HeadshotGIFURI = "character_saboteur_headshot.gif",
                    Job = CharacterJobEnum.Saboteur,
                    Attack = 2,
                    Defense = 0,
                    Speed = 3,
                },

                new CharacterModel {
                    Name = "Agent Mozzarella",
                    Description = "One of mice-kinds finest",
                    CodeName = "Mozzarella",
                    Level = 7,
                    MaxHealth = 50,
                    ImageURI = "character_spy.png",
                    HeadshotImageURI = "character_spy_headshot.png",
                    HeadshotGIFURI = "character_spy_headshot.gif",
                    Job = CharacterJobEnum.Spy,
                    Attack = 3,
                    Defense = 0,
                    Speed = 2,
                },

                 new CharacterModel {
                    Name = "Miss Bianca",
                    Description = "Rescue Aid Societies best agent",
                    CodeName = "Mascarpone",
                    Level = 5,
                    MaxHealth = 40,
                    ImageURI = "character_mozhnogirl.png",
                    HeadshotImageURI = "character_mozhnogirl_headshot.png",
                    HeadshotGIFURI = "character_mozhnogirl_headshot.gif",
                    Job = CharacterJobEnum.Spy,
                    Attack = 2,
                    Defense = 3,
                    Speed = 3,
                },
                //new CharacterModel {
                //    Name = "Mike",
                //    Description = "Archer Wannabe",
                //    Level = 1,
                //    MaxHealth = 5,
                //    ImageURI = "elf1.png",
                //    Head = HeadString,
                //    Necklass = NecklassString,
                //    PrimaryHand = PrimaryHandString,
                //    OffHand = OffHandString,
                //    Feet = FeetString,
                //    RightFinger = RightFingerString,
                //    LeftFinger = LeftFingerString,
                //},

                //new CharacterModel {
                //    Name = "Tim",
                //    Description = "Hawk eye",
                //    Level = 1,
                //    MaxHealth = 5,
                //    ImageURI = "elf2.png",
                //    Head = HeadString,
                //    Necklass = NecklassString,
                //    PrimaryHand = PrimaryHandString,
                //    OffHand = OffHandString,
                //    Feet = FeetString,
                //    RightFinger = RightFingerString,
                //    LeftFinger = LeftFingerString,
                //},

                //new CharacterModel {
                //    Name = "Doug",
                //    Description = "Warrior in training",
                //    Level = 1,
                //    MaxHealth = 8,
                //    ImageURI = "elf4.png",
                //    Head = HeadString,
                //    Necklass = NecklassString,
                //    PrimaryHand = PrimaryHandString,
                //    OffHand = OffHandString,
                //    Feet = FeetString,
                //    RightFinger = RightFingerString,
                //    LeftFinger = LeftFingerString,
                //},

                //new CharacterModel {
                //    Name = "Sue",
                //    Description = "A strong Warrior",
                //    Level = 4,
                //    MaxHealth = 38,
                //    ImageURI = "elf3.png"
                //},

                //new CharacterModel {
                //    Name = "Jea",
                //    Description = "Come and get me",
                //    Level = 5,
                //    MaxHealth = 43,
                //    ImageURI = "elf5.png"
                //},

                //new CharacterModel {
                //    Name = "Darren",
                //    Description = "The Wiz",
                //    Level = 5,
                //    MaxHealth = 43,
                //    ImageURI = "elf6.png"
                //},

                //new CharacterModel {
                //    Name = "Dani",
                //    Description = "A powerfull Cleric",
                //    Level = 5,
                //    MaxHealth = 43,
                //    ImageURI = "elf7.png"
                //}
            };

            return datalist;
        }

        /// <summary>
        /// Load Characters
        /// </summary>
        /// <param name="temp"></param>
        /// <returns></returns>
        public static List<MonsterModel> LoadData(MonsterModel temp)
        {
            var datalist = new List<MonsterModel>()
            {
                new MonsterModel {
                    Name = "Baxter",
                    Description = "Best guard this side of the milk bowl",
                    ImageURI = "monster_bodyguard.png",
                    HeadshotImageURI = "monster_bodyguard_headshot.png",
                    HeadshotGIFURI = "monster_bodyguard_headshot.gif",
                    MonsterJob = MonsterJobEnum.Bodyguard,
                    Attack = 2,
                    Defense = 2,
                    Speed = 2,
                },

                new MonsterModel {
                    Name = "Hubert",
                    Description = "Don't let the name fool you",
                    ImageURI = "monster_bountyhunter.png",
                    HeadshotImageURI = "monster_bountyhunter_headshot.png",
                    HeadshotGIFURI = "monster_bountyhunter_headshot.gif",
                    MonsterJob = MonsterJobEnum.BountyHunter,
                    Attack = 4,
                    Defense = 2,
                    Speed = 1,
                },

                new MonsterModel {
                    Name = "Whiskers",
                    Description = "The big bad",
                    ImageURI = "monster_godfather.png",
                    HeadshotImageURI = "monster_godfather_headshot.png",
                    HeadshotGIFURI = "monster_godfather_headshot.gif",
                    MonsterJob = MonsterJobEnum.Godfather,
                    Attack = 4,
                    Defense = 6,
                    Speed = 0,
                },

                new MonsterModel {
                    Name = "Gary",
                    Description = "Actually a nice guy",
                    ImageURI = "monster_guard.png",
                    HeadshotImageURI = "monster_guard_headshot.png",
                    HeadshotGIFURI = "monster_guard_headshot.gif",
                    MonsterJob = MonsterJobEnum.Guard,
                    Attack = 2,
                    Defense = 4,
                    Speed = 0,
                },

                new MonsterModel {
                    Name = "Henry",
                    Description = "Does the job",
                    ImageURI = "monster_henchman.png",
                    HeadshotImageURI = "monster_henchman_headshot.png",
                    HeadshotGIFURI = "monster_henchman_headshot.gif",
                    MonsterJob = MonsterJobEnum.Henchman,
                    Attack = 1,
                    Defense = 1,
                    Speed = 1,
                },

                new MonsterModel {
                    Name = "Alfie",
                    Description = "Line em up, knock em down",
                    ImageURI = "monster_hitman.png",
                    HeadshotImageURI = "monster_hitman_headshot.png",
                    HeadshotGIFURI = "monster_hitman_headshot.gif",
                    MonsterJob = MonsterJobEnum.Hitman,
                    Attack = 3,
                    Defense = 1,
                    Speed = 1,
                },

                new MonsterModel {
                    Name = "Fluffy",
                    Description = "The one behind it all",
                    ImageURI = "monster_mastermind.png",
                    HeadshotImageURI = "monster_mastermind_headshot.png",
                    HeadshotGIFURI = "monster_mastermind_headshot.gif",
                    MonsterJob = MonsterJobEnum.Mastermind,
                    Attack = 4,
                    Defense = 2,
                    Speed = 4,
                },

                //new MonsterModel {
                //    Name = "Green Troll",
                //    Description = "Big and Ugly",
                //    ImageURI = "troll1.png",
                //},

                //new MonsterModel {
                //    Name = "Old Troll",
                //    Description = "Old and Powerfull",
                //    ImageURI = "troll2.png",
                //},

                //new MonsterModel {
                //    Name = "Dainty Troll",
                //    Description = "and fast",
                //    ImageURI = "troll3.png",
                //},

                //new MonsterModel {
                //    Name = "Troll's Troll",
                //    Description = "wozer",
                //    ImageURI = "troll4.png",
                //},

                //new MonsterModel {
                //    Name = "Warrior Troll",
                //    Description = "with sword",
                //    ImageURI = "troll5.png",
                //},

                //new MonsterModel {
                //    Name = "Ax Troll",
                //    Description = "with Hat and Ax",
                //    ImageURI = "troll6.png",
                //},
            };

            return datalist;
        }
    }
}