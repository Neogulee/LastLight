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

    public VisualEffect create(VisualEffect vfx, Transform parent=null)
    {
        GameObject game_object = Instantiate(vfx.gameObject, parent is null ? transform : parent);
        game_object.transform.position = vfx.transform.position;
        VisualEffect new_vfx = game_object.GetComponent<VisualEffect>();
        new_vfx.Play();
        return new_vfx;
    }
    
}
