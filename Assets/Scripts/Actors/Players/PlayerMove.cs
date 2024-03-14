using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Player player;

    int jump_count = 0;
    public void Init(Player player)
    {
        this.player = player;
        player.EventRegister<OnJumpEvent>(OnJump);
        player.EventRegister<OnLeftMoveEvent>(OnLeftMove);
        player.EventRegister<OnRightMoveEvent>(OnRightMove);
        player.EventRegister<OffLeftMoveEvent>(OffLeftMove);
        player.EventRegister<OffRightMoveEvent>(OffRightMove);
    }
    public void OnJump(IEventParam event_param)
    {
        if(jump_count >= 2)
        {
            return;
        }
        jump_count++;
        if(jump_count == 2)
        {
            player.GetAnimator().SetBool("isDoubleJump", true);
        }
        else
        {
            player.GetAnimator().SetBool("isJump", true);
        }
        player.GetRigidbody().velocity = Vector2.up * player.GetJumpPower();
    }
    public void OnLeftMove(IEventParam event_param)
    {
        RaycastHit2D hit = Physics2D.BoxCast(player.transform.position+(Vector3)player.GetBoxCollider().offset, player.GetBoxCollider().size,0,Vector2.left, 0.1f, LayerMask.GetMask("Ground"));
        if (hit.collider == null)
        {
            player.GetAnimator().SetBool("isLeftRun", true);
            if(player.GetAnimator().GetCurrentAnimatorStateInfo(0).IsName("PlayerRun")||player.GetAnimator().GetCurrentAnimatorStateInfo(0).IsName("PlayerJump")||player.GetAnimator().GetCurrentAnimatorStateInfo(0).IsName("PlayerDoubleJump")||player.GetAnimator().GetCurrentAnimatorStateInfo(0).IsName("PlayerJumpDown"))
            {
                player.GetSpriteRenderer().flipX = true;
                player.GetRigidbody().velocity = Vector2.up * player.GetRigidbody().velocity.y;
                player.GetRigidbody().velocity += Vector2.left * player.GetMoveSpeed();
            }
        }
        else
        {
             player.GetRigidbody().velocity = Vector2.up * player.GetRigidbody().velocity.y;
            OffLeftMove(event_param);
        }
    }
    public void OnRightMove(IEventParam event_param)
    {
        RaycastHit2D hit = Physics2D.BoxCast(player.transform.position+(Vector3)player.GetBoxCollider().offset, player.GetBoxCollider().size,0,Vector2.right, 0.1f, LayerMask.GetMask("Ground"));
        if (hit.collider == null)
        {
            player.GetAnimator().SetBool("isRightRun", true);
            if(player.GetAnimator().GetCurrentAnimatorStateInfo(0).IsName("PlayerRun")||player.GetAnimator().GetCurrentAnimatorStateInfo(0).IsName("PlayerJump")||player.GetAnimator().GetCurrentAnimatorStateInfo(0).IsName("PlayerDoubleJump")||player.GetAnimator().GetCurrentAnimatorStateInfo(0).IsName("PlayerJumpDown"))
            {
            player.GetSpriteRenderer().flipX = false;
            player.GetRigidbody().velocity = Vector2.up * player.GetRigidbody().velocity.y;
            player.GetRigidbody().velocity += Vector2.right * player.GetMoveSpeed();
            }
        }
        else
        {
             player.GetRigidbody().velocity = Vector2.up * player.GetRigidbody().velocity.y;
             OffRightMove(event_param);
        }
    }
    public void OffLeftMove(IEventParam event_param)
    {
        player.GetAnimator().SetBool("isLeftRun", false);
    }
    public void OffRightMove(IEventParam event_param)
    {
        player.GetAnimator().SetBool("isRightRun", false);
    }

    public void DownCheck()
    {
        if(player.GetRigidbody().velocity.y <= 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(player.transform.position, Vector2.down, 1f, LayerMask.GetMask("Ground"));
            if (hit.collider != null)
            {
                jump_count = 0;
                player.GetAnimator().SetBool("isJump", false);
                player.GetAnimator().SetBool("isDoubleJump", false);
            }
            player.GetAnimator().SetBool("isFall", true);
        }
        else
        {
            player.GetAnimator().SetBool("isFall", false);
        }
    }
}
