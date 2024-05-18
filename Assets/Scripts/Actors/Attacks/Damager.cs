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
/// <summary>
/// Callbacks: on_hit()
/// </summary>
public class Damager: MonoBehaviour
{
    public GameObject subject = null;
    public LayerMask target_layer = 0;
    public int damage = 1;
    public float knockback_power = 3.0f;

    public float up_power = 0;
    public KnockbackDir knockback_dir = KnockbackDir.AUTO;
    public bool multiple_hit = false;
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
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (damaged.Contains(target) && !multiple_hit)
            return;

        if (((1 << target.gameObject.layer) & target_layer) == 0)
            return;

        damaged.Add(target);
        Actor actor = target.GetComponent<Actor>();
        ActorController actor_controller = target.GetComponent<ActorController>();
        if (actor != null)
            if (!actor.take_damage(damage)) 
            {
                SendMessage("on_hit", (damage, target), SendMessageOptions.DontRequireReceiver);
                return;
            }
        if (knockback_power > 0.0f && actor_controller != null) {
            bool is_right = knockback_dir == KnockbackDir.RIGHT;
            if (knockback_dir == KnockbackDir.AUTO)
                is_right = (target.transform.position - transform.position).x > 0.0f;
            actor_controller.take_knockback(knockback_power, is_right, up_power);
        }
        SendMessage("on_hit", (damage, target), SendMessageOptions.DontRequireReceiver);
    }
}