using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Skill_System;

public class PlayerSkillManager : MonoBehaviour
{
    private int _strength, _dexterity, _intelligence, _magic, _stamina;
    private int _dash, _fireBall, _invisibility;
    private int _skillPoints;

    public int Strength => _strength;
    public int Dexterity => _dexterity;
    public int Intelligence => _intelligence;
    public int Magic => _magic;
    public int Stamina => _stamina;

    public bool Dash => _dash > 0;
    public bool FireBall => _fireBall > 0;
    public bool Invisibility => _invisibility > 0;
    public int SkillPoints => _skillPoints;

    public UnityAction OnSkillPointsChanged;
    private List<ScriptableSkill> _unlockedSkills = new List<ScriptableSkill>();

    private void Awake()
    {
        InitializeStats();
    }

    private void InitializeStats()
    {
        _skillPoints = 10;
        _strength = 10;
        _intelligence = 10;
        _magic = 10;
        _stamina = 10;
        _dexterity = 10;
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
        _unlockedSkills.Add(skill);
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

    private void ModifyStat(ref int stat, UpgradeData data)
    {
        if (data.IsPercentage)
        {
            stat += (int)(stat * (data.SkillIncreaseAmount / 100f));
        }
        else
        {
            stat += data.SkillIncreaseAmount;
        }
    }

    public bool IsSkillUnlocked(ScriptableSkill skill)
    {
        return _unlockedSkills.Contains(skill);
    }

    public bool PreReqMet(ScriptableSkill skill)
    {
        return skill.SkillPrerequisites.Count == 0 || skill.SkillPrerequisites.All(_unlockedSkills.Contains);
    }
}
