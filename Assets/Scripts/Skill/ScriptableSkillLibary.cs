using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Scripts.Skill_System
{
    // Defines a ScriptableObject for managing a library of skills
    [CreateAssetMenu(fileName = "New Skill Library", menuName = "Skill System/New Skill Library", order = 0)]
    public class ScriptableSkillLibrary : ScriptableObject
    {
        // List to store all skills available in the library
        public List<ScriptableSkill> SkillLibrary;

        // Returns a list of skills that belong to a specific tier
        public List<ScriptableSkill> GetSkillsOfTier(int tier)
        {
            // Filters the skills based on the tier and converts the result to a list
            return SkillLibrary.Where(skill => skill.skillTier == tier).ToList();
        }
    }
}
