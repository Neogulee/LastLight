using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDash : PassiveItem
{
    void Start()
    {
        Locator.player.GetComponent<APlayerController>().DashMax = 2;
    }
}
