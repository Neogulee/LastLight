using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;


public class TargetAttacker : RangeAttacker
{
    public float before_attack_delay = 0.5f, after_attack_delay = 0.5f;
    public override async void attack(int idx)
    {
        await sub_attack(idx);
    }

    public async Task sub_attack(int idx)
    {
        prepare_attack(idx);
        Vector3 target = Locator.player.transform.position - transform.position;
        await Task.Delay((int)(before_attack_delay * 1000.0f));

        notify_attack(idx);
        var projectile = Locator.projectile_factory.create(projectile_prefabs[idx], transform.position);
        projectile.GetComponent<Projectile>().init(target);

        await Task.Delay((int)(after_attack_delay * 1000.0f));
        stop_attack(idx);
    }
}