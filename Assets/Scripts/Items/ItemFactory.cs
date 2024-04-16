using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface IItemFactory
{   
    public GameObject create(ItemInfo info);
}

public class ItemFactory: IItemFactory
{ 
    private Dictionary<string, GameObject> items = new();
    public ItemFactory(List<ItemInfo> item_infos)
    {
        foreach (var info in item_infos)
            items[info.name] = Resources.Load<GameObject>("Prefabs/Items/" + info.name);
    }

    public GameObject create(ItemInfo info)
    {   
        if (!items.ContainsKey(info.name))
            return null;

        GameObject prefab = items[info.name];
        GameObject item = Object.Instantiate(prefab, Locator.player.transform);
        item.GetComponent<Item>().init(info);
        return item;
    }
}