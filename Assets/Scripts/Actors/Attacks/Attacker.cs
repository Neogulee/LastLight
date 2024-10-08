using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;


public abstract class Attacker: MonoBehaviour
{
    public abstract bool check();
    public abstract void attack(int idx);
    public bool is_stop_on_attack { get; protected set; } = true;
}