using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script is the base skeleton script and all the
/// other skeletons would inherit from this script
/// </summary>

public class SkelBase : MonoBehaviour
{
    [SerializeField] public float damage = 1;
    [SerializeField] public float timer;
    [SerializeField] public bool isAttacking = false;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float distance;
    [SerializeField] public float health;
    public SkeletonData skeletonData; // Reference to the Scriptable Object
    public Transform target;

    // At the start, find the player
    protected virtual void Start()
    {
        moveSpeed = skeletonData.moveSpeed;
        health = skeletonData.health;
        distance = skeletonData.distance;

        FindPlayer();
    }

    // Find the player
    protected virtual void FindPlayer()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (target == null)
        {
            Debug.Log("Player not found! Make sure the player has the 'Player' tag.");
        }
    }
   

    private void Update()
    {
        Move(); // Call the move function
        
        if(isAttacking == true)
        {
            timer += Time.deltaTime;
        }

        if(timer >= 1)
        {
            target.GetComponent<PlayerController>().TakeDamage(damage);

            timer = 0;
        }
    }

    // This function is responsible for the tracking system of the skeletons, other skeletons would also inherit this function but can override it to have different behaviors.
    protected virtual void Move()
    {
        if (target != null && Vector2.Distance(transform.position, target.position) < distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
        else if (target == null)
        {
            Debug.Log("Target is null. Ensure the player is tagged correctly.");
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.TryGetComponent<PlayerController>(out PlayerController playerScript))
        {
            isAttacking = true;
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.TryGetComponent<PlayerController>(out PlayerController playerScript))
        {
            isAttacking = false;
        }
    }
}
