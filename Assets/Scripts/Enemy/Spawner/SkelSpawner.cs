using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script will be responsible for spawning either the SkelWarr or SkelArcher
/// </summary>
public class SkelSpawner : MonoBehaviour
{
    public GameObject skelWarrPrefab;  // SkelWarr prefab here in the Unity Inspector
    public GameObject archerPrefab;    // SkelArcher prefab here in the Unity Inspector
    public Transform spawnPoint;       // Drag spawn point, Transform here in the Unity Inspector

    void Start()
    {
        // Wait 1 second, then spawn every 5 seconds, made asimple spawning script with that takes a method name float time and the float repeat rate
        // so basically the script will wait for 1 second and spawn one enemy every 5 seconds after
        InvokeRepeating("SpawnEnemy", 1f, 5f); 
    }
    void SpawnEnemy()
    {
        // decide which enemy to spawn (0 for SkelWarr, 1 for Archer)
        int randomChoice = Random.Range(0, 2); // Randomly picks 0 or 1

        if (randomChoice == 0)
        {
            Instantiate(skelWarrPrefab, spawnPoint.position, spawnPoint.rotation);
            Debug.Log("Spawned SkelWarr!");
        }
        else
        {
            Instantiate(archerPrefab, spawnPoint.position, spawnPoint.rotation);
            Debug.Log("Spawned Archer!");
        }
    }

}
