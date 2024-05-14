using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingBar;
    public Text loadingText;

    public void LoadSceneWithLoadingScreen(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        // 로딩 화면 보여주기
        loadingScreen.SetActive(true);

        // 비동기 씬 로딩 시작
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // 로딩 진행 상태 확인
        while (!asyncLoad.isDone)
        {
            // 진행 상태를 보여주는 기능 추가 가능
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f); // 로딩 진행도를 0에서 1 사이로 정규화
            loadingBar.value = progress; // 진행 막대 업데이트
            loadingText.text = "로딩 중... " + Mathf.Round(progress * 100f) + "%"; // 진행 텍스트 업데이트
            yield return null;
        }

        // 씬 로딩 완료 후 로딩 화면 숨기기
        loadingScreen.SetActive(false);
    }
}