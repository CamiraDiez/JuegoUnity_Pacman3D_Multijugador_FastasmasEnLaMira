using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashManager : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip startSound;

    [Header("Fade Panel")]
    public CanvasGroup fadePanel;
    public float fadeSpeed = 1f;

    [Header("Escena")]
    public string nextSceneName = "MainMenu";

    private bool hasStarted = false;

    void Start()
    {
        // Inicia con volumen completo
        if (audioSource != null)
        {
            audioSource.volume = 1f;
            audioSource.clip = startSound;
            audioSource.Play();
        }

        // panel esta completamente transparente al inicio
        if (fadePanel != null)
            fadePanel.alpha = 0f;
    }

    void Update()
    {
        // jugador presione ENTER
        if (Input.GetKeyDown(KeyCode.Return) && !hasStarted)
        {
            hasStarted = true;
            StartCoroutine(FadeOutAndLoadScene());
        }
    }

    System.Collections.IEnumerator FadeOutAndLoadScene()
    {
        float fadeValue = 0f;

        while (fadeValue < 1f)
        {
            fadeValue += Time.deltaTime * fadeSpeed;

            
            if (fadePanel != null)
                fadePanel.alpha = fadeValue;

            // Disminuir volumen de audio suavemente
            if (audioSource != null)
                audioSource.volume = 1f - fadeValue;

            yield return null;
        }

        // Esperar un segundo adicional 
        yield return new WaitForSeconds(0.3f);

        // Cambiar de escena
        SceneManager.LoadScene(nextSceneName);
    }
}