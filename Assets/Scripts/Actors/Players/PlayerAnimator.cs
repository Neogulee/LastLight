using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAnimator : ActorAnimator
{
    [SerializeField]
    private Transform damager;
    private Player player;
    private Vector3 original_scale;
    private new SpriteRenderer renderer = null;

    public new void Awake()
    {
        base.Awake();

        player = GetComponent<Player>();
        renderer = GetComponent<SpriteRenderer>();
        original_scale = transform.localScale;
    }

    public void on_move_left()
    {
        if (is_stopped)
            return;

        renderer.flipX = true;
        Vector3 scale = original_scale;
        scale.x *= -1.0f;
        damager.transform.localScale = scale;
        animator.SetBool("isLeftRun", true);
    }

    public void on_move_right()
    {
        if (is_stopped)
            return;
            
        renderer.flipX = false;
        damager.transform.localScale = original_scale;
        animator.SetBool("isRightRun", true);
    }

    public void on_stop()
    {
        animator.SetBool("isLeftRun", false);
        animator.SetBool("isRightRun", false);
    }

    public void on_jump(int jump_cnt)
    {
        if (jump_cnt == 1)
            animator.SetBool("isJump", true);
        else
            animator.SetBool("isDoubleJump", true);
    }

    public void off_up()
    {
        animator.SetBool("isUp", false);
    }
    public void on_fall(bool a)
    {
        animator.SetBool("isFall", a);

    }
    
    public void on_ground()
    {
        animator.SetBool("isFall", false);
        animator.SetBool("isJump", false);
        animator.SetBool("isDoubleJump", false);
    }
}