using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealDrone : PassiveItem
{
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        transform.localPosition = new Vector3(0.3f, 0.3f, 0);
    }

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 1)
        {
            timer = 0;
        Locator.player.heal(1);
        }
    }
}
