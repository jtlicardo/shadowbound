using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        // Load the next scene in the build index
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ContinueGame()
    {
        // Load the scene in the build index
        SceneManager.LoadScene(GameManager.Instance.currentSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
