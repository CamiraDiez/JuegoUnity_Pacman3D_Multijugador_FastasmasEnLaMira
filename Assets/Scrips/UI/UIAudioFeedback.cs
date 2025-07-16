using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIAudioFeedback : MonoBehaviour , IPointerClickHandler, IPointerDownHandler
{
    public AudioClip clickSound;
    public AudioSource effectsSource;

    public void OnPointerClick(PointerEventData eventData)
    {
        PlaySound();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PlaySound();
    }

    void PlaySound()
    {
        if (effectsSource != null && clickSound != null)
        {
            effectsSource.PlayOneShot(clickSound);
        }
    }
}
