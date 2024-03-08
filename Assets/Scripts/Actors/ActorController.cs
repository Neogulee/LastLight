using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IActorController
{
    public float move_velocity { get; set; }
    public float jump_velocity { get; set; }
    public int max_jump_cnt { get; set; }
    public int current_jump_cnt { get; }
    public void move_left(); 
    public void move_right();
    public void stop();
    public void jump();
    public void jump(float velocity);
    public bool check_on_platform();
}

/// <summary>
/// Callbacks: on_move_left(), on_move_right(), on_jump()
/// </summary>
public class ActorController : MonoBehaviour, IActorController
{
    [field: SerializeField]
    public float move_velocity { get; set; } = 1.0f;
    [field: SerializeField]
    public float jump_velocity { get; set; } = 10.0f;
    [field: SerializeField]
    public int max_jump_cnt { get; set; } = 2;
    public int current_jump_cnt { get; private set; } = 0;

    private Actor actor;
    private new Rigidbody2D rigidbody;
    private new BoxCollider2D collider;

    void Awake()
    {
        actor = GetComponent<Actor>();
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }

    public void move_left()
    {
        var velocity = rigidbody.velocity;
        rigidbody.velocity = new Vector2(-move_velocity, velocity.y);
        SendMessage("on_move_left", SendMessageOptions.DontRequireReceiver);
    }
    
    public void move_right()
    {
        var velocity = rigidbody.velocity;
        rigidbody.velocity = new Vector2(move_velocity, velocity.y);
        SendMessage("on_move_right", SendMessageOptions.DontRequireReceiver);
    }

    public void stop()
    {
        var velocity = rigidbody.velocity;
        rigidbody.velocity = new Vector2(0.0f, velocity.y);
        SendMessage("on_stop", SendMessageOptions.DontRequireReceiver);
    }

    public void jump()
    {
        jump(jump_velocity);
    }
    
    public void jump(float velocity)
    {
        if (check_on_platform())
            current_jump_cnt = 0;

        if (current_jump_cnt >= max_jump_cnt)
            return;

        current_jump_cnt++;
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, velocity);
        SendMessage("on_jump", SendMessageOptions.DontRequireReceiver);
    }

    public bool check_on_platform()
    {
        Bounds bounds = collider.bounds;
        RaycastHit2D hit = Physics2D.Raycast(
            new Vector2(bounds.center.x, bounds.min.y),
            Vector2.down,
            0.03125f,
            LayerMask.GetMask("Ground") | LayerMask.GetMask("Platform")
        );
        return hit && rigidbody.velocity.y <= 0;
    }
}
