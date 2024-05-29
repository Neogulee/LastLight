using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerSummon : ActiveItem
{
    [SerializeField] private GameObject laserPrefab;
    private Player player;

    private void Start()
    {
        player = transform.parent.GetComponent<Player>();
    }
    public override void use()
    {
        if (!check_cooldown())
            return;
        
        Vector2 v = player.GetComponent<APlayerController>().getAimDir();
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        laser.transform.position = player.transform.position;
        laser.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg + 180);
        laser.GetComponent<Damager>().damage = damage;
    }
}
