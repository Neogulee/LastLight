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
    private PhysicsPlatformer physics = null;
    void Awake()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
        original_material = sprite_renderer.material;
        physics = GetComponent<PhysicsPlatformer>();
    }

    public void on_damaged()
    {
        Invoke("play_damage_vfx", 0.01f);
    }

    public void play_damage_vfx()
    {
        sprite_renderer.material = blink_material;
        foreach (var vfx in damage_effects)
        {
            if (vfx.HasVector2("Velocity"))
                vfx.SetVector2("Velocity", physics.velocity);
            vfx.Play();
        }
        Invoke("stop_blink", 0.15f);
    }

    public void stop_blink()
    {
        sprite_renderer.material = original_material;
    }
}
