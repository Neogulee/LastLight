using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IEnemy
{
    public int power { get; }
}

public class Enemy : Actor, IEnemy
{
    public int _power;
    public int power { get; protected set; }
    protected override void Awake()
    {
        base.Awake();
        
        power = _power;
    }
}