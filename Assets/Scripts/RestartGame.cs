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

        Debug.Log("RestartGame Restart: Respawning at checkpoint.");
        GameManager.Instance.RespawnAtCheckpoint();
        Player.respawn();

        ResetAudio();
    }

    public void RestartFullScene()
    {
        StartCoroutine(RestartFullSceneCoroutine());
    }

    void ResetAudio()
    {
        if (AudioListener.volume == 0)
        {
            AudioListener.volume = 1;
        }
    }

    private IEnumerator RestartFullSceneCoroutine()
    {
        Time.timeScale = 1f;
        DeathScreen.SetActive(false);
        pauseMenu.SetActive(false);
        Debug.Log("RestartGame RestartFullScene: Reloading entire scene.");
        ResetAudio();

        yield return null; // Wait for end of frame to ensure all changes are applied

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(1);
    }
    void Update()
    {
        if (Player != null && !Player.isAlive)
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
        pauseMenu.SetActive(false);
    }
}
