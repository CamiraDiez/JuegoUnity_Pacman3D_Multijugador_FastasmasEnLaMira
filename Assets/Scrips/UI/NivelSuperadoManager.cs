using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NivelSuperadoManager : MonoBehaviour
{
    public Button replayButton;
    public Button menuButton;
    
    public void VolverJugar()
    {
        SceneManager.LoadScene("Level_01");
    }

    public void VolverMenuPrinicipal()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
