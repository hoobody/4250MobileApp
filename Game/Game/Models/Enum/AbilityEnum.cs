using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Models
{
    /// <summary>
    /// The Types of s a Ability can have
    /// Used in Ability Crudi, and in Battles.
    /// </summary>
    public enum AbilityEnum
    {
        // Not specified
        Unknown = 0,

        // Not specified
        None = 1,

        // General Abilities 10 Range
        // Heal Self
        Bandage = 10,


        // Fighter Abilities > 20 Range
        // Buff Speed
        Nimble = 21,

        // Buff Defense
        Toughness = 22,

        // Buff Attack
        Focus = 23,


        // Cleric Abilities > 50 Range
        // Buff Speed
        Quick = 51,

        // Buff Defense
        Barrier = 52,

        // Buff Attack
        Curse = 53,

        // Heal Self
        Heal = 54,

        // classes in the 100 range
        Assassin = 100,

        Spy = 110,

        Hacker = 120,

        SpecialAgent = 130,

        Saboteur = 140,

        SurveillanceOfficer = 150,

        Detective = 160,

        Double0 = 170,
    }

    /// <summary>
    /// Friendly strings for the Enum Class
    /// </summary>
    public static class AbilityEnumExtensions
    {
        /// <summary>
        /// Display a String for the Enums
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMessage(this AbilityEnum value)
        {
            // Default String
            var Message = "None";

            switch (value)
            {
                case AbilityEnum.Bandage:
                    Message = "Apply Bandages";
                    break;

                case AbilityEnum.Nimble:
                    Message = "React Quickly";
                    break;

                case AbilityEnum.Toughness:
                    Message = "Toughen Up";
                    break;

                case AbilityEnum.Focus:
                    Message = "Mental Focus";
                    break;

                case AbilityEnum.Quick:
                    Message = "Anticipate";
                    break;

                case AbilityEnum.Barrier:
                    Message = "Barrier Defense";
                    break;

                case AbilityEnum.Curse:
                    Message = "Shout Curse";
                    break;

                case AbilityEnum.Heal:
                    Message = "Heal Self";
                    break;

                case AbilityEnum.Assassin:
                    Message = "Large Attack Buff, minor Defense and Speed buff";
                    break;

                case AbilityEnum.Spy:
                    Message = "Minor attack and Speed buff";
                    break; 

                case AbilityEnum.Hacker:
                    Message = "Attack debuff, large defense buff, minor speed buff";
                    break;

                case AbilityEnum.SpecialAgent:
                    Message = "Minor buffs to attack, defense, and speed";
                    break;

                case AbilityEnum.Saboteur:
                    Message = "Attack debuff, minor defense and speed buffs";
                    break;

                case AbilityEnum.SurveillanceOfficer:
                    Message = "Major speed buff, minor attack and defense buffs";
                    break;

                case AbilityEnum.Detective:
                    Message = "Small attack buff, moderate defense buff, speed debuff";
                    break;

                case AbilityEnum.Double0:
                    Message = "Buffs to attack, debuff to defense, and minor buff to speed";
                    break;

                case AbilityEnum.None:
                case AbilityEnum.Unknown:
                default:
                    break;
            }

            return Message;
        }
    }

    /// <summary>
    /// Helper for the Ability Enum Class
    /// </summary>
    /// TODO: Add get lists for other classes
    public static class AbilityEnumHelper
    {
        /// <summary>
        /// Returns a list of strings of the enum for Ability
        /// Removes the Abilitys that are not changable by Items such as Unknown, MaxHealth
        /// </summary>
        public static List<string> GetFullList
        {
            get
            {
                var myList = Enum.GetNames(typeof(AbilityEnum)).ToList();
                return myList;
            }
        }

        /// <summary>
        /// Returns a list of strings of the enum for Fighter
        /// </summary>
        public static List<string> GetListFighter
        {
            get
            {
                List<string> AbilityList = new List<string>{
                AbilityEnum.Nimble.ToString(),
                AbilityEnum.Toughness.ToString(),
                AbilityEnum.Focus.ToString()
                };

                AbilityList.AddRange(GetListOthers);
                return AbilityList;
            }
        }

        /// <summary>
        /// Returns a list of strings of the enum for Cleric
        /// </summary>
        public static List<string> GetListCleric
        {
            get
            {

                List<string> AbilityList = new List<string>{
                AbilityEnum.Quick.ToString(),
                AbilityEnum.Barrier.ToString(),
                AbilityEnum.Curse.ToString(),
                AbilityEnum.Heal.ToString()
                };

                AbilityList.AddRange(GetListOthers);
                return AbilityList;
            }
        }


        /// <summary>
        /// Returns a list of strings of the enum for Assassin
        /// </summary>
        public static List<string> GetListAssassin
        {
            get
            {

                List<string> AbilityList = new List<string>{
                AbilityEnum.Assassin.ToString()
                };

                AbilityList.AddRange(GetListOthers);
                return AbilityList;
            }
        }

        /// <summary>
        /// Returns a list of strings of the enum for Spy
        /// </summary>
        public static List<string> GetListSpy
        {
            get
            {

                List<string> AbilityList = new List<string>{
                AbilityEnum.Spy.ToString()
                };

                AbilityList.AddRange(GetListOthers);
                return AbilityList;
            }
        }

        /// <summary>
        /// Returns a list of strings of the enum for Hacker
        /// </summary>
        public static List<string> GetListHacker
        {
            get
            {

                List<string> AbilityList = new List<string>{
                AbilityEnum.Hacker.ToString()
                };

                AbilityList.AddRange(GetListOthers);
                return AbilityList;
            }
        }

        /// <summary>
        /// Returns a list of strings of the enum for SpecialAgent
        /// </summary>
        public static List<string> GetListSpecialAgent
        {
            get
            {

                List<string> AbilityList = new List<string>{
                AbilityEnum.SpecialAgent.ToString()
                };

                AbilityList.AddRange(GetListOthers);
                return AbilityList;
            }
        }

        /// <summary>
        /// Returns a list of strings of the enum for Saboteur
        /// </summary>
        public static List<string> GetListSaboteur
        {
            get
            {

                List<string> AbilityList = new List<string>{
                AbilityEnum.Saboteur.ToString()
                };

                AbilityList.AddRange(GetListOthers);
                return AbilityList;
            }
        }

        /// <summary>
        /// Returns a list of strings of the enum for SurveillanceOfficer
        /// </summary>
        public static List<string> GetListSurveillanceOfficer
        {
            get
            {

                List<string> AbilityList = new List<string>{
                AbilityEnum.SurveillanceOfficer.ToString()
                };

                AbilityList.AddRange(GetListOthers);
                return AbilityList;
            }
        }

        /// <summary>
        /// Returns a list of strings of the enum for Detective
        /// </summary>
        public static List<string> GetListDetective
        {
            get
            {

                List<string> AbilityList = new List<string>{
                AbilityEnum.Detective.ToString()
                };

                AbilityList.AddRange(GetListOthers);
                return AbilityList;
            }
        }

        /// <summary>
        /// Returns a list of strings of the enum for Double0
        /// </summary>
        public static List<string> GetListDouble0
        {
            get
            {

                List<string> AbilityList = new List<string>{
                AbilityEnum.Double0.ToString()
                };

                AbilityList.AddRange(GetListOthers);
                return AbilityList;
            }
        }

        /// <summary>
        /// Returns a list of strings of the enum of not Cleric or Fighter
        /// </summary>
        public static List<string> GetListOthers
        {
            get
            {

                List<string> AbilityList = new List<string>{
                AbilityEnum.Bandage.ToString(),
                };

                return AbilityList;
            }
        }

        /// <summary>
        /// Given the String for an enum, return its value.  That allows for the enums to be numbered 2,4,6 rather than 1,2,3
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static AbilityEnum ConvertStringToEnum(string value)
        {
            return (AbilityEnum)Enum.Parse(typeof(AbilityEnum), value);
        }
    }
}