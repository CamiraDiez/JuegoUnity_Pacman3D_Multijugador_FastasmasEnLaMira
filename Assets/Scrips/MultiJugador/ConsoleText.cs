using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ConsoleText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public static ConsoleText instance;

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
        Debug.Log("Console: " + text);
        _text.text = text;
    }
}
