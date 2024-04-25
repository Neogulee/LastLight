using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// Callbacks: on_attack_melee(int idx), on_stop_attack_melee(int idx), on_stop_attack_melee(int idx)
/// </summary>
public class MeleeAttacker: MonoBehaviour
{
    public List<Damager> damagers = new();

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

    public void attack(int idx)
    {
        assert_idx(idx);
        damagers[idx].enable();
        SendMessage("on_attack_melee", idx, SendMessageOptions.DontRequireReceiver);
    }

    public void stop_attack(int idx)
    {
        assert_idx(idx);
        damagers[idx].disable();
        SendMessage("on_stop_attack_melee", idx, SendMessageOptions.DontRequireReceiver);
    }
}