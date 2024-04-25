using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("Map_01", LoadSceneMode.Single);
        // 로딩 시간 체크 후에 Fade In , Fade Out을 넣을지 고려  

    }
}
