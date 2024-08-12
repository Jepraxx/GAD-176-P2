using _Scripts.Skill_System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSkillManager : MonoBehaviour
{
    private int _strength, _dexterity, _intelligence, _magic, _stamina; //stats for the player
    private int _dash, _fireBall, _invisiblity; //unlockables for the player
    private int _skillPoints;

    public int Strength => _strength;
    public int Dexterity => _dexterity;
    public int Intelligence => _intelligence;
    public int Magic => _magic;
    public int Stamina => _stamina;

    public bool Dash => _dash > 0;
    public bool FireBall => _fireBall > 0;
    public bool Invisibility => _invisiblity > 0;
    public int SkillPoints => _skillPoints;

    public UnityAction OnSkillPointsChanged;
    private List<ScriptableSkill> _UnlockedSkills = new List<ScriptableSkill>();

    private void Awake()
    {
        _skillPoints = 10;
        _intelligence = 10;
        _magic = 10;
        _stamina = 10;
        _dexterity = 10;
        _strength = 10;
    }

    public void GainSkillPoint()
    {
        _skillPoints++;
        OnSkillPointsChanged?.Invoke();
    }

    public bool CanBuySkill(ScriptableSkill skill)
    {
        return _skillPoints >= skill.Cost;
    }

    public void UnlockSkill(ScriptableSkill skill)
    {
        if (!CanBuySkill(skill)) return;
        ModifyStats(skill);
        _UnlockedSkills.Add(skill);
        _skillPoints -= skill.Cost;
        OnSkillPointsChanged?.Invoke();
    }

    private void ModifyStats(ScriptableSkill skill)
    {
        foreach (UpgradeData upgrade in skill.UpgradeData)
        {
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

    public bool IsSkillUnlocked(ScriptableSkill skill)
    {
        return _UnlockedSkills.Contains(skill);
    }

    public bool PreReqMet(ScriptableSkill skill)
    {
        return skill.SkillPrerequisites.Count == 0 || skill.SkillPrerequisites.All(_UnlockedSkills.Contains);
    }

    private void ModifyStat(ref int stat, UpgradeData data)
    {
        bool isPercentage = data.IsPercentage;
        if (isPercentage)
        {
            stat += (int)(stat * (data.SkillIncreaseAmount / 100f));
        }
        else
        {
            stat += data.SkillIncreaseAmount;
        }
    }
}
