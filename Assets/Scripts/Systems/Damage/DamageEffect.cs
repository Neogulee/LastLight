using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageEffect : MonoBehaviour
{
    private TMP_Text text;

    private float xspeed;
    private float yspeed;

    private float speed = 6;
    [SerializeField]
    private float friction;
    public void Awake()
    {
        text = GetComponent<TMP_Text>();
    }
    public void ShowDamage(Transform t, int damage, Team team)
    {
        Invoke("Dead", 1);
        transform.position = t.position;
        text.text = damage.ToString();
        if(team == Team.Enemy)
        text.color = new Color(1, 0.5971026f, 0.1568627f, 1);
        else
        text.color = new Color(0.1568627f, 0.5971026f, 1, 1);
        xspeed = Random.Range(-0.3f, 0.3f);
        yspeed = Random.Range(0.7f, 1f);
        SpeedSet();
    }
    public void ShowCritDamage(Transform t, int damage, Team team)
    {
        Invoke("Dead", 1);
        transform.position = t.position;
        text.text =damage.ToString();
        if(team == Team.Enemy)
        text.color = new Color(1, 0, 0, 1);
        else
        text.color = new Color(0, 0, 1, 1);
        xspeed = Random.Range(-0.3f, 0.3f);
        yspeed = Random.Range(0.7f, 1f);
        SpeedSet();
    }
    public void ShowHeal(Transform t, int heal, Team team)
    {
        Invoke("Dead",1);
        transform.position = t.position;
        text.text = heal.ToString();
        if(team == Team.Enemy)
        text.color = new Color(0.3002342f, 1, 0.2688679f, 1);
        else
        text.color = new Color(0.2688679f, 1, 0.3002342f, 1);
        xspeed = Random.Range(-0.3f, 0.3f);
        yspeed = Random.Range(0.7f, 1f);
        SpeedSet();
    }
    void Update()
    {
        UpdateSpeed();
        transform.Translate(Vector3.right * xspeed * Time.deltaTime + Vector3.up * yspeed * Time.deltaTime); 
    }
    void UpdateSpeed()
    {
        xspeed = xspeed * Mathf.Pow(friction, Time.deltaTime);
        yspeed = yspeed * Mathf.Pow(friction, Time.deltaTime);

        transform.position += new Vector3(xspeed, yspeed) * Time.deltaTime;

    }
    void Dead()
    {
        gameObject.SetActive(false);
    }
    void SpeedSet()
    {
        xspeed *= speed;
        yspeed *= speed;
    }
}
