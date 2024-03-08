using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Attacker: MonoBehaviour
{
    public List<Damager> damagers = new();
    public void assert_idx(int idx)
    {
        Debug.Assert(0 <= idx && idx < damagers.Count);
    }
    
    public void attack(int idx)
    {
        assert_idx(idx);
        damagers[idx].enable();
    }

    public void stop_attack(int idx)
    {
        assert_idx(idx);
        damagers[idx].disable();
    }
}