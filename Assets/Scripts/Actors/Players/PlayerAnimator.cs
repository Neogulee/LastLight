using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Player player;

    void Awake()
    {
        player = GetComponent<Player>();
    }

    public void on_move_left()
    {
        Vector3 scale = transform.localScale;
        scale.x = -1.0f;
        transform.localScale = scale;
        player.GetAnimator().SetBool("isLeftRun", true);
    }

    public void on_move_right()
    {
        Vector3 scale = transform.localScale;
        scale.x = 1.0f;
        transform.localScale = scale;
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

    public void on_ground()
    {
        player.GetAnimator().SetBool("isJump", false);
        player.GetAnimator().SetBool("isDoubleJump", false);
    }
}