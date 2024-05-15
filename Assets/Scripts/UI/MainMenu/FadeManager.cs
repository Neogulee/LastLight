using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    // UI의 Image 컴포넌트를 저장할 변수
    public UnityEngine.UI.Image fadeImage;
    public float fadeDuration = 1.0f;
    private Color currentColor = Color.black;
    private Color targetColor = new Color(0, 0, 0, 0);
    public static FadeManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        StartCoroutine(FadeIn());
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        //if (scene.name == "PhysicalTestMap")
        //{
            StartCoroutine(FadeIn());
        //}
    }
    private IEnumerator FadeIn()
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            currentColor = Color.Lerp(Color.black, Color.clear, elapsedTime / fadeDuration);
            fadeImage.color = currentColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        currentColor = Color.clear;
        fadeImage.color = currentColor;
    }
    public IEnumerator FadeOut()
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            currentColor = Color.Lerp(Color.clear, Color.black, elapsedTime / fadeDuration);
            fadeImage.color = currentColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        currentColor = Color.black;
        fadeImage.color = currentColor;
    }
    public IEnumerator LoadDiffScene(string SceneName, LoadSceneMode mode = LoadSceneMode.Single)
    {
        Debug.Log("LoadDiffScene");
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            currentColor = Color.Lerp(Color.clear, Color.black, elapsedTime / fadeDuration);
            fadeImage.color = currentColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        currentColor = Color.black;
        fadeImage.color = currentColor;
        SceneManager.LoadScene(SceneName, mode);
        // StartCoroutine(FadeIn());
    }
}
