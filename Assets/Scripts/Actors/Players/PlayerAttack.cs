using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Player player;

    public void Init(Player player)
    {
        this.player = player;
        player.EventRegister<OnAttackEvent>(OnAttack);
    }

    public void OnAttack(IEventParam event_param)
    {
        if(player.GetAnimator().GetBool("isJump") || player.GetAnimator().GetBool("isDoubleJump"))
        {
            return;
        }
        player.GetAnimator().SetBool("isAttack", true);
    }
    
    public void OffAttack()
    {
        player.GetAnimator().SetBool("isAttack",false);
    }
}
