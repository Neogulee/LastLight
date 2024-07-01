using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public abstract class ActiveItem: Item
{
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
        remain_cooldown = cooldown;
        return true;
    }
}
