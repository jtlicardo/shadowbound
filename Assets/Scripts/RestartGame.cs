using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RestartGame : MonoBehaviour
{
    public PlayerController Player;
    public GameObject DeathScreen;
    public GameObject pauseMenu;
    public string restartSceneName;

    public void Restart()
    {
        Time.timeScale = 1f;
        DeathScreen.SetActive(false);
        pauseMenu.SetActive(false);
        Player.respawn();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(1);
    }
    void Update()
    {
        if (Player != null && !Player.isAlive)
        {
            Debug.Log("RestartGame Update");
            Time.timeScale = 0f;
            DeathScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    void Start()
    {
        DeathScreen.SetActive(false);
        pauseMenu.SetActive(false);
    }
}
