using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DummyAnimator : MonoBehaviour
{
    private Attacker attacker = null;
    void Awake()
    {
        attacker = GetComponent<Attacker>();
    }

    public void on_start_attack()
    {
        attacker.attack(0);
    }
}