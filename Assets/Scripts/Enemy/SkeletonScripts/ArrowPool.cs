using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script will handle the arrow pool
/// not sure if this is going to work since I am still learning this concept
/// I am still testing if I fully understand on this one so it might or might not work as of the moment
/// </summary>
public class ArrowPool : MonoBehaviour
{
    public static ArrowPool Instance { get; private set; } // Singleton instance
    public GameObject arrowPrefab; // This is what an arrow looks like
    public int poolSize = 10; // We want 10 arrows
    private List<GameObject> arrowPool = new List<GameObject>(); // A list to keep the arrows


    void Awake()
    {
        // Check if an instance already exists
        if (Instance == null)
        {
            Instance = this; // Assign the instance to this object
            DontDestroyOnLoad(gameObject); // Keep this object when loading new scenes
        }
        else
        {
            Destroy(gameObject); // If an instance already exists, destroy the duplicate
        }
    }
    void Start()
    {
        // Make 10 arrows and hide them
        for (int i = 0; i < poolSize; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab);
            arrow.SetActive(false); // Hide the arrow
            arrowPool.Add(arrow); // Add it to the list
        }
    }

    public GameObject GetArrow()
    {
        // Find a hidden arrow
        foreach (GameObject arrow in arrowPool)
        {
            if (!arrow.activeInHierarchy)
            {
                arrow.SetActive(true); // Show the arrow
                return arrow;
            }
        }

        // If no arrows are hidden, make a new one
        GameObject newArrow = Instantiate(arrowPrefab);
        arrowPool.Add(newArrow);
        return newArrow;
    }

    public void ReturnArrow(GameObject arrow)
    {
        // Hide the arrow and put it back
        arrow.SetActive(false);
    }

}
