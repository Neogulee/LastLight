﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelManager : MonoBehaviour
{
    public ItemSelector item_selector = null;
    public float exp_multiplier = 1.1f;
    public int exp { get; private set; } = 0;
    public int exp_to_next_level { get; private set; } = 10;
    public int level { get; private set; } = 1;
    void Awake()
    {
        Locator.level_manager = this;
        Locator.event_manager.register<EnemyDestroyedEvent>(on_enemy_destroyed);
    }
    
    public void on_enemy_destroyed(IEventParam param)
    {
        EnemyDestroyedEvent args = (EnemyDestroyedEvent)param;
        increase_exp(args.enemy.exp);
    }

    public void increase_exp(int value)
    {
        exp += value;
        while (exp >= exp_to_next_level)
        {
            exp -= exp_to_next_level;
            level++;
            exp_to_next_level = (int)(exp_to_next_level * exp_multiplier);
            item_selector?.show_selection();
            // TODO: Pause
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            increase_exp(exp_to_next_level);
        }
    }
}
