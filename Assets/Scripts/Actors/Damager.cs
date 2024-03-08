using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class Damager: MonoBehaviour
{
    public GameObject subject = null;
    public LayerMask target_layer = 0;
    public int damage = 1;
    private HashSet<Collider2D> damaged = new();
    new private Collider2D collider = null;
    void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    public void enable()
    {
        damaged = new();
        collider.enabled = true;
    }

    public void disable()
    {
        collider.enabled = false;
        SendMessage("on_finsh_attack", SendMessageOptions.DontRequireReceiver);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (damaged.Contains(target))
            return;

        if (((1 << target.gameObject.layer) & target_layer) == 0)
            return;

        damaged.Add(target);
        Actor actor = target.GetComponent<Actor>();
        if (actor != null) 
            actor.take_damage(damage);
    }
}