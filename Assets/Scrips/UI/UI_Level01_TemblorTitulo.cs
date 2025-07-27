using UnityEngine;

public class UI_Level01_TemblorTitulo : MonoBehaviour
{
    public float intensidad = 1f;
    public float frecuencia = 25f;

    private Vector3 posicionInicial;

    void Start()
    {
        posicionInicial = transform.localPosition;
    }

    void Update()
    {
        float temblorX = Mathf.Sin(Time.time * frecuencia) * intensidad;
        float temblorY = Mathf.Cos(Time.time * frecuencia * 0.7f) * intensidad;
        transform.localPosition = posicionInicial + new Vector3(temblorX, temblorY, 0f);
    } 
}
