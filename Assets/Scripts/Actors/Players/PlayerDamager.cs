using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damager))]
public class PlayerDamager : MonoBehaviour
{
    public void on_hit(int damage)
    {
        Locator.event_manager.notify(new OnPlayerAttackMelee(damage));

    }
}
