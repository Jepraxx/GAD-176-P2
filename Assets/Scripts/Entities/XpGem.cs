using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpGem : MonoBehaviour
{
    public float heldXp;
    private Transform player;

    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerStats>().transform;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.TryGetComponent<PlayerStats>(out PlayerStats playerStats))
        {
            playerStats.GainXp(heldXp);
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if(Physics2D.OverlapCircle(transform.position, 0.1f, 3) != false)
        {
            Debug.Log("Замечен персонаж по цифре");
            transform.position = Vector3.MoveTowards(transform.position, player.position, 1 * Time.deltaTime);

        }
    }
    //сделать физикс детекшн для гемов
}
