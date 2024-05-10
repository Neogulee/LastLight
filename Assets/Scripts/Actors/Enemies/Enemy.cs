using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IEnemy
{
    public int power { get; }
    public int exp { get; }
}

public class Enemy : Actor, IEnemy
{
    [field: SerializeField]
    public int power { get; protected set; }
    [field: SerializeField]
    public int exp { get; protected set; }
    protected override void Awake()
    {
        base.Awake();
    }

    protected virtual void on_destroyed()
    {
        Locator.event_manager.notify(new EnemyDestroyedEvent(this));
    }
}