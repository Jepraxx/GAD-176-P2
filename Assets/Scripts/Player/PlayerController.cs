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



    public void RefreshStats()
    {
        moveSpeed = playerStats.moveSpeedStat;
        maxHealth = playerStats.maxHealthStat;
    }

    public void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
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
            Shoot();
        }
    }
}
