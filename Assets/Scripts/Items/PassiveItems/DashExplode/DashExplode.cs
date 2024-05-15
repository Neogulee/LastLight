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
    public override bool increase_level()
    {
        if (base.increase_level())
        {
            Locator.player.MaxHpUp(10);
            if (level >= 3) Locator.player.GetComponent<APlayerController>().DashMax = 3;
            return true;
        }
        return false;
    }
    IEnumerator Explode()
    {
        for(int i=0;i<4;i++)
        {
            Instantiate(explosion, Locator.player.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
