using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
/// <summary>
/// This script is entirely dedicated for SkelWarr and this 
/// script would inherit from SkelBase and would override
/// the move method that the SkelBase has
/// </summary>
public class SkelWarr : SkelBase
{
    protected override void FindPlayer()
    {
        base.FindPlayer(); // Call the base find player method
    }

    protected override void Move()
    {
        if (target != null && Vector2.Distance(transform.position, target.position) < distance)
        {
            // SkelWarr moves towards the player faster than the base class
            float modifiedSpeed = moveSpeed * 1.5f; // Adjust this if needed
            transform.position = Vector2.MoveTowards(transform.position, target.position, modifiedSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Damage();
        }
    }

    // This lets SkelWarr get faster when it collides with the player
    private void Damage()
    {
        //This part is where I will make a line of code that would access the players health and deal a certain amount of damage.
        Debug.Log("SKELWARR TOUCHED THE PLAYER!");
    }
   /* protected override void DamagePlayer(GameObject player)
    {
        base.DamagePlayer(player); // Call the base method to deduct health and handle death
        // You can add more logic here if you want SkelWarr to do something specific
    }*/
}
