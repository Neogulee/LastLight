using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class LightBlinker : MonoBehaviour
{
    public float speed = 12.0f;

    private new Light2D light = null;
    private float radius = 0.0f;
    private float time = 0.0f;
    private Material material = null;
    private new SpriteRenderer renderer = null;
    void Awake()
    {
        light = GetComponent<Light2D>();
        radius = light.pointLightOuterRadius;
        renderer = GetComponent<SpriteRenderer>();
        material = renderer.material;
    }

    void FixedUpdate()
    {
        if (!light.enabled)
            return;

        time += Time.deltaTime;
        light.pointLightOuterRadius = radius * ((Mathf.Sin(speed * time) + 1.0f) / 6.0f + 1.0f - 1.5f / 6.0f);
        material.SetFloat("_CurrentTime", time);
    }

    void OnBecameInvisible()
    {
        light.enabled = false;
    }

    void OnBecameVisible()
    {
        light.enabled = true;
    }
}
