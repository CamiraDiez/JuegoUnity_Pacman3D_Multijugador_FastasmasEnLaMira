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


    //BOTONES ANIMACION DE GRANDE-APRIMIDO-NORMAL
    //Quiero agregar lo de al hacer click se agranda y disminuye
    public void OnPointerClick(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ReboteClick());
    }

    //para el rebote
    private System.Collections.IEnumerator ReboteClick()
    {
        Vector3 escalaPequeña = escalaOriginal * 0.9f;
        Vector3 escalaGrande = escalaOriginal * 1.15f;

        //ACHICAR
        float duracion1 = 0.08f;
        float t = 0f;
        while (t < duracion1)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, escalaPequeña, t / duracion1);
            yield return null;
        }

        //REBOTE
        float duracion2 = 0.08f;
        t = 0f;
        while (t < duracion2)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, escalaGrande, t / duracion2);
            yield return null;
        }

        //NORMAL TAMAÑO
        float duracion3 = 0.08f;
        t = 0f;
        while (t < duracion3)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, escalaOriginal, t / duracion3);
            yield return null;
        }
    }
       
}
