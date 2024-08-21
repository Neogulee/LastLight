using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SmashMove : MonoBehaviour
{
    private Material material;
    private new SpriteRenderer renderer;
    private int dir = 1;
    private float duration = 0.5f;
    private float current_time = 0.0f;
    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        material = renderer.material;
    }

    void Start()
    {
        Invoke("Destroy", duration);
        dir = Locator.player.GetSpriteRenderer().flipX ? -1 : 1;
        renderer.flipX = Locator.player.GetSpriteRenderer().flipX;
    }

    public void FixedUpdate()
    {
        current_time += Time.deltaTime;
        transform.Translate(new Vector2(dir, 0) * Time.deltaTime * 30.0f);
        material.SetFloat("_CurrentTime", current_time / duration);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
