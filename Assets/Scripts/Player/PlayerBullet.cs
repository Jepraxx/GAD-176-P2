using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private Rigidbody2D rb;

    public float bulletSpeed;
    public float bulletLifeTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = (Vector2)(transform.right * bulletSpeed);

        Destroy(gameObject, bulletLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        //Destroy(gameObject);
    }
}
