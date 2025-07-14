using TMPro;
using UnityEngine;

public class ConsoleMoney2 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public static ConsoleMoney2 instance1;

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
        Debug.Log(text);
        _text.text = text;
    }
}
