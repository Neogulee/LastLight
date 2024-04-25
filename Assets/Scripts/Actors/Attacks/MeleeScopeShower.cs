using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(MeleeAttacker))]
public class MeleeScopeShower: MonoBehaviour
{
    private readonly Color color = new Color(1.0f, 0.39f, 0.39f, 0.3f);
    private List<SpriteRenderer> renderers = new();
    void Awake()
    {
        var melee_attacker = GetComponent<MeleeAttacker>();
        
        foreach (var damager in melee_attacker.damagers)
        {
            var renderer = damager.GetComponent<SpriteRenderer>();
            renderer.color = color;
            renderer.enabled = false;
        }
    }

    public void on_prepare_attack_melee(int idx)
    {
        renderers[idx].enabled = true;
    }

    public void on_attack_melee(int idx)
    {
        renderers[idx].enabled = false;
    }
}