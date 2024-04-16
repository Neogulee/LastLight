using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TempGameManager : MonoBehaviour
{
    public LevelManager level_manager = null;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            level_manager.increase_exp(2);
    }
}
