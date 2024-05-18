
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashMove : MonoBehaviour {

    int dir = 1;
    public void Start() {
        Invoke("Dead", 2.0f);
        if(Locator.player.GetSpriteRenderer().flipX)
            dir = -1;
        else
            dir = 1;
        transform.localScale = new Vector3(dir,1,1);
    }
    public void Update() {
        transform.Translate(new Vector2(dir,0) * Time.deltaTime * 20);

        
    }
    private void Dead()
    {
        Destroy(gameObject);
    }
}
