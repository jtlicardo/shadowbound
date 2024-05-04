using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RestartGame : MonoBehaviour
{
    public playerController Player;
    public GameObject DeathScreen;
    public string restartSceneName;
    public void Restart()
    {
        SceneManager.LoadScene(restartSceneName);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(1);
    }
    void Update()
    {
        Time.timeScale = 1f;
        if(!Player.isAlive)
        {
            Time.timeScale = 0f;
            DeathScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    void Start()
    {
        DeathScreen.SetActive(false);
    }
}
