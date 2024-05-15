using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFade : MonoBehaviour
{
    public float fadeDuration = 1.0f; 
    private AudioSource audioSource;
    private float startVolume;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        startVolume = audioSource.volume;
        FadeIn();
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        float currentTime = 0;

        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0, currentTime / fadeDuration);
            yield return null;
        }

        audioSource.volume = 0;
        audioSource.Stop();
    }

    private IEnumerator FadeInCoroutine()
    {
        audioSource.Play();
        audioSource.volume = 0;

        float currentTime = 0;

        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0, startVolume, currentTime / fadeDuration);
            yield return null;
        }

        audioSource.volume = startVolume;
    }
}
