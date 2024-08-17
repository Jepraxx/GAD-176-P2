using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Scripts.Skill_System
{
    // Allows creating new skills through the Unity Editor with custom properties
    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill System/New Skill", order = 0)]
    public class ScriptableSkill : ScriptableObject
    {
        // List of upgrade data related to this skill
        public List<UpgradeData> UpgradeData = new List<UpgradeData>();

        // Boolean to determine if this skill is an ability
        public bool IsAbility;

        // Name of the skill
        public string SkillName;

        // If true, allows overwriting the generated description
        public bool OverwriteDescription;

        // Description of the skill, editable in the Unity Inspector
        [TextArea(1, 3)] public string SkillDescription;

        // Icon associated with the skill
        public Sprite SkillIcon;

        // List of prerequisite skills required to unlock this skill
        public List<ScriptableSkill> SkillPrerequisites = new List<ScriptableSkill>();


        // Tier level of the skill
        public int skillTier;

        // Cost of the skill in skill points
        public int Cost;

        // Called when the scriptable object is validated in the Inspector
        private void OnValidate()
        {
            // Skip validation if no upgrade data or description is manually set
            if (UpgradeData.Count == 0) return;
            if (OverwriteDescription) return;
            if (SkillName == "") SkillName = name;

            // Generate description if not overwritten
            GenerateDescription();
        }

        // Generates the skill's description based on its properties
        private void GenerateDescription()
        {
            // If the skill is an ability, generate a specific description
            if (IsAbility)
            {
                switch (UpgradeData[0].StatType)
                {
                    case StatTypes.Dash:
                        SkillDescription = $"{SkillName} You have unlocked Dash";
                        break;

                    case StatTypes.FireBall:
                        SkillDescription = $"{SkillName} You have unlocked Fire Ball";
                        break;

                    case StatTypes.Invisibility:
                        SkillDescription = $"{SkillName} You have unlocked Invisibility";
                        break;
                }
            }
            else
            {
                // Generate a description for skills that increase stats
                StringBuilder sb = new StringBuilder();
                sb.Append($"{SkillName} increases");

                for (int i = 0; i < UpgradeData.Count; i++)
                {
                    sb.Append($" {UpgradeData[i].StatType}");
                    sb.Append(" by ");
                    sb.Append(UpgradeData[i].SkillIncreaseAmount.ToString());
                    sb.Append(UpgradeData[i].IsPercentage ? "%" : " point(s)");

                    if (i == UpgradeData.Count - 2) sb.Append(" and");
                    else sb.Append(i < UpgradeData.Count - 1 ? ", " : ".");
                }

                SkillDescription = sb.ToString();
            }
        }
    }

    // Class representing the data for upgrading a stat
    [System.Serializable]
    public class UpgradeData
    {
        public StatTypes StatType; // Type of stat being upgraded
        public int SkillIncreaseAmount; // Amount by which the stat is increased
        public bool IsPercentage; // Whether the increase amount is a percentage
    }

    // Enum for different types of stats that can be upgraded
    public enum StatTypes
    {
        Strength,
        Dexterity,
        Intelligence,
        Magic,
        Stamina,
        Dash,
        FireBall,
        Invisibility
    }
}
