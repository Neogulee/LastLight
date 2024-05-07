using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// Callbacks: on_attack_range()
/// </summary>
public abstract class RangeAttacker: Attacker
{
    public LayerMask layer;
    public float attack_distance = 8.0f;
    [SerializeField]
    protected List<GameObject> projectile_prefabs;

    public override bool check()
    {
        Vector3 player_pos = Locator.player.transform.position;
        if ((player_pos - transform.position).magnitude > attack_distance)
            return false;
        
        Vector3 delta = player_pos - transform.position;
        var hit = Physics2D.Raycast(transform.position, delta, delta.magnitude, layer);
        return !hit;
    }

    protected void prepare_attack(int idx)
    {
        SendMessage("on_prepare_attack_range", idx, SendMessageOptions.DontRequireReceiver);
    }
    
    protected void notify_attack(int idx)
    {
        SendMessage("on_attack_range", idx, SendMessageOptions.DontRequireReceiver);
    }

    protected void stop_attack(int idx)
    {
        SendMessageUpwards("on_finsh_attack", SendMessageOptions.DontRequireReceiver);
        SendMessage("on_stop_attack_range", idx, SendMessageOptions.DontRequireReceiver);
    }
}