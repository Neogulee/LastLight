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

    public void on_jump(int jump_cnt)
    {
        animator.SetBool("is_jumping", true);
    }
    
    public void on_move_left()
    {
        Vector3 scale = transform.localScale;
        scale.x = -1.0f;
        transform.localScale = scale;
        animator.SetBool("is_walking", true);
    }
    
    public void on_move_right()
    {
        Vector3 scale = transform.localScale;
        scale.x = 1.0f;
        transform.localScale = scale;
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

    public void on_start_attack()
    {
        animator.SetTrigger("attack");
    }
}