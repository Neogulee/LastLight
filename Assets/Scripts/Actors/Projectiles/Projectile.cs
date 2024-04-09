using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    public float speed = 0.0f;
    public Vector3 direction { get; private set; } = Vector3.zero;

    public virtual void init(Vector3 dir)
    {
        direction = dir.normalized;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.Atan2(dir.y, dir.x) * 180.0f / Mathf.PI);
    }

    void FixedUpdate()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}