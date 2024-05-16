using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private void Update()
    {
        IEventManager event_manager = Locator.event_manager;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            event_manager.notify(new OnJumpEvent());
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            event_manager.notify(new OnLeftMoveEvent());
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            event_manager.notify(new OnRightMoveEvent());
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            event_manager.notify(new OffLeftMoveEvent());
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            event_manager.notify(new OffRightMoveEvent());
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            event_manager.notify(new OnUpEvent());
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            event_manager.notify(new OffUpEvent());
        }
        if(Input.GetKeyDown(KeyCode.Z))
        {
            event_manager.notify(new OnAttackEvent(0));
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            event_manager.notify(new OnAttackEvent(1));
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            event_manager.notify(new OnShiftEvent());
        }
        if(Input.GetKeyDown(KeyCode.Q))
            event_manager.notify(new OnItemKeyPressed(0));
        if(Input.GetKeyDown(KeyCode.W))
            event_manager.notify(new OnItemKeyPressed(1));
        if(Input.GetKeyDown(KeyCode.E))
            event_manager.notify(new OnItemKeyPressed(2));
        if (Input.GetKeyDown(KeyCode.Escape))
            event_manager.notify(new OptionEvent());


    }
}
