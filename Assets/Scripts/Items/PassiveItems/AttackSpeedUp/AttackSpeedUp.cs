using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedUp : PassiveItem
{
    Player player;
    private void Start()
    {
        player = transform.parent.GetComponent<Player>();        
    }
    void Update()
    {
        if (player.GetAnimator().GetCurrentAnimatorStateInfo(0).IsName("PlayerGroundAttack1") ||
            player.GetAnimator().GetCurrentAnimatorStateInfo(0).IsName("PlayerGroundAttack2") ||
            player.GetAnimator().GetCurrentAnimatorStateInfo(0).IsName("PlayerAirAttack1") ||
            player.GetAnimator().GetCurrentAnimatorStateInfo(0).IsName("PlayerAirAttack2"))
        {
            player.GetAnimator().speed = level*0.5f + 1;
        }
        else
        {
            player.GetAnimator().speed = 1;
        }
    }
}
