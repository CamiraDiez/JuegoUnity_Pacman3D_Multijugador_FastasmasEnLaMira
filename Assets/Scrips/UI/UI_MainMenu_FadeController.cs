using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_MainMenu_FadeController : MonoBehaviour
{
    public Image fadeImage;

    public AudioSource audioSource;
    public AudioClip whooshJugarOpciones;
    public AudioClip whooshSalir;

    public MenuMusicController menuMusicController;

    private void Awake()
    {
        Debug.Log("FadeController: Awake ejecutado."); 
        
        if (fadeImage == null)
            Debug.LogError("FadeController: Falta asignar el fadeImage.");

        if (audioSource == null)
            Debug.LogWarning("FadeController: Falta asignar audioSource.");
    }
    
    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeToScene(string sceneName, string soundType)
    {
        StartCoroutine(FadeOut(sceneName, soundType));
    }

    private IEnumerator FadeIn()
    {
        fadeImage.gameObject.SetActive(true);

        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;

        while (color.a > 0f)
        {
            color.a -= Time.deltaTime * 1.5f;
            fadeImage.color = color;
            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
    }

    private IEnumerator FadeOut(string sceneName, string soundType)
    {
        fadeImage.gameObject.SetActive(true);

        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;

        //Desvanecer la música de fondo
        if (menuMusicController != null)
        {
            menuMusicController.FadeOutMusic();
        }

        // Reproducir sonido de botón
        if (audioSource != null)
        {
            if (soundType == "salir" && whooshSalir != null)
            {
                audioSource.clip = whooshSalir;
            }
            else if (whooshJugarOpciones != null)
            {
                audioSource.clip = whooshJugarOpciones;
            }

            audioSource.Play();
        }

        //  Oscurecer pantalla
        while (color.a < 1f)
        {
            color.a += Time.deltaTime * 1.5f;
            fadeImage.color = color;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}