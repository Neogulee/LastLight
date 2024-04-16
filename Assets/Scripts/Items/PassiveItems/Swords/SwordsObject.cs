using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class SwordsObject : MonoBehaviour
{
    public float damage;
    public LayerMask groundLayer;
    Rigidbody2D rigid;
    int itemId = 0;
    Vector2 dir;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, Vector2 curDir, float size, int id)
    {
        itemId = id;
        dir = curDir;
        this.damage = damage;
        Invoke("GenerateBullet", 0.025f); // 0.05초 간격으로 GenerateBullet 호출
        Invoke("DestroyBullet", 3f);
    }

    void GenerateBullet()
    {
        // 현재 위치를 가져옵니다.
        Vector3 origin = transform.position;

        // X축으로 1만큼 이동한 위치를 구합니다.
        Vector3 direction = (dir == Vector2.right ? new Vector3(0.5f, 0, 0) : new Vector3(-0.5f, 0, 0));

        // 레이캐스트를 쏩니다. 아래 방향으로 레이캐스트를 쏩니다.
        RaycastHit2D hit = Physics2D.Raycast(transform.position+ direction, Vector2.down, Mathf.Infinity, groundLayer);
        if (hit.collider != null)
        {
            float distance = hit.distance;
            // 일정 거리 이하일 때만 생성
            if (distance < 1.0f)
            {
                Debug.Log(distance);
                // 충돌 지점에 무기 생성
                Generate(hit.point);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    void Generate(Vector2 spawnPosition)
    {
        // 무기 생성
        // Transform bullet = TempGameManager.instance.poolManager.Get(itemId).transform;
        // Damager damager = bullet.GetComponent<Damager>();
        // damager.disable();
        // damager.enable();
        // // 무기의 위치를 충돌 지점으로 설정
        // bullet.position = spawnPosition;
        // bullet.position += new Vector3(0, 1f, 0);

        // // 무기의 하단 좌표 계산
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

        // bullet.GetComponent<SwordsObject>().Init(damage, dir, 1, itemId);
        // bullet.GetComponent<Damager>().damage = (int)damage;
    }

    void DestroyBullet()
    {
        gameObject.SetActive(false);
    }
}