using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectShadow : MonoBehaviour
{

    void Update()
    {
        GetComponent<SpriteRenderer>().color = new Color(1,1,1,GetComponent<SpriteRenderer>().color.a - 1f*Time.deltaTime);
        if(GetComponent<SpriteRenderer>().color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
