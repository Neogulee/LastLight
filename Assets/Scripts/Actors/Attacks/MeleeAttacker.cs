using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// Callbacks: on_attack_melee(int idx), on_stop_attack_melee(int idx), on_stop_attack_melee(int idx)
/// </summary>
public class MeleeAttacker: Attacker
{
    public float distance_to_attack = 1.5f;
    public float after_delay = 0.5f;
    public List<Damager> damagers = new();

    public override bool check()
    {
        return (Locator.player.transform.position - transform.position).magnitude <= distance_to_attack;
    }

    public void assert_idx(int idx)
    {
        Debug.Assert(0 <= idx && idx < damagers.Count);
    }
    
    public void on_move_left()
    {
        foreach (Damager damager in damagers)
            damager.knockback_dir = KnockbackDir.LEFT;
    }

    public void on_move_right()
    {
        foreach (Damager damager in damagers)
            damager.knockback_dir = KnockbackDir.RIGHT;
    }

    public void prepare_attack(int idx)
    {
        assert_idx(idx);
        SendMessage("on_prepare_attack_melee", idx, SendMessageOptions.DontRequireReceiver);
    }

    public override void attack(int idx)
    {
        assert_idx(idx);
        damagers[idx].enable();
        SendMessage("on_attack_melee", idx, SendMessageOptions.DontRequireReceiver);
    }

    public async void stop_attack(int idx)
    {
        assert_idx(idx);
        damagers[idx].disable();

        await Task.Delay((int)(1000.0f * after_delay));
        SendMessageUpwards("on_finsh_attack", SendMessageOptions.DontRequireReceiver);
        SendMessage("on_stop_attack_melee", idx, SendMessageOptions.DontRequireReceiver);
    }
}