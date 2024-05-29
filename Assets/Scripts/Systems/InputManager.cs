using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : MonoBehaviour
{
    private enum KeyType
    {
        KEY,
        KEY_DOWN,
        KEY_UP
    };

    private void Update()
    {
        try_notify(new OnInputItemSelection(0), KeyType.KEY_DOWN, KeyCode.Alpha1);
        try_notify(new OnInputItemSelection(1), KeyType.KEY_DOWN, KeyCode.Alpha2);
        try_notify(new OnInputItemSelection(2), KeyType.KEY_DOWN, KeyCode.Alpha3);
        
        if (Locator.pause_controller.is_paused)
            return;

        try_notify(new OnJumpEvent(),           KeyType.KEY_DOWN, KeyCode.Space, KeyCode.C);
        try_notify(new OnAttackEvent(0),        KeyType.KEY_DOWN, KeyCode.Z);
        try_notify(new OnAttackEvent(1),        KeyType.KEY_DOWN, KeyCode.X);
        try_notify(new OnLeftMoveEvent(),       KeyType.KEY,      KeyCode.LeftArrow);
        try_notify(new OnRightMoveEvent(),      KeyType.KEY,      KeyCode.RightArrow);
        try_notify(new OffLeftMoveEvent(),      KeyType.KEY_UP,   KeyCode.LeftArrow);
        try_notify(new OffRightMoveEvent(),     KeyType.KEY_UP,   KeyCode.RightArrow);
        try_notify(new OnUpEvent(),             KeyType.KEY,      KeyCode.UpArrow);
        try_notify(new OffUpEvent(),            KeyType.KEY_UP,   KeyCode.UpArrow);
        try_notify(new OnShiftEvent(),          KeyType.KEY_DOWN, KeyCode.LeftShift);
        try_notify(new OnItemKeyPressed(0),     KeyType.KEY_DOWN, KeyCode.Q, KeyCode.A);
        try_notify(new OnItemKeyPressed(1),     KeyType.KEY_DOWN, KeyCode.W, KeyCode.S);
        try_notify(new OnItemKeyPressed(2),     KeyType.KEY_DOWN, KeyCode.E, KeyCode.D);
        try_notify(new OptionEvent(),           KeyType.KEY_DOWN, KeyCode.Escape);
    }

    private void try_notify(IEventParam param, KeyType key_type, params KeyCode[] key_codes)
    {
        Func<KeyCode, bool> check_key = Input.GetKey;
        if (key_type == KeyType.KEY_DOWN)
            check_key = Input.GetKeyDown;
        else if (key_type == KeyType.KEY_UP)
            check_key = Input.GetKeyUp;

        foreach (KeyCode key_code in key_codes)
        {
            if (check_key(key_code)) {
                Locator.event_manager.notify(param);
                return;
            }
        }
    }
}
