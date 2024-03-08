using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private EventManager event_manager;
    private void Update()
    {
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
        if(Input.GetKeyDown(KeyCode.Z))
        {
            event_manager.notify(new OnAttackEvent());
        }
    }
}
