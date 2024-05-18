using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    IEnumerator destroy_vfx(GameObject vfx_object)
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(vfx_object);
    }

    public VisualEffect create(VisualEffect vfx, Transform parent=null)
    {
        GameObject game_object = Instantiate(vfx.gameObject, parent is null ? transform : parent);
        game_object.transform.position = vfx.transform.position;
        VisualEffect new_vfx = game_object.GetComponent<VisualEffect>();
        new_vfx.Play();
        StartCoroutine(destroy_vfx(game_object));
        return new_vfx;
    }
    
}
