using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOutAndReturn : MonoBehaviour
{
    public CanvasGroup fadePanel;
    public float fadeDuration = 1f;
    public string sceneToLoad = "Level_01"; // Nos vamos a volver al nivel 1 para jugar

    public void LoadSceneWithFade()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void OnReturnPressed()
    {
        StartCoroutine(FadeOutAndLoad());
    }

    System.Collections.IEnumerator FadeOutAndLoad()
    {
        fadePanel.gameObject.SetActive(true);

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            fadePanel.alpha = elapsed / fadeDuration;
            yield return null;
        }

        SceneManager.LoadScene(sceneToLoad);
    }
}
