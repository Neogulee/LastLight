using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderObject : MonoBehaviour
{
    [SerializeField] bool self_target;
    [SerializeField] SpriteRenderer sprite_target;
    [SerializeField] Sprite[] sprite_ani;
    [SerializeField] float time;

    int reset_count = 0;
    Damager damager;
    private void Start()
    {
        damager = GetComponent<Damager>();
    }
    public void Init()
    {
        //damager.disable();
        //damager.enable();
        PlayAnimation();
        damager.enable();
    }
    private void PlayAnimation()
    {
        if(reset_count < sprite_ani.Length)
        {
            sprite_target.sprite = sprite_ani[reset_count];
            reset_count++;
        }
        else
        {
            reset_count = 0;
        }
        if (reset_count == sprite_ani.Length)
        {
            DestroyItem();
        }
        else
        {
            Invoke("PlayAnimation", time); 
        }
    }


    void DestroyItem()
    {
        damager.disable();
        gameObject.SetActive(false);
    }
}
