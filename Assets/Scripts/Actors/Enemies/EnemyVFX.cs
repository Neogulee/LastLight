using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class EnemyVFX : MonoBehaviour
{
    public List<VisualEffect> damage_effects;
    public Material blink_material;
    private SpriteRenderer sprite_renderer;
    private Material original_material;

    void Awake()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
        original_material = sprite_renderer.material;
    }

    public void on_damaged()
    {
        sprite_renderer.material = blink_material;
        foreach (var vfx in damage_effects)
            vfx.SendEvent("OnPlay");
        Invoke("stop_blink", 0.15f);
    }

    public void stop_blink()
    {
        sprite_renderer.material = original_material;
    }
}
