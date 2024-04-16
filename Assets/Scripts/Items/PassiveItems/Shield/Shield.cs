using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;


public class Shield : PassiveItem
{
    public float speed = 150f;
    public int count = 1;
    void Update()
    {
        // transform.Rotate(Vector3.back * speed * Time.deltaTime);
        // transform.position = TempGameManager.instance.player.transform.position;
    }

    private void Start()
    {
        Batch();
    }

    
    public void Batch()
    {
        // Debug.Log("Create Shild");
        // for (int i = 0; i < count; i++)
        // {
        //     Transform bullet;

            
        //     if (i < transform.childCount)
        //     {
        //         bullet = transform.GetChild(i);
        //     }
        //     else
        //     {
        //         bullet = TempGameManager.instance.poolManager.Get(id).transform;
        //         bullet.parent = transform;
        //     }
            

        //     bullet.localPosition = Vector3.zero;
        //     bullet.localRotation = Quaternion.identity;

        //     Vector3 rotVec = Vector3.forward * 360 * i/ count ;
        //     bullet.Rotate(rotVec);
        //     bullet.Translate(bullet.up*1.5f, Space.World);

        //     bullet.GetComponent<Damager>().damage = (int)damage;
        // }
    }
}