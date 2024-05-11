using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BombDestroyer: MonoBehaviour
{
    public void on_finish_attack()
    {
        Destroy(gameObject);
    }
}