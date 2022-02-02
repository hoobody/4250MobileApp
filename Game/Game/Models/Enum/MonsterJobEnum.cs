using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Models
{
    /// <summary>
    /// The Types of Jobs a Monster can have
    /// Used in Monster Crudi, and in Battles.
    /// </summary>
    public enum MonsterJobEnum
    {
        // Not specified
        Unknown = 0,

        // Fighters hit hard and have fight abilities
        Guard = 73,

        // Clerics defend well and have buff abilities
        Dog = 76,

        Camera = 79,

        Bodyguard = 82,

        Henchman = 85,

        RightHandMan = 88,

        Hitman = 91,

        BountyHunter = 94,

        Mercenary = 97,

        Godfather = 100,

        Mastermind = 103

    }

    /// <summary>
    /// Friendly strings for the Enum Class
    /// </summary>
    public static class MonsterJobEnumExtensions
    {
        /// <summary>
        /// Display a String for the Enums
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMessage(this MonsterJobEnum value)
        {
            // Default String
            var Message = "Monster";

            switch (value)
            {
                case MonsterJobEnum.Guard:
                    Message = "Security Guard";
                    break;

                case MonsterJobEnum.Dog:
                    Message = "Guard Dog";
                    break;

                case MonsterJobEnum.Camera:
                    Message = "Security Camera";
                    break;

                case MonsterJobEnum.Bodyguard:
                    Message = "Bodyguard";
                    break;

                case MonsterJobEnum.Henchman:
                    Message = "Henchman";
                    break;

                case MonsterJobEnum.RightHandMan:
                    Message = "Right Hand Man";
                    break;
                case MonsterJobEnum.Hitman:
                    Message = "Hitman";
                    break;

                case MonsterJobEnum.BountyHunter:
                    Message = "Bounty Hunter";
                    break;

                case MonsterJobEnum.Mercenary:
                    Message = "Mercenary";
                    break;

                case MonsterJobEnum.Godfather:
                    Message = "Godfather";
                    break;

                case MonsterJobEnum.Mastermind:
                    Message = "Mastermind";
                    break;

                case MonsterJobEnum.Unknown:
                default:
                    break;
            }

            return Message;
        }
    }

    /// <summary>
    /// Helper for Item Locations
    /// </summary>
    public static class MonsterJobEnumHelper
    {

        /// <summary>
        ///  Gets the list of jobs a Monster can have
        /// </summary>
        public static List<string> GetJobList
        {
            get
            {
                var myList = Enum.GetNames(typeof(MonsterJobEnum)).ToList();
                var myReturn = myList.Where(a =>
                                           a.ToString() != MonsterJobEnum.Unknown.ToString()
                                            )
                                            .OrderBy(a => a)
                                            .ToList();

                return myReturn;
            }
        }

        /// <summary>
        /// Given the String for an enum, return its value.  That allows for the enums to be numbered 2,4,6 rather than 1,2,3 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static MonsterJobEnum ConvertStringToEnum(string value)
        {
            return (MonsterJobEnum)Enum.Parse(typeof(MonsterJobEnum), value);
        }

    }


}