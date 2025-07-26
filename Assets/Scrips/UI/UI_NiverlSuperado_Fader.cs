using UnityEngine;
using System.Collections;

public class UI_NiverlSuperado_Fader : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogError("No hay CanvasGroup en " + gameObject.name);
        }

        canvasGroup.alpha = 0f;   //la ui comience invisible 
        gameObject.SetActive(true);
    }

    public void FadeIn(float duration = 0.5f)
    {
        //gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(FadeCanvas(0f, 1f, duration));
    }

    public void FadeOut(float duration = 0.5f)
    {
        StopAllCoroutines();
        StartCoroutine(FadeCanvas(1f, 0f, duration, true));
    }

    private IEnumerator FadeCanvas(float startAlpha, float endAlpha, float duration, bool disableAfter = false)
    {
        float elapsed = 0f;
        canvasGroup.alpha = startAlpha;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            yield return null;
        }

        canvasGroup.alpha = endAlpha;

        if (disableAfter)
        {
            gameObject.SetActive(false);
        }
    }

}
