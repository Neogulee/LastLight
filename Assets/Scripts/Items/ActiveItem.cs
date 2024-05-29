using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public abstract class ActiveItem: Item
{
    protected float cooldown_timer = 0.0f;
    public abstract void use();
    public void FixedUpdate()
    {
        if (cooldown_timer > 0)
            cooldown_timer -= Time.deltaTime;
    }

    public bool check_cooldown()
    {
        if (cooldown_timer > 0.0f)
            return false;
        cooldown_timer = cooldown;
        return true;
    }
}
