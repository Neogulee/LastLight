using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// Callbacks: on_attack_melee(int idx), on_stop_attack_melee(int idx), on_stop_attack_melee(int idx)
/// </summary>
public class BossAttacker: MeleeAttacker
{
    public int attack_idx { get; private set; } = 0;
    public override bool check()
    {
        Vector3 delta_pos = Locator.player.transform.position - transform.position;
        return Mathf.Abs(delta_pos.x) <= distance_to_attack && Mathf.Abs(delta_pos.y) <= 0.5f;
    }

    public override void attack(int idx)
    {
        base.attack(idx);
        attack_idx = (attack_idx + 1) % 2;
    }
}