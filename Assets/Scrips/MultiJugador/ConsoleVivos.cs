using TMPro;
using UnityEngine;

public class ConsoleVivos : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public static ConsoleVivos instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void RegisterText(string text)
    {
        Debug.Log(text);
        _text.text = text;
    }
}
