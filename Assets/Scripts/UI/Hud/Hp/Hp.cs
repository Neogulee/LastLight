using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HpBar : MonoBehaviour
{
    [SerializeField] private Transform hp_bar;
    private void Start()
    {
        Locator.event_manager.register<OnHpChangeEvent>(OnHpChange);
    }
    
    public void OnHpChange(IEventParam event_param)
    {
        OnHpChangeEvent hp_change_event = (OnHpChangeEvent)event_param;
        hp_bar.localScale = new Vector3((float)hp_change_event.hp / Locator.player.max_hp, 1, 1);
    }
}
