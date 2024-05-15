using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;


public class Shield : PassiveItem
{
    public float speed = 50f;
    private GameObject orb;
    private void Start()
    {
        orb = transform.GetChild(0).gameObject;
        orbUp();
    }
    void Update()
    {
        transform.Rotate(Vector3.back * speed * Time.deltaTime);
    }


    public override bool increase_level()
    {
        if (base.increase_level())
        {
            orbUp();
            return true;
        }
        return false;

    }
    public void orbUp()
    {
        GameObject G = Instantiate(orb, transform);
        G.SetActive(true);
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).localPosition = Vector3.zero;
            transform.GetChild(i).rotation = Quaternion.Euler(Vector3.back * 360 / (transform.childCount - 1) * (i - 1));
            transform.GetChild(i).Translate(Vector3.up * 1.5f);
            transform.GetChild(i).rotation = Quaternion.Euler(0, 0, 0);
            transform.GetChild(i).GetComponent<Damager>().damage = damage;
        }
        speed += 20f;
    }
}