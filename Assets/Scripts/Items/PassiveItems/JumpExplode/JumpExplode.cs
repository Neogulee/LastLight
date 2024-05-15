using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpExplode : PassiveItem
{
    [SerializeField]
    private GameObject explosion;
    void Start()
    {
        Locator.event_manager.register<OnGroundEvent>(OnExplode);
    }
    public void OnExplode(IEventParam param)
    {
        if (param is OnGroundEvent)
        {
            OnGroundEvent p = param as OnGroundEvent;
            Debug.Log(p.power);
            if (p.power < -10)
            {
                GameObject G = Instantiate(explosion, Locator.player.transform.position, Quaternion.identity);
                G.GetComponent<Damager>().damage = damage;
                G.transform.localScale = (Vector3.one * Mathf.Abs(p.power) / 10.0f*2);
            }
        }
    }
}
