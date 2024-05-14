using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void Onclick()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}
