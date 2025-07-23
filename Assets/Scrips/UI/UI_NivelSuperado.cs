using UnityEngine;

public class UI_NivelSuperado : MonoBehaviour
{
    [SerializeField] private GameObject confettiManager;

    public void Mostrar()
    {
        gameObject.SetActive(true);

        // Activa el Confetti 
        confettiManager.SetActive(true);
    }

    public void Ocultar()
    {
        gameObject.SetActive(false);
        confettiManager.SetActive(false);
    }
}
