using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    void Update()
    {
        transform.position = TempGameManager.instance.player.transform.position;
    }
}
