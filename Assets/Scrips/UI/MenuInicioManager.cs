using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInicioManager : MonoBehaviour
{
    public Button playButton;
    public Button optionButton;
    public Button quitButton;

    //fADE
    public UI_MainMenu_FadeController fadeController;

    public void CargarNivel1()
    {
        //SceneManager.LoadScene("Level_01");
        fadeController.FadeToScene("Level_01", "jugar");
    }

    public void Salir()
    {
        //SceneManager.LoadScene("Portada");
        fadeController.FadeToScene("Portada", "salir");

    }

    public void Opciones()
    {
        //SceneManager.LoadScene("OptionMenu");
        fadeController.FadeToScene("OptionMenu", "jugar");
    }

}
