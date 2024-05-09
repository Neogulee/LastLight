using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;





public class KillCountUi : MonoBehaviour
{
    public int killcount  = 0;
    TMP_Text killCountUiText;
    private void Start()
    {
        killCountUiText = GetComponent<TMP_Text>();
    }
    public void AddKillCount(int num)
    {
        killcount+=num;
    }

    private void LateUpdate()
    {
        killCountUiText.text = killcount.ToString();
    }


}
