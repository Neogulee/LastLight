using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player player;
    private IActorController controller;

    public void Init(Player player)
    {
        this.player = player;
        controller = GetComponent<IActorController>();
        player.EventRegister<OnJumpEvent>(OnJump);
        player.EventRegister<OnLeftMoveEvent>(OnLeftMove);
        player.EventRegister<OnRightMoveEvent>(OnRightMove);
        player.EventRegister<OffLeftMoveEvent>(OffLeftMove);
        player.EventRegister<OffRightMoveEvent>(OffRightMove);
        player.EventRegister<PlayerDash>(OnDash);
    }
    public void OnDash(IEventParam event_param)
    {
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
