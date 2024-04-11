using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldObject : MonoBehaviour
{
    // 타이머
    private float timer = 0.0f;
    Damager damager; 
    private void Start()
    {
        damager = GetComponent<Damager>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.7f)
        {
            timer = 0.0f;
            damager.disable();
            damager.enable();
        }
    }
}
