using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class PassiveItem: Item
{
    void Start()
    {
        // 부모 지정
        GameObject parentObject = GameObject.Find("WeaponManager");
        transform.parent = parentObject.transform;
        transform.localPosition = Vector3.zero;

    }
}
