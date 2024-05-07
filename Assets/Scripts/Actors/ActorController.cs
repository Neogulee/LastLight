using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Callbacks: on_move_left(), on_move_right(), on_stop(), on_jump(int jump_cnt)
/// </summary>
public interface IActorController
{
    public float move_velocity { get; set; }
    public float jump_velocity { get; set; }
    public int max_jump_cnt { get; set; }
    public int current_jump_cnt { get; }
    public void move(Vector2 dir);
    public void move_left(); 
    public void move_right();
    public void stop();
    public void jump();
    public void jump(float velocity);
    public bool check_on_platform();
}


[RequireComponent(typeof(PhysicsPlatformer))]
public class ActorController : MonoBehaviour, IActorController
{
    [field: SerializeField]
    public float move_velocity { get; set; } = 10.0f;
    [field: SerializeField]
    public float jump_velocity { get; set; } = 20.0f;
    [field: SerializeField]
    public int max_jump_cnt { get; set; } = 2;
    [field: SerializeField]
    public int current_jump_cnt { get; private set; } = 2;

    private const float KNOCKBACK_TIME = 0.35f;
    private Vector2 left_knockback_dir = new Vector2(Mathf.Cos(2.0f * Mathf.PI / 3.0f), Mathf.Sin(2.0f * Mathf.PI / 3.0f)).normalized;
    private Vector2 right_knockback_dir = new Vector2(Mathf.Cos(Mathf.PI / 3.0f), Mathf.Sin(Mathf.PI / 3.0f)).normalized;
    private float knockback_power = 0.0f;
    private float knockback_time = 0.0f;
    protected PhysicsPlatformer physics = null;
    private bool last_on_ground = false;
    protected void Awake()
    {
        physics = GetComponent<PhysicsPlatformer>();
    }

    protected void FixedUpdate()
    {
        bool on_ground = check_on_platform();
        if (!last_on_ground && on_ground && physics.velocity.y <= 0.0f) {
            current_jump_cnt = 0;
            SendMessage("on_ground", SendMessageOptions.DontRequireReceiver);
        }
        last_on_ground = on_ground;
        
        if (knockback_time > 0.0f) {
            knockback_time = Mathf.Max(0.0f, knockback_time - Time.fixedDeltaTime);
            float current_power = knockback_time / KNOCKBACK_TIME * knockback_power;
            if (physics.gravity == 0.0f)
                physics.velocity = new Vector2(current_power, current_power);
            else
                physics.velocity = new Vector2(current_power, physics.velocity.y);
        }
    }
    
    public void move(Vector2 dir)
    {
        if (knockback_time > 0.0f)
            return;

        physics.velocity = move_velocity * dir.normalized;
        if (dir.x != 0.0f)
            SendMessage(dir.x < 0 ? "on_move_left" : "on_move_right", SendMessageOptions.DontRequireReceiver);
    }

    public void move_left()
    {
        if (knockback_time > 0.0f)
            return;

        if(physics.velocity.x > -move_velocity)
        physics.velocity = new Vector2(-move_velocity, physics.velocity.y);
        SendMessage("on_move_left", SendMessageOptions.DontRequireReceiver);
    }

    public void move_right()
    {
        if (knockback_time > 0.0f)
            return;
        if(physics.velocity.x < move_velocity)
        physics.velocity = new Vector2(move_velocity, physics.velocity.y);
        SendMessage("on_move_right", SendMessageOptions.DontRequireReceiver);
    }

    public void stop()
    {
        if (knockback_time > 0.0f)
            return;

        physics.velocity = new Vector2(0.0f, physics.velocity.y);
        SendMessage("on_stop", SendMessageOptions.DontRequireReceiver);
    }

    public void jump()
    {
        jump(jump_velocity);
    }
    
    public void jump(float jump_velocity)
    {
        if (current_jump_cnt >= max_jump_cnt)
            return;

        current_jump_cnt++;
        physics.velocity = new Vector2(physics.velocity.x, jump_velocity * Mathf.Sqrt(physics.gravity / 10.0f));
        SendMessage("on_jump", current_jump_cnt, SendMessageOptions.DontRequireReceiver);
    }

    public bool check_on_platform()
    {
        return physics.collision_info.below;
    }

    public void take_knockback(float amount, bool is_right, float upPower = 0)
    {
        knockback_time = KNOCKBACK_TIME;
        if (is_right) {
            physics.velocity = right_knockback_dir * amount;
            physics.velocity += new Vector2(0.0f, upPower);
            knockback_power = amount;
        }
        else {
            physics.velocity = left_knockback_dir * amount;
            physics.velocity += new Vector2(0.0f, upPower);
            knockback_power = -amount;
        }
    }
}