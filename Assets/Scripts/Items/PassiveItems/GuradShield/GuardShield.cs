using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardShield : PassiveItem
{
    // Start is called before the first frame update

    float time = 0;
    void Start()
    {
        Locator.event_manager.register<OnGuardDamageEvent>(Guard);
        Locator.player.defenceAttack = 1;
    }
    void Update()
    {
        if(time > 0 )
        {
            time -= Time.deltaTime;
            if(time < 0)
            {
                Locator.player.defenceAttack = 1;
                GetComponent<SpriteRenderer>().color = new Color(1, 1,1,1);
                time = 0;
            }
        }
        transform.Rotate(Vector3.back * 150 * Time.deltaTime);
    }
    public void Guard(IEventParam event_param)
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1,1,0);
        time = 3;
    }
}
