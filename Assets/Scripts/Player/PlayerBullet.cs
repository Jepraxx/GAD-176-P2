using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private Rigidbody2D rb;

    public float bulletSpeed;
    public float damage;
    public float bulletLifeTime;

    public virtual void BulletEffect()
    {

    }

    public virtual void OnDestroy()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        // if(col.gameObject.TryGetComponent<SkelBase>(out SkelBase enemyScript))
        // {
        //     enemyScript.TakeDamage(damage);
        // }

        Destroy(gameObject);
    }

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
        BulletEffect();
    }


    //сделать инхеритансе и полиморфизм для пуль 
}
