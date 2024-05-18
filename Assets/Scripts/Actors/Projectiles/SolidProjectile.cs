using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SolidProjectile : Projectile
{
    public LayerMask target_layer = 0;
    
    public void on_hit((int damage, Collider2D target) args)
    {
        Destroy(gameObject, 0.01f);    
    }

    void OnTriggerEnter2D(Collider2D target)
    {   
        if (((1 << target.gameObject.layer) & target_layer) == 0)
            return;

        Destroy(gameObject, 0.01f);
    }
}