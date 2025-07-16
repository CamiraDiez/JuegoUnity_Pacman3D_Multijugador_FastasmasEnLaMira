using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameOverMenuManager : MonoBehaviour
{
    public Image fadePanel;
    public float fadeSpeed = 1f;
    public AudioSource gameOverAudio;

    void Start()
    {
        fadePanel.gameObject.SetActive(false); // Ocultamos el panel 
        gameOverAudio.Play();  // Reproduce al entrar
    }

    public void RetryLevel()
    {
        StartCoroutine(FadeOutAndLoadScene("Level_01"));
    }

    public void GoToMainMenu()
    {
        StartCoroutine(FadeOutAndLoadScene("MainMenu"));
    }

    System.Collections.IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        fadePanel.gameObject.SetActive(true);
        Color panelColor = fadePanel.color;
        panelColor.a = 0f;
        fadePanel.color = panelColor;

        while (panelColor.a < 1f)
        {
            panelColor.a += Time.deltaTime * fadeSpeed;
            fadePanel.color = panelColor;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}
