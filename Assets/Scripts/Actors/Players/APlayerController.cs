using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APlayerController : ActorController
{
    [SerializeField] private GameObject effect_shadow;

    private bool shadow;
    public void Dash()
    {
        Vector2 vec = Vector2.zero;

        float speed = 40;
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            vec.x = -1.0f;
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            vec.x = 1.0f;
        }
        if(Input.GetKey(KeyCode.UpArrow))
        {
            vec.y = 1.0f;
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            vec.y = -1.0f;
        }
        physics.velocity = vec.normalized * speed;
        shadow = true;
        Invoke("shadow_off", 0.2f);
        Invoke("stop", 0.2f);
        //SendMessage("on_jump", current_jump_cnt, SendMessageOptions.DontRequireReceiver);
    }
    public void shadow_off()
    {
        shadow = false;
    }
    public void Update()
    {
        if(shadow)
        {
            GameObject G = Instantiate(effect_shadow, transform.position, Quaternion.identity);
            G.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
            G.transform.localScale = transform.localScale;
        }
    }
}