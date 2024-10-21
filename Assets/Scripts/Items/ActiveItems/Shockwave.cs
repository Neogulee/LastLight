using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shockwave : ActiveItem
{
    [SerializeField]
    private GameObject shockwave_prefab;
    
    public override void use()
    {
        if (!check_cooldown())
            return;

        Instantiate(shockwave_prefab, transform.position, Quaternion.identity);
    }
}
