using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkelArcher : SkelBase
{
    protected override void FindPlayer()
    {
        base.FindPlayer(); // Call the base find player method

    }

    protected override void Move()
    {
        if (target != null && Vector2.Distance(transform.position, target.position) < distance)
        {
            if (target != null && Vector2.Distance(transform.position, target.position) < distance)
            {
                //SkelArcher moves towards the player faster than the base class and SkelWarr since the movespeed is changed and  multiplied by 2
                float modifiedSpeed = moveSpeed * 2f;
                transform.position = Vector2.MoveTowards(transform.position, target.position, modifiedSpeed * Time.deltaTime);
            }
        }
    }
}
