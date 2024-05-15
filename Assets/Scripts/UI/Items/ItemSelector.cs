using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;


public interface IItemSelector
{
    public void show_selection();
    public void on_selected(ItemInfo info);
    public void set_active(bool value);
}

public class ItemSelector : MonoBehaviour, IItemSelector
{
    public List<ItemSelectionOption> options = new();
    
    void Awake()
    {
        set_active(false);
    }

    public void show_selection()
    {
        IItemManager item_manager = Locator.item_manager;
        IItemPool item_pool = Locator.item_pool;
        
        List<Item> items = item_manager.get_items();        
        List<ItemInfo> infos = item_pool.get_infos();
        foreach (Item item in items)
            if (item.level == item.info.max_level)
                infos.Remove(item.info);
        
        var random = new System.Random();
        List<ItemInfo> selected = infos.OrderBy(x => random.Next()).Take(options.Count).ToList();
        for (int i = 0; i < options.Count; i++)
        {
            ItemInfo info = selected[i];
            int level = item_manager.get_level(info) + 1;
            options[i].init(this, selected[i], level);
        }
        set_active(true);
        Locator.pause_controller.pause();
    }

    public void on_selected(ItemInfo info)
    {
        set_active(false);
        Locator.pause_controller.unpause();

        Locator.item_manager.add_item(info);
    }

    public void set_active(bool value)
    {
        foreach (var option in options)
            option.gameObject.SetActive(value);
    }
}
