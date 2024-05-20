using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAnimator : MonoBehaviour
{
    protected Animator animator;
    protected SpriteRenderer sprite_renderer;
    private Vector3 original_scale; 
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        sprite_renderer = GetComponent<SpriteRenderer>();
        original_scale = transform.localScale;
    }

    public void on_jump(int jump_cnt)
    {
        animator.SetBool("is_jumping", true);
    }
    
    public void on_move_left()
    {
        Vector3 scale = original_scale;
        scale.x *= -1.0f;
        transform.localScale = scale;
        animator.SetBool("is_walking", true);
    }
    
    public void on_move_right()
    {
        transform.localScale = original_scale;
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

    public virtual void on_start_attack()
    {
        animator.SetTrigger("attack");
    }
}