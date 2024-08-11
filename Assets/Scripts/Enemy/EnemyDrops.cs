using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject xpGem;
    public float xpAmount;

    private void OnDestroy()
    {
        GameObject xpDropped = Instantiate(xpGem, transform.position, transform.rotation);
        xpDropped.GetComponent<XpGem>().heldXp = xpAmount;
    }
}
