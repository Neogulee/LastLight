using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;


public interface IItem
{
    public int damage { get; }
    public float cooldown { get; }
    public void init(ItemInfo info);
    public ItemInfo info { get; }
    public bool increase_level();
}

public abstract class Item : MonoBehaviour, IItem
{
    public ItemInfo info { get; private set; } = null;
    public int level { get; private set; } = 1;

    public void init(ItemInfo info)
    {
        this.info = info;
    }

    public virtual bool increase_level()
    {
        if (level >= info.max_level)
            return false;
        level++;
        return true;
    }

    public int damage { get { return info.damage[level - 1]; } }
    public float cooldown { get { return info.cooldown[level - 1]; } }
}
