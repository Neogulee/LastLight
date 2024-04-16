using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderWeapon : PassiveItem
{ 
    // Update is called once per frame
    void Update()
    {
        // timer += Time.deltaTime;
        // if(timer > shootThreshold)
        // {
        //     timer = 0;
        //     Fire();
        // }
    }
    public void Fire()
    {
        // Transform enermy = TempGameManager.instance.weaponManager.GetComponent<Scanner>().nearestTarget;
        // if (enermy != null)
        // {
        //     Transform bullet = TempGameManager.instance.poolManager.Get(id).transform;

        //     Vector3 enemyPosition = enermy.position;
        //     SpriteRenderer enemySpriteRenderer = enermy.GetComponent<SpriteRenderer>();
        //     float enemyHeight = enemySpriteRenderer.bounds.size.y; // 적 스프라이트의 높이를 가져옴

        //     SpriteRenderer bulletSpriteRenderer = bullet.GetComponent<SpriteRenderer>();
        //     float bulletHeight = bulletSpriteRenderer.bounds.size.y; // 번개 스프라이트의 높이를 가져옴

        //     Vector3 bulletPosition = enemyPosition - new Vector3(0, enemyHeight / 2 - bulletHeight / 2, 0);

        //     bullet.position = bulletPosition; // 조정된 위치를 적용
        //     bullet.GetComponent<ThunderObject>().Init();
        //     bullet.GetComponent<Damager>().damage = (int)damage;
        // }
    }
}
