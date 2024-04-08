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
    
    public void Init(float damage , Vector3 dir , float size) 
    {
        this.damage = damage;
        rigid.velocity = dir*15f;
        Invoke("DestroyBullet", 3f);
        // 나중에 scale 올리는 코드 추가 
    }

    void DestroyBullet()
    {
        gameObject.SetActive(false);
    }
}
