using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player player;
    private APlayerController controller;

    public void Init(Player player)
    {
        this.player = player;
        controller = GetComponent<APlayerController>();
        player.EventRegister<OnJumpEvent>(OnJump);
        player.EventRegister<OnLeftMoveEvent>(OnLeftMove);
        player.EventRegister<OnRightMoveEvent>(OnRightMove);
        player.EventRegister<OffLeftMoveEvent>(OffLeftMove);
        player.EventRegister<OffRightMoveEvent>(OffRightMove);
        player.EventRegister<OnUpEvent>(OnUp);
        player.EventRegister<OffUpEvent>(OffUp);
        player.EventRegister<OnShiftEvent>(OnDash);
    }
    public void OnDash(IEventParam event_param)
    {
        controller.Dash();
    }
    public void OnUp(IEventParam event_param)
    {
        player.GetAnimator().SetBool("isUp", true);
    }
    public void OffUp(IEventParam event_param)
    {
        player.GetAnimator().SetBool("isUp", false);
    }
    public void OnJump(IEventParam event_param)
    {
        controller.jump();
    }

    public void OnLeftMove(IEventParam event_param)
    {
        controller.move_left();
    }

    public void OnRightMove(IEventParam event_param)
    {
        controller.move_right();
    }

    public void OffLeftMove(IEventParam event_param)
    {
        controller.stop();
    }

    public void OffRightMove(IEventParam event_param)
    {
        controller.stop();
    }
}
