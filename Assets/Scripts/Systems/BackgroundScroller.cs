using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float speed = 0.5f;

    private new Camera camera;
    private SpriteRenderer sprite_renderer;
    void Start()
    {
        camera = Camera.main;
        sprite_renderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        Vector3 size = sprite_renderer.size;
        // size.Scale(transform.localScale);
        Vector3 camera_position = camera.transform.position;

        float weighted_x = camera_position.x * speed;
        float x_position = weighted_x + (int)((camera_position.x - weighted_x + Mathf.Sign(weighted_x) * size.x / 6) / (size.x / 3)) * (size.x / 3);
        transform.position = new Vector3(x_position, camera_position.y, transform.position.z);
    }
}
