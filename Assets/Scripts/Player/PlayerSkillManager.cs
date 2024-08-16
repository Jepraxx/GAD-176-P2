using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Skill_System;

public class PlayerSkillManager : MonoBehaviour
{
    // Private fields for player stats and skill points
    private int _strength, _dexterity, _intelligence, _magic, _stamina;
    private int _dash, _fireBall, _invisibility; // Skill levels (assuming integer values represent unlocked skills)
    private int _skillPoints; // Number of skill points available

    // Public properties to expose player stats and skill status
    public int Strength => _strength;
    public int Dexterity => _dexterity;
    public int Intelligence => _intelligence;
    public int Magic => _magic;
    public int Stamina => _stamina;

    // Properties to determine if skills are unlocked based on their values
    public bool Dash => _dash > 0;
    public bool FireBall => _fireBall > 0;
    public bool Invisibility => _invisibility > 0;
    public int SkillPoints => _skillPoints;

    // Event invoked when skill points change
    public UnityAction OnSkillPointsChanged;

    // List of unlocked skills
    private List<ScriptableSkill> _unlockedSkills = new List<ScriptableSkill>();

    // Called when the script instance is being loaded
    private void Awake()
    {
        InitializeStats(); // Initializes player stats
    }

    // Initializes player stats and skill points to default values
    private void InitializeStats()
    {
        _skillPoints = 10; // Default skill points
        _strength = 10;
        _intelligence = 10;
        _magic = 10;
        _stamina = 10;
        _dexterity = 10;
    }

    // Increases skill points and invokes the OnSkillPointsChanged event
    public void GainSkillPoint()
    {
        _skillPoints++;
        OnSkillPointsChanged?.Invoke(); // Notify subscribers of skill point change
    }

    // Checks if a skill can be bought based on its cost and current skill points
    public bool CanBuySkill(ScriptableSkill skill)
    {
        return _skillPoints >= skill.Cost;
    }

    // Unlocks a skill if prerequisites are met and skill can be bought
    public void UnlockSkill(ScriptableSkill skill)
    {
        if (!CanBuySkill(skill)) return; // Exit if skill cannot be bought
        if (!PreReqMet(skill)) return; // Exit if prerequisites are not met

        ModifyStats(skill); // Apply skill upgrades to player stats
        _unlockedSkills.Add(skill); // Add skill to the list of unlocked skills
        _skillPoints -= skill.Cost; // Deduct the skill cost from skill points
        OnSkillPointsChanged?.Invoke(); // Notify subscribers of skill point change
    }

    // Applies the skill's upgrades to the player's stats
    private void ModifyStats(ScriptableSkill skill)
    {
        foreach (UpgradeData upgrade in skill.UpgradeData)
        {
            // Updates the corresponding stat based on the upgrade data
            switch (upgrade.StatType)
            {
                case StatTypes.Strength:
                    ModifyStat(ref _strength, upgrade);
                    break;
                case StatTypes.Dexterity:
                    ModifyStat(ref _dexterity, upgrade);
                    break;
                case StatTypes.Intelligence:
                    ModifyStat(ref _intelligence, upgrade);
                    break;
                case StatTypes.Magic:
                    ModifyStat(ref _magic, upgrade);
                    break;
                case StatTypes.Stamina:
                    ModifyStat(ref _stamina, upgrade);
                    break;
            }
        }
    }

    // Modifies a stat based on whether the upgrade is a percentage or fixed amount
    private void ModifyStat(ref int stat, UpgradeData data)
    {
        if (data.IsPercentage)
        {
            // Increases stat by a percentage
            stat += (int)(stat * (data.SkillIncreaseAmount / 100f));
        }
        else
        {
            // Increases stat by a fixed amount
            stat += data.SkillIncreaseAmount;
        }
    }

    // Checks if a skill is unlocked
    public bool IsSkillUnlocked(ScriptableSkill skill)
    {
        return _unlockedSkills.Contains(skill);
    }

    // Checks if all prerequisites for a skill are met
    public bool PreReqMet(ScriptableSkill skill)
    {
        // Returns true if there are no prerequisites or all prerequisites are unlocked
        return skill.SkillPrerequisites.Count == 0 || skill.SkillPrerequisites.All(_unlockedSkills.Contains);
    }
}
