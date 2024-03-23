using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponNumber1 : Weapon
{
    void Update()
    {

        timer += Time.deltaTime;
        if (timer > shootThreshold)
        {
            timer = 0;
            Fire();
        }   
    }

    public override void IncreaseLevel() {
        shootThreshold *= 0.8f;
        damage *= 1.2f;

    }
    public void Fire()
    {
        Player player = Locator.player;
        Vector2 targetDirection = (player.transform.localScale.x == -1) ? Vector2.left : Vector2.right;
        Transform bullet = TempGameManager.instance.poolManager.Get(id).transform;
        bullet.position = transform.position;
        Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, targetDirection);
        bullet.rotation = targetRotation;
        bullet.GetComponent<RangedItem>().Init(damage, targetDirection , size);
        bullet.GetComponent<Damager>().damage = (int)damage;
    }
}
