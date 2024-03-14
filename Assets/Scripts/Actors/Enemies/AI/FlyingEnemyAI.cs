using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Utils;


public class FlyingEnemyAI : EnemyAI
{
    public GridDetector grid_detector = null;

    protected override void Awake()
    {
        base.Awake();
        grid_detector = FindObjectOfType<GridDetector>();
    }

    public void find_path()
    {
        Vector2Int player_pos = grid_detector.get_cell(Locator.player.transform.position);
        Vector2Int current_pos = grid_detector.get_cell(transform.position);

        PriorityQueue<(Vector2Int last, Vector2Int current, float cost), float> pq = new();
        pq.Enqueue((current_pos, current_pos, 0.0f), 0);

        // while (pq.Count > 0)
        // {
        //     (int last, float jump_speed, int platform, float cost) = pq.Dequeue();
        //     if (cost > 100.0f)
        //         break;
                
        //     if (cost >= min_cost[platform])
        //         continue;

        //     min_cost[platform] = cost;
        //     last_platform[platform] = (last, jump_speed);
        //     if (platform == player_platform)
        //         return backtrack_path_finding(last_platform, platform);

        //     var edges = platform_detector.get_edges(platform);
        //     Vector2 pos = platform_detector.get_pos(platform);
        //     foreach (var edge in edges)
        //     {
        //         if (edge.jump_speed * 15.0f / actor_controller.move_velocity > actor_controller.jump_velocity)
        //             continue;

        //         Vector2 dest_pos = platform_detector.get_pos(edge.dest);
        //         float next_cost = cost + Mathf.Abs(dest_pos.x - pos.x);
        //         float h_cost = (player_pos - dest_pos).magnitude;
        //         float jump_cost = edge.jump_speed > 0.0f ? (1.0f + edge.jump_speed / 15.0f) : 0.0f;
        //         pq.Enqueue((
        //             platform, edge.jump_speed * 15.0f / actor_controller.move_velocity, edge.dest, next_cost),
        //             next_cost + h_cost + jump_cost
        // }
        //         );
        //     }
        // return null;
    }

    void FixedUpdate()
    {
    
    }
}