using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;


public class TestActiveItem : ActiveItem
{
    public override void use()
    {
        Debug.Log("Test Item Use");
    }
}
