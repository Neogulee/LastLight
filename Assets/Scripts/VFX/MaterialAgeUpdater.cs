using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialAgeUpdater : MonoBehaviour
{
    private float age = 0.0f;
    private Material material;
    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    void FixedUpdate()
    {
        age += Time.fixedDeltaTime;
        material.SetFloat("_Age", age);

        if (age >= 1.0f)
            Destroy(gameObject);
    }
}
