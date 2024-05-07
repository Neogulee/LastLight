using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;


public class FrontalAttacker : RangeAttacker
{
    public float before_attack_delay = 0.5f, after_attack_delay = 0.5f;
    private bool is_left = false;
    public override bool check()
    {
        Vector3 delta_pos = Locator.player.transform.position - transform.position;
        var hit = Physics2D.Raycast(transform.position, new Vector2(Mathf.Sign(delta_pos.x), 0.0f), attack_distance, layer);
        return !hit && Mathf.Abs(delta_pos.x) <= attack_distance && Mathf.Abs(delta_pos.y) <= 1.0f;
    }

    public override void attack(int idx)
    {
        prepare_attack(idx);
        Vector3 target = is_left ? Vector3.left : Vector3.right;

        notify_attack(idx);
        var projectile = Locator.projectile_factory.create(projectile_prefabs[idx], transform.position);
        projectile.GetComponent<Projectile>().init(target);

        stop_attack(idx);
    }
    
    public void on_move_left()
    {
        is_left = true;
    }

    public void on_move_right()
    {
        is_left = false;
    }
}