using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTileMap : MonoBehaviour
{

    void Start()
    {
        Invoke("DestroyObject", 10f);
    }



    private void DestroyObject()
    {
        Destroy(gameObject);
    }

}
