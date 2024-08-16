using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerStats : MonoBehaviour
{
    // variables 
    public float moveSpeedStat;
    public int maxHealthStat;

    public float fireRate;
    public float damage;

    public float bulletSpeed;
    public int bulletAmount;

    public float bulletLifeTime;

    public float xpMultiplier;

    [SerializeField] public GameObject ui;

    [SerializeField] private float xp;
    [SerializeField] private float lvlUpRequirement;
    [SerializeField] private float requirementIncrease = 50;
    [SerializeField] private int level;

    // Gaining exp with multiplier from stats
    public void GainXp(float gainedXp)
    {
        xp += gainedXp * (1 + xpMultiplier);

        if (xp >= lvlUpRequirement)
        {
            LevelUp();
        }
    }

    // When required exp gained player level ups and increases requirements
    public void LevelUp()
    {
        level += 1;
        lvlUpRequirement += requirementIncrease;

        requirementIncrease += level * 10;

        UIManager ui = Object.FindObjectOfType<UIManager>();

        Time.timeScale = 0;
        ui.TurnOn();
    
    }
}
