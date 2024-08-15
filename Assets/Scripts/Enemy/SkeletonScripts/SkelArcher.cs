using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// This script is entirely dedicated for SkelArcher and this 
/// script would inherit from SkelBase 
/// </summary>
public class SkelArcher : SkelBase
{

    public GameObject arrowPrefab;  //  arrow prefab here in the Inspector
    public Transform shootPoint;    // shoot point (where the arrow will spawn)
    public float shootInterval = 2f; // Time between each shot
    public float arrowSpeed = 5f;   // Speed of the arrow
    protected override void Start()
    {
        base.Start();
        InvokeRepeating("ShootArrow", 1f, shootInterval);  // Start shooting after 1 second, repeat every shootInterval seconds
    }
    protected override void TakeDamage(float damageAmount = 1)
    {
        base.TakeDamage(damageAmount);
    }
    protected override void FindPlayer()
    {
        base.FindPlayer(); // Call the base find player method
    }


    protected override void Move()
    {
        if (target != null && Vector2.Distance(transform.position, target.position) < distance)
        {
            // SkelArcher moves towards the player faster than the base class
            float modifiedSpeed = moveSpeed * 1.5f; // Adjust this if needed
            transform.position = Vector2.MoveTowards(transform.position, target.position, modifiedSpeed * Time.deltaTime);
        }
    }
    // This method handles shooting arrows towards the player
    void ShootArrow()
    {
        if (target != null)
        {
            // Create an arrow at the shoot point
            GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation);

            // Point the arrow towards the player
            Vector2 direction = (target.position - shootPoint.position).normalized;
            arrow.GetComponent<Rigidbody2D>().velocity = direction * arrowSpeed;

            // Optional: Destroy the arrow after 5 seconds to avoid clutter
            Destroy(arrow, 5f);
        }

    }
}
