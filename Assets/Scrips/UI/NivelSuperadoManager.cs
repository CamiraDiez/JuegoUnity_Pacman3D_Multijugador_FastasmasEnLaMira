using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using TMPro;  //para el texto animado

public class NivelSuperadoManager : MonoBehaviour
{
    public Button replayButton;
    public Button menuButton;
    public UI_NivelSuperado uiNivelSuperado;     //esto es para que me funcione el fakein y fakeout

    //para que siempre que de click se va a la escena
    private bool yaCambioDeEscena = false;

    //Cuando toque la cerea nos envia a esta ui
    void Start()
    {
        if (uiNivelSuperado != null)
        {
            uiNivelSuperado.Mostrar();
        }
        else
        {
            Debug.LogWarning("UI_NivelSuperado no está asignado en el inspector.");
        }
    }

    
    public void VolverJugar()
    {
        if (yaCambioDeEscena) return;
        yaCambioDeEscena = true;
        SceneManager.LoadScene("Level_01");
    }

    public void VolverMenuPrincipal()
    {
        if (yaCambioDeEscena) return;
        yaCambioDeEscena = true;
        SceneManager.LoadScene("MainMenu");
    }
}
