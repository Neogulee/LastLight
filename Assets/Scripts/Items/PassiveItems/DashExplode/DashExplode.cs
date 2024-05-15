using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashExplode : PassiveItem
{
    [SerializeField]
    private GameObject explosion;
    void Start()
    {
        Locator.event_manager.register<OnDashEvent>(OnExplode);
    }
    public void OnExplode(IEventParam param)
    {
        StartCoroutine(Explode());
    }
    IEnumerator Explode()
    {
        for(int i=0;i<4;i++)
        {
            GameObject G = Instantiate(explosion, Locator.player.transform.position, Quaternion.identity);
            G.GetComponent<Damager>().damage = damage;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
