using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThunderWeapon : PassiveItem
{ 
    private float timer = 0;
    private Scanner scanner;
    private void Start()
    {
        scanner = GetComponent<Scanner>();
        if (scanner == null)
        {
            Debug.LogError("Scanner 스크립트를 찾을 수 없습니다.");
            return;
        }

        // 부모 지정
        GameObject parentObject = GameObject.Find("WeaponManager");
        transform.parent = parentObject.transform;
        transform.localPosition = Vector3.zero;

    }
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > cooldown)
        {
            timer = 0;
            Fire();
        }
    }
    public void Fire()
    {
        Transform enermy = scanner.nearestTarget;
        if (enermy != null)
        {
            Transform bullet = PoolManager.Instance.Get(4).transform;
            Vector3 enemyposition = enermy.position;
            SpriteRenderer enemyspriterenderer = enermy.GetComponent<SpriteRenderer>();
            float enemyheight = enemyspriterenderer.bounds.size.y; 

            SpriteRenderer bulletspriterenderer = bullet.GetComponent<SpriteRenderer>();
            float bulletheight = bulletspriterenderer.bounds.size.y; // 번개 스프라이트의 높이를 가져옴

            Vector3 bulletposition = enemyposition - new Vector3(0, enemyheight / 2 - bulletheight / 2, 0);

            bullet.position = bulletposition; // 조정된 위치를 적용
            bullet.GetComponent<ThunderObject>().Init();
            bullet.GetComponent<Damager>().damage = (int)damage;
        }
    }
}
