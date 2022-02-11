using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Models
{
    /// <summary>
    /// The Types of Jobs a character can have
    /// Used in Character Crudi, and in Battles.
    /// </summary>
    public enum CharacterJobEnum
    {
        // Not specified
        Unknown = 0,

        // Fighters hit hard and have fight abilities
        Assassin = 10,

        // Clerics defend well and have buff abilities
        Spy = 15,

        Hacker = 20,
        
        SpecialAgent = 25,

        Saboteur = 30,

        SurveillanceOfficer = 35,

        Detective = 40,
        
        Double0 = 45,

        Fighter = 55,

        Cleric = 60,

        Wizard = 62,

        Archer = 63,

        Knight = 66,

    }

    /// <summary>
    /// Friendly strings for the Enum Class
    /// </summary>
    public static class CharacterJobEnumExtensions
    {
        /// <summary>
        /// Display a String for the Enums
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToMessage(this CharacterJobEnum value)
        {
            // Default String
            var Message = "Player";

            switch (value)
            {
                case CharacterJobEnum.Assassin:
                    Message = "Assassin";
                    break;

                case CharacterJobEnum.Spy:
                    Message = "Spy";
                    break;

                case CharacterJobEnum.Hacker:
                    Message = "Hacker";
                    break;

                case CharacterJobEnum.SpecialAgent:
                    Message = "SpecialAgent";
                    break;

                case CharacterJobEnum.Saboteur:
                    Message = "Saboteur";
                    break;

                case CharacterJobEnum.SurveillanceOfficer:
                    Message = "SurveillanceOfficer";
                    break;
                case CharacterJobEnum.Detective:
                    Message = "Detective";
                    break;

                case CharacterJobEnum.Double0:
                    Message = "Double0";
                    break;

                case CharacterJobEnum.Cleric:
                    Message = "Cleric";
                    break;

                case CharacterJobEnum.Fighter:
                    Message = "Fighter";
                    break;

                case CharacterJobEnum.Wizard:
                    Message = "Wizard";
                    break;

                case CharacterJobEnum.Archer:
                    Message = "Archer";
                    break;

                case CharacterJobEnum.Knight:
                    Message = "Knight";
                    break;

                case CharacterJobEnum.Unknown:
                default:
                    break;
            }

            return Message;
        }
    }

    /// <summary>
    /// Helper for Item Locations
    /// </summary>
    public static class CharacterJobEnumHelper
    {

        /// <summary>
        ///  Gets the list of jobs a character can have
        /// </summary>
        public static List<string> GetJobList
        {
            get
            {
                var myList = Enum.GetNames(typeof(CharacterJobEnum)).ToList();
                var myReturn = myList.Where(a =>
                                           a.ToString() != CharacterJobEnum.Unknown.ToString() ||
                                           a.ToString() != CharacterJobEnum.Fighter.ToString() ||
                                           a.ToString() != CharacterJobEnum.Cleric.ToString() ||
                                           a.ToString() != CharacterJobEnum.Wizard.ToString() ||
                                           a.ToString() != CharacterJobEnum.Archer.ToString() ||
                                           a.ToString() != CharacterJobEnum.Knight.ToString()
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
        public static CharacterJobEnum ConvertStringToEnum(string value)
        {
            return (CharacterJobEnum)Enum.Parse(typeof(CharacterJobEnum), value);
        }

    }


}