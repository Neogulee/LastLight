using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IActor
{
    public int hp { get; }
    public bool take_damage(int damage);
    public void heal(int amount);
    public void destroy();
}

/// <summary>
/// Callbacks: on_damaged(int damage), on_destroyed()
/// </summary>
public abstract class Actor : MonoBehaviour, IActor
{
    [field: SerializeField]
    public int max_hp  { get; protected set; } = 100;
    public int hp { get; protected set; }
    protected virtual void Awake()
    {
        hp = max_hp;
    }

    public virtual bool take_damage(int damage)
    {
        hp = Mathf.Max(0, hp - damage);
        SendMessage("on_damaged", damage, SendMessageOptions.DontRequireReceiver);
        if (hp <= 0)
            destroy();
        return true;
    }

    public void heal(int amount)
    {
        hp = Mathf.Min(max_hp, hp + amount);
    }

    public void destroy()
    {
        SendMessage("on_destroyed", SendMessageOptions.DontRequireReceiver);
        Destroy(gameObject, 0.01f);
    }
}