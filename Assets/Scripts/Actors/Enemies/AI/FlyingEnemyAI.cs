using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Utils;


[RequireComponent(typeof(RangeAttacker))]
public class FlyingEnemyAI : EnemyAI
{
    public LayerMask layer = 0;
    public float before_attack_delay = 0.5f, after_attack_delay = 0.5f;
    private GridDetector grid_detector = null;
    private Vector2Int current_move = Vector2Int.zero;
    private float update_time = 0.02f;
    private float current_time = 0.0f;
    private float last_moved_time = 0.0f;
    private readonly object stop_lock = new object();
    private bool is_stopped = false;
    private RangeAttacker range_attacker = null;

    private readonly Vector2Int[] CARDINAL_DIR = {
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0),
        new Vector2Int(0, 1),
        new Vector2Int(0, -1)
    };

    protected override void Awake()
    {
        base.Awake();
        grid_detector = FindObjectOfType<GridDetector>();
        range_attacker = GetComponent<RangeAttacker>();
        last_moved_time = Time.time;
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
        if (grid_detector.is_tile(player_pos))
            return null;

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

            for (int i = 0; i < CARDINAL_DIR.Length; i++)
            {
                Vector2Int dir = CARDINAL_DIR[i];
                Vector2Int next_dir = CARDINAL_DIR[(i + 1) % CARDINAL_DIR.Length];

                bool enqueue(Vector2Int pos, float weight)
                {
                    if (grid_detector.is_tile(pos))
                        return false;

                    float next_cost = cost + weight;
                    float h_cost = (pos - player_pos).magnitude;
                    pq.Enqueue(
                        (current, pos, next_cost),
                        next_cost + h_cost
                    );
                    return true;
                }

                if (!enqueue(current + dir, 1.0f))
                    continue;

                if (!grid_detector.is_tile(current + next_dir))
                    enqueue(current + dir + next_dir, Mathf.Sqrt(2.0f));
            }
        }
        
        return null;
    }

    private Vector2 find_blank_dir()
    {
        Vector2Int current = grid_detector.get_cell(transform.position);

        foreach (var dir in CARDINAL_DIR.Concat(new Vector2Int[]{ Vector2Int.zero }))
        {
            Vector2Int pos = current + dir;
            if (!grid_detector.is_tile(pos))
                return pos - (Vector2)transform.position;
        }
        return current;
    }

    private void move_dir(Vector2 dir)
    {
        actor_controller.move(dir);
        last_moved_time = Time.time;
    }

    private void update_move()
    {
        if (Time.time - last_moved_time > 1.0f) {
            move_dir(find_blank_dir());
            return;
        }

        Vector3 position = transform.position;
        float dx = Mathf.Abs(Mathf.Round(position.x) - position.x);
        float dy = Mathf.Abs(Mathf.Round(position.y) - position.y);
        if (dx <= 0.35f || dy <= 0.35f)
            return;

        var path = find_path();
        if (path is null || path.Count == 0)
            move_dir(Vector2.zero);
        else
            move_dir(path[0]);
    }

    private bool check_attack()
    {
        Vector3 player_pos = Locator.player.transform.position;
        if ((player_pos - transform.position).magnitude > 8.0f)
            return false;
        
        Vector3 delta = player_pos - transform.position;
        var hit = Physics2D.Raycast(transform.position, delta, delta.magnitude, layer);
        return !hit;
    }

    private async void attack()
    {
        lock (stop_lock)
        {
            if (is_stopped)
                return;
            is_stopped = true;
        }
        
        await range_attacker.attack();

        last_moved_time = Time.time;
        lock (stop_lock)
            is_stopped = false;
    }
    
    void FixedUpdate()
    {
        lock (stop_lock)
            if (is_stopped)
                return;

        current_time += Time.deltaTime;
        if (current_time < update_time)
            return;
        current_time -= update_time;
        update_move();
        
        if (check_attack()) {
            actor_controller.move(Vector2.zero);
            attack();
        }
    }
}