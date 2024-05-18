using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class HpDrainVFX : MonoBehaviour
{
    private VisualEffect vfx = null;
    void Awake()
    {
        vfx = GetComponent<VisualEffect>();
    }

    void FixedUpdate()
    {
        vfx.SetVector2("TargetPosition", Locator.player.transform.position);
    }
}
