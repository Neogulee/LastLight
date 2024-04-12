using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float xspeed;
    private float yspeed;
    private void Start()
    {
        Locator.event_manager.register<OnHpChangeEvent>(OnChange);
    }
    private void OnChange(IEventParam event_param)
    {
        Debug.Log("dd");
        xspeed = Random.Range(-3f, 3f);
        yspeed = Random.Range(-3f, 3f);
    }

    private void Update()
    {

        xspeed -= transform.localPosition.x/10;
        yspeed -= transform.localPosition.y/10;
        
        xspeed /= 1.2f;
        yspeed /= 1.2f;

        transform.Translate(Vector3.right * xspeed * Time.deltaTime + Vector3.up * yspeed * Time.deltaTime);
    }
}
