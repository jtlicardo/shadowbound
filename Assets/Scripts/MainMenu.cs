using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public Animator fadeAnimator; // Animator component of the fade panel
    public float fadeDuration = 3f;

    public void NewGame()
    {
        StartCoroutine(StartNewGameWithFade());
    }

    private IEnumerator StartNewGameWithFade()
    {
        // Trigger the fade-out animation
        fadeAnimator.SetTrigger("FadeOut");

        float elapsedTime = 0f;
        float startVolume = AudioListener.volume;

        // Gradually reduce volume while fading out
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / fadeDuration;
            AudioListener.volume = Mathf.Lerp(startVolume, 0f, t);
            yield return null;
        }

        // Ensure volume is set to 0
        AudioListener.volume = 0f;

        // Load the next scene in the build index
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextSceneIndex);

        // Wait until the scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Reset the volume for the new scene
        AudioListener.volume = startVolume;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
