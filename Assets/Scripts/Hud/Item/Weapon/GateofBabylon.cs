using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateofBabylon : Weapon
{
    void Update()
    {

        timer += Time.deltaTime;
        if (timer > shootThreshold && Input.GetKeyDown(KeyCode.Z))
        {
            timer = 0;
            Fire();
        }
    }

    public override void IncreaseLevel()
    {
        shootThreshold *= 0.8f;
        damage *= 1.2f;

    }
    public void Fire()
    {
        Player player = Locator.player;
        Transform bullet = TempGameManager.instance.poolManager.Get(id).transform;

        bullet.position = transform.position;
        // Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, Vector2.left + Vector2.up);
        // bullet.rotation = targetRotation;
        bullet.GetComponent<GateofBabylonObject>().Init(damage, Vector2.left, size,id);
        bullet.GetComponent<Damager>().damage = (int)damage;

        Transform bullet1 = TempGameManager.instance.poolManager.Get(id).transform;

        bullet1.position = transform.position;
        //Quaternion targetRotation1 = Quaternion.FromToRotation(Vector3.up, Vector2.right + Vector2.up);
        // bullet1.rotation = targetRotation1;
        bullet1.GetComponent<GateofBabylonObject>().Init(damage, Vector2.right , size,id);
        bullet1.GetComponent<Damager>().damage = (int)damage;
    }
}
