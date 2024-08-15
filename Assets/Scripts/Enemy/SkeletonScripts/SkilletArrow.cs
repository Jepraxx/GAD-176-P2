using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkilletArrow : MonoBehaviour
{
    [SerializeField] public float damage = 1;   

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.TryGetComponent<PlayerController>(out PlayerController playerScript))
        {
            playerScript.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
