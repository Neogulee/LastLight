using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TempGameManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Minus))
            Locator.level_manager.increase_exp(20);
    }
}
