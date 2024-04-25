using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainScreenButton : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}
