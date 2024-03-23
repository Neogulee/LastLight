using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class Weapon : MonoBehaviour
{
    protected float size = 1.0f;
    protected int id;
    protected float damage;
    protected float timer = 0.0f;
    protected float shootThreshold = 3.0f;

    public void Init(ItemData data)
    {
        Player player = TempGameManager.instance.player;

        name = "Weapon" + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        id = data.itemId;
        damage = data.baseDamage;
    }
    public abstract void IncreaseLevel();


}
