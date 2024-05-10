using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void SceneChange()
    {
        // Debug.Log("Scene Change");
        StartCoroutine(FadeManager.Instance.LoadDiffScene("PhysicalTestMap"));

    }
}
