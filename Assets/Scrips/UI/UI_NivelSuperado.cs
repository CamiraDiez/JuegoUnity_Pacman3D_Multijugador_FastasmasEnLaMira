using UnityEngine;

public class UI_NivelSuperado : MonoBehaviour
{
    [SerializeField] private GameObject confettiManager;              // Confetti
    [SerializeField] private Animator tituloAnimator;                 // Para la animación del título
    [SerializeField] private GameObject tituloNivelSuperado;          // Para que el título no esté activado al comienzo
    [SerializeField] private AudioSource victoryAudio;                // Sonido 1: victoria
    [SerializeField] private AudioSource ambientLoopAudio;            // Sonido 2: ambiente, suena continuamente y bajito
    [SerializeField] private UI_NiverlSuperado_Fader fader;           // Para el fade-in y fade-out suave

    private bool ambientStarted = false;

    public void Mostrar()
    {
        gameObject.SetActive(true);
        //fader.FadeIn();
        //fader.FadeIn(0.1f);  //un fader mas rapido
        if (tituloNivelSuperado != null)
        {
            tituloNivelSuperado.SetActive(true);
        }
        if (confettiManager != null)
        {
            confettiManager.SetActive(true);
            confettiManager.GetComponent<Window>().IniciarConfetti();
        }

        //fade visible...para ver si puedo corregir mi error :/
        fader.FadeIn(0.1f);

        Debug.Log("Activando título...");

        // Activar el título
        if (tituloNivelSuperado != null)
        {
            tituloNivelSuperado.SetActive(true);
            StartCoroutine(EsperarYActivarAnimacion());
            //Canvas.ForceUpdateCanvases(); // Probar si ayuda
            //tituloAnimator.Play("Anim_NivelSuperadoEntrada", 0, 0f); // Línea de prueba
        }

        // Sonido 1: victoria
        if (victoryAudio != null)
        {
            Debug.Log("Reproduciendo sonido 1");
            victoryAudio.Play();
            StartCoroutine(EsperarFinSonidoVictoria());
            //Invoke(nameof(IniciarSonidoAmbiente), victoryAudio.clip.length);  // Alternativa
            //Invoke(nameof(IniciarSonidoAmbiente), 3f);                         // Alternativa
        }

        // Activar y reiniciar confetti
        if (confettiManager != null)
        {
            confettiManager.SetActive(true);
            confettiManager.GetComponent<Window>().IniciarConfetti();
        }

        // Animación del título
        if (tituloAnimator != null)
        {
            
            tituloAnimator.Play("Anim_NivelSuperadoEntrada", 0, 0f);
            tituloAnimator.SetBool("Play", true);
            Debug.Log("✔️ Se activó el Animator con Play = true");
        }
        else
        {
            Debug.LogError("No se encontró el Animator del título");
        }
    }

    // Sonido 2: ambiente
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

    /*
    // PRUEBA PARA MIRAR COMO ARREGLO MI PROBLEMA CON EL FADE
    private void Start()
    {
        Mostrar();
    }
    */

    private System.Collections.IEnumerator EsperarFinSonidoVictoria()
    {
        float duracion = victoryAudio.clip.length;
        float adelanto = 6f;   //6 me gusto

        yield return new WaitForSeconds(duracion - adelanto);
        IniciarSonidoAmbiente();
        
    }

    private System.Collections.IEnumerator EsperarYActivarAnimacion()
    {
        yield return new WaitForSeconds(0.3f);  //tiempo para que apareca  0.3
        if (tituloAnimator != null)
        {
            Canvas.ForceUpdateCanvases();            //LINEA PROBAR
            tituloAnimator.SetBool("Play", true);
            Debug.Log("✔️ Animación activada luego de espera breve");
        }
    }
}