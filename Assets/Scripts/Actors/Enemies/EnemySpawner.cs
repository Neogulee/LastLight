using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public EnemySpawnInfo spawn_info = null;
    public float spawn_frequency = 2.0f;
    public PlatformDetector platform_detector = null;
    public Vector2 min_spawn_range = new Vector2(10.0f, 10.0f);
    public Vector2 max_spawn_range = new Vector2(30.0f, 30.0f);

    private float current_time = 0.0f;

    void FixedUpdate()
    {
        current_time += Time.deltaTime;
        if (current_time < spawn_frequency)
            return;

        current_time -= spawn_frequency;
        GameObject enemy = spawn_info.sample();

        float x = Random.Range(min_spawn_range.x, max_spawn_range.x) * (Random.Range(0, 2) == 1 ? 1 : -1);
        float y = Random.Range(min_spawn_range.y, max_spawn_range.y) * (Random.Range(0, 2) == 1 ? 1 : -1);
        int platform = platform_detector.get_nearest_platform(Locator.player.transform.position + new Vector3(x, y));
        Vector2 pos = platform_detector.get_pos(platform) + Vector2.down * 0.01f;

        var hit_left = Physics2D.Raycast(pos + 0.25f * Vector2.left, Vector2.up, 1.0f, LayerMask.GetMask("Ground"));
        var hit_right = Physics2D.Raycast(pos + 0.25f * Vector2.right, Vector2.up, 1.0f, LayerMask.GetMask("Ground"));
        if (!hit_left && !hit_right)
            return;

        float y_pos = Mathf.Max(hit_left.point.y, hit_right.point.y);

        BoxCollider2D collider = enemy.GetComponent<BoxCollider2D>();
        Vector2 spawn_pos = new Vector2(pos.x, y_pos + collider.size.y / 2.0f - collider.offset.y);
        Instantiate(enemy, spawn_pos, Quaternion.identity);
    }
}