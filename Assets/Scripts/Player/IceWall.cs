using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWall : MonoBehaviour
{
    [SerializeField] private float timeToDestroy;
    [SerializeField] private float timer;

    // Timer to delete the wall
    void Update()
    {
        timer += Time.deltaTime;

        if(timeToDestroy <= timer)
        {
            Destroy(gameObject);
        }
    }
}
