using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// Callbacks: on_attack_range()
/// </summary>
public abstract class RangeAttacker: MonoBehaviour
{
    [SerializeField]
    protected List<GameObject> projectile_prefabs;
    public abstract Task attack();

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
        SendMessage("on_stop_attack_range", idx, SendMessageOptions.DontRequireReceiver);
    }
}