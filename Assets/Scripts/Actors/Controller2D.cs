using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class Controller2D : MonoBehaviour, IActorController
{
    [field: SerializeField]
    public float move_velocity { get; set; } = 10.0f;
    [field: SerializeField]
    public float jump_velocity { get; set; } = 20.0f;
    [field: SerializeField]
    public int max_jump_cnt { get; set; } = 2;
    [field: SerializeField]
    public int current_jump_cnt { get; private set; } = 2;
    public float gravity = 10.0f;
    public Vector2 velocity = Vector2.zero;

    public const float SKIN_WIDTH = 0.015f;

    public LayerMask collision_mask;
    public LayerMask one_way_collision_mask;
    public CollisionInfo collision_info;

    public int horizontal_ray_count = 4;
    public int vertical_ray_count = 4;
    public int max_climb_angle = 45;
    public int max_descend_angle = 45;

    private bool is_jumping_down = false;

    float horizontal_ray_spacing;
    float vertical_ray_spacing;

    private new BoxCollider2D collider;
    private RaycastOrigins raycast_origins;
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        calculate_ray_spacing();
    }

    void FixedUpdate()
    {
        move();
    }

    public void move_left()
    {
        velocity = new Vector2(-move_velocity, velocity.y);
        SendMessage("on_move_left", SendMessageOptions.DontRequireReceiver);
    }

    public void move_right()
    {
        velocity = new Vector2(move_velocity, velocity.y);
        SendMessage("on_move_right", SendMessageOptions.DontRequireReceiver);
    }

    public void stop()
    {
        velocity = new Vector2(0.0f, velocity.y);
        SendMessage("on_stop", SendMessageOptions.DontRequireReceiver);
    }

    public void jump()
    {
        jump(jump_velocity);
    }
    
    public void jump(float jump_velocity)
    {
        if (check_on_platform())
            current_jump_cnt = 0;

        if (current_jump_cnt >= max_jump_cnt)
            return;

        current_jump_cnt++;
        velocity = new Vector2(velocity.x, jump_velocity * Mathf.Sqrt(gravity / 10.0f));
        SendMessage("on_jump", SendMessageOptions.DontRequireReceiver);
    }

    public bool check_on_platform()
    {
        return collision_info.below;
    }

    public void move()
    {
        update_raycast_origins();
        bool last_on_ground = check_on_platform();
        collision_info.reset();
        
        Vector2 vec = velocity * Time.fixedDeltaTime;
        // if (vec.y < 0)
        //     descend_slope(ref vec);
        if (vec.x != 0 && vec.y != 0.0f)
            horizontal_collisions(ref vec);
        if (vec.y != 0.0f)
            vertical_collisions(ref vec);
        if (vec.x != 0.0f && vec.y == 0.0f)
            climb_stair(ref vec);

        transform.Translate(vec);
        velocity.y -= gravity * Time.fixedDeltaTime;
        if (check_on_platform() && velocity.y < 0.0f)
            velocity.y = 0.0f;
        if (!last_on_ground && check_on_platform())
            SendMessage("on_ground", SendMessageOptions.DontRequireReceiver);
    }

    Collider2D[] horizontal_collisions(ref Vector2 velocity, int layers_to_check_passing = 0)
    {
        float direction_x = Mathf.Sign(velocity.x);
        float ray_length = Mathf.Abs(velocity.x) + SKIN_WIDTH;
        Collider2D[] ret = null;
        HashSet<Collider2D> colliders_set = new HashSet<Collider2D>();
        for (int i = 0; i < horizontal_ray_count; i++) {
            Vector2 ray_origin = (direction_x == -1) ? raycast_origins.bottom_left : raycast_origins.bottom_right;
            ray_origin += Vector2.up * horizontal_ray_spacing * i;
            RaycastHit2D raycast_hit = Physics2D.Raycast(ray_origin, Vector2.right * direction_x, ray_length, collision_mask);

            float move_distance = ray_length;

            Debug.DrawRay(ray_origin, direction_x * Vector2.right * ray_length, Color.red);

            if (raycast_hit.collider != null) {
                float slope_angle = Vector2.Angle(raycast_hit.normal, Vector2.up);
                if (i == 0 && slope_angle <= max_climb_angle) {
                    float distance_to_slope = raycast_hit.distance - SKIN_WIDTH;

                    Vector2 slope_velocity = get_slope_velocity(ray_origin, Vector2.right * direction_x, ray_length);
                    if (velocity.y <= slope_velocity.y) {
                        velocity = slope_velocity;
                        collision_info.below = true;
                    }
                }
                else {
                    velocity.x = direction_x * (raycast_hit.distance - SKIN_WIDTH);
                }
                ray_length = Mathf.Abs(velocity.x) + SKIN_WIDTH;

                collision_info.left = (direction_x == -1);
                collision_info.right = (direction_x == 1);

                move_distance = raycast_hit.distance;
            }

            if (layers_to_check_passing != 0) {
                RaycastHit2D[] ray_all_hit = Physics2D.RaycastAll(ray_origin, Vector2.right * direction_x, move_distance, layers_to_check_passing);
                for (int j = 0; j < ray_all_hit.Length; j++)
                    colliders_set.Add(ray_all_hit[j].collider);
            }
        }
        int cnt = 0;
        ret = new Collider2D[colliders_set.Count];
        foreach (Collider2D collider in colliders_set)
            ret[cnt++] = collider;

        return ret;
    }

    void vertical_collisions(ref Vector2 velocity)
    {
        float direction_y = Mathf.Sign(velocity.y);
        float ray_length = Mathf.Abs(velocity.y) + SKIN_WIDTH;

        bool is_one_way_hit = false;
        for (int i = 0; i < vertical_ray_count; i++)
        {
            Vector2 ray_origin = (direction_y == -1) ? raycast_origins.bottom_left : raycast_origins.top_left;
            ray_origin += Vector2.right * (vertical_ray_spacing * i + velocity.x);

            RaycastHit2D raycast_hit = Physics2D.Raycast(
                ray_origin, Vector2.up * direction_y, ray_length,
                collision_mask | (direction_y == -1 && !is_jumping_down ? one_way_collision_mask : (LayerMask)0)
            );

            Debug.DrawRay(ray_origin, direction_y * Vector2.down * ray_length, Color.red);
            if (raycast_hit.collider != null) {
                if (direction_y == -1.0f)
                    collision_info.below = true;

                velocity.y = direction_y * (raycast_hit.distance - SKIN_WIDTH);
                ray_length = raycast_hit.distance;
                is_jumping_down = false;

                collision_info.below = direction_y == -1;
                collision_info.above = direction_y == 1;
            }

            if (is_jumping_down) {
                RaycastHit2D one_way_hit = Physics2D.Raycast(ray_origin, Vector2.up * direction_y, ray_length, one_way_collision_mask);
                if (one_way_hit.collider != null)
                    is_one_way_hit = true;
            }
        }

        if (!is_one_way_hit)
            is_jumping_down = false;
    }

    void climb_slope(ref Vector2 velocity, float slope_angle)
    {
        float move_distance = Mathf.Abs(velocity.x);
        float climb_velocity_y = Mathf.Sin(Mathf.Deg2Rad * slope_angle) * move_distance;
        if (velocity.y <= climb_velocity_y) {
            velocity.y = climb_velocity_y;
            velocity.x = Mathf.Cos(Mathf.Deg2Rad * slope_angle) * move_distance * Mathf.Sign(velocity.x);
            collision_info.below = true;
            collision_info.is_climbing_sloop = true;
        }
    }

    Vector2 get_slope_velocity(Vector2 ray_origin, Vector2 ray_direction, float move_distance)
    {
        // TODO: fix error value
        float ray_distance = move_distance;
        float direction_x = Mathf.Sign(ray_direction.x);
        bool is_finished_climbing = false;
        Vector2 next_direction = Vector2.zero;
        Vector2 next_origin = Vector2.zero;

        for (int i = 0; i < horizontal_ray_count; i++) {
            Vector2 current_ray_origin = ray_origin + Vector2.up * horizontal_ray_spacing * i;
            RaycastHit2D raycast_hit = Physics2D.Raycast(current_ray_origin, ray_direction, ray_distance + SKIN_WIDTH, collision_mask);
            Debug.DrawRay(current_ray_origin, Vector2.Scale(ray_direction, new Vector2(ray_distance, ray_distance)), Color.yellow);

            if (i == 0) {
                if (raycast_hit.collider == null) {
                    is_finished_climbing = true;
                    continue;
                }

                float slope_angle = Vector2.Angle(raycast_hit.normal, Vector2.up);
                if (slope_angle > max_climb_angle) {
                    is_finished_climbing = true;
                }

                next_direction = new Vector2(Mathf.Cos(Mathf.Deg2Rad * slope_angle) * direction_x, Mathf.Sin(Mathf.Deg2Rad * slope_angle));
                next_origin = raycast_hit.point - new Vector2(SKIN_WIDTH * direction_x, 0);
                // next_origin = raycast_hit.point - Vector2.Scale(ray_direction, new Vector2(SKIN_WIDTH, SKIN_WIDTH));
                ray_distance = raycast_hit.distance;
            }
            else if (raycast_hit.collider != null) { 
                is_finished_climbing = true;
                ray_distance = raycast_hit.distance - SKIN_WIDTH;
            }
        }
        for (int i = 0; i < horizontal_ray_count; i++) {
            Vector2 current_ray_origin = raycast_origins.top_left + Vector2.right * vertical_ray_spacing * i;
            RaycastHit2D raycast_hit = Physics2D.Raycast(current_ray_origin, ray_direction, ray_distance + SKIN_WIDTH, collision_mask);
            Debug.DrawRay(current_ray_origin, Vector2.Scale(ray_direction, new Vector2(ray_distance, ray_distance)), Color.yellow);

            if (raycast_hit.collider != null) {
                is_finished_climbing = true;
                ray_distance = raycast_hit.distance - SKIN_WIDTH;
            }
        }

        if (is_finished_climbing) 
            return Vector2.Scale(ray_direction, new Vector2(ray_distance, ray_distance));

        // Vector2 climb_velocity = new Vector2(ray_direction.x * ray_distance - SKIN_WIDTH * direction_x, ray_direction.y * ray_distance);
        Vector2 climb_velocity = new Vector2(ray_direction.x * ray_distance - SKIN_WIDTH * direction_x, ray_direction.y * ray_distance);
        return climb_velocity + get_slope_velocity(next_origin, next_direction, move_distance - ray_distance);
    }

    void descend_slope(ref Vector2 velocity)
    {
        float direction_x = Mathf.Sign(velocity.x);
        Vector2 ray_origin = (direction_x == -1) ? raycast_origins.bottom_right : raycast_origins.bottom_left;
        RaycastHit2D raycast_hit = Physics2D.Raycast(ray_origin, Vector2.down, Mathf.Infinity, collision_mask);
        if(raycast_hit.collider != null) {
            float slope_angle = Vector2.Angle(raycast_hit.normal, Vector2.up);
            if(slope_angle != 0 && slope_angle <= max_descend_angle && Mathf.Sign(raycast_hit.normal.x) == direction_x
                && raycast_hit.distance - SKIN_WIDTH <= Mathf.Tan(slope_angle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x)) {
                float move_distance = Mathf.Abs(velocity.x);
                float descend_velocity_y = Mathf.Sin(slope_angle * Mathf.Deg2Rad) * move_distance;

                velocity.x = Mathf.Cos(slope_angle * Mathf.Deg2Rad) * move_distance * Mathf.Sign(velocity.x);
                velocity.y -= descend_velocity_y;

                // vertical_collisions.descending_slope = true;
                collision_info.below = true;
            }
        }
    }

    public bool detect_vertical_collision(float direction, float dist)
    {
        RaycastHit2D right_hit = Physics2D.Raycast(raycast_origins.top_right, Vector2.up, dist + SKIN_WIDTH, collision_mask);
        RaycastHit2D left_hit = Physics2D.Raycast(raycast_origins.top_left, Vector2.up, dist + SKIN_WIDTH, collision_mask);
        return right_hit.collider != null || left_hit.collider != null;
    }

    void update_raycast_origins()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(SKIN_WIDTH * -2);

        raycast_origins.bottom_left = new Vector2(bounds.min.x, bounds.min.y);
        raycast_origins.bottom_right = new Vector2(bounds.max.x, bounds.min.y);
        raycast_origins.top_left = new Vector2(bounds.min.x, bounds.max.y);
        raycast_origins.top_right = new Vector2(bounds.max.x, bounds.max.y);
    }

    public void calculate_ray_spacing()
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(SKIN_WIDTH * -2);

        horizontal_ray_count = Mathf.Clamp(horizontal_ray_count, 2, int.MaxValue);
        vertical_ray_count = Mathf.Clamp(vertical_ray_count, 2, int.MaxValue);

        horizontal_ray_spacing = bounds.size.y / (horizontal_ray_count - 1);
        vertical_ray_spacing = bounds.size.x / (vertical_ray_count - 1);
    }

    public Vector2 sub_climb_stair(Vector2 pos, Vector2 velocity)
    {
        RaycastHit2D raycast_hit = Physics2D.Raycast(
            pos, Vector2.right,
            velocity.x, collision_mask
        );
        
        if (raycast_hit.collider != null) {
            Vector2 point = raycast_hit.point + new Vector2(SKIN_WIDTH * Mathf.Sign(velocity.x), SKIN_WIDTH);

            RaycastHit2D point_hit = Physics2D.Raycast(
                point, Vector2.up, 0.5f, collision_mask
            );
            if (point_hit.collider != null) {
                Vector2 next_pos = new Vector2(raycast_hit.point.x -SKIN_WIDTH * Mathf.Sign(velocity.x), point_hit.point.y + SKIN_WIDTH);
                Vector2 next_velocity = velocity - next_pos + pos;
                next_velocity.y = Mathf.Max(0.0f, next_velocity.y);
                return sub_climb_stair(next_pos, next_velocity);
            }
            else
                return raycast_hit.point + new Vector2(-SKIN_WIDTH * Mathf.Sign(velocity.x), velocity.y);
        }   
        return pos + velocity;
    }

    public void climb_stair(ref Vector2 velocity)
    {
        float direction_x = Mathf.Sign(velocity.x);
        Vector2 ray_origin = (direction_x == -1) ? raycast_origins.bottom_left : raycast_origins.bottom_right;
        velocity = sub_climb_stair(ray_origin, velocity) - ray_origin;
    }

    struct RaycastOrigins
    {
        public Vector2 top_left, top_right;
        public Vector2 bottom_left, bottom_right;
    }

    public struct CollisionInfo
    {
        public bool above, below, left, right;
        public bool is_climbing_sloop;
        public float old_splope_angle;
        public void reset()
        {
            above = false;
            below = false;
            left = false;
            right = false;
            is_climbing_sloop = false;
        }
    }
}