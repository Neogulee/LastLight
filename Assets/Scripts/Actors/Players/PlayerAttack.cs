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
        //player.EventRegister<OnSpinAttackEvent>(OnSpinAttack);
    }

    public void OnAttack(IEventParam event_param)
    {
        if(player.GetAnimator().GetBool("isJump") || player.GetAnimator().GetBool("isDoubleJump"))
        {
            player.GetAnimator().SetBool("isAttack", true);
        }
        else
        {
            player.GetAnimator().SetBool("isAttack", true);
        }
    }
    public void OnSpinAttack(IEventParam event_param)
    {
        
        player.GetAnimator().SetBool("isSpinAttack",true);
    }
    
    public void OffAttack()
    {
        player.GetAnimator().SetBool("isAttack",false);
        player.GetAnimator().SetBool("isSpinAttack",false);
    }
    void on_attack_melee(int idx)
    {
        Locator.event_manager.notify(new OnAttackerEvent(idx));
    }
    void on_stop_attack_melee(int idx)
    {
        Locator.event_manager.notify(new OffAttackerEvent(idx));
    }
}
