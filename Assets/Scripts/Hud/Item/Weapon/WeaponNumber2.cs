using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponNumber2 : Weapon
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

    public override void IncreaseLevel()
    {
        shootThreshold *= 0.8f;
        damage *= 1.2f;

    }
    public void Fire()
    {
        Player player = Locator.player;
        // Vector2 targetDirection = (player.transform.localScale.x == -1) ? Vector2.left+Vector2.up : Vector2.right+Vector2.up;

        // 나중에 깔끔하게 코드 수정 필요
        Transform bullet = TempGameManager.instance.poolManager.Get(id).transform;
        bullet.position = transform.position;
        Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, Vector2.left + Vector2.up);
        bullet.rotation = targetRotation;
        bullet.GetComponent<Item2>().Init(damage, Vector2.left + Vector2.up, size);
        bullet.GetComponent<Damager>().damage = (int)damage;

        Transform bullet1 = TempGameManager.instance.poolManager.Get(id).transform;
        bullet1.position = transform.position;
        Quaternion targetRotation1 = Quaternion.FromToRotation(Vector3.up, Vector2.right + Vector2.up);
        bullet1.rotation = targetRotation1;
        bullet1.GetComponent<Item2>().Init(damage, Vector2.right + Vector2.up, size);
        bullet1.GetComponent<Damager>().damage = (int)damage;
    }

}