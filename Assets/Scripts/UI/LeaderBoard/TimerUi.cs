using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUi : MonoBehaviour
{
    public float time = 0f;
    TMP_Text timerUiText;
    private void Start()
    {
        timerUiText = GetComponent<TMP_Text>();
    }
    private void LateUpdate()
    {
        time += Time.deltaTime;
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timerUiText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
