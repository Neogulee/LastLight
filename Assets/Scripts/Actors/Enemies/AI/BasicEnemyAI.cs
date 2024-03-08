using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Utils;


public class BasicEnemyAI : EnemyAI
{
    public Grid grid;
    public int jump_height = 3;
    public int horizontal_dist_per_jump = 2;

    new private BoxCollider2D collider;
    private float update_time = 0.1f;
    private float current_time = 0.0f;
    private Moves current_move = Moves.IDLE;
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
    }

    private int get_l1norm(Vector2Int vec)
    {
        return Mathf.Abs(vec.x) + Mathf.Abs(vec.y);
    }

    private List<int> find_path()
    {
        Player player = Locator.player;
        Vector2Int player_pos = (Vector2Int)grid.WorldToCell(player.transform.position);
        Vector2Int current_pos = (Vector2Int)grid.WorldToCell(transform.position);

        Dictionary<Vector2Int, int> min_dist = new();
        PriorityQueue<(Vector2Int pos, int remain_jump, int jump_dist, int cost, List<int> moves), int> pq = new();
        pq.Enqueue((current_pos, actor_controller.max_jump_cnt - actor_controller.current_jump_cnt, 0, 0, new List<int>()), 0);


        List<int> result = null;
        while (pq.Count > 0)
        {
            (Vector2Int pos, int remain_jump, int jump_dist, int cost, List<int> moves) = pq.Dequeue();
            if (pos == player_pos) {
                result = moves;
                break;
            }
            if (cost > 100)
                break;
            if (min_dist.ContainsKey(pos) && min_dist[pos] < cost)
                continue;

            Action<int> enqueue_next = (int move) => {                
                int new_cost = cost + 1;
                int new_remain_jump = remain_jump;
                int new_jump_dist = jump_dist;
                List<int> new_moves = moves.ToList();
                new_moves.Add(0);
                if ((move & (int)Moves.JUMP) != 0 && remain_jump > 0) {
                    new_cost += 2;
                    new_jump_dist = jump_height;
                    new_remain_jump--;
                    new_moves[^1] |= (int)Moves.JUMP;
                }
                
                Vector2Int new_pos = pos;
                if ((move & (int)Moves.LEFT) != 0)
                    new_pos.x -= 1;
                if ((move & (int)Moves.RIGHT) != 0)
                    new_pos.x += 1;
                if (new_jump_dist > 0) {
                    new_pos.y += 1;
                    new_jump_dist--;
                }
                
                if (min_dist.ContainsKey(new_pos) && min_dist[new_pos] <= new_cost)
                    return;

                Vector3 pos_u = grid.CellToWorld((Vector3Int)pos) + grid.cellSize / 2.0f;
                Vector3 pos_v = grid.CellToWorld((Vector3Int)new_pos) + grid.cellSize / 2.0f;
                Vector3 ray_vec = pos_v - pos_u;

                int dist = get_l1norm(player_pos - new_pos);
                RaycastHit2D hit = Physics2D.Raycast(
                    pos_u,
                    ray_vec,
                    ray_vec.magnitude,
                    LayerMask.GetMask("Ground") | LayerMask.GetMask("Platform")
                );

                if (!hit) {
                    new_moves[^1] |= move & ~(int)Moves.JUMP;
                    pq.Enqueue((new_pos, new_remain_jump, new_jump_dist, new_cost, new_moves), new_cost + dist);
                    min_dist[new_pos] = new_cost;
                }
            };

            enqueue_next((int)Moves.LEFT);
            enqueue_next((int)Moves.RIGHT);
            if (actor_controller.check_on_platform()) {
                enqueue_next((int)Moves.LEFT | (int)Moves.JUMP);
                enqueue_next((int)Moves.RIGHT | (int)Moves.JUMP);
            }
        }
        return result;
    }

    void FixedUpdate()
    {
        current_time += Time.deltaTime;
        if (current_time >= update_time) {
            current_time -= update_time;
            List<int> path = find_path();
            if (path is not null) {
                if ((path[^1] & (int)Moves.JUMP) != 0)
                    actor_controller.jump();
                if ((path[^1] & (int)Moves.LEFT) != 0)
                    current_move = Moves.LEFT;
                if ((path[^1] & (int)Moves.RIGHT) != 0)
                    current_move = Moves.RIGHT;
            }
        }

        if (current_move == Moves.LEFT)
            actor_controller.move_left();
        if (current_move == Moves.RIGHT)
            actor_controller.move_right();
    }
}