using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    // Variables
    private Camera cam;
    [SerializeField] private float rotationSpeed;

    Vector3 dir;
    Ray mousePos;


    void Start()
    {

    }

    // Player's aim system
    void Update()
    {
        // print(Input.mousePosition);
         mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
        dir = mousePos.direction;
        dir.z = 0;

        transform.up = Vector3.MoveTowards(transform.up, dir, rotationSpeed * Time.deltaTime);
    }
}
