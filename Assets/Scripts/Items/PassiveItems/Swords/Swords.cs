using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swords : PassiveItem
{
    public LayerMask groundLayer;
    private void Start()
    {
        groundLayer = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        // timer += Time.deltaTime;
        // if (timer > shootThreshold && Input.GetKeyDown(KeyCode.Z) && TempGameManager.instance.actorController.check_on_platform())
        // {
        //     timer = 0;
        //     Fire();
        // }
    }

    public void Fire()
    {
        // Player player = Locator.player;
        // Transform bullet = TempGameManager.instance.poolManager.Get(id).transform;
        // Damager damager = bullet.GetComponent<Damager>();
        // damager.disable();
        // damager.enable();

        // bullet.position = transform.position;

        // SpriteRenderer bulletSpriteRenderer = bullet.GetComponent<SpriteRenderer>();
        // Vector2 bulletBottomPosition = new Vector2(bullet.position.x, bullet.position.y - bulletSpriteRenderer.bounds.extents.y);

        // // 땅과의 거리 계산
        // RaycastHit2D hit = Physics2D.Raycast(bulletBottomPosition, Vector2.down, Mathf.Infinity, groundLayer);
        // if (hit.collider != null)
        // {
        //     float distanceToGround = hit.distance;

        //     // 무기의 위치를 땅과의 거리만큼 조정하여 땅에 붙이기
        //     Vector2 newPosition = bullet.position + new Vector3(0, -distanceToGround, 0);
        //     bullet.position = newPosition;
        // }

        // bullet.GetComponent<GateofBabylonObject>().Init(damage, Vector2.left, size,id);
        // bullet.GetComponent<Damager>().damage = (int)damage;

        // Transform bullet1 = TempGameManager.instance.poolManager.Get(id).transform;
        // Damager damager1 = bullet.GetComponent<Damager>();
        // damager1.disable();
        // damager1.enable();

        // bullet1.position = transform.position;
        // if (hit.collider != null)
        // {
        //     float distanceToGround = hit.distance;

        //     // 무기의 위치를 땅과의 거리만큼 조정하여 땅에 붙이기
        //     Vector2 newPosition = bullet1.position + new Vector3(0, -distanceToGround, 0);
        //     bullet1.position = newPosition;
        // }

        // bullet1.GetComponent<GateofBabylonObject>().Init(damage, Vector2.right , size,id);
        // bullet1.GetComponent<Damager>().damage = (int)damage;
    }
}
