using UnityEngine;

public class FadeInUI : MonoBehaviour
{
    public CanvasGroup fadePanel;
    public float fadeDuration = 1f;  

    private void Start()
    {
        if (fadePanel != null)
        {
            // Inicia completamente visible
            fadePanel.alpha = 1f;
            // Lanza la rutina de desvanecimiento
            StartCoroutine(FadeIn());
        }
    }

    System.Collections.IEnumerator FadeIn()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            fadePanel.alpha = 1f - (elapsed / fadeDuration);
            yield return null;
        }

        // quede invisible del todo
        fadePanel.alpha = 0f;

        // Desactivamos el panel para que no moleste más
        fadePanel.gameObject.SetActive(false);
    }
}
