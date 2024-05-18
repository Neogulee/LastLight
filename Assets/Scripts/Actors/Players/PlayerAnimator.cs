using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]
    private Transform damager;
    private Player player;
    private Vector3 original_scale;
    private new SpriteRenderer renderer = null;

    void Awake()
    {
        player = GetComponent<Player>();
        renderer = GetComponent<SpriteRenderer>();
        original_scale = transform.localScale;
    }

    public void on_move_left()
    {
        renderer.flipX = true;
        Vector3 scale = original_scale;
        scale.x *= -1.0f;
        damager.transform.localScale = scale;
        player.GetAnimator().SetBool("isLeftRun", true);
    }

    public void on_move_right()
    {
        renderer.flipX = false;
        damager.transform.localScale = original_scale;
        player.GetAnimator().SetBool("isRightRun", true);
    }

    public void on_stop()
    {
        player.GetAnimator().SetBool("isLeftRun", false);
        player.GetAnimator().SetBool("isRightRun", false);
    }

    public void on_jump(int jump_cnt)
    {
        if (jump_cnt == 1)
            player.GetAnimator().SetBool("isJump", true);
        else
            player.GetAnimator().SetBool("isDoubleJump", true);
    }

    public void off_up()
    {
        player.GetAnimator().SetBool("isUp", false);
    }
    public void on_fall(bool a)
    {
        player.GetAnimator().SetBool("isFall", a);

    }
    
    public void on_ground()
    {
        player.GetAnimator().SetBool("isFall", false);
        player.GetAnimator().SetBool("isJump", false);
        player.GetAnimator().SetBool("isDoubleJump", false);
    }
}