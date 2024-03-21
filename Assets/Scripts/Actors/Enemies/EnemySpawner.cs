using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Utils;


public class EnemySpawner : MonoBehaviour
{
    public StageSpawnInfo spawn_info = null;
    public PlatformDetector platform_detector = null;
    public Vector2 min_spawn_range = new Vector2(10.0f, 10.0f);
    public Vector2 max_spawn_range = new Vector2(30.0f, 30.0f);

    private float current_time = 0.0f;
    private LinkedList<(PeriodicSpawnInfo, float)> current_spawns = new();
    private int periodic_idx = 0, burst_idx = 0;
    void Awake()
    {
        
    }
    
    public bool spawn(GameObject enemy)
    {
        float x = Random.Range(min_spawn_range.x, max_spawn_range.x) * (Random.Range(0, 2) == 1 ? 1 : -1);
        float y = Random.Range(min_spawn_range.y, max_spawn_range.y) * (Random.Range(0, 2) == 1 ? 1 : -1);
        int platform = platform_detector.get_nearest_platform(Locator.player.transform.position + new Vector3(x, y));
        Vector2 pos = platform_detector.get_pos(platform) + Vector2.down * 0.01f;

        var hit_left = Physics2D.Raycast(pos + 0.25f * Vector2.left, Vector2.up, 1.0f, LayerMask.GetMask("Ground"));
        var hit_right = Physics2D.Raycast(pos + 0.25f * Vector2.right, Vector2.up, 1.0f, LayerMask.GetMask("Ground"));
        if (!hit_left && !hit_right)
            return false;

        float y_pos = Mathf.Max(hit_left.point.y, hit_right.point.y) + 0.015f;
        BoxCollider2D collider = enemy.GetComponent<BoxCollider2D>();
        Vector2 spawn_pos = new Vector2(pos.x, y_pos + collider.size.y / 2.0f - collider.offset.y);
        Instantiate(enemy, spawn_pos, Quaternion.identity);
        return true;
    }

    private void spawn_periodic()
    {
        while (periodic_idx < spawn_info.periodic_spawns.Count)
        {
            PeriodicSpawnInfo info = spawn_info.periodic_spawns[periodic_idx];
            if (current_time < info.start_time)
                break;

            current_spawns.AddLast((info, current_time - info.start_time));
            periodic_idx++;
        }
            
        var node = current_spawns.First;
        while (node != null)
        {
            (PeriodicSpawnInfo current, float time) = node.Value;
            time += Time.deltaTime;
            float spawn_time = 1.0f / current.spawn_rate;
            if (time >= spawn_time) {
                time -= spawn_time;
                spawn(current.enemy);
            }
            
            node.Value = (current, time);
            var next = node.Next;
            if (current_time >= current.end_time)
                current_spawns.Remove(node);
            node = next;
        }
    }

    private void spawn_burst()
    {
        while (burst_idx < spawn_info.burst_spawns.Count)
        {
            BurstSpawnInfo info = spawn_info.burst_spawns[burst_idx];
            if (current_time < info.start_time)
                break;
            
            for (int i = 0; i < info.amount; i++)
                spawn(info.enemy);
            burst_idx++;
        }
    }

    void FixedUpdate()
    {
        current_time += Time.deltaTime;
        spawn_periodic();
        spawn_burst();
    }
}