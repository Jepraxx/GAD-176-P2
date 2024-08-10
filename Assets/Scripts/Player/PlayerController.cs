using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerStats playerStats;

    public Transform firePoint;
    public Transform bulletPrefab;

    public float moveSpeed;
    public int maxHealth;
    public int health;

    public PlayerWeapons currentWeapon;

    private bool isFiring = false;
    private float fireTimer;


    public void RefreshStats()
    {
        moveSpeed = playerStats.moveSpeedStat;
        maxHealth = playerStats.maxHealthStat;
    }

    public void Shoot()
    {
        for(int i = 0; i < currentWeapon.weaponBulletAmount; i++)
        {
            Transform spawnedBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            PlayerBullet bullet = spawnedBullet.GetComponent<PlayerBullet>();
            bullet.damage = currentWeapon.weaponDamage * (1 + -playerStats.damage);
             bullet.bulletSpeed = currentWeapon.weaponBulletSpeed;
            spawnedBullet.Rotate(0, 0, Random.Range(-currentWeapon.weaponBulletSpread, currentWeapon.weaponBulletSpread));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();

        RefreshStats();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(moveX * moveSpeed, moveY * moveSpeed);

        if(Input.GetMouseButtonDown(0))
        {
            isFiring = true;
        }
        if(Input.GetMouseButtonUp(0))
        {
            isFiring = false;
        }

        if(isFiring)
        {
            if(fireTimer > 1 / currentWeapon.weaponFireRate + (1 + -playerStats.fireRate))
            {
                fireTimer = 0;
                Shoot();
            }

            fireTimer += Time.deltaTime;
        }
    }
}
