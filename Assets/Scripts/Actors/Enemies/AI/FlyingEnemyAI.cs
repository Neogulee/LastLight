using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Utils;


public class FlyingEnemyAI : EnemyAI
{
    public GridDetector grid_detector = null;
    new private BoxCollider2D collider;
    private Vector2Int current_move = Vector2Int.zero;
    private float update_time = 0.02f;
    private float current_time = 0.0f;
    private bool is_attacking = false;


    private readonly Vector2Int[] CARDINAL_DIR = {
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0),
        new Vector2Int(0, 1),
        new Vector2Int(0, -1)
    };
    private readonly Vector2Int[] DIAGONAL_DIR = {
        new Vector2Int(1, 1),
        new Vector2Int(1, -1),
        new Vector2Int(-1, 1),
        new Vector2Int(-1, -1)
    };

    protected override void Awake()
    {
        base.Awake();
        grid_detector = FindObjectOfType<GridDetector>();
    }

    private List<Vector2Int> backtrack_path_finding(Dictionary<Vector2Int, Vector2Int> last_pos, Vector2Int dest)
    {
        List<Vector2Int> path = new();

        Vector2Int pos = dest;
        while (true)
        {
            Vector2Int last = last_pos[pos];
            if (last == pos)
                break;

            path.Add(pos - last);
            pos = last;
        }
        path.Reverse();

        return path;
    }

    public List<Vector2Int> find_path()
    {
        Vector2Int player_pos = grid_detector.get_cell(Locator.player.transform.position);
        Vector2Int current_pos = grid_detector.get_cell(transform.position);

        Dictionary<Vector2Int, Vector2Int> last_pos = new();
        PriorityQueue<(Vector2Int last, Vector2Int current, float cost), float> pq = new();
        pq.Enqueue((current_pos, current_pos, 0.0f), 0);

        while (pq.Count > 0)
        {
            (Vector2Int last, Vector2Int current, float cost) = pq.Dequeue();
            if (cost > 100.0f)
                break;
                
            if (last_pos.ContainsKey(current))
                continue;
            last_pos[current] = last;
            
            if (current == player_pos)
                return backtrack_path_finding(last_pos, current);

            void find_next(Vector2Int[] dirs, float edge_cost)
            {
                foreach (Vector2Int dir in dirs)
                {
                    Vector2Int next = current + dir;
                    if (grid_detector.is_tile(next))
                        continue;
                    
                    float next_cost = cost + edge_cost;
                    float h_cost = (next - player_pos).magnitude;
                    pq.Enqueue(
                        (current, next, next_cost),
                        next_cost + h_cost
                    );
                }
            }

            find_next(CARDINAL_DIR, 1.0f);
            find_next(DIAGONAL_DIR, Mathf.Sqrt(2.0f));
        }
        
        return null;
    }

    private void update_move()
    {
        var path = find_path();
        if (path is null || path.Count == 0)
            current_move = Vector2Int.zero;
        else
            current_move = path[0];
        actor_controller.move(current_move);
    }

    void FixedUpdate()
    {
        if (is_attacking)
            return;

        current_time += Time.deltaTime;
        if (current_time >= update_time) {
            current_time -= update_time;
            update_move();
        }

        if ((Locator.player.transform.position - transform.position).magnitude <= 1.5f) {
            // TODO: check finished
            // SendMessage("on_attack", SendMessageOptions.DontRequireReceiver);
            // is_attacking = true;
            actor_controller.stop();
        }
    }
}