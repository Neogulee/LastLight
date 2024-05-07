using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public Vector2 target;
    public Vector2 dir;

    private bool grap;

    private float ing = 1;
    private Vector2 grapPos;
    void Start()
    {
        grap = false;
        target = Locator.player.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            grap = true;
            grapPos = Locator.player.transform.position;
        }
    }
    void Update()
    {
        if(!grap)
        {
        target+= dir * Time.deltaTime * 30;
        transform.position = target;
        transform.GetComponent<SpriteRenderer>().size =  new Vector3(Vector2.Distance(transform.position, Locator.player.transform.position)*0.5f, 0.5f, 1);
        //target위치에서 player위치를 바라보게 회전
        Vector2 dir2 = Locator.player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir2.y, dir2.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    else
    {
        Locator.player.transform.position = transform.position +( (Vector3)grapPos -  transform.position) * ing; 
        transform.GetComponent<SpriteRenderer>().size =  new Vector3(Vector2.Distance(transform.position, Locator.player.transform.position)*0.5f, 0.5f, 1);
        ing -= Time.deltaTime * 2;
        if(ing < 0)
        {
            Destroy(gameObject);
        }
    }
    }
}
