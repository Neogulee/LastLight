using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HpBar : MonoBehaviour
{
    public float y_offset = -50.0f;
    [SerializeField]
    private Transform hp_bar;

    void Awake()
    {
        Locator.event_manager.register<OnHpChangedEvent>(OnHpChange);
    }
    
    void LateUpdate()
    {
        Vector3 position = Camera.main.WorldToScreenPoint(Locator.player.transform.position);
        position.y += y_offset;
        transform.position = position;
    }

    public void OnHpChange(IEventParam event_param)
    {
        float hp = ((OnHpChangedEvent)event_param).hp;
        hp_bar.localScale = new Vector3(hp / Locator.player.max_hp, 1, 1);
    }
}
