using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item2 : MonoBehaviour
{
    public float damage;

    Rigidbody2D rigid;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // 계속 회전 
    private void Update()
    {
        transform.Rotate(0, 0, 10);
    }
    public void Init(float damage, Vector3 dir, float size)
    {
        this.damage = damage;
        rigid.velocity = dir * 15f;
        Invoke("DestroyBullet", 3f);
    }

    void DestroyBullet()
    {
        gameObject.SetActive(false);
    }
}
