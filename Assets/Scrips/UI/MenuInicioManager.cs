using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInicioManager : MonoBehaviour
{
    public Button playButton;
    public Button optionButton;
    public Button quitButton;
    public void CargarNivel1()
    {
        SceneManager.LoadScene("Level_01");
    }

    public void Salir()
    {
        SceneManager.LoadScene("Portada");
    }

    public void Opciones()
    {
        SceneManager.LoadScene("OptionMenu");
    }
}
