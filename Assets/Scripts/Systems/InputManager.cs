using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Locator.event_manager.notify(new OnJumpEvent());
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Locator.event_manager.notify(new OnLeftMoveEvent());
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            Locator.event_manager.notify(new OnRightMoveEvent());
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            Locator.event_manager.notify(new OffLeftMoveEvent());
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            Locator.event_manager.notify(new OffRightMoveEvent());
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Locator.event_manager.notify(new OnUpEvent());
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            Locator.event_manager.notify(new OffUpEvent());
        }
        if(Input.GetKeyDown(KeyCode.Z))
        {
            Locator.event_manager.notify(new OnAttackEvent());
        }
    }
}
