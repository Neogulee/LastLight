using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : MonoBehaviour
{
    [SerializeField] private Transform hp_bar;
    [SerializeField] EventManager event_manager;
    private void Start()
    {
       event_manager.register<OnHpChangeEvent>(OnHpChange);
    }
    
    public void OnHpChange(IEventParam event_param)
    {
        OnHpChangeEvent hp_change_event = (OnHpChangeEvent)event_param;
        hp_bar.localScale = new Vector3(hp_change_event.hp / 100f, 1, 1);
    }
}
