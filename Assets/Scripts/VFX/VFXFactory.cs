using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.VFX;


public class VFXFactory : MonoBehaviour
{
    void Awake()
    {
        transform.position = Vector3.zero;
        Locator.vfx_factory = this;
    }

    public VisualEffect create(VisualEffect vfx)
    {
        GameObject game_object = Instantiate(vfx.gameObject, transform);
        game_object.transform.position = vfx.transform.position;
        VisualEffect new_vfx = game_object.GetComponent<VisualEffect>();
        new_vfx.Play();
        return new_vfx;
    }
}
