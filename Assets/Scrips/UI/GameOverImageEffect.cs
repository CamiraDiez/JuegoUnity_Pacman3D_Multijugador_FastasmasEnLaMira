using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverImageEffect : MonoBehaviour
{
    public Image gameOverImage;         // La imagen de "Game Over"
    public float fadeZoomDuration = 0.5f;
    public float shakeDuration = 0.3f;
    public float shakeMagnitude = 3f;   // Temblor valor

    private Vector3 originalScale;
    private Vector2 originalPosition;

    void Start()
    {
        // escala y posición original
        originalScale = gameOverImage.rectTransform.localScale;
        originalPosition = gameOverImage.rectTransform.anchoredPosition;

        // opacidad 0 a más grande
        Color color = gameOverImage.color;
        color.a = 0f;
        gameOverImage.color = color;
        gameOverImage.rectTransform.localScale = originalScale * 1.3f;

        // Iniciar animación
        StartCoroutine(PlayIntroAnimation());
    }

    IEnumerator PlayIntroAnimation()
    {
        float time = 0f;
        while (time < fadeZoomDuration)
        {
            time += Time.deltaTime;
            float t = time / fadeZoomDuration;

            // Fade-in
            Color color = gameOverImage.color;
            color.a = Mathf.Lerp(0f, 1f, t);
            gameOverImage.color = color;

            // Zoom pasa a  tamaño original
            gameOverImage.rectTransform.localScale = Vector3.Lerp(originalScale * 1.3f, originalScale, t);

            yield return null;
        }

        // 
        gameOverImage.color = new Color(1, 1, 1, 1);
        gameOverImage.rectTransform.localScale = originalScale;

        // Fase 2: efecto temblor (shake)
        yield return StartCoroutine(ShakeImage());
    }

    IEnumerator ShakeImage()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            elapsed += Time.deltaTime;

            float offsetX = Random.Range(-1f, 1f) * shakeMagnitude;
            float offsetY = Random.Range(-1f, 1f) * shakeMagnitude;

            gameOverImage.rectTransform.anchoredPosition = originalPosition + new Vector2(offsetX, offsetY);

            yield return null;
        }

        // Volver a la posición original
        gameOverImage.rectTransform.anchoredPosition = originalPosition;
    }
}
