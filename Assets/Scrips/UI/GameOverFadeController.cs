using UnityEngine;
using UnityEngine.UI;

public class GameOverFadeController : MonoBehaviour
{
    public Image fadePanel;
    public float fadeSpeed = 1f;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    System.Collections.IEnumerator FadeIn()
    {
        Color panelColor = fadePanel.color;
        while (panelColor.a > 0f)
        {
            panelColor.a -= Time.deltaTime * fadeSpeed;
            fadePanel.color = panelColor;
            yield return null;
        }
        fadePanel.gameObject.SetActive(false); // Al terminar se oculta
    }
}
