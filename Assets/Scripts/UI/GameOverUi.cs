using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
public class GameOverUi : MonoBehaviour
{
    public UnityEngine.UI.Image image1;
    public UnityEngine.UI.Image image2;
    public TextMeshProUGUI textMeshPro;
    public SoundFade bgm;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameOverStart();
        }
    }
    public void GameOverStart()
    {
        StartCoroutine(ChangeAlphaOverTime());
    }

    IEnumerator ChangeAlphaOverTime()
    {
        bgm.FadeOut();
        float elapsedTime = 0f;
        while (elapsedTime < 2f)
        {
            Color image1Color = image1.color;
            image1Color.a = Mathf.Lerp(0f, 0.6f, elapsedTime / 2f);
            image1.color = image1Color;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        
        elapsedTime = 0f;
        while (elapsedTime < 2f)
        {
            Color image2Color = image2.color;
            image2Color.a = Mathf.Lerp(0f, 0.8f, elapsedTime / 2f);
            image2.color = image2Color;

            Color textMeshProColor = textMeshPro.color;
            textMeshProColor.a = Mathf.Lerp(0f, 1f, elapsedTime / 2f);
            textMeshPro.color = textMeshProColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(FadeManager.Instance.LoadDiffScene("Main"));
    }
}