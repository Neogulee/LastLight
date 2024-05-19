using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashSummon  : PassiveItem
{

    [SerializeField] private GameObject summon_prefab = null;

    private void Start()
    {
        Locator.event_manager.register<OnAttackerEvent>(on_hit);
    }

    public void on_hit(IEventParam param)
    {
        if(param is OnAttackerEvent)
        {
        OnAttackerEvent e = param as OnAttackerEvent;
        GameObject G = Instantiate(summon_prefab, Locator.player.transform.position, Quaternion.identity);
        G.GetComponent<Damager>().damage = damage;
        }
    }
}
