using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;


public interface IItemManager
{
    public int max_active_num { get; }
    public int max_passive_num { get; }
    public int active_num { get; }
    public int passive_num { get; }
    public List<Item> get_items();
    public IEnumerable<ActiveItem> get_active_items();
    public IEnumerable<PassiveItem> get_passive_items();
    public bool add_item(ItemInfo info);
    public int get_level(ItemInfo info);
}

public class ItemManager: IItemManager
{
    [field: SerializeField]
    public int max_active_num { get; private set; } = 3;
    [field: SerializeField]
    public int max_passive_num { get; private set; } = 3;
    private List<ActiveItem> active_items = new();
    private List<PassiveItem> passive_items = new();
    
    public ItemManager()
    {
        Locator.event_manager.register<OnItemKeyPressed>(use_item);
    }
    
    public List<Item> get_items()
    {
        List<Item> temp_active = active_items.Cast<Item>().ToList();
        List<Item> temp_passive = passive_items.Cast<Item>().ToList();
        return temp_active.Concat(temp_passive).ToList();
    }

    public IEnumerable<ActiveItem> get_active_items()
    {
        return active_items.AsEnumerable();
    }

    public IEnumerable<PassiveItem> get_passive_items()
    {
        return passive_items.AsEnumerable();
    }

    public bool add_item(ItemInfo info)
    {
        foreach (var item in get_items())
        {
            if (item.info.name == info.name) {
                item.increase_level();
                Locator.event_manager.notify(new ItemAddedEvent(item));
                return true;
            }
        }
        if ((info.is_active && active_num >= max_active_num)
                || (!info.is_active && passive_num >= max_passive_num))
            return false;

        Item new_item = Locator.item_factory.create(info).GetComponent<Item>();
        if (info.is_active)
            active_items.Add(new_item.GetComponent<ActiveItem>());
        else
            passive_items.Add(new_item.GetComponent<PassiveItem>());
        Locator.event_manager.notify(new ItemAddedEvent(new_item));
        return true;
    }

    public int get_level(ItemInfo info)
    {
        Item item = get_items().Find(x => x.info == info);
        if (item is null)
            return 0;
        return item.level;
    }

    public void use_item(IEventParam param)
    {
        int num = ((OnItemKeyPressed)param).num;
        if (num >= active_num)
            return;
        
        active_items[num].use();
    }
    
    public int active_num { get { return active_items.Count; } }
    public int passive_num { get { return active_items.Count; } }
}
