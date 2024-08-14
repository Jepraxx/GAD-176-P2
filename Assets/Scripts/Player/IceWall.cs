using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWall : MonoBehaviour
{
    public float timer;

    // Timer to delete the wall
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= 1)
        {
            Destroy(gameObject);
        }
    }
}
