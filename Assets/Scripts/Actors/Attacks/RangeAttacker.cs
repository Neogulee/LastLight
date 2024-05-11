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
    public float after_delay = 0.5f;
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

    protected async void stop_attack(int idx)
    {
        await Task.Delay((int)(1000.0f * after_delay));
        SendMessageUpwards("on_finish_attack", SendMessageOptions.DontRequireReceiver);
        SendMessage("on_stop_attack_range", idx, SendMessageOptions.DontRequireReceiver);
    }
}