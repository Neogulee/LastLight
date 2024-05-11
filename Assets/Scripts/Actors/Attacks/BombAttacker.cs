using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;


public class BombAttacker : RangeAttacker
{
    public float attack_delay = 1.5f;
    public override async void attack(int idx)
    {
        await sub_attack(idx);
    }
    
    public async Task sub_attack(int idx)
    {
        prepare_attack(idx);
        await Task.Delay((int)(attack_delay * 1000.0f));

        notify_attack(idx);
        Locator.projectile_factory.create(projectile_prefabs[idx], transform.position);
        stop_attack(idx);
    }
}