using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Utils;


[RequireComponent(typeof(Attacker))]
public class GhostEnemyAI : EnemyAI
{
    public float speed = 5.0f;
    private Vector2Int current_move = Vector2Int.zero;
    private bool is_attacking = false;
    private Attacker attacker = null;

    protected override void Awake()
    {
        base.Awake();
        attacker = GetComponent<Attacker>();
    }

    private void update_move()
    {
        Vector3 dir = (Locator.player.transform.position - transform.position).normalized;
        actor_controller.move(dir);
    }

    public void on_finish_attack()
    {
        is_attacking = false;
    }
    
    void FixedUpdate()
    {
        if (is_attacking) {
            if (!attacker.is_stop_on_attack)
                update_move();
            return;
        }

        update_move();
        if (attacker.check()) {
            SendMessage("on_start_attack", SendMessageOptions.DontRequireReceiver);
            is_attacking = true;
            if (attacker.is_stop_on_attack)
                actor_controller.move(Vector2.zero);
        }
    }
}