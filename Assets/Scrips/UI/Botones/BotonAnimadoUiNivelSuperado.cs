using UnityEngine;
using UnityEngine.EventSystems;

public class BotonAnimadoUiNivelSuperado : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 escalaOriginal;
    public float escalaAumentada = 1.1f;
    public float velocidad = 10f;

    private bool agrandando = false;
    private bool reduciendo = false;

    void Start()
    {
        escalaOriginal = transform.localScale;
    }

    void Update()
    {
        if (agrandando)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, escalaOriginal * escalaAumentada, Time.deltaTime * velocidad);
        }
        else if (reduciendo)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, escalaOriginal, Time.deltaTime * velocidad);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        agrandando = true;
        reduciendo = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        agrandando = false;
        reduciendo = true;
    }
   
}
