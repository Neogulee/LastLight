using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpDrain : PassiveItem
{
    [SerializeField]
    private float drainRate = 0.1f;
    void Start()
    {
        Locator.event_manager.register<OnGroundEvent>(OnDrain);
    }
    public void OnDrain(IEventParam param)
    {

    }
}
