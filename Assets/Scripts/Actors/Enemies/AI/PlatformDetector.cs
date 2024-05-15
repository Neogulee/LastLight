using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;


public interface IPlatformDetector
{
    public int get_platforms_count();
    public Vector2 get_pos(int platform);
    public int get_nearest_platform(Vector2 pos);
    public List<(int dest, float jump_speed, List<Vector2> path)> get_edges(int platform);
}

[Serializable]
public class SVector2
{
    [SerializeField]
    public float x, y;
    public SVector2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }
}

public class PlatformDetector : MonoBehaviour, IPlatformDetector
{
    const float EPSILON = 1e-4f;
    public Grid grid;
    [SerializeField]
    private List<Vector2> platforms = new();
    [SerializeField]
    private List<List<(int dest, float jump_speed, List<Vector2> path)>> edges = new();
    private UnionFind platform_groups = new(0);
    private class Vector2Comparer : IEqualityComparer<Vector2>
    {
        public bool Equals(Vector2 a, Vector2 b) 
        {
            return a.x == b.x;
        }

        public int GetHashCode(Vector2 obj)
        {
            return obj.GetHashCode();
        }
    }

    protected virtual void Awake()
    {
        if (load_graph())
            return;
        build();
        save_graph();
    }

    private (Stream nodes_stream, Stream edges_stream) get_streams(bool is_write)
    {
        string path = "Assets/Resources/Datas/" + SceneManager.GetActiveScene().name;
        string nodes_name = path + "_nodes.bin";
        string edges_name = path + "_edges.bin";

        FileMode file_mode = FileMode.Create;
        FileAccess file_access = FileAccess.Write;
        if (!is_write) {
            if (!new FileInfo(nodes_name).Exists || !new FileInfo(edges_name).Exists)
                return (null, null);
            file_mode = FileMode.Open;
            file_access = FileAccess.Read;
        }

        Stream nodes_stream = new FileStream(nodes_name, file_mode, file_access);
        Stream edges_stream = new FileStream(edges_name, file_mode, file_access);
        nodes_stream.Position = 0;
        edges_stream.Position = 0;
        return (nodes_stream, edges_stream);
    }

    private List<SVector2> convert_ser(List<Vector2> list)
    {
        List<SVector2> ret = new();
        foreach (var vec in list)
            ret.Add(new SVector2(vec.x, vec.y));
        return ret;
    }

    private List<Vector2> deconvert_ser(List<SVector2> list)
    {
        List<Vector2> ret = new();
        foreach (var vec in list)
            ret.Add(new Vector2(vec.x, vec.y));
        return ret;
    }

    private void save_graph()
    {
        IFormatter formatter = new BinaryFormatter();
        (var nodes_stream, var edges_stream) = get_streams(true);
        List<SVector2> serialize_platforms = convert_ser(platforms);
        List<List<(int dest, float jump_speed, List<SVector2> path)>> serialize_edges = new();
        foreach (var row in edges)
        {
            List<(int dest, float jump_speed, List<SVector2> path)> temp = new();
            foreach (var edge in row)
                temp.Add((edge.dest, edge.jump_speed, convert_ser(edge.path)));
            serialize_edges.Add(temp);
        }

        formatter.Serialize(nodes_stream, serialize_platforms);
        formatter.Serialize(edges_stream, serialize_edges);

        nodes_stream.Close();
        edges_stream.Close();
    }
    
    private bool load_graph()
    {
        IFormatter formatter = new BinaryFormatter();
        (var nodes_stream, var edges_stream) = get_streams(false);
        if (nodes_stream is null)
            return false;

        platforms = deconvert_ser((List<SVector2>)formatter.Deserialize(nodes_stream));
        var ser_edges = (List<List<(int dest, float jump_speed, List<SVector2> path)>>)formatter.Deserialize(edges_stream);
        edges = new();
        foreach (var row in ser_edges)
        {
            List<(int dest, float jump_speed, List<Vector2> path)> temp = new();
            foreach (var edge in row)
                temp.Add((edge.dest, edge.jump_speed, deconvert_ser(edge.path)));
            edges.Add(temp);
        }

        nodes_stream.Close();
        edges_stream.Close();
        return true;
    }
    
    public int get_platforms_count()
    {
        return platforms.Count;
    }

    public Vector2 get_pos(int platform)
    {
        return platforms[platform];
    }

    public int get_nearest_platform(Vector2 pos)
    {
        float min_dist = 1e8f;
        int min_idx = -1;
        for (int i = 0; i < platforms.Count; i++)
        {
            Vector2 platform = platforms[i];
            float dist = (platform - pos).magnitude;
            if (dist < min_dist) {
                min_dist = dist;
                min_idx = i;
            }
        }
        return min_idx;
    }

    public List<(int dest, float jump_speed, List<Vector2> path)> get_edges(int platform)
    {
        return edges[platform];
    }

    public void build()
    {
        foreach (var tilemap_collider in grid.GetComponentsInChildren<TilemapCollider2D>())
        {
            CompositeCollider2D collider = tilemap_collider.composite;
            for (int idx = 0; idx < collider.pathCount; idx++)
            {
                List<Vector2> vertices = new(collider.GetPathPointCount(idx));
                tilemap_collider.composite.GetPath(idx, vertices);
                build_platforms(vertices);
            }
        }
        build_edges();
    }

    public bool check_equal(float a, float b)
    {
        return a - EPSILON < b && b < a + EPSILON;
    }
    
    public void build_platforms(List<Vector2> vertices)
    {
        Vector2 min_pos = vertices[0], max_pos = vertices[0];
        int min_idx = -1, max_idx = -1;
        for (int i = 0; i < vertices.Count; i++)
        {
            Vector2 current = vertices[i];
            if ((check_equal(current.x, min_pos.x) && current.y > min_pos.y)
                    || current.x < min_pos.x) {
                min_pos = current;
                min_idx = i;
            }
            if ((check_equal(current.x, max_pos.x) && current.y > max_pos.y)
                    || current.x > max_pos.x) {
                max_pos = current;
                max_idx = i;
            }
        }
        Debug.Assert(min_idx != -1 && max_idx != -1);

        int idx = min_idx;
        while (idx != max_idx)
        {
            int next_idx = (vertices.Count + idx - 1) % vertices.Count;
            Vector2 current = vertices[idx];
            Vector2 next = vertices[next_idx];
            if (check_equal(current.x, next.x)) {
                idx = next_idx;
                continue;
            }

            int last_idx = -1; // TEST
            for (float x = Mathf.Round(current.x - EPSILON); x < next.x - 0.5f + EPSILON; x += 1.0f)
            {
                platforms.Add(new Vector2(x + 0.5f, current.y));
                platform_groups.increase();
                edges.Add(new List<(int dest, float jump_speed, List<Vector2> path)>());

                // TEST
                if (last_idx != -1) {
                    int current_idx = platforms.Count - 1;
                    edges[last_idx].Add((
                        current_idx, 0.0f,
                        new List<Vector2> { platforms[last_idx], platforms[current_idx] }
                    ));
                    edges[current_idx].Add((
                        last_idx, 0.0f,
                        new List<Vector2> { platforms[current_idx], platforms[last_idx] }
                    ));
                    platform_groups.merge(last_idx, current_idx);
                }
                last_idx = platforms.Count - 1;
            }
            
            idx = next_idx;
        }
    }

    public void build_edges()
    {
        for (int i = 0; i < platforms.Count; i++)
        {
            for (int j = 0; j < platforms.Count; j++)
            {
                if (i == j)
                    continue;
                
                if (platform_groups.find(i) == platform_groups.find(j))
                    continue;
                
                float dy = platforms[j].y - platforms[i].y;
                if (Mathf.Abs(platforms[i].x - platforms[j].x) <= 1.0f + 2 * EPSILON
                        && -5.0f - EPSILON <= dy && dy <= 0.5f + EPSILON) {
                    List<Vector2> walking_path = new() {
                        platforms[i],
                        platforms[j]
                    };
                    edges[i].Add((j, 0.0f, walking_path));
                    continue;
                }

                // TODO: Raycast
                (float jump_speed, List<Vector2> path) = calc_jump_speed(platforms[i], platforms[j], 5.0f);
                if (jump_speed < 0.0f || jump_speed > 15.0f)
                    continue;

                edges[i].Add((j, jump_speed, path));

                // if (jump_speed <= Mathf.Sqrt(10.0f))
                //     jump_speed = 0.0f;
            }
        }
    }

    (float jump_speed, List<Vector2> path) calc_jump_speed(Vector2 start, Vector2 dest, float move_speed)
    {
        Vector2 target = dest - start;
        float X = Mathf.Abs(target.x / move_speed);
        float Y = target.y;

        float jump_speed = Y / X + 5.0f * X;
        float time = X;

        List<Vector2> path = new();
        for (int i = 0; i <= 10; i++)
        {
            float t = Mathf.Clamp(time / 10.0f * i, 0.01f, time - 0.01f);
            float x = t * move_speed;
            float y = -5.0f * t * t + jump_speed * t;
            if (target.x < 0.0f)
                x *= -1.0f;
            path.Add(start + new Vector2(x, y));
        }
        for (int i = 0; i < path.Count - 1; i++)
        {
            Vector2 dir = path[i + 1] - path[i];
            RaycastHit2D hit = Physics2D.Raycast(path[i], dir, dir.magnitude);
            if (hit)
                return (-1.0f, null);
        }
        return (jump_speed, path);
    }

    void OnDrawGizmos()
    {
        foreach (var pos in platforms)
            Gizmos.DrawSphere(pos, 0.1f);
        
        for (int start = 0; start < edges.Count; start++)
        {
            foreach ((int dest, float jump_speed, List<Vector2> path) in edges[start])
            {
                if (start % 13 == 0) {
                    Gizmos.color = Color.green;

                    for (int i = 0; i < path.Count - 1; i++)
                        Gizmos.DrawLine(path[i], path[i + 1]);
                    // Gizmos.DrawLine(platforms[start], platforms[dest]);
                    // Gizmos.DrawSphere(platforms[start], 0.1f);
                }
            }
        }
    }
}