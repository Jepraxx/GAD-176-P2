using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBullet : PlayerBullet
{
    //Variables
    [SerializeField] private LayerMask enemyLayer; 
    [SerializeField] private GameObject iceWallPrefab; 

    [SerializeField] private float detectionRadius = 10f; 
    private bool enemyDetected = false; 
    private float timer = 0f; 

    public override void BulletEffect()
    {
        // Checking for enemies in the radius
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayer);
        if (enemies.Length > 0)
        {
            enemyDetected = true; 
        }
        else
        {
            enemyDetected = false;
            timer = 0f; 
        }

        if (enemyDetected)
        {
            timer += Time.deltaTime; 

            if (timer >= 1f)
            {
                Explode(); 
            }
        }
    }

    private void Explode()
    {
        // Create an ice wall to stop the enemies
        if (iceWallPrefab != null)
        {
            
            Instantiate(iceWallPrefab, transform.position, transform.rotation);
        }
        else
        {
            Debug.Log("Ice Wall Prefab is not attached to the script");
        }
        // Delete the bullet after exploding
        Destroy(gameObject); 
    }

    // Draw the radius around bullet for detecting enemies
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}

