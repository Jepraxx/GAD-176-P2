using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitAndRestart : MonoBehaviour
{
    public GameObject reloadButton;
    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
}
