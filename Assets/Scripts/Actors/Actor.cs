using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IActor
{
    public int hp { get; }
    public void take_damage(int damage);
    public void heal(int amount);
    public void destroy();
}

/// <summary>
/// Callbacks: on_damaged(int damage)
/// </summary>
public abstract class Actor : MonoBehaviour, IActor
{
    public int _max_hp = 100;

    public int max_hp  { get; protected set; }
    [field: SerializeField]
    public int hp { get; protected set; }
    protected virtual void Awake()
    {
        max_hp = _max_hp;
        hp = max_hp;
    }

    public virtual void take_damage(int damage)
    {
        hp = Mathf.Max(0, hp - damage);
        SendMessage("on_damaged", damage, SendMessageOptions.DontRequireReceiver);
        if (hp <= 0)
            destroy();
    }

    public void heal(int amount)
    {
        hp = Mathf.Min(max_hp, hp + amount);
    }

    public void destroy()
    {
        Destroy(gameObject);
    }
}