using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;


public class WeaponNumber0 : Weapon
{
    public float speed = 150f;
    public int count = 1;
    void Update()
    {
        transform.Rotate(Vector3.back * speed* Time.deltaTime);
    }

    private void Start()
    {
        Debug.Log("check1");
        Batch();
    }
    
    public void Batch()
    {
        Debug.Log("check2");
        for (int i = 0; i < count; i++)
        {
            Transform bullet;

            
            if (i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
                bullet = TempGameManager.instance.poolManager.Get(id).transform;
                bullet.parent = transform;
            }
            

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * i/ count ;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up*1.5f, Space.World);

            bullet.GetComponent<Damager>().damage = (int)damage;
        }
    }
    
    
    public override void IncreaseLevel() 
    {
        count++;
        Batch();
    }
}