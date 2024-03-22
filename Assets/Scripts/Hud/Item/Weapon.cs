using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Weapon : MonoBehaviour
{
    public int id;
    public float damage;

    float timer = 0;

    private void Awake()
    {
        
    }

    void Update()
    {
        switch (id)
        {
            case 0:
                timer += Time.deltaTime;
                if (timer > 1f)
                {
                    timer = 0;
                    Fire();
                }
                break;
            case 1:
                break;
        }
    }
    public void Init(ItemData data)
    {
        Player player = Locator.player;

        name = "Weapon" + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        id = data.itemId;
        damage = data.baseDamage;
    }

    public void Fire()
    {
        Player player = Locator.player;

        Vector2 targetDirection = (player.transform.localScale.x == -1) ? Vector2.left : Vector2.right;
        
        Transform bullet = TempGameManager.instance.poolManager.Get(id).transform;
        bullet.position = transform.position;

        Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, targetDirection);
        bullet.rotation = targetRotation;

        bullet.GetComponent<RangedItem>().Init(damage, targetDirection);
    }
}
