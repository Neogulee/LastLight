using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossAnimator : EnemyAnimator
{
    private int current_idx = 0;
    private BossAttacker boss_attacker = null;
    private List<string> attack_triggers = new() { "attack_melee", "attack_range" };
    protected override void Awake()
    {
        base.Awake();
        boss_attacker = GetComponent<BossAttacker>();
    }

    public override void on_start_attack()
    {
        animator.SetTrigger(attack_triggers[boss_attacker.attack_idx]);
    }
}