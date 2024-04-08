using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoundsOfWind : Weapon
{
    void Update()
    {

        timer += Time.deltaTime;
        if (timer > shootThreshold && Input.GetKeyDown(KeyCode.Z))
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
        float angleOffset = Random.Range(-10f, 10f);
        Player player = Locator.player;
        Vector2 targetDirection = (player.transform.localScale.x == -1) ? Vector2.left : Vector2.right;
        targetDirection = Quaternion.Euler(0, 0, angleOffset) * targetDirection;
        Transform bullet = TempGameManager.instance.poolManager.Get(id).transform;
        Damager damager = bullet.GetComponent<Damager>();
        damager.disable();
        damager.enable();
        bullet.position = transform.position;
        Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, targetDirection) * Quaternion.Euler(0, 0, angleOffset);
        // Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, targetDirection);
        bullet.rotation = targetRotation;
        bullet.GetComponent<RangedItem>().Init(damage, targetDirection , size);
        bullet.GetComponent<Damager>().damage = (int)damage;
    }
}
