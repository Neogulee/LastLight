using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;


public class HpDrain : PassiveItem
{
    public VisualEffect drain_vfx = null;
    private void Start()
    {
        Locator.event_manager.register<OnPlayerAttackMelee>(on_hit);
    }

    public void on_hit(IEventParam param)
    {
        OnPlayerAttackMelee temp = param as OnPlayerAttackMelee;
        Locator.player.heal((int)(temp.damage * (level / 5f + 1) / 5));

        var vfx = Locator.vfx_factory.create(drain_vfx, Locator.player.transform);
        vfx.SetVector2("TargetPosition", temp.target.transform.position);
    }
}
