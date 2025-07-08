// MenuInicio.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicio : MonoBehaviour
{
    public void CargarNivel1()
    {
        SceneManager.LoadScene("Level_01");
    }
}
