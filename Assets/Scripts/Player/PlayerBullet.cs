using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    // Variables
    private Rigidbody2D rb;

    public float bulletSpeed;
    public float damage;
    public float bulletLifeTime;


    // Virtual function for different types of bullets
    public virtual void BulletEffect()
    {

    }

    // Deal damage to the enemies bullet touched
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.TryGetComponent<SkelBase>(out SkelBase enemyScript))
        {
            enemyScript.TakeDamage(damage);

            Destroy(gameObject);
        }
    }

    // Bullet movement and physics
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = (Vector2)(transform.right * bulletSpeed);

        Destroy(gameObject, bulletLifeTime);
    }

    // Call virtual function
    void Update()
    {
        BulletEffect();
    }
}
