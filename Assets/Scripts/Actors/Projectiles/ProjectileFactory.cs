using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileFactory: MonoBehaviour
{
    void Awake()
    {
        Locator.projectile_factory = this;
        transform.position = Vector3.zero;
    }

    public GameObject create(GameObject projectile, Vector2 position)
    {
        return Instantiate(projectile, position, Quaternion.identity, transform);
    }
}