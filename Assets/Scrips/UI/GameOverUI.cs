using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverUI : MonoBehaviour
{
    // Ir 
    public void RetryLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    // Volver al menú principal
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Asegúrate que el nombre esté bien escrito
    }
}
