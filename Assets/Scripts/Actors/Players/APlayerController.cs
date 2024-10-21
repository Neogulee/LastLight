using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APlayerController : ActorController
{
    [SerializeField] private GameObject effect_shadow;

    private int dashMax = 1; 
    public int DashMax{
        get{return dashMax;}
        set{dashMax = value;}
    }
    private int dashCount = 1;
    private bool shadow;

    private float velocityY = 0.0f;
    private float last_dashed_time = 0.0f;
    private float dash_cool_down = 0.5f;
    private float last_dir_x = 1.0f;
    private float afterimage_time = 0.0f;
    private float afterimage_interval_delay = 0.02f;
    new void Awake()
    {
        base.Awake();
        IEventManager event_manager = Locator.event_manager;
        event_manager.register<OnLeftMoveEvent>(on_move_left_event);
        event_manager.register<OnRightMoveEvent>(on_move_right_event);
    }
    private void on_move_left_event(IEventParam event_param) { last_dir_x = -1.0f; }
    private void on_move_right_event(IEventParam event_param) { last_dir_x = 1.0f; }

    public Vector2 getAimDir()
    {
        Vector2 vec = Vector2.zero;

        if(Input.GetKey(KeyCode.LeftArrow))
            vec.x = -1.0f;
        if(Input.GetKey(KeyCode.RightArrow))
            vec.x = 1.0f;
        if(Input.GetKey(KeyCode.UpArrow))
            vec.y = 1.0f;
        if(Input.GetKey(KeyCode.DownArrow))
            vec.y = -1.0f;
        if (vec == Vector2.zero)
            vec.x = last_dir_x;
        return vec.normalized;
    }
    public void Dash()
    {
        if (check_on_platform())
            dashCount = 1;

        if (dashCount <= 0 || (Time.time - last_dashed_time) < dash_cool_down || getAimDir() == Vector2.zero)
            return;

        dashCount--;
        last_dashed_time = Time.time;
        
        Locator.event_manager.notify(new OnDashEvent());
        float speed = 40;
        Vector2 vec = getAimDir();
        physics.fix_velocity(vec.normalized * speed);
        afterimage_time = 0.0f;
        shadow = true;
        Invoke("shadow_off", 0.2f);
        Invoke("stop", 0.2f);
        //SendMessage("on_jump", current_jump_cnt, SendMessageOptions.DontRequireReceiver);
    }

    private void DownCheck()
    {
        if (physics.velocity.y < 0.0f)
        {
            SendMessage("on_fall", true, SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            SendMessage("on_fall", false, SendMessageOptions.DontRequireReceiver);
        }
    }

    public void shadow_off()
    {
        physics.unfix_velocity();
        shadow = false;
    }

    void on_damaged(int damage)
    {
        
    }

    void on_collision((Vector2Int dir, Vector2 velocity) param)
    {
        (Vector2Int dir, Vector2 velocity) = param;
        if (dir.y == -1) {
            dashCount = dashMax;
            Locator.event_manager.notify(new OnGroundEvent(velocity.y));
        }
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();
        DownCheck();
        velocityY = GetComponent<PhysicsPlatformer>().velocity.y;
        if (shadow) {
            afterimage_time += Time.deltaTime;
            if (afterimage_time >= afterimage_interval_delay) {
                afterimage_time -= afterimage_interval_delay;
                GameObject image_object = Instantiate(effect_shadow, transform.position, Quaternion.identity);
                SpriteRenderer renderer = image_object.GetComponent<SpriteRenderer>();
                renderer.sprite = GetComponent<SpriteRenderer>().sprite;
                renderer.flipX = GetComponent<SpriteRenderer>().flipX;
                image_object.transform.localScale = transform.localScale;
            }
        }
    }
}