using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerController player;

    public static GameManager instance;

    private void OnDestroy()
    {
        player.IsPlayerDeadEvent -= PlayerIsDead;
    }

    private void PlayerIsDead()
    {
        SceneManager.LoadScene("GameOver");
    }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        player.IsPlayerDeadEvent += PlayerIsDead;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
