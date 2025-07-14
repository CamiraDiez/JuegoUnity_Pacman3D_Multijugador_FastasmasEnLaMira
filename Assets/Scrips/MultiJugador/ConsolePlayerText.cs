using TMPro;
using UnityEngine;

public class ConsolePlayerText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public static ConsolePlayerText instance1;

    private void Awake()
    {
        if (instance1 == null)
        {
            instance1 = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void RegisterText(string text)
    {
        Debug.Log("NoJugadores: " + text);
        _text.text = text;
    }
}
