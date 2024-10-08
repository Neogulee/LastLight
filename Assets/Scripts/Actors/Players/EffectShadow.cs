using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectShadow : MonoBehaviour
{
    private SpriteRenderer sprite_renderer;
    private Material material;
    private Color original_color;
    private float age = 0.0f;
    private float life = 0.5f;
    void Awake()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
        material = sprite_renderer.material;
        original_color = material.color;
    }

    void FixedUpdate()
    {
        age += Time.deltaTime;
        Color temp = original_color;
        temp.a *= 1.0f - age / life;
        material.color = temp;
        if (age >= life)
            Destroy(gameObject);
    }
}
