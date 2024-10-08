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
    void Awake()
    {
        
    }

    public override bool check()
    {
        float dist = distance_to_attack * (attack_idx == 1 ? 3.0f : 1.0f);
        Vector3 delta_pos = Locator.player.transform.position - transform.position;
        return Mathf.Abs(delta_pos.x) <= dist && Mathf.Abs(delta_pos.y) <= 0.5f;
    }

    public override void attack(int idx)
    {
        base.attack(idx);
        attack_idx = (attack_idx + 1) % 2;
    }
}