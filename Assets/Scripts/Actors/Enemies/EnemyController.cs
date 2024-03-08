using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    private Enemy enemy;
    private ActorController actor_controller;
    protected virtual void Awake()
    {
        enemy = GetComponent<Enemy>();
        actor_controller = GetComponent<ActorController>();
    }

    void FixedUpdate()
    {
        actor_controller.jump();
    }
}