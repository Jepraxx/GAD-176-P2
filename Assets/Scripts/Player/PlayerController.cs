using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerStats playerStats;

    public delegate void IsPlayerDead();
    public IsPlayerDead IsPlayerDeadEvent;

    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform bulletPrefab;

    [SerializeField] private float moveSpeed;
    [SerializeField] private int maxHealth;
    [SerializeField] private int health;

    [SerializeField] private PlayerWeapons currentWeapon;

    private bool isFiring = false;
    private float fireTimer;


    public void RefreshStats()
    {
        moveSpeed = playerStats.moveSpeedStat;
        maxHealth = playerStats.maxHealthStat;
    }

    private void Die()
    {
        if(health <= 0)
        {
            if(IsPlayerDeadEvent != null)
            {
                IsPlayerDeadEvent();
            }
        }
    }

        private void PlayerMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(moveX * moveSpeed, moveY * moveSpeed);
    }

        public void Shoot()
    {
        for(int i = 0; i < currentWeapon.weaponBulletAmount + playerStats.bulletAmount; i++)
        {
            Transform spawnedBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            PlayerBullet bullet = spawnedBullet.GetComponent<PlayerBullet>();

            bullet.damage = currentWeapon.weaponDamage * (1 + playerStats.damage);
            bullet.bulletSpeed = currentWeapon.weaponBulletSpeed * (1 + playerStats.bulletSpeed);
            bullet.bulletLifeTime *= 1 + playerStats.bulletLifeTime;
            
            spawnedBullet.Rotate(0, 0, Random.Range(-currentWeapon.weaponBulletSpread, currentWeapon.weaponBulletSpread));
        }
    }

    private void Shooting()
    {
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
            fireTimer += Time.deltaTime;
            if(fireTimer > 1 / currentWeapon.weaponFireRate + (1 + -playerStats.fireRate))
            {
                fireTimer = 0;
                Shoot();
            }

            
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
        PlayerMovement();

        Shooting();
    }
}
