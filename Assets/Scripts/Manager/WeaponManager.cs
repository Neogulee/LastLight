using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Player player;
    private void LateUpdate()
    {
        transform.position = player.transform.position;
    }
}
