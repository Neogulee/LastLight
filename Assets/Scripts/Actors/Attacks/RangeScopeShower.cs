using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class RangeScopeShower: MonoBehaviour
{
    private readonly Color color = new Color(1.0f, 0.39f, 0.39f, 0.3f);
    [SerializeField]
    private List<SpriteRenderer> renderers = new();
    public int idx = 0;
    void Awake()
    {
        foreach (var renderer in renderers)
            renderer.color = color;
    }

    public void on_prepare_attack_range(int idx)
    {
        var renderer = renderers[idx];
        renderer.enabled = true;
        Vector3 delta = Locator.player.transform.position - transform.position;
        Vector3 rotation = new Vector3(0.0f, 0.0f, Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg);
        renderer.transform.rotation = Quaternion.Euler(rotation);
    }
    public void on_attack_range(int idx)
    {
        renderers[idx].enabled = false;
    }
}