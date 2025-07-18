using TMPro;
using UnityEngine;

public class ConsoleLives2 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public static ConsoleLives2 instance1;

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
