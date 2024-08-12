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
                //ratSis moves towards the player faster than the base class and ratBro since the movespeed is modded and  multiplied by 5
                float modifiedSpeed = moveSpeed * 5f;
                transform.position = Vector2.MoveTowards(transform.position, target.position, modifiedSpeed * Time.deltaTime);
            }
        }
    }

    //Here I will be making a function that would shoot the player but instead of manually shooting I would maybe make a timer that will handle the shooting so lets say every 3 seconds the archer is going to shoot
    
}
