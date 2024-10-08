using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Card", menuName = "Weapon Card")]
public class PlayerWeapons : ScriptableObject
{
    // Variables for creating weapon scriptable objects
    public float weaponFireRate;
    public float weaponDamage;

    public float weaponBulletSpeed;
    public float weaponBulletSpread;
    public int weaponBulletAmount;

    public string weaponName;
}
