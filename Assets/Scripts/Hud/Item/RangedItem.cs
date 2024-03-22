using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedItem : MonoBehaviour
{
    public float damage;

    Rigidbody2D rigid;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    
    public void Init(float dagame , Vector3 dir) 
    {
        rigid.velocity = dir*15f;
        Invoke("DestroyBullet", 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 나중에 몬스터 충돌 함수 추가 

    }
    void DestroyBullet()
    {
        gameObject.SetActive(false);
    }
}
