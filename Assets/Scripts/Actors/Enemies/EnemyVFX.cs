using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


public class EnemyVFX : MonoBehaviour
{
    public List<VisualEffect> damage_effects, destroy_effects, attack_effects;
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

    public void on_start_attack()
    {
        foreach (var vfx in attack_effects)
            Locator.vfx_factory.create(vfx);
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
            var created = Locator.vfx_factory.create(vfx);
            if (created.HasVector2("Velocity"))
                created.SetVector2("Velocity", physics.velocity);
            created.Play();
        }
        Invoke("stop_blink", 0.15f);
    }

    public void on_destroyed()
    {
        foreach (var vfx in destroy_effects)
            Locator.vfx_factory.create(vfx);
    }

    public void stop_blink()
    {
        sprite_renderer.material = original_material;
    }

    public void show_damage()
    {
        // GameObject parent_object = Instantiate(empty_object, hp_object.transform.parent);
        // parent_object.transform.localScale = new Vector3(1.875f, 1.875f, 1.0f);
        
        // GameObject vfx_object = Instantiate(hp_object, parent_object.transform);
        // vfx_object.transform.SetParent(parent_object.transform, true);
        // vfx_object.name = "VFXText";
        // vfx_object.GetComponent<TextMeshPro>().text = info.amount.ToString();
        // vfx_object.AddComponent<Animation>();
        // vfx_object.AddComponent<Destroyer>();
        
        // Animation animation = vfx_object.GetComponent<Animation>();
        // animation.AddClip(damage_animation, "DamageAnimation");
        // animation.Play("DamageAnimation");
    }
}
