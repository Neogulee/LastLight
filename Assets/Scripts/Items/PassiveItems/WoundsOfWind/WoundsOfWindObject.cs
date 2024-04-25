using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoundsOfWindObject : MonoBehaviour
{
    public float damage;
    public float forceDecayRate = 0.9f;
    Rigidbody2D rigid;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    
    public void Init(float damage , Vector3 dir) 
    {
        this.damage = damage;
        rigid.velocity = dir*50f;
        Invoke("DestroyBullet", 0.3f);
        // 나중에 scale 올리는 코드 추가 
    }

    void Update()
    {
        // Decay the velocity gradually over time
        rigid.velocity *=(1-forceDecayRate * Time.fixedDeltaTime);
    }

    void DestroyBullet()
    {
        gameObject.SetActive(false);
    }
}
