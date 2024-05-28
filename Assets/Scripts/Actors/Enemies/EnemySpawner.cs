using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Utils;


public class EnemySpawner : MonoBehaviour
{
    public StageSpawnInfo spawn_info = null;
    public PlatformDetector platform_detector = null;
    public Vector2 min_spawn_range = new Vector2(7.11f, 4.0f);
    public Vector2 max_spawn_range = new Vector2(21.33f, 12.0f);
    public float relocate_time = 10.0f;
    public LayerMask layer = 0;

    private const int RAY_NUMS = 3;
    private const float SKIN_WIDTH = 0.015f;
    private float current_time = 0.0f, update_time = 0.0f;
    private int periodic_idx = 0, burst_idx = 0;
    private LinkedList<(PeriodicSpawnInfo, float)> current_spawns = new();
    private LinkedList<(GameObject enemy, float time)> enemies = new();

    void Awake()
    {
        spawn_info.sort();
    }

    public Vector2 get_random_position(GameObject enemy)
    {
        BoxCollider2D collider = enemy.GetComponent<BoxCollider2D>();
        float x = Random.Range(min_spawn_range.x, max_spawn_range.x) * (Random.Range(0, 2) == 1 ? 1 : -1);
        float y = Random.Range(min_spawn_range.y, max_spawn_range.y) * (Random.Range(0, 2) == 1 ? 1 : -1);

        int platform = platform_detector.get_nearest_platform(Locator.player.transform.position + new Vector3(x, y));
        Vector2 pos = platform_detector.get_pos(platform) + Vector2.down * 0.01f;
        float left_x = pos.x + collider.offset.x - collider.size.x / 2.0f;

        bool is_hit = false;
        float y_pos = -Mathf.Infinity;
        for (int i = 0; i < RAY_NUMS; i++)
        {
            var hit = Physics2D.Raycast(
                origin: new Vector2(left_x + i / (RAY_NUMS - 1) * collider.size.x, pos.y),
                direction: Vector2.up,
                distance: 1.0f,
                layerMask: layer
            );
            is_hit |= hit;
            y_pos = Mathf.Max(y_pos, hit.point.y + SKIN_WIDTH);
        }
        if (!is_hit)
            return new Vector2(Mathf.Infinity, Mathf.Infinity);

        Vector2 spawn_pos = new Vector2(pos.x, y_pos + collider.size.y / 2.0f - collider.offset.y + 1.0f);
        return spawn_pos;
    }

    public bool spawn(GameObject enemy)
    {
        Vector2 spawn_pos = get_random_position(enemy);
        if (float.IsInfinity(spawn_pos.x))
            return false;
        GameObject new_enemy = Instantiate(enemy, spawn_pos, Quaternion.identity);
        enemies.AddLast((new_enemy, 0.0f));
        return true;
    }

    public bool relocate(GameObject enemy)
    {
        Vector2 spawn_pos = get_random_position(enemy);
        if (float.IsInfinity(spawn_pos.x))
            return false;
        enemy.transform.position = spawn_pos;
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
            time += Time.fixedDeltaTime;
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

    private void relocate_enemies()
    {
        var node = enemies.First;
        while (node != null)
        {
            (GameObject enemy, float time) = node.Value;
            var next = node.Next;
            if (enemy == null) {
                enemies.Remove(node);
                node = next;
                continue;
            }

            Vector2 delta_pos = enemy.transform.position - Locator.player.transform.position;
            time += 1.0f;
            if (Mathf.Abs(delta_pos.x) <= min_spawn_range.x && Mathf.Abs(delta_pos.y) <= min_spawn_range.y)
                time = 0.0f;
            if (time >= relocate_time) {
                relocate(enemy);
                time = 0.0f;
            }
            node.Value = (enemy, time);
            node = next;
        }
    }

    void FixedUpdate()
    {
        current_time += Time.deltaTime;
        update_time += Time.deltaTime;
        if (update_time > 1.0f) {
            update_time -= 1.0f;
            relocate_enemies();
        }
        spawn_periodic();
        spawn_burst();
    }
}