using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public SoundFade soundFade;
    private void Start()
    {
        soundFade = GameObject.Find("BackGroundmusic").GetComponent<SoundFade>();
    }
    public void SceneChange()
    {
        soundFade.FadeOut();
        StartCoroutine(FadeManager.Instance.LoadDiffScene("PhysicalTestMap"));
    }
}
