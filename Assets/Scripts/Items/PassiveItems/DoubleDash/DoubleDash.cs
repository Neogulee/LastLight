using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDash : PassiveItem
{
    void Start()
    {
        Locator.player.GetComponent<APlayerController>().DashMax = 2;
    }
    public override bool increase_level()
    {
        if (base.increase_level())
        {
            Locator.player.MaxHpUp(10);
            if(level>=3)Locator.player.GetComponent<APlayerController>().DashMax = 3;
            return true;
        }
        return false;
    }
}
