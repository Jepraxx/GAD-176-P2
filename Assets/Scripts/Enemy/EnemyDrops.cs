using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrops : MonoBehaviour
{

    [SerializeField] private GameObject xpGem;
    [SerializeField] private float xpAmount;

    // When object destoys creates xp gems 
    private void OnDestroy()
    {
        GameObject xpDropped = Instantiate(xpGem, transform.position, transform.rotation);
        xpDropped.GetComponent<XpGem>().heldXp = xpAmount;
    }
}
