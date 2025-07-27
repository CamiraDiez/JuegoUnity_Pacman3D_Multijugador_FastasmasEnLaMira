using UnityEngine;
using UnityEngine.UI;

public class UI_Level01_PulsarBrillo : MonoBehaviour
{
    public float velocidad = 2f;
    public float alphaMin = 0.5f;
    public float alphaMax = 1f;

    private Image imagen;
    private Color colorOriginal;

    void Start()
    {
        imagen = GetComponent<Image>();
        colorOriginal = imagen.color;
    }

    void Update()
    {
        float alpha = Mathf.Lerp(alphaMin, alphaMax, (Mathf.Sin(Time.time * velocidad) + 1f) / 2f);
        imagen.color = new Color(colorOriginal.r, colorOriginal.g, colorOriginal.b, alpha);
    }
    

}
