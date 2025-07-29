using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_MainMenu_ButtonHandler : MonoBehaviour
{
    public Button playButton;
    public Button optionButton;
    public Button quitButton;

    public UI_MainMenu_FadeController fadeController;

    private bool transitionStarted = false;

    // Voy a hacer que el click sí lo tome porque me toca darle muchos clics para que vaya a la otra UI
    private void Start()
    {
        // Botones habilitados al comienzo
        playButton.interactable = true;
        optionButton.interactable = true;
        quitButton.interactable = true;
    }

    public void OnPlayPressed()
    {
        if (transitionStarted) return;
        transitionStarted = true;
        DisableAllButtons();
        fadeController.FadeToScene("Level_01", "jugar");
    }

    public void OnOptionsPressed()
    {
        if (transitionStarted) return;
        transitionStarted = true;
        DisableAllButtons();
        fadeController.FadeToScene("OptionMenu", "jugar");
    }

    public void OnQuitPressed()
    {
        if (transitionStarted) return;
        transitionStarted = true;
        DisableAllButtons();
        StartCoroutine(QuitGameWithFade());
    }

    private void DisableAllButtons()
    {
        playButton.interactable = false;
        optionButton.interactable = false;
        quitButton.interactable = false;
    }

    // Reproducir el sonido de alarma en el botón de salida
    private IEnumerator QuitGameWithFade()
    {
        if (fadeController.audioSource != null && fadeController.whooshSalir != null)
        {
            fadeController.audioSource.clip = fadeController.whooshSalir;
            fadeController.audioSource.Play();
        }

        // Para que el sonido de salida sea más pasito
        if (fadeController.menuMusicController != null)
        {
            fadeController.menuMusicController.FadeOutMusic();
        }

        // Fade de la pantalla negra
        Image fadeImage = fadeController.fadeImage;
        fadeImage.gameObject.SetActive(true);
        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;

        while (color.a < 1f)
        {
            color.a += Time.deltaTime * 1.5f;
            fadeImage.color = color;
            yield return null;
        }

        // Salir del juego completamente (también cuando se complete el juego)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
 
