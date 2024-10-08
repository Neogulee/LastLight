using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public abstract class ActiveItem: Item
{
    private float _cooldown_reduction = 0.0f;
    public float remain_cooldown { get; protected set; } = 0.0f;
    public abstract void use();
    public void FixedUpdate()
    {
        if (remain_cooldown > 0)
            remain_cooldown -= Time.deltaTime;
    }

    public bool check_cooldown()
    {
        if (remain_cooldown > 0.0f)
            return false;
        remain_cooldown = cooldown * (1.0f - cooldown_reduction);
        return true;
    }
    
    public float cooldown_reduction
    {
        get {
            return _cooldown_reduction;
        }
        protected set {
            remain_cooldown = remain_cooldown / (1.0f - _cooldown_reduction) * (1.0f - value);
            _cooldown_reduction = value;
        }
    }
    
    /// <summary> value with cooldown reduction applied </summary>
    public float current_cooldown {
        get {
            return cooldown * (1.0f - cooldown_reduction);
        }
    }
}
