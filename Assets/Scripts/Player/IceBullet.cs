using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IceBullet : PlayerBullet
{
    [SerializeField] private GameObject iceWallPrefab; 

    public float timer;

    public override void BulletEffect()
    {
        if(Physics2D.OverlapCircle(transform.position, 5, 7))
        {
            timer += Time.deltaTime;
        }

        if(timer >= 1)
        {
            Destroy(gameObject);
        }
    }

    public override void OnDestroy()
    {
        GameObject iceWallSpawned = Instantiate(iceWallPrefab, transform.position, transform.rotation);
    }
}
