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
