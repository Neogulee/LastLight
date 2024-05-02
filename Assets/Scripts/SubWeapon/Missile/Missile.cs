using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public Transform team;

    public float spinSpeed = 400;
    public float speed = 5;
    void Update()
    {
        Move();
    }
    void Move()
    {
        if (target == null)
        {
            return;
        }
        Vector3 myPos = transform.position;
        Vector3 quaternionToTarget;
        quaternionToTarget = Quaternion.Euler(0, 0, 1) * (target.position - myPos);
        Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: quaternionToTarget);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime*spinSpeed*Random.Range(0.8f, 1.2f));

        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
    void on_hit()
    {
        Destroy(gameObject);
    }
}
