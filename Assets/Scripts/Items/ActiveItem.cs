using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;


public abstract class ActiveItem: Item
{
    public abstract void use();
    protected float cooldown_timer = 0;
    public void Update()
    {
        if (cooldown_timer > 0)
            cooldown_timer-=Time.deltaTime;
    }
}
