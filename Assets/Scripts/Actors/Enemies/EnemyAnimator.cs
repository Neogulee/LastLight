using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAnimator : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer sprite_renderer;
    protected void Awake()
    {
        animator = GetComponent<Animator>();
        sprite_renderer = GetComponent<SpriteRenderer>();
    }

    public void on_jump()
    {
        animator.SetBool("is_jumping", true);
    }
    
    public void on_move_left()
    {
        sprite_renderer.flipX = true;
        animator.SetBool("is_walking", true);
    }
    
    public void on_move_right()
    {
        sprite_renderer.flipX = false;
        animator.SetBool("is_walking", true);
    }

    public void on_stop()
    {
        animator.SetBool("is_walking", false);
    }

    public void on_ground()
    {
        animator.SetBool("is_jumping", false);       
    }

    public void on_attack()
    {
        animator.SetTrigger("attack");
    }
}