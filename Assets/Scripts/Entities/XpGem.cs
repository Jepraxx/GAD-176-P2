using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpGem : MonoBehaviour
{
    // Variables 
    public float heldXp;
    private Transform player;

    void Start()
    {
        // Searching for the player's position
        player = GameObject.FindObjectOfType<PlayerStats>().transform;
    }

    // When player and gem touches player gain xp and gem deletes
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.TryGetComponent<PlayerStats>(out PlayerStats playerStats))
        {
            playerStats.GainXp(heldXp);
            Destroy(gameObject);
        }
    }

    // Moving of them gem to the player
    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, player.position, 4 * Time.deltaTime);

    }
}
