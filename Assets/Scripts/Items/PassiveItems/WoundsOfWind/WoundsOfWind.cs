using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoundsOfWind : PassiveItem
{
    private float timer = 0;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > cooldown && Input.GetKeyDown(KeyCode.Z))
        {
            timer = 0;
            Fire();

            if (Random.Range(0, 100) < 30)
            {
                for (int i = 1; i < 10; i++)
                {

                    Invoke("Fire", 0.05f * i);
                }
            }

        }
    }
    
    public void Fire()
    {
        float angleOffset = Random.Range(-5f, 5f);
        Player player = Locator.player;
        Vector2 targetDirection = (player.transform.localScale.x == -1) ? Vector2.left : Vector2.right;
        targetDirection = Quaternion.Euler(0, 0, angleOffset) * targetDirection;
        Transform bullet = PoolManager.Instance.Get(1).transform;
        Damager damager = bullet.GetComponent<Damager>();
        damager.disable();
        damager.enable();
        bullet.position = transform.position;
        Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, targetDirection) * Quaternion.Euler(0, 0, angleOffset);
        // Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, targetDirection);
        bullet.rotation = targetRotation;
        bullet.GetComponent<WoundsOfWindObject>().Init(damage, targetDirection);
        bullet.GetComponent<Damager>().damage = (int)damage;
    }
}
