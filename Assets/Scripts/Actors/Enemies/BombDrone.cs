using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BombDrone: MonoBehaviour
{
    [SerializeField]
    private Material blink_material = null;
    private Material original_material = null;
    private new SpriteRenderer renderer = null;
    private BombAttacker attacker = null;
    private float current_time = 0.0f;
    private bool is_attacking = false;
    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        attacker = GetComponent<BombAttacker>();
        original_material = renderer.material;
    }

    void FixedUpdate()
    {
        if (!is_attacking)
            return;

        current_time += Time.deltaTime;
        if ((int)Mathf.Pow(current_time / attacker.attack_delay * 3.0f, 3) % 2 == 0)
            renderer.material = blink_material;
        else
            renderer.material = original_material;
    }

    public void on_prepare_attack_range(int idx)
    {
        is_attacking = true;
    }

    public void on_attack_range()
    {
        Destroy(gameObject);
    }
}