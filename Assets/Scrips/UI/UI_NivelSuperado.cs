using UnityEngine;

public class UI_NivelSuperado : MonoBehaviour
{
    [SerializeField] private GameObject confettiManager;  // Confetti
    [SerializeField] private Animator tituloAnimator;     // Para la animación del título
    [SerializeField] private GameObject tituloNivelSuperado;  // El título (desactivado al inicio)
    [SerializeField] private AudioSource victoryAudio;    // Sonido de victoria
    [SerializeField] private AudioSource ambientLoopAudio;  // Sonido de fondo (suave y continuo)
    [SerializeField] private UI_NiverlSuperado_Fader fader;  // Fade in/out

    private bool ambientStarted = false;

    public void Mostrar()
    {
        gameObject.SetActive(true);
        fader.FadeIn();

        Debug.Log("Activando título...");

        // Activar título
        if (tituloNivelSuperado != null)
        {
            tituloNivelSuperado.SetActive(true);
        }

        // Reproducir sonido de victoria (S1)
        if (victoryAudio != null)
        {
            victoryAudio.Play();
            // Iniciar sonido 2 después de que termine el sonido 1
            Invoke(nameof(IniciarSonidoAmbiente), victoryAudio.clip.length - 0.1f);
        }

        // Activar confetti
        if (confettiManager != null)
        {
            confettiManager.SetActive(true);
            confettiManager.GetComponent<Window>().IniciarConfetti();
        }

        // Reproducir animación del título
        if (tituloAnimator != null)
        {
            tituloAnimator.SetBool("Play", true);
        }
    }

    private void IniciarSonidoAmbiente()
    {
        if (!ambientStarted && ambientLoopAudio != null)
        {
            ambientLoopAudio.Play();
            ambientStarted = true;
        }
    }

    public void Ocultar()
    {
        fader.FadeOut();

        if (confettiManager != null)
        {
            confettiManager.SetActive(false);
        }

        if (ambientLoopAudio != null)
        {
            ambientLoopAudio.Stop();
        }

        if (tituloNivelSuperado != null)
        {
            tituloNivelSuperado.SetActive(false);
        }
    }
}