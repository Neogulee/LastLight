using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum KnockbackDir
{
    AUTO = 0,
    LEFT = 1,
    RIGHT = 2
}

[RequireComponent(typeof(Collider2D))]
public class Damager: MonoBehaviour
{
    public GameObject subject = null;
    public LayerMask target_layer = 0;
    public int damage = 1;
    public float knockback_power = 3.0f;
    public KnockbackDir knockback_dir = KnockbackDir.AUTO;
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
        SendMessageUpwards("on_finsh_attack", SendMessageOptions.DontRequireReceiver);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (damaged.Contains(target))
            return;

        if (((1 << target.gameObject.layer) & target_layer) == 0)
            return;

        damaged.Add(target);
        Actor actor = target.GetComponent<Actor>();
        ActorController actor_controller = target.GetComponent<ActorController>();
        if (actor != null)
            actor.take_damage(damage);
        if (knockback_power > 0.0f && actor_controller != null) {
            bool is_right = knockback_dir == KnockbackDir.RIGHT;
            if (knockback_dir == KnockbackDir.AUTO)
                is_right = (target.transform.position - transform.position).x < 0.0f;
            actor_controller.take_knockback(knockback_power, is_right);
        }
    }
}