using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public interface IActor
{
    public int hp { get; }
    public bool take_damage(int damage);
    public void heal(int amount);
    public void destroy();
    public void apply_status_effect(IStatusEffect status_effect);
    public void on_status_effect_finished(IStatusEffect status_effect);
}

/// <summary>
/// Callbacks: on_damaged(int damage), on_destroyed()
/// </summary>
public abstract class Actor : MonoBehaviour, IActor
{
    [field: SerializeField]
    public int max_hp  { get; protected set; } = 100;
    public int hp { get; protected set; }
    public List<IStatusEffect> status_effects { get; protected set; } = new();
    private List<IStatusEffect> finished_effects = new();
    protected virtual void Awake()
    {
        hp = max_hp;
    }

    public void FixedUpdate()
    {
        foreach (IStatusEffect status_effect in status_effects)
            status_effect.update();

        foreach (IStatusEffect status_effect in finished_effects)
            status_effects.Remove(status_effect);
        finished_effects.Clear();
    }

    public virtual bool take_damage(int damage)
    {
        hp = Mathf.Max(0, hp - damage);
        SendMessage("on_damaged", damage, SendMessageOptions.DontRequireReceiver);
        if (hp <= 0)
            destroy();
        return true;
    }

    public virtual void heal(int amount)
    {
        hp = Mathf.Min(max_hp, hp + amount);
    }

    public virtual void destroy()
    {
        SendMessage("on_destroyed", SendMessageOptions.DontRequireReceiver);
        Destroy(gameObject, 0.01f);
    }

    public void apply_status_effect(IStatusEffect status_effect)
    {
        foreach (IStatusEffect item in status_effects)
            if (item.GetType() == status_effect.GetType())
                return;

        status_effects.Add(status_effect);
        status_effect.on_applied();
    }

    public void on_status_effect_finished(IStatusEffect status_effect)
    {
        finished_effects.Add(status_effect);
    }
}