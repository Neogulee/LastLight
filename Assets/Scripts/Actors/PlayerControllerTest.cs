using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTest : PlayerMove
{
    Player player;
    Controller2D controller;

    int jump_count = 0;
    public new void Init(Player player)
    {
        this.player = player;
        controller = GetComponent<Controller2D>();
        player.EventRegister<OnJumpEvent>(OnJump);
        player.EventRegister<OnLeftMoveEvent>(OnLeftMove);
        player.EventRegister<OnRightMoveEvent>(OnRightMove);
        player.EventRegister<OffLeftMoveEvent>(OffLeftMove);
        player.EventRegister<OffRightMoveEvent>(OffRightMove);
    }
    public new void OnJump(IEventParam event_param)
    {
        controller.jump();
    }

    public new void OnLeftMove(IEventParam event_param)
    {
        controller.move_left();
    }
    public new void OnRightMove(IEventParam event_param)
    {
        controller.move_right();
    }
    public new void OffLeftMove(IEventParam event_param)
    {
        player.GetAnimator().SetBool("isLeftRun", false);
    }
    public new void OffRightMove(IEventParam event_param)
    {
        player.GetAnimator().SetBool("isRightRun", false);
    }
    public new void DownCheck() { }
}
