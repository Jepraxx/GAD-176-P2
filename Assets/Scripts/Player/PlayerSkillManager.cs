using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSkillManager : MonoBehaviour
{
    private int _strength, _dexterity, _intelligence, _magic, _stamina; //stats for the player
    private int _doubleJump, _dash, _fireBall, _invisiblity; //unlocables for the player
    private int _skillPoints;

    public int Strength => _strength;
    public int Dexterity => _dexterity;
    public int Intelligence => _intelligence;
    public int Magic => _magic;
    public int Stamina => _stamina;

    public bool DoubleJump => _doubleJump > 0;
    public bool Dash => _dash > 0;
    public bool FireBall => _fireBall > 0;
    public bool Invisibility => _invisiblity > 0;
    public int SkillPoints => _skillPoints;

    public UnityAction OnSkillPointsChanged;
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
}
