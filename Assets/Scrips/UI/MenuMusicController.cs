using UnityEngine;
using System.Collections;

public class MenuMusicController : MonoBehaviour
{
    public AudioSource musicSource;
    public float fadeDuration = 1.5f;

    private void Start()
    {
        StartCoroutine(FadeInMusic());
    }

    private IEnumerator FadeInMusic()
    {
        float timer = 0f;
        musicSource.volume = 0f;
        musicSource.Play();

        while (timer < fadeDuration)
        {
            musicSource.volume = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        musicSource.volume = 1f;
    }

    public void FadeOutMusic()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        float startVolume = musicSource.volume;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            musicSource.volume = Mathf.Lerp(startVolume, 0f, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }

        musicSource.volume = 0f;
        musicSource.Stop();
    }
}