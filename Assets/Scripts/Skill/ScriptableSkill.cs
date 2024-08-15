using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace _Scripts.Skill_System
{
    [CreateAssetMenu(fileName = "New Skill", menuName = "Skill System/New Skill", order = 0)]
    public class ScriptableSkill : ScriptableObject
    {
        public List<UpgradeData> UpgradeData = new List<UpgradeData>();
        public bool IsAbility;
        public string SkillName;
        public bool OverwriteDescription;
        [TextArea(1, 3)] public string SkillDescription;
        public Sprite SkillIcon;
        public List<ScriptableSkill> SkillPrerequisites = new List<ScriptableSkill>();
        public int skillTier;
        public int Cost;

        private void OnValidate()
        {
            if (UpgradeData.Count == 0) return;
            if (OverwriteDescription) return;
            if (SkillName == "") SkillName = name;

            GenerateDescription();
        }

        private void GenerateDescription()
        {
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

    [System.Serializable]
    public class UpgradeData
    {
        public StatTypes StatType;
        public int SkillIncreaseAmount;
        public bool IsPercentage;
    }

    public enum StatTypes
    {
        Strength,
        Dexterity,
        Intelligence,
        Magic,
        Stamina,
        Dash,
        FireBall,
        Invisibility,
    }
}