using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject xpGem;
    [SerializeField] private float xpAmount;

    private void OnDestroy()
    {
        GameObject xpDropped = Instantiate(xpGem, transform.position, transform.rotation);
        xpDropped.GetComponent<XpGem>().heldXp = xpAmount;
    }
}
