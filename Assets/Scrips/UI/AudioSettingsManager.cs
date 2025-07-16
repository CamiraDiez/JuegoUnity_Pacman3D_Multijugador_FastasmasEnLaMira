using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Audio;

public class AudioSettingsManager : MonoBehaviour
{
    public Slider musicSlider;
    public Slider effectsSlider;

    public AudioSource musicSource;
    public AudioSource effectsSource;

    void Start()
    {
        // Cargar valores guardados 
        float musicVol = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float effectsVol = PlayerPrefs.GetFloat("EffectsVolume", 1f);

        musicSlider.value = musicVol;
        effectsSlider.value = effectsVol;

        ApplyMusicVolume(musicVol);
        ApplyEffectsVolume(effectsVol);

        // Añadir listeners a los sliders
        musicSlider.onValueChanged.AddListener(ApplyMusicVolume);
        effectsSlider.onValueChanged.AddListener(ApplyEffectsVolume);
    }

    public void ApplyMusicVolume(float value)
    {
        musicSource.volume = value;
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void ApplyEffectsVolume(float value)
    {
        effectsSource.volume = value;
        PlayerPrefs.SetFloat("EffectsVolume", value);
    }
}

