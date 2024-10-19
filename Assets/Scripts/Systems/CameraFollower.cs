using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public float velocity = 0.25f;
    void FixedUpdate()
    {
        Vector2 new_pos = Vector2.Lerp(transform.position, Locator.player.transform.position, velocity);
        transform.position = new Vector3(new_pos.x, new_pos.y, transform.position.z);
    }
}
