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

}
