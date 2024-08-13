using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float moveSpeedStat;
    public int maxHealthStat;

    public float fireRate;
    public float damage;

    public float bulletSpeed;
    public int bulletAmount;

    public float bulletLifeTime;

    public float xpMultiplier;


    [SerializeField] private float xp;
    [SerializeField] private float lvlUpRequirement;
    [SerializeField] private float requirementIncrease = 50;
    [SerializeField] private int level;


    public void GainXp(float gainedXp)
    {
        xp += gainedXp * (1 + xpMultiplier);

        if (xp >= lvlUpRequirement)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        level += 1;
        lvlUpRequirement += requirementIncrease;

        requirementIncrease += level * 10;
    }
}
