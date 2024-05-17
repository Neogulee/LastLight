using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpDrain : PassiveItem
{
    private void Start()
    {
        Locator.event_manager.register<OnPlayerAttackMelee>(on_hit);
    }
    public void on_hit(IEventParam param)
    {
        OnPlayerAttackMelee temp = param as OnPlayerAttackMelee;
        Locator.player.heal((int)(temp.damage * (level/10f + 1) / 10));
    }
}
