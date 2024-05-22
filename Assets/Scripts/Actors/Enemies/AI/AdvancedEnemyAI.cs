using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

/// <summary>
/// Callbacks: on_start_attack()
/// </summary>
public class AdvancedEnemyAI : EnemyAI
{
    public PlatformDetector platform_detector = null;
    new private BoxCollider2D collider;
    private float update_time = 0.125f;
    private float current_time = 0.0f;
    private float idle_delay = 3.0f;
    private float idle_time = 0.5f;
    private float current_idle_time = 0.0f;
    private Moves current_move = Moves.IDLE;
    private Attacker attacker = null;
    private bool is_attacking = false;
    private object is_attacking_lock = new object();
    private enum Moves 
    {
        IDLE = 0,
        LEFT = 1,
        RIGHT = 2,
        JUMP = 4
    }
    protected override void Awake()
    {
        base.Awake();
        collider = GetComponent<BoxCollider2D>();
        attacker = GetComponent<Attacker>();
        platform_detector = FindObjectOfType<PlatformDetector>();
    }

    private List<(Moves, float)> backtrack_path_finding(List<(int, float)> last_platform, int dest)
    {
        List<(Moves, float)> path = new();

        int platform = dest;
        while (true)
        {
            (int last, float jump_speed) = last_platform[platform];
            if (last == platform)
                break;

            Moves move = Moves.RIGHT;
            if ((platform_detector.get_pos(platform) - platform_detector.get_pos(last)).x < 0.0f)
                move = Moves.LEFT;
            
            path.Add((move, jump_speed));
            platform = last;
        }
        path.Reverse();

        return path;
    }

    private List<(Moves, float)> find_path()
    {
        Vector2 player_pos = Locator.player.transform.position;
        int player_platform = platform_detector.get_nearest_platform(player_pos);
        int current_platform = platform_detector.get_nearest_platform(transform.position);

        List<(int, float)> last_platform = new(new (int, float)[platform_detector.get_platforms_count()]);
        List<float> min_cost = Enumerable.Repeat(1e8f, platform_detector.get_platforms_count()).ToList();
        PriorityQueue<(int last, float jump_speed, int platform, float cost), float> pq = new();
        pq.Enqueue((current_platform, 0.0f, current_platform, 0.0f), 0);

        while (pq.Count > 0)
        {
            (int last, float jump_speed, int platform, float cost) = pq.Dequeue();
            if (cost > 200.0f || pq.Count > 200.0f)
                break;
                
            if (cost >= min_cost[platform])
                continue;

            min_cost[platform] = cost;
            last_platform[platform] = (last, jump_speed);
            if (platform == player_platform)
                return backtrack_path_finding(last_platform, platform);

            var edges = platform_detector.get_edges(platform);
            Vector2 pos = platform_detector.get_pos(platform);
            foreach (var edge in edges)
            {
                if (edge.jump_speed * 16.0f / actor_controller.move_velocity > actor_controller.jump_velocity)
                    continue;

                Vector2 dest_pos = platform_detector.get_pos(edge.dest);
                float next_cost = cost + Mathf.Abs(dest_pos.x - pos.x);
                float h_cost = (player_pos - dest_pos).magnitude;
                float jump_cost = edge.jump_speed > 0.0f ? (1.0f + edge.jump_speed / 16.0f) : 0.0f;
                pq.Enqueue((
                    platform, edge.jump_speed * 16.0f / actor_controller.move_velocity, edge.dest, next_cost),
                    next_cost + h_cost + jump_cost
                );
            }
        }
        return null;
    }

    private void update_move()
    {
        if (!actor_controller.check_on_platform())
            return;

        current_idle_time += update_time;
        if (current_idle_time >= idle_delay) {
            current_move = Moves.IDLE;
            if (current_idle_time >= idle_delay + idle_time)
                current_idle_time = 0.0f;
            return;
        }

        var path = find_path();
        if (path is null || path.Count == 0)
            current_move = Moves.IDLE;
        else {
            // Debug.Log(String.Join(", ", path));
            (Moves move, float jump_speed) = path[0];
            current_move = move;
            if (jump_speed > 0.0f)
                actor_controller.jump(jump_speed);
        }
    }

    public void on_finish_attack()
    {
        lock (is_attacking_lock)
            is_attacking = false;
    }

    public void on_ground()
    {
        current_move = Moves.IDLE;
    }

    void FixedUpdate()
    {
        lock (is_attacking_lock)
            if (is_attacking)
                return;

        current_time += Time.deltaTime;
        if (current_time >= update_time) {
            current_time -= update_time;
            update_move();
        }

        if (attacker.check()) {
            // TODO: check finished
            SendMessage("on_start_attack", SendMessageOptions.DontRequireReceiver);
            lock (is_attacking_lock)
                is_attacking = true;
            actor_controller.stop();
        }
        else if (current_move == Moves.LEFT)
            actor_controller.move_left();
        else if (current_move == Moves.RIGHT)
            actor_controller.move_right();
        else
            actor_controller.stop();
    }
}