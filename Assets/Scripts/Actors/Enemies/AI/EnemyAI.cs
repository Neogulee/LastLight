using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class EnemyAI : MonoBehaviour
{
    protected Enemy enemy;
    protected IActorController actor_controller;
    protected virtual void Awake()
    {
        enemy = GetComponent<Enemy>();
        actor_controller = GetComponent<IActorController>();
    }
}